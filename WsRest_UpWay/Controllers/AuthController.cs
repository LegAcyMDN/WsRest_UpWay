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
    private readonly PasswordHasher<CompteClient> passwordHasher = new();
    private readonly IDataRepository<CompteClient> userManager;

    public AuthController(IDataRepository<CompteClient> userManager, IConfiguration config)
    {
        this.userManager = userManager;
        _config = config;
    }

    /// <summary>
    /// Enregistre un nouvel utilisateur.
    /// </summary>
    /// <param name="body">Les informations d'enregistrement de l'utilisateur.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'utilisateur est enregistré avec succès.</response>
    /// <response code="400">Lorsque l'email existe déjà.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    // Register
    public async Task<ActionResult<UserAuthResponse>> Register([FromBody] UserRegistrationRequest body)
    {
        if ((await userManager.GetByStringAsync(body.Email)).Value != null)
            return BadRequest(UserAuthResponse.Error("Email already exists"));

        var hashed = passwordHasher.HashPassword(null, body.Password);

        var user = new CompteClient
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

    /// <summary>
    /// Authentifie un utilisateur.
    /// </summary>
    /// <param name="body">Les informations de connexion de l'utilisateur.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'utilisateur est authentifié avec succès.</response>
    /// <response code="400">Lorsque l'email n'existe pas ou le mot de passe est incorrect.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Authentifie un utilisateur avec TOTP.
    /// </summary>
    /// <param name="body">Les informations de connexion avec OTP de l'utilisateur.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'utilisateur est authentifié avec succès.</response>
    /// <response code="400">Lorsque l'email n'existe pas, le mot de passe est incorrect ou le code OTP ne correspond pas.</response>
    [HttpPost("login-otp")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Configure l'authentification à deux facteurs (OTP).
    /// </summary>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque l'OTP est configuré avec succès.</response>
    /// <response code="400">Lorsque l'utilisateur n'existe pas ou que l'OTP est déjà activé.</response>
    [HttpPost("setup-otp")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

    /// <summary>
    /// Confirme la configuration de l'authentification à deux facteurs (OTP).
    /// </summary>
    /// <param name="body">Les informations de confirmation de l'OTP.</param>
    /// <returns>Http response</returns>
    /// <response code="200">Lorsque la configuration de l'OTP est confirmée avec succès.</response>
    /// <response code="400">Lorsque l'utilisateur n'existe pas ou que le code OTP ne correspond pas.</response>
    [HttpPost("setup-otp/confirmation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<SetupOTPResponse>> ConfirmOtpSetup([FromBody] ConfirmOTPSetupRequest body)
    {
        var user = (await userManager.GetByStringAsync(User.GetEmail())).Value;
        if (user == null) return BadRequest(SetupOTPResponse.Error("User account doesn't exist!"));

        if (!string.IsNullOrEmpty(user.TwoFactorSecret) && user.TwoFactorConfirmedAt != null)
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