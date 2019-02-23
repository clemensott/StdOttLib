using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs3EventHandler(object input0, object input1, object input2);
    public delegate object ConvertInputs3RefEventHandler(ref object input0, ref object input1, ref object input2);

    public class MultipleInputs3Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs10Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput();
        }

        private bool isUpdating;
        private ConvertInputs3EventHandler convert;
        private ConvertInputs3RefEventHandler convertRef;

        public ConvertInputs3EventHandler Convert
        {
            get { return convert; }
            set
            {
                if (value == convert) return;

                convert = value;
                SetOutput();
            }
        }

        public ConvertInputs3RefEventHandler ConvertRef
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

        public object Input0
        {
            get { return GetValue(Input0Property); }
            set { SetValue(Input0Property, value); }
        }

        public object Input1
        {
            get { return GetValue(Input1Property); }
            set { SetValue(Input1Property, value); }
        }

        public object Input2
        {
            get { return GetValue(Input2Property); }
            set { SetValue(Input2Property, value); }
        }

        private void SetOutput()
        {
            if (Convert != null) SetOutputNonRef();
            else if (ConvertRef != null) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = Convert(Input0, Input1, Input2);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2;

            Output = ConvertRef(ref input0, ref input1, ref input2);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;

            isUpdating = false;
        }
    }
}
