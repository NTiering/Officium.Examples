using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;

namespace Officium._2Examples
{
    public class BeforeAndAfterEveryRequest
    {
        private readonly IRequestResolver requestResolver;
        public BeforeAndAfterEveryRequest(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("BeforeAndAfterEveryRequest")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing BeforeAndAfterEveryRequest function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class BeforeHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            response.Result = new HandlerResult { BeforeMessage = "Before Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url") };
        }
    }
    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            ((HandlerResult)response.Result).Message = "Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url");
        }
    }
    public class AfterHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            ((HandlerResult)response.Result).AfterMessage = "After Hello " + request.GetValue("name").WithDefault("Stranger ! add a name param to the url");
        }
    }

    /// <summary>
    /// Used to contain an example result
    /// </summary>
    public class HandlerResult
    {
        public string BeforeMessage { get; set; }
        public string Message { get; set; }
        public string AfterMessage { get; set; }
    }
}
