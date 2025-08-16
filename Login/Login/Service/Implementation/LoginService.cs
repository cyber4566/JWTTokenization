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


        public LoginService(IConfiguration config) { 
        
             _config = config;
        
        }
        public string getUser(string password, string username)
        {
            MemoryCache cache = MemoryCache.Default;
            string User = string.Empty;

            if (cache.Contains(CacheUserkey))
            {

                return (string)cache.Get(CacheUserkey);


            }
            else {


                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("Auth"))) {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@username", username);
                    parameters.Add("@password", password);

                    List<string> user = con.Query<string>("SELECT Username from [AuthNZ].[dbo].[Users] where password =@password and @username=username").ToList();
                    if (user != null) { 
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
