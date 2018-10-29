using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class IntNullableConverter : ToStringTwoWayConverter<int?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out int? newValue)
        {
            int output;
            bool parsed = int.TryParse(newText, out output);

            newValue = parsed ? (int?)output : null;

            return parsed;
        }
    }
}
