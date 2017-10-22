using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace InOutLog.Desk
{
    public class MultiVisibilityConverter : IMultiValueConverter
    {
        private IValueConverter _visibilityConverter;

        public bool IsConjuction { get; set; }

        public MultiVisibilityConverter()
        {
            _visibilityConverter = new BooleanToVisibilityConverter();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValues = new bool[0];

            if (values != null && values.Length > 0 && values.All(x => x is bool))
            {
                boolValues = values.Select(x => (bool)x).ToArray();
            }

            if (boolValues.Length == 0 || (IsConjuction && boolValues.Any(x => !x)) || (!IsConjuction && boolValues.Any(x => x)))
            {
                return _visibilityConverter.Convert(false, targetType, parameter, culture);
            }
            else
            {
                return _visibilityConverter.Convert(true, targetType, parameter, culture);
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
