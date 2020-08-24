using StdOttStandard.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class StringsToLinesConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty RemoveEmptyLinesProperty =
            DependencyProperty.Register(nameof(RemoveEmptyLines), typeof(bool), typeof(StringsToLinesConverter),
                new PropertyMetadata(false, OnRemoveEmptyLinesPropertyChanged));

        private static void OnRemoveEmptyLinesPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            StringsToLinesConverter s = (StringsToLinesConverter)sender;
            bool value = (bool)e.NewValue;
        }

        public bool RemoveEmptyLines
        {
            get => (bool)GetValue(RemoveEmptyLinesProperty);
            set => SetValue(RemoveEmptyLinesProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((IEnumerable<string>)value)?.Join() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] array = ((string)value)?.Replace("\r\n", "\n").Split('\n');
            if (RemoveEmptyLines) array = array.Where(l => l.Length > 0).ToArray();

            return array;
        }
    }
}
