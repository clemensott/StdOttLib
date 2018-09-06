using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttWpfLib.Converters
{
    public abstract class ToStringTwoWayConverter<T> : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AutoParseNullOrWhiteSpaceProperty =
            DependencyProperty.Register("AutoParseNullOrWhiteSpace", typeof(bool), typeof(ToStringTwoWayConverter<T>),
                new PropertyMetadata(null, new PropertyChangedCallback(OnAutoParseNullOrWhiteSpacePropertyChanged)));

        private static void OnAutoParseNullOrWhiteSpacePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (ToStringTwoWayConverter<T>)sender;
            var value = (T)e.NewValue;
        }

        public static readonly DependencyProperty NullOrWhiteSpaceValueProperty =
            DependencyProperty.Register("NullOrWhiteSpaceValue", typeof(T), typeof(ToStringTwoWayConverter<T>),
                new PropertyMetadata(null, new PropertyChangedCallback(OnNullOrWhiteSpaceValuePropertyChanged)));

        private static void OnNullOrWhiteSpaceValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (ToStringTwoWayConverter<T>)sender;
            var value = (T)e.NewValue;
        }

        protected T sourceValue;
        private string text;

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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ValueChanged((T)value, targetType, parameter, culture)) return text;

            SetSourceValue((T)value, targetType, parameter, culture);

            return text = ToString(sourceValue, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            T newValue;

            if (AutoParseNullOrWhiteSpace && string.IsNullOrWhiteSpace((string)value)) newValue = NullOrWhiteSpaceValue;
            else if (!TryParse((string)value, targetType, parameter, culture, out newValue)) return sourceValue;

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
