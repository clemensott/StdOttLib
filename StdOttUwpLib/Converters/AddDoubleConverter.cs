using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace StdOttUwp.Converters
{
    public class AddDoubleConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AddProperty =
            DependencyProperty.Register("Add", typeof(double?), typeof(AddDoubleConverter), new PropertyMetadata(null));

        public double? Add
        {
            get => (double?)GetValue(AddProperty);
            set => SetValue(AddProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double add = Add ?? ToDouble(parameter);

            return ToDouble(value) + add;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            double add = Add ?? ToDouble(parameter);

            return ToDouble(value) - add;
        }

        private static double ToDouble(object obj, double defaultValue = 0)
        {
            try
            {
                return obj is double value ? value : System.Convert.ToDouble(obj);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
