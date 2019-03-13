using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public abstract class EnumConverter : DependencyObject, IValueConverter
    {

        public static readonly DependencyProperty EnumTypeProperty =
            DependencyProperty.Register("EnumType", typeof(Type), typeof(EnumConverter));

        private static void OnEnumTypePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (EnumConverter)sender;
            var newValue = (Type)e.NewValue;
            var oldValue = (Type)e.OldValue;

            if (!newValue.IsEnum) s.EnumType = oldValue;
        }

        public static readonly DependencyProperty ExampleProperty =
            DependencyProperty.Register("Example", typeof(Enum), typeof(EnumConverter));

        private Enum currentValue;

        public Type EnumType
        {
            get => (Type)GetValue(EnumTypeProperty);
            set => SetValue(EnumTypeProperty, value);
        }

        public Enum Example
        {
            get => (Enum)GetValue(ExampleProperty);
            set => SetValue(ExampleProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            currentValue = (Enum)value;

            return currentValue.Equals(GetValue(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value) return currentValue = GetValue(parameter.ToString());

            return currentValue;
        }

        protected virtual Enum GetValue(string name)
        {
            return (Enum)(EnumType != null ? Enum.Parse(EnumType, name) : Enum.Parse(Example.GetType(), name));
        }
    }
}
