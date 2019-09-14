using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class DateTimeConverter : ToStringTwoWayConverter<DateTime>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out DateTime newValue)
        {
            return DateTime.TryParse(newText, out newValue);
        }
    }
}
