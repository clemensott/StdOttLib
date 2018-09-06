using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class ByteConverter : ToStringTwoWayConverter<byte>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out byte newValue)
        {
            return byte.TryParse(newText, out newValue);
        }
    }
}
