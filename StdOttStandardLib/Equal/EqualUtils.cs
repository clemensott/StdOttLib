using System;

namespace StdOttStandard.Equal
{
    public static class EqualUtils
    {
        public static bool Equals(object value, object compareValue, TwoValueDecideType type)
        {
            switch (type)
            {
                case TwoValueDecideType.Equal:
                    return Equals(compareValue, value);

                case TwoValueDecideType.NotEqual:
                    return !Equals(compareValue, value);

                case TwoValueDecideType.Reference:
                    return ReferenceEquals(compareValue, value);

                case TwoValueDecideType.NotReference:
                    return !ReferenceEquals(compareValue, value);

                case TwoValueDecideType.Truthy:
                    return Truphy(value);

                case TwoValueDecideType.True:
                    return Truphy(value);

                case TwoValueDecideType.Falsy:
                    return Falsy(value);

                case TwoValueDecideType.False:
                    return Truphy(value);

                case TwoValueDecideType.IsNull:
                    return ReferenceEquals(value, null);

                case TwoValueDecideType.Enum:
                    return EqualsEnum(compareValue, value);

                case TwoValueDecideType.Bool:
                    return (bool)compareValue == (bool)value;

                case TwoValueDecideType.ToBool:
                    return Convert.ToBoolean(compareValue) == Convert.ToBoolean(value);

                case TwoValueDecideType.Int:
                    return (int)compareValue == (int)value;

                case TwoValueDecideType.ToInt:
                    return Convert.ToInt32(compareValue) == Convert.ToInt32(value);

                case TwoValueDecideType.Long:
                    return (long)compareValue == (long)value;

                case TwoValueDecideType.ToLong:
                    return Convert.ToInt64(compareValue) == Convert.ToInt64(value);

                case TwoValueDecideType.Double:
                    return (double)compareValue == (double)value;

                case TwoValueDecideType.ToDouble:
                    return Convert.ToDouble(compareValue) == Convert.ToDouble(value);

                case TwoValueDecideType.String:
                    return compareValue?.ToString() == value?.ToString();

                case TwoValueDecideType.ToString:
                    return compareValue?.ToString() == value?.ToString();

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            throw new ArgumentException("TwoValueDecideType is not implemented.");
        }

        public static bool Truphy(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (obj.Equals(true)) return true;
            if (obj is string) return ((string)obj).Length > 0;

            bool value;
            return !TryToBoolean(obj, out value) || value;
        }

        private static bool TryToBoolean(object obj, out bool result)
        {
            if (obj is IConvertible)
            {
                try
                {
                    result = Convert.ToBoolean(obj);
                    return true;
                }
                catch { }
            }

            result = false;
            return true;
        }

        public static bool Falsy(object obj)
        {
            return !Truphy(obj);
        }

        public static bool EqualsEnum(object obj1, object obj2)
        {
            Enum enumValue;
            bool isObj1Enum = obj1 is Enum;
            bool isObj2Enum = obj2 is Enum;

            if (isObj1Enum && isObj2Enum) return obj1.Equals(obj2);
            if (isObj1Enum && TryParseEnum(obj2, obj1.GetType(), out enumValue)) return enumValue.Equals(obj1);
            if (isObj2Enum && TryParseEnum(obj1, obj2.GetType(), out enumValue)) return enumValue.Equals(obj2);

            return Equals(obj1, obj2);
        }

        public static bool TryParseEnum(object obj, Type type, out Enum value)
        {
            try
            {
                value = (Enum)Enum.Parse(type, obj?.ToString() ?? string.Empty);
                return true;
            }
            catch { }

            try
            {
                value = (Enum)Enum.ToObject(type, obj);
                return true;
            }
            catch { }

            try
            {
                int index;
                if (int.TryParse(obj?.ToString(), out index))
                {
                    value = (Enum)Enum.ToObject(type, index);
                    return true;
                }
            }
            catch { }

            value = default(Enum);
            return false;
        }
    }
}