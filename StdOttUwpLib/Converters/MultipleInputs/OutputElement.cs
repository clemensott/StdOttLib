using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public abstract class OutputElement : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(OutputElement), new PropertyMetadata(null));

        public object Output
        {
            get => GetValue(OutputProperty);
            set => SetValue(OutputProperty, value);
        }
    }
}
