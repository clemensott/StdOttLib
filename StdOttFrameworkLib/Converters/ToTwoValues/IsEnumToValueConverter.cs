using System;
using System.Globalization;

namespace StdOttFramework.Converters
{
    public class IsEnumToValueConverter : IsValueToTwoValueConverter
    {
        protected override bool IsValue(object input, Type targetType, object parameter, CultureInfo culture)
        {
            Enum compareValue = CompareValue is Enum ? (Enum)CompareValue : (Enum)Enum.ToObject(input.GetType(), CompareValue);

            if (ReferenceEquals(compareValue, input)) return true;
            if (!ReferenceEquals(compareValue, null)) return compareValue.Equals(input);
            if (!ReferenceEquals(input, null)) return input.Equals(compareValue);

            throw new NotImplementedException();
        }
    }
}
