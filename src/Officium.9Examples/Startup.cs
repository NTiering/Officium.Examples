using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._9Examples.Startup))]
namespace Officium._9Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
          
            using (var b = new Builder(builder.Services))
            {
                b.Authorise<AuthHandler>()
                 .BeforeEveryRequest<BeforeEveryRequestHandler>()
                 .ValidateRequest<ValidationRequestHandler>(RequestMethod.GET, "/api/kitchensink/")
                 .OnRequest<RequestHandler>(RequestMethod.GET, "/api/kitchensink/")
                 .OnNotHandled<NotHandledRequestHandler>()
                 .AfterEveryRequest<AfterEveryRequestHandler>();
            }
        }
    }  
}
