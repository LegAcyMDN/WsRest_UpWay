using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WsRest_UpWay.Controllers
{
    [Route("api/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }
    }
}
