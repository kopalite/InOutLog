using System;
using InOutLog.Core;

namespace InOutLog.Droid
{
    public class ClockConverter
    {
        public object Convert(object value)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null || !dateTime.HasValue)
            {
                return "00:00:00";
            }

            dateTime = dateTime.GetValueOrDefault().ToLocalTime(); 
            return string.Format("{0}:{1}:{2}", dateTime.Value.Hour.PadLeft02(), dateTime.Value.Minute.PadLeft02(), dateTime.Value.Second.PadLeft02());
        }
    }
}
