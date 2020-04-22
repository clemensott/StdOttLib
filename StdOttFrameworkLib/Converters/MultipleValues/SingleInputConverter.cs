using StdOttStandard.Equal;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace StdOttFramework.Converters
{
    public delegate object ConvertInputs1EventHandler(object sender, object input, int changedInput, object oldValue);
    public delegate object ConvertInputs1RefEventHandler(object sender, ref object input, int changedInput, object oldValue);

    public class SingleInputConverter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(SingleInputConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(object), typeof(SingleInputConverter),
                new PropertyMetadata(null, OnInputPropertyChanged));


        private static void OnInputPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SingleInputConverter)sender).SetOutput(0);
        }

        private bool isUpdating;
        private readonly List<ConvertInputs1EventHandler> converts = new List<ConvertInputs1EventHandler>();
        private readonly List<ConvertInputs1RefEventHandler> convertRefs = new List<ConvertInputs1RefEventHandler>();

        public event ConvertInputs1EventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput(-1);
            }
            remove
            {
                converts.Remove(value);
                SetOutput(-1);
            }
        }

        public event ConvertInputs1RefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput(-1);
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput(-1);
            }
        }

        public object Output
        {
            get => GetValue(OutputProperty);
            set => SetValue(OutputProperty, value);
        }

        public object Input
        {
            get => GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        private void SetOutput(int changedIndex)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex)
        {
            Output = converts.Last()(Input, changedIndex);
        }

        private void SetOutputRef(int changedIndex)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input = Input;

            Output = convertRefs.Last()(ref input, changedIndex);

            if (!Equals(Input, input)) Input = input;

            isUpdating = false;
        }
    }
}
