using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            Loginclient = body.Login,
            Motdepasseclient = hashed,
            Prenomclient = body.FirstName,
            Nomclient = body.LastName,
            Emailclient = body.Email,
            Usertype = Policies.User
        };

        await userManager.AddAsync(user);

        var jwt = user.GenerateJwtToken(_config);

        return Ok(UserAuthResponse.Success(jwt));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    // Register
    public async Task<ActionResult<UserAuthResponse>> Login([FromBody] UserLoginRequest body)
    {
        var user = (await userManager.GetByStringAsync(body.Login)).Value;
        if (user == null) return BadRequest(UserAuthResponse.Error("Email Does not exist!"));

        var res = passwordHasher.VerifyHashedPassword(user, user.Motdepasseclient, body.Password);

        if (res != PasswordVerificationResult.Success) return BadRequest(UserAuthResponse.Error("Wrong password!"));

        var jwt = user.GenerateJwtToken(_config);

        return Ok(UserAuthResponse.Success(jwt));
    }
}