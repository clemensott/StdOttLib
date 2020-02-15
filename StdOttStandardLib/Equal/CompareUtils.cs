using System;

namespace StdOttStandard.Equal
{
    public static class CompareUtils
    {
        public static bool Compare(object value, object compareValue, TwoValueDecideType type)
        {
            switch (type)
            {
                case TwoValueDecideType.Equal:
                    return ReferenceEqualsOrEquals(compareValue, value);

                case TwoValueDecideType.Reference:
                    return ReferenceEquals(compareValue, value);

                case TwoValueDecideType.Truthy:
                    return Truphy(value);

                case TwoValueDecideType.Falsy:
                    return Falsy(value);

                case TwoValueDecideType.Enum:
                    return EqualsEnum(compareValue, value);

                case TwoValueDecideType.Bool:
                    return (bool)compareValue == (bool)value;

                case TwoValueDecideType.Int:
                    return (int)compareValue == (int)value;

                case TwoValueDecideType.Long:
                    return (long)compareValue == (long)value;

                case TwoValueDecideType.Double:
                    return (double)compareValue == (double)value;

                case TwoValueDecideType.String:
                    return compareValue?.ToString() == value?.ToString();
            }

            throw new ArgumentException("TwoValueDecideType is not implemented.");
        }

        public static bool ReferenceEqualsOrEquals(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (!ReferenceEquals(obj1, null)) return obj1.Equals(obj2);
            if (!ReferenceEquals(obj2, null)) return obj2.Equals(obj1);

            throw new NotImplementedException();
        }

        public static bool Truphy(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (obj.Equals(true)) return true;
            if (obj is string) return !string.IsNullOrEmpty((string)obj);

            try
            {
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return true;
            }
        }

        public static bool Falsy(object obj)
        {
            return !Truphy(obj);
        }

        public static bool EqualsEnum(object obj1, object obj2)
        {
            Enum enumValue;

            if (obj1 is Enum && obj2 is Enum) return obj1.Equals(obj2);
            if (obj1 is Enum && TryParseEnum(obj2, obj1.GetType(), out enumValue)) return enumValue.Equals(obj1);
            if (obj2 is Enum && TryParseEnum(obj1, obj2.GetType(), out enumValue)) return enumValue.Equals(obj1);

            return ReferenceEqualsOrEquals(obj1, obj2);
        }

        public static bool TryParseEnum(object obj, Type type, out Enum value)
        {
            try
            {
                value = (Enum)Enum.ToObject(type, obj);
                return true;
            }
            catch { }

            try
            {
                value = (Enum)Enum.Parse(type, obj?.ToString());
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
