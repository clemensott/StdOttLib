using System;

namespace StdOttUwp.Converters
{
    public class UIntConverter : ToStringTwoWayConverter<uint>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out uint newValue)
        {
            return uint.TryParse(newText, out newValue);
        }
    }
}
