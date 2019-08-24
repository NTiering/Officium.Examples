using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._4Examples
{
    public class IoCFunction
    {
        private readonly IRequestResolver requestResolver;
        public IoCFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("IoC")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "IoC/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing IoC function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class PreIoCHandler : IHandler
    {
        private readonly ITextProvider textProvider;

        public PreIoCHandler(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.SetInternalValue("Greeting", textProvider.GetGreeting() );
        }
    }

    public class IoCHandler : IHandler
    {
        private readonly ITextProvider textProvider;
        public IoCHandler(ITextProvider textProvider)
        {
            this.textProvider = textProvider;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result =
                new
                { 
                    Id = request.Id,
                    Greeting = request.GetValue("greeting"),
                    Name = textProvider.GetName()
                };
        }
    }
}
