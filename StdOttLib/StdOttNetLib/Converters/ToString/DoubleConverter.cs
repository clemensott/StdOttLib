using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class DoubleConverter : ToStringTwoWayConverter<double>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out double newValue)
        {
            return double.TryParse(newText, out newValue);
        }
    }
}
