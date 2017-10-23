using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InOutLog.Core
{
    public static class DateTimeExtensions
    {
        public static DateTime ToUtc(this DateTime value)
        {
            return value.ToUniversalTime();
        }

        public static DateTime? ToUtc(this DateTime? value)
        {
            if (!value.HasValue)
            {
                return null;
            }
            return value.Value.ToUniversalTime();
        }
    }
}
