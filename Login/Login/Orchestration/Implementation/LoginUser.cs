using Login.Login.Orchestration.Interface;
using Login.Login.Service.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Login.Orchestration.Implementation
{
    public class LoginUser : ILogin
    {

        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;

        public LoginUser(IConfiguration config, ILoginService loginService) {
        
             _config = config;
            _loginService = loginService;
        }



        bool ILogin.UserValid(string username, string password)
        {
            string user = string.Empty;

            user = _loginService.getUser(password, username);

            if (user == string.Empty)
            {

                return false;
            }
            else { 
            
                 return true;
            
            }


                //return true;



        }
    }
}
