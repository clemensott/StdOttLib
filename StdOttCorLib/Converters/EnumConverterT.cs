using System;
using System.Globalization;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public abstract class EnumConverter<T> : IValueConverter where T : struct, IComparable, IFormattable, IConvertible
    {
        private T currentValue;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            currentValue = (T)value;

            return currentValue.Equals(GetValue(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value) return currentValue = GetValue(parameter.ToString());

            return currentValue;
        }

        protected virtual T GetValue(string name)
        {
            return (T)Enum.Parse(typeof(T), name);
        }
    }
}
