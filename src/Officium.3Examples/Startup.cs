using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._3Examples.Startup))]
namespace Officium._3Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.BeforeEveryRequest<PreVariablesHandler>();

                b.OnRequest<VariablesHandler>(
                    RequestMethod.GET,
                    "/api/Variables/{somename}");
            }

            builder.Services.AddHttpClient();
        }
    }
}
