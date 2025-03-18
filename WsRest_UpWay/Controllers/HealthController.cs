using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// V�rifie si l'application est en cours d'ex�cution et en bonne sant�.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">Lorsque l'application est en bonne sant�.</response>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("OK");
        }
    }
}
