using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WsRest_UpWay.Helpers;
using WsRest_UpWay.Models;
using WsRest_UpWay.Models.EntityFramework;
using WsRest_UpWay.Models.Repository;

namespace WsRest_UpWay.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IDataRepository<CompteClient> userManager;

    public UserController(IDataRepository<CompteClient> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet("me")]
    [Authorize(Policy = Policies.User)]
    public async Task<ActionResult<CompteClient>> GetMe()
    {
        var user = (await userManager.GetByStringAsync(User.GetEmail())).Value;
        if (user == null) return Forbid();

        return Ok(user);
    }
}