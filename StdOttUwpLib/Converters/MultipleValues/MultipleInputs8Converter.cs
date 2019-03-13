using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs8EventHandler(object input0, object input1, object input2, object input3, object input4, object input5, object input6, object input7, int changedInput);
    public delegate object ConvertInputs8RefEventHandler(ref object input0, ref object input1, ref object input2, ref object input3, ref object input4, ref object input5, ref object input6, ref object input7, int changedInput);

    public class MultipleInputs8Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs8Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput0PropertyChanged)));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(0);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput1PropertyChanged)));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(1);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput2PropertyChanged)));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(2);
        }

        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput3PropertyChanged)));


        private static void OnInput3PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(3);
        }

        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput4PropertyChanged)));


        private static void OnInput4PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(4);
        }

        public static readonly DependencyProperty Input5Property =
            DependencyProperty.Register("Input5", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput5PropertyChanged)));


        private static void OnInput5PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(5);
        }

        public static readonly DependencyProperty Input6Property =
            DependencyProperty.Register("Input6", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput6PropertyChanged)));


        private static void OnInput6PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(6);
        }

        public static readonly DependencyProperty Input7Property =
            DependencyProperty.Register("Input7", typeof(object), typeof(MultipleInputs8Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInput7PropertyChanged)));


        private static void OnInput7PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs8Converter)sender).SetOutput(7);
        }

        private bool isUpdating;
        private List<ConvertInputs8EventHandler> converts = new List<ConvertInputs8EventHandler>();
        private List<ConvertInputs8RefEventHandler> convertRefs = new List<ConvertInputs8RefEventHandler>();

        public event ConvertInputs8EventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput(-1);
            }
            remove
            {
                converts.Remove(value);
                SetOutput(-1);
            }
        }

        public event ConvertInputs8RefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput(-1);
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput(-1);
            }
        }

        public object Output
        {
            get => GetValue(OutputProperty);
            set => SetValue(OutputProperty, value);
        }

        public object Input0
        {
            get => GetValue(Input0Property);
            set => SetValue(Input0Property, value);
        }

        public object Input1
        {
            get => GetValue(Input1Property);
            set => SetValue(Input1Property, value);
        }

        public object Input2
        {
            get => GetValue(Input2Property);
            set => SetValue(Input2Property, value);
        }

        public object Input3
        {
            get => GetValue(Input3Property);
            set => SetValue(Input3Property, value);
        }

        public object Input4
        {
            get => GetValue(Input4Property);
            set => SetValue(Input4Property, value);
        }

        public object Input5
        {
            get => GetValue(Input5Property);
            set => SetValue(Input5Property, value);
        }

        public object Input6
        {
            get => GetValue(Input6Property);
            set => SetValue(Input6Property, value);
        }

        public object Input7
        {
            get => GetValue(Input7Property);
            set => SetValue(Input7Property, value);
        }

        private void SetOutput(int changedIndex)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex)
        {
            Output = converts.Last()(Input0, Input1, Input2, Input3, Input4, Input5, Input6, Input7, changedIndex);
        }

        private void SetOutputRef(int changedIndex)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2, input3 = Input3, input4 = Input4, input5 = Input5, input6 = Input6, input7 = Input7;

            Output = convertRefs.Last()(ref input0, ref input1, ref input2, ref input3, ref input4, ref input5, ref input6, ref input7, changedIndex);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            Input4 = input4;
            Input5 = input5;
            Input6 = input6;
            Input7 = input7;

            isUpdating = false;
        }
    }
}
