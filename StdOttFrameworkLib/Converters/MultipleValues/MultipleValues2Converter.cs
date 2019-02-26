using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StdOttFramework.Converters
{
    public delegate object ConvertInputs2EventHandler(object input0, object input1);
    public delegate object ConvertInputs2RefEventHandler(ref object input0, ref object input1);

    public class MultipleInputs2Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs2Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs2Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs2Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs2Converter)sender).SetOutput();
        }

        private bool isUpdating;
        private List<ConvertInputs2EventHandler> converts = new List<ConvertInputs2EventHandler>();
        private List<ConvertInputs2RefEventHandler> convertRefs = new List<ConvertInputs2RefEventHandler>();

        public event ConvertInputs2EventHandler Convert
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

        public event ConvertInputs2RefEventHandler ConvertRef
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

        private void SetOutput()
        {
            if (converts.Count > 0) SetOutputNonRef();
            else if (convertRefs.Count > 0) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = converts.Last()(Input0, Input1);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1;

            Output = convertRefs.Last()(ref input0, ref input1);

            Input0 = input0;
            Input1 = input1;

            isUpdating = false;
        }
    }
}
