using System.Windows;

namespace StdOttFramework.Converters.Specialized
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
