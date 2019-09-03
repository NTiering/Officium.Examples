using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Handlers;

namespace Officium._1aExamples
{
    public class HelloWorldFunction
    {
        private readonly IRequestResolver requestResolver;
        public HelloWorldFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Validation")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post", 
            Route = null)]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Validation function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class ValidatorHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            if (string.IsNullOrWhiteSpace(request.GetValue("name")))
            {
                response.ValidationErrors.Add(new ValidationError("name", "Please supply a value for name"));
            }
        }
    }

    public class HelloWorldHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            response.Result = new { Message = "Hello " + request.GetValue("name") };
        }
    }
}
