using StdOttStandard.Equal;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public class IsValueToTwoValueConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty CompareValueProperty =
            DependencyProperty.Register("CompareValue", typeof(object),
                typeof(IsValueToTwoValueConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty EqualsValueProperty =
            DependencyProperty.Register("EqualsValue", typeof(object),
                typeof(IsValueToTwoValueConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty NotEqualsValueProperty =
            DependencyProperty.Register("NotEqualsValue", typeof(object),
                typeof(IsValueToTwoValueConverter), new PropertyMetadata(null));

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
            get => (TwoValueDecideType)GetValue(DecideTypeProperty);
            set => SetValue(DecideTypeProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return IsValue(value, targetType, parameter, language) ? EqualsValue : NotEqualsValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        protected virtual bool IsValue(object input, Type targetType, object parameter, string language)
        {
            return EqualUtils.Equals(input, CompareValue, DecideType);
        }
    }
}
