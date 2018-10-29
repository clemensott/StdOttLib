using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class DoubleNullableConverter : ToStringTwoWayConverter<double?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out double? newValue)
        {
            double output;
            bool parsed = double.TryParse(newText, out output);

            newValue = parsed ? (double?)output : null;

            return parsed;
        }
    }
}
