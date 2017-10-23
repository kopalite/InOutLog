using InOutLog;
using System;
using System.Globalization;
using System.Windows.Data;
using InOutLog.Core;

namespace InOutLog.Desk
{
    public class ClockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            if (dateTime == null || !dateTime.HasValue)
            {
                return "00:00:00";
            }

            dateTime = dateTime.GetValueOrDefault().ToLocalTime(); 
            return string.Format("{0}:{1}:{2}", dateTime.Value.Hour.PadLeft02(), dateTime.Value.Minute.PadLeft02(), dateTime.Value.Second.PadLeft02());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
