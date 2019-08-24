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

namespace Officium._8Examples
{
    public class AuthFunction
    {
        private readonly IRequestResolver requestResolver;
        public AuthFunction(IRequestResolver requestResolver)
        {
            this.requestResolver = requestResolver;
        }

        [FunctionName("Auth")]
        public IActionResult Run(
            [HttpTrigger(
            AuthorizationLevel.Function, "get", "post",
            Route = "Auth/")]
            HttpRequest req,
            ILogger log)
        {
            log.LogDebug($"Executing Auth function");
            var reqContext = req.MakeRequestContext();
            var resContext = requestResolver.Execute(reqContext);
            return resContext.GetActionResult();
        }
    }

    public class AuthHandler : IHandler
    {
        private readonly TokenResolver tokenResolver;

        public AuthHandler(TokenResolver tokenResolver)
        {
            this.tokenResolver = tokenResolver;
        }
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            var token = request.GetHeaderValue("Authorization"); // get the user unique token 
            request.Identity = tokenResolver.GetIdentity(token);
        }
    }

    public class RequestHandler : IHandler
    {
        private static readonly Claim GlobalAdminClaim = new Claim("Role", "GlobalAdmin"); // reference claim to compare against
        public void HandleRequest(RequestContext request, ResponseContent response)
        {
            if (request.Identity.HasClaim(GlobalAdminClaim))
            {
                response.Result = "Welcome our new admin";
            }
            else
            {
                response.Result = "Hi there";
            }
        }
    }  

}
