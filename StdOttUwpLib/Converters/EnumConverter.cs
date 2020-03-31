using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public abstract class EnumConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty EnumTypeProperty = DependencyProperty.Register("EnumType",
            typeof(Type), typeof(EnumConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty ExampleProperty = DependencyProperty.Register("Example",
            typeof(Enum), typeof(EnumConverter), new PropertyMetadata(null));

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

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            currentValue = (Enum)value;

            return currentValue.Equals(GetValue(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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