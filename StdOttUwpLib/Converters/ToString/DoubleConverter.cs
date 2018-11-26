using System;

namespace StdOttUwp.Converters
{
    public class DoubleConverter : ToStringTwoWayConverter<double>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out double newValue)
        {
            return double.TryParse(newText, out newValue);
        }
    }
}
