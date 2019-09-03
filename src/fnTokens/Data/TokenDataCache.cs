using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Data
{
    public class TokenDataCache : ITokenDataCache
    {
        public object Get(string token)
        {
            throw new NotImplementedException();
        }

        public void Set(string token, object values, DateTime expiresOn)
        {
            throw new NotImplementedException();
        }
    }
}
