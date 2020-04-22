using StdOttStandard.Converter.MultipleInputs;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class MultipleInputs9Converter : MultipleInputsConverter<MultiplesInputsConvert9EventArgs>
    {
        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(1, e.OldValue);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput2PropertyChanged));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(2, e.OldValue);
        }

        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput3PropertyChanged));


        private static void OnInput3PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(3, e.OldValue);
        }

        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput4PropertyChanged));


        private static void OnInput4PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(4, e.OldValue);
        }

        public static readonly DependencyProperty Input5Property =
            DependencyProperty.Register("Input5", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput5PropertyChanged));


        private static void OnInput5PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(5, e.OldValue);
        }

        public static readonly DependencyProperty Input6Property =
            DependencyProperty.Register("Input6", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput6PropertyChanged));


        private static void OnInput6PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(6, e.OldValue);
        }

        public static readonly DependencyProperty Input7Property =
            DependencyProperty.Register("Input7", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput7PropertyChanged));


        private static void OnInput7PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(7, e.OldValue);
        }

        public static readonly DependencyProperty Input8Property =
            DependencyProperty.Register("Input8", typeof(object), typeof(MultipleInputs9Converter),
                new PropertyMetadata(null, OnInput8PropertyChanged));


        private static void OnInput8PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs9Converter)sender).SetOutput(8, e.OldValue);
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

        public object Input8
        {
            get => GetValue(Input8Property);
            set => SetValue(Input8Property, value);
        }
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert9EventArgs args = new MultiplesInputsConvert9EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
                Input4 = Input4,
                Input5 = Input5,
                Input6 = Input6,
                Input7 = Input7,
                Input8 = Input8,
            };

            Output = GetLastConvert()(this, args);
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert9EventArgs args = new MultiplesInputsConvert9EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
                Input4 = Input4,
                Input5 = Input5,
                Input6 = Input6,
                Input7 = Input7,
                Input8 = Input8,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input0, args.Input0)) Input0 = args.Input0;
            if (!Equals(Input1, args.Input1)) Input1 = args.Input1;
            if (!Equals(Input2, args.Input2)) Input2 = args.Input2;
            if (!Equals(Input3, args.Input3)) Input3 = args.Input3;
            if (!Equals(Input4, args.Input4)) Input4 = args.Input4;
            if (!Equals(Input5, args.Input5)) Input5 = args.Input5;
            if (!Equals(Input6, args.Input6)) Input6 = args.Input6;
            if (!Equals(Input7, args.Input7)) Input7 = args.Input7;
            if (!Equals(Input8, args.Input8)) Input8 = args.Input8;
        }
    }
}
