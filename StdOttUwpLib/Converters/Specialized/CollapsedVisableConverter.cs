using Windows.UI.Xaml;

namespace StdOttUwp.Converters.Specialized
{
    public class CollapsedVisableConverter : IsTrueToValueConverter
    {
        public CollapsedVisableConverter()
        {
            EqualsValue = Visibility.Collapsed;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
