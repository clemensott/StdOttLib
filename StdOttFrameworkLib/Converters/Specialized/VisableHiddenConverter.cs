using System.Windows;

namespace StdOttFramework.Converters.Specialized
{
    public class VisableHiddenConverter : IsTrueToValueConverter
    {
        public VisableHiddenConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Hidden;
        }
    }
}
