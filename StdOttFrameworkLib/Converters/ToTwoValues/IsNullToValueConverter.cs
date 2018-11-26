using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

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
