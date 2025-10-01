using Login.JWT;
using Login.Login.Orchestration.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenization.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {

        private ILogin _login;
        private TokenProvider _tokenProvider;

        public AdminController(ILogin login, TokenProvider tokenProvider) { 
        
               _login = login;
              _tokenProvider = tokenProvider;
        }

        [HttpGet]
        [Route("Login")]
        
        public ActionResult<string> Login(string username, string password) {

            if (_login.UserValid(username, password)) {

                return _tokenProvider.Create(username);
            
            }

            return "";
        }
    }
}
