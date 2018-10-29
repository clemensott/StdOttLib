using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class ByteNullableConverter : ToStringTwoWayConverter<byte?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out byte? newValue)
        {
            byte output;
            bool parsed = byte.TryParse(newText, out output);

            newValue = parsed ? (byte?)output : null;

            return parsed;
        }
    }
}
