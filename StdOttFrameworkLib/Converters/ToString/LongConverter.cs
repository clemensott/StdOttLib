using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class LongConverter : ToStringTwoWayConverter<long>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out long newValue)
        {
            return long.TryParse(newText, out newValue);
        }
    }
}
