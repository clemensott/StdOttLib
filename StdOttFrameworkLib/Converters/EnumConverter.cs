using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class EnumConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register("EnumType",
            typeof(Type), typeof(EnumConverter), new PropertyMetadata(), ValidateEnumType);

        private static bool ValidateEnumType(object value)
        {
            return value == null || ((Type)value).IsEnum;
        }

        public static readonly DependencyProperty ExampleProperty = DependencyProperty.Register("Example",
            typeof(Enum), typeof(EnumConverter), new PropertyMetadata(), ValidateExample);

        private static bool ValidateExample(object value)
        {
            return value?.GetType().IsEnum ?? true;
        }

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
            if (EnumType != null) return (Enum)Enum.Parse(EnumType, name, true);
            if (Example != null) return (Enum)Enum.Parse(Example.GetType(), name, true);

            return (Enum)Enum.Parse(currentValue.GetType(), name, true);
        }
    }
}