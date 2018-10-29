using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class ULongNullableConverter : ToStringTwoWayConverter<ulong?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out ulong? newValue)
        {
            ulong output;
            bool parsed = ulong.TryParse(newText, out output);

            newValue = parsed ? (ulong?)output : null;

            return parsed;
        }
    }
}
