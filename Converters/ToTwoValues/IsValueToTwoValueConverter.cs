using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttWpfLib.Converters
{
    public class IsValueToTwoValueConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register("CompareValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public static readonly DependencyProperty EqualsValueProperty =
            DependencyProperty.Register("EqualsValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public static readonly DependencyProperty NotEqualsValueProperty =
            DependencyProperty.Register("NotEqualsValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public object CompareValue
        {
            get { return GetValue(CompareValueProperty); }
            set { SetValue(CompareValueProperty, value); }
        }

        public object EqualsValue
        {
            get { return GetValue(EqualsValueProperty); }
            set { SetValue(EqualsValueProperty, value); }
        }

        public object NotEqualsValue
        {
            get { return GetValue(NotEqualsValueProperty); }
            set { SetValue(NotEqualsValueProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsValue(value, targetType, parameter, culture) ? EqualsValue : NotEqualsValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw Utils.GetNotImplementedExeption(this, nameof(ConvertBack));
        }

        protected virtual bool IsValue(object input, Type targetType, object parameter, CultureInfo culture)
        {
            if (ReferenceEquals(CompareValue, input)) return true;
            if (!ReferenceEquals(CompareValue, null)) return CompareValue.Equals(input);
            if (!ReferenceEquals(input, null)) return input.Equals(CompareValue);

            throw Utils.GetNotImplementedExeption(this, nameof(IsValue));
        }
    }
}
