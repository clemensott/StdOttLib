using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
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
            ((SingleInputConverter)sender).SetOutput(0, e.OldValue);
        }

        private bool isUpdating;
        private readonly List<ConvertInputs1EventHandler> converts = new List<ConvertInputs1EventHandler>();
        private readonly List<ConvertInputs1RefEventHandler> convertRefs = new List<ConvertInputs1RefEventHandler>();

        public event ConvertInputs1EventHandler Convert
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

        public event ConvertInputs1RefEventHandler ConvertRef
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

        public object Input
        {
            get => GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        private void SetOutput(int changedIndex, object oldValue)
        {
            if (converts.Count > 0) SetOutputNonRef(changedIndex, oldValue);
            else if (convertRefs.Count > 0) SetOutputRef(changedIndex, oldValue);
            else Output = null;
        }

        private void SetOutputNonRef(int changedIndex, object oldValue)
        {
            Output = converts.Last()(this, Input, changedIndex, oldValue);
        }

        private void SetOutputRef(int changedIndex, object oldValue)
        {
            if (isUpdating) return;
            isUpdating = true;

            object input = Input;

            Output = convertRefs.Last()(this, ref input, changedIndex, oldValue);

            if (!Equals(Input, input)) Input = input;

            isUpdating = false;
        }
    }
}
