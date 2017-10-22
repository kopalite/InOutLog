using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InOutLog.Desk
{
    public class VisibilityInverter : IValueConverter
    {
        BooleanToVisibilityConverter _visibilityConverter;

        public VisibilityInverter()
        {
            _visibilityConverter = new BooleanToVisibilityConverter();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = ((Visibility)_visibilityConverter.Convert(value, targetType, parameter, culture));

            if (visibility == Visibility.Hidden || visibility == Visibility.Collapsed)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
