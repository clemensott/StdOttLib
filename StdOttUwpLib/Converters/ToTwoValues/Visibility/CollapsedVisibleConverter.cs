using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class CollapsedVisibleConverter : TruthyConverter
    {
        public CollapsedVisibleConverter()
        {
            EqualsValue = Visibility.Collapsed;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
