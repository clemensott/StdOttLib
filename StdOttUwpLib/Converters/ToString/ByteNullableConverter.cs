using System;

namespace StdOttUwp.Converters
{
    public class ByteNullableConverter : ToStringTwoWayConverter<byte?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out byte? newValue)
        {
            byte output;
            bool parsed = byte.TryParse(newText, out output);

            newValue = parsed ? (byte?)output : null;

            return parsed;
        }
    }
}
