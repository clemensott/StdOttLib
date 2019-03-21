using Windows.UI.Xaml;

namespace StdOttUwp.Converters.Specialized
{
    public class CollapsedVisibleConverter : IsTrueToValueConverter
    {
        public CollapsedVisibleConverter()
        {
            EqualsValue = Visibility.Collapsed;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
