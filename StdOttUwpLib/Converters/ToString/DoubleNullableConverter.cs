using System;

namespace StdOttUwp.Converters
{
    public class DoubleNullableConverter : ToStringTwoWayConverter<double?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out double? newValue)
        {
            double output;
            bool parsed = double.TryParse(newText, out output);

            newValue = parsed ? (double?)output : null;

            return parsed;
        }
    }
}
