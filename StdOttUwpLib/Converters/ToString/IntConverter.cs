using System;
using System.Globalization;

namespace StdOttUwp.Converters
{
    public class IntConverter : ToStringTwoWayConverter<int>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out int newValue)
        {
            return int.TryParse(newText, out newValue);
        }
    }
}
