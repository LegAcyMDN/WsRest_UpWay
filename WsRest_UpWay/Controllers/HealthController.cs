using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// Vérifie si l'application est en cours d'exécution et en bonne santé.
        /// </summary>
        /// <returns>Http response</returns>
        /// <response code="200">Lorsque l'application est en bonne santé.</response>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("OK");
        }
    }
}
