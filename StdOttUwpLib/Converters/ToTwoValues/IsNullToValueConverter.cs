using System;

namespace StdOttUwp.Converters
{
    public class IsNullToValueConverter : IsValueToTwoValueConverter
    {
        protected virtual bool IsValueNull(object value, Type targetType, object parameter, string language)
        {
            return ReferenceEquals(value, null);
        }
    }
}
