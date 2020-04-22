using StdOttStandard.Converter.MultipleInputs;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class MultipleInputs3Converter : MultipleInputsConverter<MultiplesInputsConvert3EventArgs>
    {
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
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert3EventArgs args = new MultiplesInputsConvert3EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
            };

            Output = GetLastConvert()(this, args); 
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert3EventArgs args = new MultiplesInputsConvert3EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
                Input2 = Input2,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input0, args.Input0)) Input0 = args.Input0;
            if (!Equals(Input1, args.Input1)) Input1 = args.Input1;
            if (!Equals(Input2, args.Input2)) Input2 = args.Input2;
        }
    }
}
