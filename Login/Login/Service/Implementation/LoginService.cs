using Cryptography.Orchestration.Interface;
using Dapper;
using Login.Login.Service.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Login.Login.Service.Implementation
{
    public class LoginService : ILoginService
    {

        private readonly string CacheUserkey = "User";
        private IConfiguration _config;
        private ICryptography _crypt;


        public LoginService(IConfiguration config,ICryptography crypt) { 
        
             _config = config;
             _crypt = crypt;
        }
        public string getUser(string password, string username)
        {
            MemoryCache cache = MemoryCache.Default;
            string User = string.Empty;

            password = _crypt.Encrypt(password);

            if (cache.Contains(CacheUserkey))
            {

                return (string)cache.Get(CacheUserkey);


            }
            else {


                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnectionStringAuthNZ"))) {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@username", username);
                    parameters.Add("@password", password);

                    List<string> user = con.Query<string>("SELECT Username from [AuthNZ].[dbo].[Users] where password =@password and @username=username",parameters).ToList();
                    if (user.Count != 0) { 
                        User = user[0];
                        AddToCache(cache, CacheUserkey, User, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(1) });
                    }
                
                }
            
            
            }

            return User;
        }


        private void AddToCache(MemoryCache cache,string key, string username,CacheItemPolicy duration) {
        
             cache.Add(key, username, duration);
        
        }

    }
}
