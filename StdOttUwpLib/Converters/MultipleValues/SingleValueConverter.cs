using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputEventHandler(object input);
    public delegate object ConvertInputRefEventHandler(ref object input);

    public class SingleValueConverter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
             typeof(object), typeof(SingleValueConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(object), typeof(SingleValueConverter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));

        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SingleValueConverter)sender).SetOutput();
        }

        private bool isUpdating;
        private ConvertInputEventHandler convert;
        private ConvertInputRefEventHandler convertRef;

        public ConvertInputEventHandler Convert
        {
            get { return convert; }
            set
            {
                if (value == convert) return;

                convert = value;
                SetOutput();
            }
        }

        public ConvertInputRefEventHandler ConvertRef
        {
            get { return convertRef; }
            set
            {
                if (value == convertRef) return;

                convertRef = value;
                SetOutput();
            }
        }

        public object Output
        {
            get { return GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public object Input
        {
            get { return GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private void SetOutput()
        {
            if (Convert != null) SetOutputNonRef();
            else if (ConvertRef != null) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = Convert(Input);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input = Input;
            Output = ConvertRef(ref input);
            Input = input;
            isUpdating = false;
        }
    }
}
