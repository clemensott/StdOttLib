using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputEventHandler(object input);
    public delegate object ConvertInputRefEventHandler(ref object input);

    public class SingleValueConverter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
             typeof(object), typeof(SingleValueConverter), new PropertyMetadata(null));

        public static readonly DependencyProperty InputProperty =
            DependencyProperty.Register("Input", typeof(object), typeof(SingleValueConverter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));

        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((SingleValueConverter)sender).SetOutput();
        }

        private bool isUpdating;
        private List<ConvertInputEventHandler> converts = new List<ConvertInputEventHandler>();
        private List<ConvertInputRefEventHandler> convertRefs = new List<ConvertInputRefEventHandler>();

        public event ConvertInputEventHandler Convert
        {
            add
            {
                converts.Add(value);
                SetOutput();
            }
            remove
            {
                converts.Remove(value);
                SetOutput();
            }   
        }

        public event ConvertInputRefEventHandler ConvertRef
        {
            add
            {
                convertRefs.Add(value);
                SetOutput();
            }
            remove
            {
                convertRefs.Remove(value);
                SetOutput();
            }
        }

        public object Output
        {
            get { return GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public object Input
        {
            get { return GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private void SetOutput()
        {
            if (converts.Count > 0) SetOutputNonRef();
            else if (convertRefs.Count > 0) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = converts.Last()(Input);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input = Input;
            Output = convertRefs.Last()(ref input);
            Input = input;
            isUpdating = false;
        }
    }
}
