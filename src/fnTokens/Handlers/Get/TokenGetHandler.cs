using fnTokens.Data;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Handlers.Get
{
    public class TokenGetHandler : IHandler
    {
        private readonly ITokenDataCache _tokenDataCache;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TokenGetHandler(IDateTimeProvider dateTimeProvider, ITokenDataCache tokenDataCache)
        {
            _tokenDataCache = tokenDataCache;
            _dateTimeProvider = dateTimeProvider;           
        }

        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            var token = request.GetValue("TokenId");
            response.Result = _tokenDataCache.Get(token);
        }
    }
}
