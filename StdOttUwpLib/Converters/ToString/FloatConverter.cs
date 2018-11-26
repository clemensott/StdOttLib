using System;

namespace StdOttUwp.Converters
{
    public class FloatConverter : ToStringTwoWayConverter<float>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out float newValue)
        {
            return float.TryParse(newText, out newValue);
        }
    }
}
