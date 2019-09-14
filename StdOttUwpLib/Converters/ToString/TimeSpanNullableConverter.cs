using System;

namespace StdOttUwp.Converters
{
    public class TimeSpanNullableConverter : ToStringTwoWayConverter<TimeSpan?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out TimeSpan? newValue)
        {
            TimeSpan output;
            bool parsed = TimeSpan.TryParse(newText, out output);

            newValue = parsed ? (TimeSpan?)output : null;

            return parsed;
        }
    }
}
