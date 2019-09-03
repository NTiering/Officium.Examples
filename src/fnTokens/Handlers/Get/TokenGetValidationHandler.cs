using fnTokens.Data;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Handlers.Get
{
    public class TokenGetValidationHandler : IHandler
    {
        private readonly ITokenDataCache _tokenDataCache;

        public TokenGetValidationHandler(ITokenDataCache tokenDataCache)
        {
            _tokenDataCache = tokenDataCache;
        }
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            throw new NotImplementedException();
        }
    }
}
