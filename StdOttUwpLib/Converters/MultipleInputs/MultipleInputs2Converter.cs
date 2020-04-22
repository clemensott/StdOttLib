using StdOttStandard.Converter.MultipleInputs;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs2EventHandler(object sender, object input0, object input1, int changedInput, object oldValue);
    public delegate object ConvertInputs2RefEventHandler(object sender, ref object input0, ref object input1, int changedInput, object oldValue);

    public class MultipleInputs2Converter : MultipleInputsConverter<MultiplesInputsConvert2EventArgs>
    {
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
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert2EventArgs args = new MultiplesInputsConvert2EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
            };

            Output = GetLastConvert()(this, args); 
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            MultiplesInputsConvert2EventArgs args = new MultiplesInputsConvert2EventArgs(changedIndex, oldValue)
            {
                Input0 = Input0,
                Input1 = Input1,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input0, args.Input0)) Input0 = args.Input0;
            if (!Equals(Input1, args.Input1)) Input1 = args.Input1;
        }
    }
}
