using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._1Examples
{
    public class HelloWorldFunction
    {
        private readonly IRequestResolver requestResolver;
        public HelloWorldFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("HelloWorld")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Hello world function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            response.Result = new { Message = "Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url") };
        }
    }
}
