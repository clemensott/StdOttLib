using System;
using System.Globalization;

namespace StdOttWpfLib.Converters
{
    public class IntConverter : ToStringTwoWayConverter<int>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out int newValue)
        {
            return int.TryParse(newText, out newValue);
        }
    }
}
