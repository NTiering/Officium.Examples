using fnTokens.Data;
using fnTokens.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using System;

[assembly: FunctionsStartup(typeof(fnTokens.Startup))]
namespace fnTokens
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ITokenDataCache, TokenDataCache>();

            using (var b = new Builder(builder.Services))
            {
                b.ValidateRequest<Handlers.Get.TokenGetValidationHandler>( // make sure we have some values
                    RequestMethod.GET,
                    "/api/Token/{TokenId}");

                b.OnRequest<Handlers.Get.TokenGetHandler>( // return value from token 
                    RequestMethod.GET,
                    "/api/Token/{TokenId}");

                b.ValidateRequest<Handlers.Post.TokenPostValidationHandler>( // make sure we have some values
                    RequestMethod.POST,
                    "/api/Token/");

                b.OnRequest<Handlers.Post.TokenPostHandler>( // set values to be held against the token 
                    RequestMethod.POST,
                    "/api/Token/");
            }

            builder.Services.AddSingleton(GetAppSettings());
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        }

        private static IAppSettings GetAppSettings()
        {
            var config = new ConfigurationBuilder()
                            .SetBasePath(Environment.CurrentDirectory)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();


            var rtn = new AppSettings(config);
            return rtn;
        }
    }
}

