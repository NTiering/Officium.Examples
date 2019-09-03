using System;

namespace fnTokens.Data
{
    public interface ITokenDataCache
    {
        void Set(string token, object values, DateTime expiresOn);
        object Get(string token);
    }
}