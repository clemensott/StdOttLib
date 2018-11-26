using System;

namespace StdOttUwp.Converters
{
    public class FloatNullableConverter : ToStringTwoWayConverter<float?>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, string language, out float? newValue)
        {
            float output;
            bool parsed = float.TryParse(newText, out output);

            newValue = parsed ? (float?)output : null;

            return parsed;
        }
    }
}
