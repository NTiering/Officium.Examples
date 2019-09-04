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
       
        public void HandleRequest(IRequestContext request, IResponseContent response)
        {
            if (HasIllegalExpiry(request))
            {
                response.ValidationErrors.Add(new ValidationError("expiresInMinutes", "Not a valid number"));
            }
        }

        private bool HasIllegalExpiry(IRequestContext request)
        {
            var i = request.GetValue("expiresInMinutes");
            if (string.IsNullOrWhiteSpace(i)) return false;
            var rtn = !(int.TryParse(i, out int t));
            return rtn;
        }
    }
}
