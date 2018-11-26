using System;

namespace StdOttUwp.Converters
{
    public class UIntNullableConverter : ToStringTwoWayConverter<uint?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out uint? newValue)
        {
            uint output;
            bool parsed = uint.TryParse(newText, out output);

            newValue = parsed ? (uint?)output : null;

            return parsed;
        }
    }
}
