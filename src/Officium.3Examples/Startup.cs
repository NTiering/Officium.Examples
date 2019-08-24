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
                b.BeforeEveryRequest<PreVariblesHandler>();

                b.OnRequest<VariblesHandler>(
                    RequestMethod.GET,
                    "/api/Varibles/{somename}");
            }

            builder.Services.AddHttpClient();
        }
    }
}
