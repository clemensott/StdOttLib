using System;
using System.Globalization;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public delegate object ConvertEventHandler(object value, Type targetType, object parameter, CultureInfo culture);
    public delegate object ConvertBackEventHandler(object value, Type targetType, object parameter, CultureInfo culture);

    public class ValueConverter : IValueConverter
    {
        public event ConvertEventHandler ConvertEvent;
        public event ConvertBackEventHandler ConvertBackEvent;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertEvent?.Invoke(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ConvertBackEvent?.Invoke(value, targetType, parameter, culture);
        }
    }
}
