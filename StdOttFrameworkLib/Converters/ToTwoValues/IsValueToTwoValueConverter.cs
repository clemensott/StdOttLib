using StdOttStandard.Equal;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class IsValueToTwoValueConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register("CompareValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public static readonly DependencyProperty EqualsValueProperty =
            DependencyProperty.Register("EqualsValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public static readonly DependencyProperty NotEqualsValueProperty =
            DependencyProperty.Register("NotEqualsValue", typeof(object), typeof(IsValueToTwoValueConverter));

        public static readonly DependencyProperty DecideTypeProperty =
            DependencyProperty.Register("DecideType", typeof(TwoValueDecideType),
                typeof(IsValueToTwoValueConverter), new PropertyMetadata(TwoValueDecideType.Equal));

        public object CompareValue
        {
            get => GetValue(CompareValueProperty);
            set => SetValue(CompareValueProperty, value);
        }

        public object EqualsValue
        {
            get => GetValue(EqualsValueProperty);
            set => SetValue(EqualsValueProperty, value);
        }

        public object NotEqualsValue
        {
            get => GetValue(NotEqualsValueProperty);
            set => SetValue(NotEqualsValueProperty, value);
        }

        public TwoValueDecideType DecideType
        {
            get => (TwoValueDecideType)GetValue(NotEqualsValueProperty);
            set => SetValue(NotEqualsValueProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return IsValue(value, targetType, parameter, culture) ? EqualsValue : NotEqualsValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsValue(object input, Type targetType, object parameter, CultureInfo culture)
        {
            return CompareUtils.Compare(input, CompareValue, DecideType);
        }
    }
}
