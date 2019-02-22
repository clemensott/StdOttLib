using System.Windows;

namespace StdOttFramework.Converters.Specialized
{
    public class HiddenVisableConverter : IsTrueToValueConverter
    {
        public HiddenVisableConverter()
        {
            EqualsValue = Visibility.Hidden;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
