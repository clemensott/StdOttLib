using System.Windows;

namespace StdOttFramework.Converters.Specialized
{
    public class HiddenVisibleConverter : IsTrueToValueConverter
    {
        public HiddenVisibleConverter()
        {
            EqualsValue = Visibility.Hidden;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
