using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;
using System;

namespace Officium._7Examples
{
    public class NoHandlerFunction
    {
        private readonly IRequestResolver requestResolver;
        public NoHandlerFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("NoHandler")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "NoHandler/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing NoHandler function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class NoHandlerFoundHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.StatusCode = 404;
            response.Result = "That resource is not present";
        }
    }  

}
