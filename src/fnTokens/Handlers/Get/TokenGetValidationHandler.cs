using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;

namespace fnTokens.Handlers.Get
{
    public class TokenGetValidationHandler : IHandler
    {
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            var tokenId = request.GetValue("TokenId");
            if (string.IsNullOrWhiteSpace(tokenId))
            {
                response.ValidationErrors.Add(new ValidationError { ErrorMessage = "Supply a TokenId", PropertyName = "TokenId" });
            }
        }
    }
}
