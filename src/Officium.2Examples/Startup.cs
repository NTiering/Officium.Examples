using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._2Examples.Startup))]
namespace Officium._2Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.OnRequest<HelloWorldHandler>(
                    RequestMethod.GET,
                    "/api/BeforeAndAfterEveryRequest"); 

                b.BeforeEveryRequest<BeforeHandler>();
                b.AfterEveryRequest<AfterHandler>();
            }

            builder.Services.AddHttpClient();
        }
    }
}
