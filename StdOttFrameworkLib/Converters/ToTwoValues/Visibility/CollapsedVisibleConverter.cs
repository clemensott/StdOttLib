using System.Windows;

namespace StdOttFramework.Converters
{
    public class CollapsedVisibleConverter : TruthyConverter
    {
        public CollapsedVisibleConverter()
        {
            EqualsValue = Visibility.Collapsed;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
