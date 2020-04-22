using StdOttStandard.Converter.MultipleInputs;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public class SingleInputConverter : MultipleInputsConverter<SingleInputsConvertEventArgs>
    {
        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(object), typeof(SingleInputConverter),
                new PropertyMetadata(null, OnInputPropertyChanged));


        private static void OnInputPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SingleInputConverter)sender).SetOutput(0, e.OldValue);
        }
        
        public object Input
        {
            get => GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }
        
        protected override void SetOutputNonRef(int changedIndex, object oldValue)
        {
            SingleInputsConvertEventArgs args = new SingleInputsConvertEventArgs(changedIndex, oldValue)
            {
                Input = Input,
            };

            Output = GetLastConvert()(this, args); 
        }

        protected override void SetOutputRef(int changedIndex, object oldValue)
        {
            SingleInputsConvertEventArgs args = new SingleInputsConvertEventArgs(changedIndex, oldValue)
            {
                Input = Input,
            };

            Output = GetLastConvertRef()(this, args);

            if (!Equals(Input, args.Input)) Input = args.Input;
        }
    }
}
