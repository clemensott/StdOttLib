using System;

namespace StdOttUwp.Converters
{
    public class DateTimeConverter : ToStringTwoWayConverter<DateTime>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out DateTime newValue)
        {
            return DateTime.TryParse(newText, out newValue);
        }
    }
}
