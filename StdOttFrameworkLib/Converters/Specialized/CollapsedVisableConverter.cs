using System.Windows;

namespace StdOttFramework.Converters.Specialized
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
