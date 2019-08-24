using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._4Examples.Startup))]
namespace Officium._4Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {  
            using (var b = new Builder(builder.Services))
            {
                b.BeforeEveryRequest<PreIoCHandler>();

                b.OnRequest<IoCHandler>(
                    RequestMethod.GET,
                    "/api/IoC/");
            }

            // add our service here 
            builder.Services.AddSingleton<ITextProvider, TextProvider>();
            builder.Services.AddHttpClient();
        }
    }

    public interface ITextProvider
    {
        string GetGreeting();
        string GetName();
    }

    public class TextProvider : ITextProvider
    {
        public string GetGreeting()
        {
            return "Hello";
        }

        public string GetName()
        {
            return "Timmy";
        }
    }
}
