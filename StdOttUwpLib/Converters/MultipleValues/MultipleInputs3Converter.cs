using StdOttStandard.Equal;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs3EventHandler(object sender, object input0, object input1, object input2, int changedInput, object oldValue);
    public delegate object ConvertInputs3RefEventHandler(object sender, ref object input0, ref object input1, ref object input2, int changedInput, object oldValue);

    public class MultipleInputs3Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs3Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(1, e.OldValue);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs3Converter),
                new PropertyMetadata(null, OnInput2PropertyChanged));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs3Converter)sender).SetOutput(2, e.OldValue);
        }

        private bool isUpdating;
        private readonly List<ConvertInputs3EventHandler> converts = new List<ConvertInputs3EventHandler>();
        private readonly List<ConvertInputs3RefEventHandler> convertRefs = new List<ConvertInputs3RefEventHandler>();

        public event ConvertInputs3EventHandler Convert
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

        public event ConvertInputs3RefEventHandler ConvertRef
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

        public object Input2
        {
            get => GetValue(Input2Property);
            set => SetValue(Input2Property, value);
        }

        private void SetOutput(int changedIndex, object oldValue)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex, oldValue);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex, oldValue);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex, object oldValue)
        {
            Output = converts.Last()(this, Input0, Input1, Input2, changedIndex, oldValue);
        }

        private void SetOutputRef(int changedIndex, object oldValue)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2;

            Output = convertRefs.Last()(this, ref input0, ref input1, ref input2, changedIndex, oldValue);

            if (!Equals(Input0, input0)) Input0 = input0;
            if (!Equals(Input1, input1)) Input1 = input1;
            if (!Equals(Input2, input2)) Input2 = input2;

            isUpdating = false;
        }
    }
}
