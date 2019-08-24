using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._8Examples.Startup))]
namespace Officium._8Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<TokenResolver,TokenResolver>();

            using (var b = new Builder(builder.Services))
            {
                b.Authorise<AuthHandler>();
                b.OnRequest<RequestHandler>(
                   RequestMethod.GET,
                   "/api/Auth/");
            }
        }
    }

    public class TokenResolver
    {  
        public ClaimsIdentity GetIdentity(string token)
        {
            var claims = new List<Claim>
            {
                new Claim("Role", "WidgetAdmin"),
                new Claim("Role", "GlobalAdmin")
            };
            var rtn = new ClaimsIdentity(claims);
            return rtn;
        }
    }
}
