using fnTokens.Data;
using fnTokens.Settings;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;

namespace fnTokens.Handlers.Post
{
    public class TokenPostHandler : IHandler
    {
        private readonly ITokenDataCache _tokenDataCache;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IAppSettings _appSettings;

        public TokenPostHandler(IAppSettings appSettings, IDateTimeProvider dateTimeProvider, ITokenDataCache tokenDataCache)
        {
            _tokenDataCache = tokenDataCache;
            _dateTimeProvider = dateTimeProvider;
            _appSettings = appSettings;            
        }
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            var expiresIn = MakeExpiryDateTime(_appSettings, _dateTimeProvider, request.GetValue("expiresInMinutes"));
            var token = request.GetValue("TokenId");
            var claims = request.GetValue("ClaimsJson");
            _tokenDataCache.Set(token, claims, expiresIn);
        }
        private static DateTime MakeExpiryDateTime(IAppSettings appSettings, IDateTimeProvider dateTimeProvider, string expiresInMinutes)
        {
            int timeSpanInMins;
            DateTime rtn;
            if (int.TryParse(expiresInMinutes, out timeSpanInMins))
            {
                rtn = dateTimeProvider.Now.AddMinutes(timeSpanInMins);
            }
            else
            {
                rtn = dateTimeProvider.Now.AddMinutes(appSettings.DefaultExpiryInMins);
            }
          
            return rtn;
        }
    }
}
