using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class TimeSpanConverter : ToStringTwoWayConverter<TimeSpan>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out TimeSpan newValue)
        {
            return TimeSpan.TryParse(newText, out newValue);
        }
    }
}
