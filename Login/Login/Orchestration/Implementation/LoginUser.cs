using Login.Login.Orchestration.Interface;
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

        public LoginUser(IConfiguration config) {
        
             _config = config;
        }



        bool ILogin.UserValid(string username, string password)
        {

            return true;



        }
    }
}
