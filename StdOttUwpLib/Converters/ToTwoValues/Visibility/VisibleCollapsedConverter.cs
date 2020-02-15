using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class VisibleCollapsedConverter : TruthyConverter
    {
        public VisibleCollapsedConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Collapsed;
        }
    }
}
