using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class FloatConverter : ToStringTwoWayConverter<float>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out float newValue)
        {
            return float.TryParse(newText, out newValue);
        }
    }
}
