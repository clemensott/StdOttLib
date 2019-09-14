using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class DateTimeNullableConverter : ToStringTwoWayConverter<DateTime?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out DateTime? newValue)
        {
            DateTime output;
            bool parsed = DateTime.TryParse(newText, out output);

            newValue = parsed ? (DateTime?)output : null;

            return parsed;
        }
    }
}
