using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using Officium.Tools.Helpers;
using System.Security.Claims;
using System;

namespace Officium._9Examples
{
    public class KitchenSinkFunction
    {
        private readonly IRequestResolver requestResolver;
        public KitchenSinkFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("KitchenSink")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "KitchenSink/{n?}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Kitchen Sink function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class AuthHandler : IHandler
    {       
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }

    public class BeforeEveryRequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }

    public class AfterEveryRequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }

    public class ValidationRequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }

    public class NotHandledRequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }

    public class RequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            Console.WriteLine($"Calling {this.GetType().Name}");
        }
    }  

}
