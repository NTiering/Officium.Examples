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

namespace Officium._6Examples
{
    public class ErrorFunction
    {
        private readonly IRequestResolver requestResolver;
        public ErrorFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("IoC")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Errors/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Error function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class RequestHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            throw new System.IO.FileNotFoundException("Cant Find important file !!!! !");
        }
    }

    public class ErrorHandler : IHandler
    {
        private readonly SimpleLogger logger;

        public ErrorHandler(SimpleLogger logger)
        {
            this.logger = logger; 
        }
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            logger.Log($"Oh no an error occured of type {response.Exception.GetType().Name} with the message '{response.Exception.Message}'");
            response.Exception = new Exception("sorry a very generic problem occured. Nothing to see here ");
        }
    }

    public class SimpleLogger
    {
        public void Log(string message)
        {
            var originalFgColour = System.Console.ForegroundColor;
            var originalbgColour = System.Console.BackgroundColor;
            System.Console.ForegroundColor = System.ConsoleColor.White;
            System.Console.BackgroundColor = System.ConsoleColor.Red;
            System.Console.WriteLine(message);
            System.Console.ForegroundColor = originalFgColour;
            System.Console.BackgroundColor = originalbgColour;
        }
    }

}
