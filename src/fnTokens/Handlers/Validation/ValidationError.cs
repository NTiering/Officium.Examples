using Officium.Tools.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Handlers.Validation
{
    public class ValidationError : IValidationError
    {
        public ValidationError(string propertyName , string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; private set; }

        public string PropertyName { get; private set; }
    }
}
