using Windows.UI.Xaml;

namespace StdOttUwp.Converters.Specialized
{
    public class VisableCollapsedConverter : IsTrueToValueConverter
    {
        public VisableCollapsedConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Collapsed;
        }
    }
}
