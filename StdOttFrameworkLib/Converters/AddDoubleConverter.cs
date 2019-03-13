﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace StdOttFramework.Converters
{
    public class AddDoubleConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty AddProperty =
            DependencyProperty.Register("Add", typeof(double?), typeof(AddDoubleConverter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnAddPropertyChanged)));

        private static void OnAddPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (AddDoubleConverter)sender;
            var value = (double?)e.NewValue;
        }

        public double? Add
        {
            get => (double?)GetValue(AddProperty);
            set => SetValue(AddProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double add = Add ?? (double)parameter;

            return (double)value + add;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double add = Add ?? (double)parameter;

            return (double)value - (double)parameter;
        }
    }
}
