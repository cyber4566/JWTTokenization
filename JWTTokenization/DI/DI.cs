using Cryptography.Orchestration.Implementation;
using Cryptography.Orchestration.Interface;
using Login.JWT;
using Login.Login.Orchestration.Implementation;
using Login.Login.Orchestration.Interface;
using Login.Login.Service.Implementation;
using Login.Login.Service.Interface;

namespace JWTTokenization.DI
{
    public class DI
    {

        public static void DependencyInjection(WebApplicationBuilder builder) { 
        
        
               builder.Services.AddSingleton<ICryptography,Crypt>();
               builder.Services.AddScoped<ILoginService,LoginService>();
               builder.Services.AddScoped<ILogin,LoginUser>();
               builder.Services.AddScoped<TokenProvider>();
        
        
        }


    }
}
