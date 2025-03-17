using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OtpNet;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;
using WsRest_UpWay.Models.Requests;
using WsRest_UpWay.Models.Responses;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly PasswordHasher<Compteclient> passwordHasher = new();
    private readonly IDataRepository<Compteclient> userManager;

    public AuthController(IDataRepository<Compteclient> userManager, IConfiguration config)
    {
        this.userManager = userManager;
        _config = config;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    // Register
    public async Task<ActionResult<UserAuthResponse>> Register([FromBody] UserRegistrationRequest body)
    {
        if ((await userManager.GetByStringAsync(body.Email)).Value != null)
            return BadRequest(UserAuthResponse.Error("Email already exists"));

        var hashed = passwordHasher.HashPassword(null, body.Password);

        var user = new Compteclient
        {
            LoginClient = body.Login,
            MotDePasseClient = hashed,
            PrenomClient = body.FirstName,
            NomClient = body.LastName,
            EmailClient = body.Email,
            Usertype = Policies.User
        };

        await userManager.AddAsync(user);

        var jwt = user.GenerateJwtToken(_config);

        return Ok(UserAuthResponse.Success(jwt));
    }

    [HttpPost("confirm-email")]
    public async Task<ActionResult<UserAuthResponse>> ConfirmEmail()
    {
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    // Login
    public async Task<ActionResult<UserAuthResponse>> Login([FromBody] UserLoginRequest body)
    {
        var user = (await userManager.GetByStringAsync(body.Login)).Value;
        if (user == null) return BadRequest(UserAuthResponse.Error("Email Does not exist!"));

        var res = passwordHasher.VerifyHashedPassword(user, user.MotDePasseClient, body.Password);

        if (res != PasswordVerificationResult.Success) return BadRequest(UserAuthResponse.Error("Wrong password!"));

        if (!string.IsNullOrEmpty(user.TwoFactorSecret) && user.TwoFactorConfirmedAt != null)
            return Ok(UserAuthResponse.OTPRequired());

        var jwt = user.GenerateJwtToken(_config);

        return Ok(UserAuthResponse.Success(jwt));
    }

    [HttpPost("login-otp")]
    [AllowAnonymous]
    // Login with TOTP
    public async Task<ActionResult<UserAuthResponse>> LoginOtp([FromBody] UserLoginOTPRequest body)
    {
        var user = (await userManager.GetByStringAsync(body.Login)).Value;
        if (user == null) return BadRequest(UserAuthResponse.Error("Email Does not exist!"));

        var res = passwordHasher.VerifyHashedPassword(user, user.MotDePasseClient, body.Password);

        if (res != PasswordVerificationResult.Success) return BadRequest(UserAuthResponse.Error("Wrong password!"));

        if (string.IsNullOrEmpty(user.TwoFactorSecret)) return Ok(UserAuthResponse.Error("OTP not required."));

        var totp = new Totp(Convert.FromBase64String(user.TwoFactorSecret));

        long timeWindowUsed;
        if (!totp.VerifyTotp(body.Code, out timeWindowUsed, VerificationWindow.RfcSpecifiedNetworkDelay))
            return BadRequest(UserAuthResponse.Error("OTP code doesn't match!"));

        var jwt = user.GenerateJwtToken(_config);

        return Ok(UserAuthResponse.Success(jwt));
    }

    [HttpPost("setup-otp")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<SetupOTPResponse>> SetupOtp()
    {
        var user = (await userManager.GetByStringAsync(User.GetEmail())).Value;
        if (user == null) return BadRequest(SetupOTPResponse.Error("User account doesn't exist!"));

        if (!string.IsNullOrEmpty(user.TwoFactorSecret) && user.TwoFactorConfirmedAt != null)
            return BadRequest(SetupOTPResponse.Error("OTP Already enabled!"));

        var secret = KeyGeneration.GenerateRandomKey(2048);
        var strSecret = Convert.ToBase64String(secret);

        user.TwoFactorSecret = strSecret;
        await userManager.UpdateAsync(user, user);

        return Ok(SetupOTPResponse.Success(strSecret));
    }

    [HttpPost("setup-otp/confirmation")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<SetupOTPResponse>> ConfirmOtpSetup([FromBody] ConfirmOTPSetupRequest body)
    {
        var user = (await userManager.GetByStringAsync(User.GetEmail())).Value;
        if (user == null) return BadRequest(SetupOTPResponse.Error("User account doesn't exist!"));


        if (string.IsNullOrEmpty(user.TwoFactorSecret))
            return BadRequest(SetupOTPResponse.Error("OTP setup not started."));

        if (user.TwoFactorConfirmedAt != null)
            return BadRequest(SetupOTPResponse.Error("OTP Already enabled!"));

        var totp = new Totp(Convert.FromBase64String(user.TwoFactorSecret));

        long timeWindowUsed;
        if (!totp.VerifyTotp(body.Code, out timeWindowUsed, VerificationWindow.RfcSpecifiedNetworkDelay))
            return BadRequest(UserAuthResponse.Error("OTP code doesn't match!"));

        user.TwoFactorConfirmedAt = DateTime.Now;
        await userManager.UpdateAsync(user, user);

        return Ok();
    }
}