using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class ULongConverter : ToStringTwoWayConverter<ulong>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out ulong newValue)
        {
            return ulong.TryParse(newText, out newValue);
        }
    }
}
