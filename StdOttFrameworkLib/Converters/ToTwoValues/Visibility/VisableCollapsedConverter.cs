using System.Windows;

namespace StdOttFramework.Converters
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
