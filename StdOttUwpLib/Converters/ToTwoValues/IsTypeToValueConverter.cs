using StdOttStandard;
using System;

namespace StdOttUwp.Converters
{
    public class IsTypeToValueConverter : IsValueToTwoValueConverter
    {
        protected override bool IsValue(object input, Type targetType, object parameter, string language)
        {
            Type inputType = input.GetType();

            if (Equal(inputType, CompareValue)) return true;

            foreach (Type subType in inputType.GetBaseTypes())
            {
                if (Equal(subType, CompareValue)) return true;
            }

            return false;
        }

        private static bool Equal(Type inputType, object compareTypeOrName)
        {
            if (compareTypeOrName is Type) return compareTypeOrName.Equals(inputType);

            return compareTypeOrName?.ToString() == inputType.Name ||
                compareTypeOrName?.ToString() == inputType.FullName;
        }
    }
}
