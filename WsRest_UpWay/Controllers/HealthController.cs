using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        // Allows the deployment environment to check if the app is running and healthy
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
