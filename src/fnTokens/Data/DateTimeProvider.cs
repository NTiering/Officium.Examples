using System;
using System.Collections.Generic;
using System.Text;

namespace fnTokens.Data
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}
