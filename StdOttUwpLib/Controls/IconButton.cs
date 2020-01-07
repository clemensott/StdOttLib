using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace StdOttUwp.Controls
{
    public class IconButton : Button
    {
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register("Symbol", typeof(Symbol),
            typeof(IconButton), new PropertyMetadata(null, OnSymbolPropertyChanged));

        private static void OnSymbolPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            IconButton s = (IconButton)sender;
            Symbol value = (Symbol)e.NewValue;

            s.iconControl.Symbol = value;
        }

        private readonly SymbolIcon iconControl;

        public Symbol Symbol
        {
            get => (Symbol)GetValue(SymbolProperty);
            set => SetValue(SymbolProperty, value);
        }

        public IconButton()
        {
            Content = iconControl = new SymbolIcon();
        }
    }
}
