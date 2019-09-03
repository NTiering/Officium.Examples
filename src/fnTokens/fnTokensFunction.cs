using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Officium.Tools.Request;

namespace fnTokens
{
    public class fnTokensFunction
    {
        private readonly IRequestResolver requestResolver;
        public fnTokensFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("fnTokensFunction")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "v1/Token/{n?}")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing fnTokensFunction world function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }
}
