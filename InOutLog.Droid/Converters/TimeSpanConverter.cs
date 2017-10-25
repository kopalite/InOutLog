using System;
using InOutLog.Core;

namespace InOutLog.Droid
{
    public class TimeSpanConverter
    {
        public object Convert(object value)
        {
            
            if (value == null || !(value is TimeSpan))
            {
                return "00:00:00";
            }

            var span = ((TimeSpan?)value).GetValueOrDefault();
            return  string.Format("{0}:{1}:{2}", span.Hours.PadLeft02(), span.Minutes.PadLeft02(), span.Seconds.PadLeft02());
        }
    }
}
