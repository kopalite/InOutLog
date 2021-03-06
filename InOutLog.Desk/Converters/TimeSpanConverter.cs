﻿using System;
using System.Globalization;
using System.Windows.Data;
using InOutLog.Core;
using System.Windows.Controls;

namespace InOutLog.Desk
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is TimeSpan))
            {
                return "00:00:00";
            }

            var span = ((TimeSpan?)value).GetValueOrDefault();
            return  string.Format("{0}:{1}:{2}", span.Hours.PadLeft02(), span.Minutes.PadLeft02(), span.Seconds.PadLeft02());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
