using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._6Examples.Startup))]
namespace Officium._6Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<SimpleLogger, SimpleLogger>();

            using (var b = new Builder(builder.Services))
            {               
                b.OnRequest<RequestHandler>(
                    RequestMethod.GET,
                    "/api/Errors/");

                b.OnError<ErrorHandler>();
            }           
        }
    }

}
