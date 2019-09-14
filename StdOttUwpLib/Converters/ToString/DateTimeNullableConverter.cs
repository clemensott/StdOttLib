using System;

namespace StdOttUwp.Converters
{
    public class DateTimeNullableConverter : ToStringTwoWayConverter<DateTime?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out DateTime? newValue)
        {
            DateTime output;
            bool parsed = DateTime.TryParse(newText, out output);

            newValue = parsed ? (DateTime?)output : null;

            return parsed;
        }
    }
}
