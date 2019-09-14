using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class TimeSpanNullableConverter : ToStringTwoWayConverter<TimeSpan?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out TimeSpan? newValue)
        {
            TimeSpan output;
            bool parsed = TimeSpan.TryParse(newText, out output);

            newValue = parsed ? (TimeSpan?)output : null;

            return parsed;
        }
    }
}
