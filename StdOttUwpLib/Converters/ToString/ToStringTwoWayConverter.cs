using System;
using Windows.System.UserProfile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public abstract class ToStringTwoWayConverter<T> : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AutoParseNullOrWhiteSpaceProperty =
            DependencyProperty.Register("AutoParseNullOrWhiteSpace", typeof(bool),
                typeof(ToStringTwoWayConverter<T>),new PropertyMetadata(false));

        public static readonly DependencyProperty NullOrWhiteSpaceValueProperty =
            DependencyProperty.Register("NullOrWhiteSpaceValue", typeof(object),
                typeof(ToStringTwoWayConverter<T>),new PropertyMetadata(null));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), 
                typeof(ToStringTwoWayConverter<T>),new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
                typeof(ToStringTwoWayConverter<T>),new PropertyMetadata(string.Empty));

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

        public T Value
        {
            get { return (T)(GetValue(ValueProperty) ?? default(T)); }
            set { SetValue(ValueProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string Convert(T value, object parameter = null)
        {
            return (string)Convert(value, typeof(string), parameter, GlobalizationPreferences.Languages[0]);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (ValueChanged((T)value, targetType, parameter, language)) return Text;

            SetSourceValue((T)value, targetType, parameter, language);

            return Text = ToString(Value, targetType, parameter, language);
        }

        public T ConvertBack(string value, object parameter = null)
        {
            return (T)ConvertBack(value, typeof(T), parameter, GlobalizationPreferences.Languages[0]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            T newValue;

            Text = (string)value;

            if (AutoParseNullOrWhiteSpace && string.IsNullOrWhiteSpace((string)value)) newValue = NullOrWhiteSpaceValue;
            else if (!TryParse(Text, targetType, parameter, language, out newValue)) return Value;

            SetSourceValue(newValue, targetType, parameter, language);

            return newValue;
        }

        protected virtual bool ValueChanged(T newValue, Type targetType, object parameter, string language)
        {
            if (ReferenceEquals(Value, newValue)) return true;
            if (ReferenceEquals(Value, null)) return false;
            if (ReferenceEquals(newValue, null)) return false;

            return Value.Equals(newValue);
        }

        protected virtual void SetSourceValue(T value, Type targetType, object parameter, string language)
        {
            Value = value;
        }

        protected virtual string ToString(T value, Type targetType, object parameter, string language)
        {
            return value.ToString();
        }

        protected abstract bool TryParse(string newText, Type targetType, object parameter, string language, out T newValue);
    }
}
