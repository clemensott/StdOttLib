using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class UIntNullableConverter : ToStringTwoWayConverter<uint?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out uint? newValue)
        {
            uint output;
            bool parsed = uint.TryParse(newText, out output);

            newValue = parsed ? (uint?)output : null;

            return parsed;
        }
    }
}
