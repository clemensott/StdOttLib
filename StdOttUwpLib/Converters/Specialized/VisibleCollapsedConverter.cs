using Windows.UI.Xaml;

namespace StdOttUwp.Converters.Specialized
{
    public class VisibleCollapsedConverter : IsTrueToValueConverter
    {
        public VisibleCollapsedConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Collapsed;
        }
    }
}
