using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._3Examples
{
    public class VariablesFunction
    {
        private readonly IRequestResolver requestResolver;
        public VariablesFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Variables")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Variables/{n?}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Variables function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class PreVariablesHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            request.SetInternalValue("Greeting", "Hello");
        }
    }

    public class VariablesHandler : IHandler
    {
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            response.Result =
                new
                { 
                    Id = request.Id,
                    Greeting = request.GetInternalValue("greeting"),
                    Name = request.GetValue("somename")
                };
        }
    }
}
