using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Officium.Tools.Handlers;
using Officium.Tools.Request;

[assembly: FunctionsStartup(typeof(Officium._7Examples.Startup))]
namespace Officium._7Examples
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {           

            using (var b = new Builder(builder.Services))
            {
                b.OnNotHandled<NoHandlerFoundHandler>();              
            }           
        }
    }
}
