using System.Windows;

namespace StdOttFramework.Converters.Specialized
{
    public class VisibleHiddenConverter : IsTrueToValueConverter
    {
        public VisibleHiddenConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Hidden;
        }
    }
}
