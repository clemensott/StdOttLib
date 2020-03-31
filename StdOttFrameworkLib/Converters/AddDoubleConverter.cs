using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class AddDoubleConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AddProperty =
            DependencyProperty.Register("Add", typeof(double?), typeof(AddDoubleConverter));

        public double? Add
        {
            get => (double?) GetValue(AddProperty);
            set => SetValue(AddProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double add = Add ?? ToDouble(parameter);

            return ToDouble(value) + add;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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