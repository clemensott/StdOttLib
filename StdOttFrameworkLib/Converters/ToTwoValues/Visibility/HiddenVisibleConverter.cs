using System.Windows;

namespace StdOttFramework.Converters
{
    public class HiddenVisibleConverter : TruthyConverter
    {
        public HiddenVisibleConverter()
        {
            EqualsValue = Visibility.Hidden;
            NotEqualsValue = Visibility.Visible;
        }
    }
}
