using Microsoft.AspNetCore.Mvc;

namespace JWTTokenization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> Login(string username, string password) {


            return "";
        }
    }
}
