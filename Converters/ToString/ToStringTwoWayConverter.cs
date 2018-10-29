using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttWpfLib.Converters
{
    public abstract class ToStringTwoWayConverter<T> : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AutoParseNullOrWhiteSpaceProperty =
            DependencyProperty.Register("AutoParseNullOrWhiteSpace", typeof(bool), typeof(ToStringTwoWayConverter<T>));

        public static readonly DependencyProperty NullOrWhiteSpaceValueProperty =
            DependencyProperty.Register("NullOrWhiteSpaceValue", typeof(object), typeof(ToStringTwoWayConverter<T>));

        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(object), typeof(ToStringTwoWayConverter<T>));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToStringTwoWayConverter<T>));

        public bool AutoParseNullOrWhiteSpace
        {
            get { return (bool)GetValue(AutoParseNullOrWhiteSpaceProperty); }
            set { SetValue(AutoParseNullOrWhiteSpaceProperty, value); }
        }

        public T NullOrWhiteSpaceValue
        {
            get { return (T)GetValue(NullOrWhiteSpaceValueProperty); }
            set { SetValue(NullOrWhiteSpaceValueProperty, value); }
        }

        public T CurrentValue
        {
            get { return (T)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Convert(T value, object parameter = null)
        {
            return (string)Convert(value, typeof(string), parameter, CultureInfo.CurrentCulture);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ValueChanged((T)value, targetType, parameter, culture)) return Text;

            SetSourceValue((T)value, targetType, parameter, culture);

            return Text = ToString(CurrentValue, targetType, parameter, culture);
        }

        public T ConvertBack(string value, object parameter = null)
        {
            return (T)ConvertBack(value, typeof(T), parameter, CultureInfo.CurrentCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T newValue;

            Text = (string)value;

            if (AutoParseNullOrWhiteSpace && string.IsNullOrWhiteSpace((string)value)) newValue = NullOrWhiteSpaceValue;
            else if (!TryParse(Text, targetType, parameter, culture, out newValue)) return CurrentValue;

            SetSourceValue(newValue, targetType, parameter, culture);

            return newValue;
        }

        protected virtual bool ValueChanged(T newValue, Type targetType, object parameter, CultureInfo culture)
        {
            if (ReferenceEquals(CurrentValue, newValue)) return true;
            if (ReferenceEquals(CurrentValue, null)) return false;
            if (ReferenceEquals(newValue, null)) return false;

            return CurrentValue.Equals(newValue);
        }

        protected virtual void SetSourceValue(T value, Type targetType, object parameter, CultureInfo culture)
        {
            CurrentValue = value;
        }

        protected virtual string ToString(T value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        protected abstract bool TryParse(string newText, Type targetType, object parameter, CultureInfo culture, out T newValue);
    }
}
