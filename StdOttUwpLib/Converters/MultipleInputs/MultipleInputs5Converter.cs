using StdOttStandard.Converter.MultipleInputs;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class MultipleInputs5Converter : MultipleInputsConverter<MultiplesInputsConvert5EventArgs>
    {
        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput(1, e.OldValue);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, OnInput2PropertyChanged));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput(2, e.OldValue);
        }

        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, OnInput3PropertyChanged));


        private static void OnInput3PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput(3, e.OldValue);
        }

        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs5Converter),
                new PropertyMetadata(null, OnInput4PropertyChanged));


        private static void OnInput4PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs5Converter)sender).SetOutput(4, e.OldValue);
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
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert5EventArgs args = new MultiplesInputsConvert5EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
                Input4 = Input4,
            };

            Output = GetLastConvert()(this, args); 
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert5EventArgs args = new MultiplesInputsConvert5EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
                Input4 = Input4,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input0, args.Input0)) Input0 = args.Input0;
            if (!Equals(Input1, args.Input1)) Input1 = args.Input1;
            if (!Equals(Input2, args.Input2)) Input2 = args.Input2;
            if (!Equals(Input3, args.Input3)) Input3 = args.Input3;
            if (!Equals(Input4, args.Input4)) Input4 = args.Input4;
        }
    }
}
