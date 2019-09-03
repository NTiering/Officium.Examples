using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.FileExtensions;
using System;

[assembly: FunctionsStartup(typeof(Officium._1aExamples.Startup))]
namespace Officium._1aExamples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            using (var b = new Builder(builder.Services))
            {
                b.ValidateRequest<ValidatorHandler>(
                    RequestMethod.GET,
                    "/api/Validation");

                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/Validation");
            }

            builder.Services.AddHttpClient();  
        }

    }

    
}
