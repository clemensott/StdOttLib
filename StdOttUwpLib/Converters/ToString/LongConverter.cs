using System;

namespace StdOttUwp.Converters
{
    public class LongConverter : ToStringTwoWayConverter<long>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out long newValue)
        {
            return long.TryParse(newText, out newValue);
        }
    }
}
