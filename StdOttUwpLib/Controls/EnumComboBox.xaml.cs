using StdOttStandard.Linq;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace StdOttUwp.Controls
{
    public sealed partial class EnumComboBox : UserControl
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(EnumComboBox),
                new PropertyMetadata(null, OnSelectedItemPropertyChanged));

        private static void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EnumComboBox s = (EnumComboBox)sender;
            object value = e.NewValue;
            
            s.cbx.SelectedIndex = s.cbx.Items.IndexOf(v => Equals(v, value));
        }


        public static readonly DependencyProperty NamesProperty =
            DependencyProperty.Register("Names", typeof(IDictionary<object, string>), typeof(EnumComboBox),
                new PropertyMetadata(null, OnNamesPropertyChanged));

        private static void OnNamesPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            EnumComboBox s = (EnumComboBox)sender;
            IDictionary<object, string> value = (IDictionary<object, string>)e.NewValue;

            s.cbx.ItemsSource = value?.Keys;

            object selectedItem = s.SelectedItem;
            s.cbx.SelectedIndex = s.cbx.Items.IndexOf(v => Equals(v, selectedItem));
        }

        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public IDictionary<object, string> Names
        {
            get => (IDictionary<object, string>)GetValue(NamesProperty);
            set => SetValue(NamesProperty, value);
        }


        public EnumComboBox()
        {
            this.InitializeComponent();
        }

        private object NameConverter_ConvertEvent(object value, Type targetType, object parameter, string language)
        {
            string text;
            return Names.TryGetValue(value, out text) ? text : value?.ToString();
        }

        private void Cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = cbx.SelectedItem;
        }
    }
}
