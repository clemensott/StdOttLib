using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class UIntConverter : ToStringTwoWayConverter<uint>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out uint newValue)
        {
            return uint.TryParse(newText, out newValue);
        }
    }
}
