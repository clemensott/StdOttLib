using System.Windows;

namespace StdOttFramework.Converters.Specialized
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
