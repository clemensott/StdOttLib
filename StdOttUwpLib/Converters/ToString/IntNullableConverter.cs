using System;

namespace StdOttUwp.Converters
{
    public class IntNullableConverter : ToStringTwoWayConverter<int?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out int? newValue)
        {
            int output;
            bool parsed = int.TryParse(newText, out output);

            newValue = parsed ? (int?)output : null;

            return parsed;
        }
    }
}
