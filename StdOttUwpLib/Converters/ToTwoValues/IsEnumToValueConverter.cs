﻿using System;

namespace StdOttUwp.Converters
{
    public class IsEnumToValueConverter : IsValueToTwoValueConverter
    {
        protected override bool IsValue(object input, Type targetType, object parameter, string language)
        {
            if (ReferenceEquals(CompareValue, input)) return true;
            if (ReferenceEquals(input, null) || ReferenceEquals(CompareValue, null)) return false;

            Enum compareValue;

            if (CompareValue is Enum) compareValue = (Enum)CompareValue;
            else
            {
                try
                {
                    compareValue = (Enum)Enum.Parse(input.GetType(), CompareValue.ToString());
                }
                catch
                {
                    compareValue = (Enum)Enum.ToObject(input.GetType(), CompareValue);
                }

            }

            return compareValue.Equals(input);
        }
    }
}
