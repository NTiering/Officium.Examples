using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._5Examples
{
    public class HeadersFunction
    {
        private readonly IRequestResolver requestResolver;
        public HeadersFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("IoC")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Headers/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Headers function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }   

    public class HeaderHandler : IHandler
    {        
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result = "This is your Cache-Control value ='" + request.GetHeaderValue("Cache-Control") + "' ";
        }
    }
}
