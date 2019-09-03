using fnTokens.Data;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Handlers.Post
{
    public class TokenPostValidationHandler : IHandler
    {
        private readonly ITokenDataCache _tokenDataCache;

        public TokenPostValidationHandler(ITokenDataCache tokenDataCache)
        {
            _tokenDataCache = tokenDataCache;
        }
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            throw new NotImplementedException();
        }
    }
}
