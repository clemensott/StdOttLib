using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StdOttFramework.Converters
{
    public delegate object ConvertInputs4EventHandler(object input0, object input1, object input2, object input3);
    public delegate object ConvertInputs4RefEventHandler(ref object input0, ref object input1, ref object input2, ref object input3);

    public class MultipleInputs4Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs4Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs4Converter)sender).SetOutput();
        }

        private bool isUpdating;
        private List<ConvertInputs4EventHandler> converts = new List<ConvertInputs4EventHandler>();
        private List<ConvertInputs4RefEventHandler> convertRefs = new List<ConvertInputs4RefEventHandler>();

        public event ConvertInputs4EventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput();
            }
            remove
            {
                converts.Remove(value);
                SetOutput();
            }
        }

        public event ConvertInputs4RefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput();
            }
            remove
            {
                convertRefs.Remove(value);
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

        public object Input3
        {
            get { return GetValue(Input3Property); }
            set { SetValue(Input3Property, value); }
        }

        private void SetOutput()
        {
            if (converts.Count > 0) SetOutputNonRef();
            else if (convertRefs.Count > 0) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = converts.Last()(Input0, Input1, Input2, Input3);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2, input3 = Input3;

            Output = convertRefs.Last()(ref input0, ref input1, ref input2, ref input3);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;

            isUpdating = false;
        }
    }
}
