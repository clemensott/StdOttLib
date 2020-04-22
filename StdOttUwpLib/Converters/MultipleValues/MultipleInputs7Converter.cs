using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs7EventHandler(object sender, object input0, object input1, object input2, object input3, object input4, object input5, object input6, int changedInput, object oldValue);
    public delegate object ConvertInputs7RefEventHandler(object sender, ref object input0, ref object input1, ref object input2, ref object input3, ref object input4, ref object input5, ref object input6, int changedInput, object oldValue);

    public class MultipleInputs7Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs7Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(1, e.OldValue);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput2PropertyChanged));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(2, e.OldValue);
        }

        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput3PropertyChanged));


        private static void OnInput3PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(3, e.OldValue);
        }

        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput4PropertyChanged));


        private static void OnInput4PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(4, e.OldValue);
        }

        public static readonly DependencyProperty Input5Property =
            DependencyProperty.Register("Input5", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput5PropertyChanged));


        private static void OnInput5PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(5, e.OldValue);
        }

        public static readonly DependencyProperty Input6Property =
            DependencyProperty.Register("Input6", typeof(object), typeof(MultipleInputs7Converter),
                new PropertyMetadata(null, OnInput6PropertyChanged));


        private static void OnInput6PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs7Converter)sender).SetOutput(6, e.OldValue);
        }

        private bool isUpdating;
        private readonly List<ConvertInputs7EventHandler> converts = new List<ConvertInputs7EventHandler>();
        private readonly List<ConvertInputs7RefEventHandler> convertRefs = new List<ConvertInputs7RefEventHandler>();

        public event ConvertInputs7EventHandler Convert
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

        public event ConvertInputs7RefEventHandler ConvertRef
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

        private void SetOutput(int changedIndex, object oldValue)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex, oldValue);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex, oldValue);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex, object oldValue)
        {
            Output = converts.Last()(this, Input0, Input1, Input2, Input3, Input4, Input5, Input6, changedIndex, oldValue);
        }

        private void SetOutputRef(int changedIndex, object oldValue)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2, input3 = Input3, input4 = Input4, input5 = Input5, input6 = Input6;

            Output = convertRefs.Last()(this, ref input0, ref input1, ref input2, ref input3, ref input4, ref input5, ref input6, changedIndex, oldValue);

            if (!Equals(Input0, input0)) Input0 = input0;
            if (!Equals(Input1, input1)) Input1 = input1;
            if (!Equals(Input2, input2)) Input2 = input2;
            if (!Equals(Input3, input3)) Input3 = input3;
            if (!Equals(Input4, input4)) Input4 = input4;
            if (!Equals(Input5, input5)) Input5 = input5;
            if (!Equals(Input6, input6)) Input6 = input6;

            isUpdating = false;
        }
    }
}
