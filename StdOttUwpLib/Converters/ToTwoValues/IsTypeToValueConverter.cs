using System;

namespace StdOttUwp.Converters
{
    public class IsTypeToValueConverter<T> : IsValueToTwoValueConverter
    {
        protected override bool IsValue(object input, Type targetType, object parameter, string language)
        {
            return input is T;
        }
    }
}
