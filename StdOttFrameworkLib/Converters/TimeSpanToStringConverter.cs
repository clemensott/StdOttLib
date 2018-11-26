using System;
using System.Globalization;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return StdOttStandard.Utils.ToString((TimeSpan)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse((string)value);
        }
    }
}
