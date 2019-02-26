using System;

namespace StdOttUwp.Converters
{
    public class ByteConverter : ToStringTwoWayConverter<byte>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out byte newValue)
        {
            return byte.TryParse(newText, out newValue);
        }
    }
}
