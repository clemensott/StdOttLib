using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs2EventHandler(object sender, object input0, object input1, int changedInput, object oldValue);
    public delegate object ConvertInputs2RefEventHandler(object sender, ref object input0, ref object input1, int changedInput, object oldValue);

    public class MultipleInputs2Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs2Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs2Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs2Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs2Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs2Converter)sender).SetOutput(1, e.OldValue);
        }

        private bool isUpdating;
        private readonly List<ConvertInputs2EventHandler> converts = new List<ConvertInputs2EventHandler>();
        private readonly List<ConvertInputs2RefEventHandler> convertRefs = new List<ConvertInputs2RefEventHandler>();

        public event ConvertInputs2EventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput(-1, null);
            }
            remove
            {
                converts.Remove(value);
                SetOutput(-1, null);
            }
        }

        public event ConvertInputs2RefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput(-1, null);
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput(-1, null);
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

        private void SetOutput(int changedIndex, object oldValue)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex, oldValue);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex, oldValue);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex, object oldValue)
        {
            Output = converts.Last()(this, Input0, Input1, changedIndex, oldValue);
        }

        private void SetOutputRef(int changedIndex, object oldValue)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1;

            Output = convertRefs.Last()(this, ref input0, ref input1, changedIndex, oldValue);

            if (!Equals(Input0, input0)) Input0 = input0;
            if (!Equals(Input1, input1)) Input1 = input1;

            isUpdating = false;
        }
    }
}
