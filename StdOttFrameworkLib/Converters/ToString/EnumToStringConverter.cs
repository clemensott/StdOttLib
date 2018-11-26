using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class EnumToStringConverter : ToStringTwoWayConverter<Enum>
    {
        protected override bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out Enum newValue)
        {
            return TryParse(newText, targetType, out newValue);
        }

        public static bool TryParse<T>(string text, out T value)
        {
            value = default(T);

            Enum enumValue;
            if (!TryParse(text, typeof(T), out enumValue)) return false;

            value = (T)(object)enumValue;
            return true;
        }

        public static bool TryParse(string text, Type enumType, out Enum value)
        {
            try
            {
                value = (Enum)Enum.Parse(enumType, text, false);
                return true;
            }
            catch { }

            try
            {
                value = (Enum)Enum.Parse(enumType, text, true);
                return true;
            }
            catch { }

            try
            {
                value = (Enum)Enum.ToObject(enumType, int.Parse(text));
                return true;
            }
            catch { }

            value = default(Enum);
            return false;
        }
    }
}
