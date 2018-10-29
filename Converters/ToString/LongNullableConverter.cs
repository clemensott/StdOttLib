using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class LongNullableConverter : ToStringTwoWayConverter<long?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out long? newValue)
        {
            long output;
            bool parsed = long.TryParse(newText, out output);

            newValue = parsed ? (long?)output : null;

            return parsed;
        }
    }
}
