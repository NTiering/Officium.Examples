using fnTokens.Data;
using Officium.Tools.Handlers;
using Officium.Tools.Request;
using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Handlers.Get
{
public class ValidationError : IValidationError
    {
        public string ErrorMessage { get; set; }

        public string PropertyName { get; set; }
    }
}