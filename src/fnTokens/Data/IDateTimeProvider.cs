using System;

namespace fnTokens.Data
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}