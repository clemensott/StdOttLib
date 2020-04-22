using StdOttStandard.Converter.MultipleInputs;
using System.Windows;

namespace StdOttFramework.Converters
{
    public class MultipleInputs4Converter : MultipleInputsConverter<MultiplesInputsConvert4EventArgs>
    {
        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, OnInput0PropertyChanged));


        private static void OnInput0PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs4Converter)sender).SetOutput(0, e.OldValue);
        }

        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, OnInput1PropertyChanged));


        private static void OnInput1PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs4Converter)sender).SetOutput(1, e.OldValue);
        }

        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, OnInput2PropertyChanged));


        private static void OnInput2PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs4Converter)sender).SetOutput(2, e.OldValue);
        }

        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs4Converter),
                new PropertyMetadata(null, OnInput3PropertyChanged));


        private static void OnInput3PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs4Converter)sender).SetOutput(3, e.OldValue);
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
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert4EventArgs args = new MultiplesInputsConvert4EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
            };

            Output = GetLastConvert()(this, args); 
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert4EventArgs args = new MultiplesInputsConvert4EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
                Input3 = Input3,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input0, args.Input0)) Input0 = args.Input0;
            if (!Equals(Input1, args.Input1)) Input1 = args.Input1;
            if (!Equals(Input2, args.Input2)) Input2 = args.Input2;
            if (!Equals(Input3, args.Input3)) Input3 = args.Input3;
        }
    }
}
