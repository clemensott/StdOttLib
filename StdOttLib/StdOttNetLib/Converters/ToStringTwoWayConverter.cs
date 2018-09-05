using System;
using System.Globalization;
using System.Windows.Data;

namespace StdOttNetLib.Converters
{
    public abstract class ToStringTwoWayConverter<T> : IValueConverter
    {
        protected T sourceValue;
        private string text;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ValueChanged((T)value, targetType, parameter, culture)) return text;

            SetSourceValue((T)value, targetType, parameter, culture);

            return text = ToString(sourceValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T newValue;

            if (!TryParse((string)value, targetType, parameter, culture, out newValue)) return sourceValue;

            SetSourceValue(newValue, targetType, parameter, culture);

            return newValue;
        }

        protected virtual bool ValueChanged(T newValue, Type targetType, object parameter, CultureInfo culture)
        {
            if (ReferenceEquals(sourceValue, newValue)) return true;
            if (ReferenceEquals(sourceValue, null)) return false;
            if (ReferenceEquals(newValue, null)) return false;

            return sourceValue.Equals(newValue);
        }

        protected virtual void SetSourceValue(T value, Type targetType, object parameter, CultureInfo culture)
        {
            sourceValue = value;
        }

        protected virtual string ToString(T value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        protected abstract bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out T newValue);
    }
}
