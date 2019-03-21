using System.Windows;

namespace StdOttFramework.Converters.Specialized
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
