using System.Windows;

namespace StdOttFramework.Converters
{
    public class VisibleHiddenConverter : TruthyConverter
    {
        public VisibleHiddenConverter()
        {
            EqualsValue = Visibility.Visible;
            NotEqualsValue = Visibility.Hidden;
        }
    }
}
