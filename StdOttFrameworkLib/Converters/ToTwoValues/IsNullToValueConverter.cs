using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class IsNullToValueConverter : IsValueToTwoValueConverter
    {
        protected virtual bool IsValueNull(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ReferenceEquals(value, null);
        }
    }
}
