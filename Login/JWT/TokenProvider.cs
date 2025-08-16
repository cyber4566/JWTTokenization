using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Login.JWT
{
    public class TokenProvider
    {
        private readonly IConfiguration _config;
        private readonly string cachekey = "secretKey";

        public TokenProvider(IConfiguration config) { 
        
               _config = config;
        
        }

        public string Create(string username) { 
        
        
                     string secretKey = GetSecret();

                     var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

                     var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new System.Security.Claims.ClaimsIdentity([new Claim("username", username)]),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials

            };

            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);

                     return token;
        
        
        }


        private string GetSecret() {


            MemoryCache cache = MemoryCache.Default;

            if (cache.Contains(cachekey))
            {

                return (string)cache.Get(cachekey);


            }
            else {

                using (SqlConnection con = new SqlConnection(_config.GetConnectionString("ConnectionStringAuthNZ")))
                {

                    List<string> key = con.Query<string>("SELECT KeyValue FROM SecurityKeys WHERE Application = 'CustomAuth' and KeyPurpose = 'Encryption Key for Authentication' ORDER BY ID ASC").ToList();

                    CacheItemPolicy policy = new CacheItemPolicy();
                    policy.AbsoluteExpiration = DateTimeOffset.Now.AddHours(1);

                    

                    




                    if (key.Count != 0)
                    {
                        var key_greater_256_bits = key[0] + key[0] + "1";
                        cache.Add(cachekey, key_greater_256_bits, policy);
                        return key_greater_256_bits;
                    }
                }




            }


            return "";


        }



    }
}
