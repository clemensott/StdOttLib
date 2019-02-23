﻿using Windows.UI.Xaml;

namespace StdOttUwp.Converters
{
    public delegate object ConvertInputs10EventHandler(object input0, object input1, object input2, 
        object input3, object input4, object input5, object input6, object input7, object input8, object input9);
    public delegate object ConvertInputs10RefEventHandler(ref object input0, ref object input1, ref object input2, ref object input3,
        ref object input4, ref object input5, ref object input6, ref object input7, ref object input8, ref object input9);

    public class MultipleInputs10Converter : FrameworkElement
    {
        public static readonly DependencyProperty OutputProperty = DependencyProperty.Register("Output",
            typeof(object), typeof(MultipleInputs10Converter), new PropertyMetadata(null));

        public static readonly DependencyProperty Input0Property =
            DependencyProperty.Register("Input0", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input1Property =
            DependencyProperty.Register("Input1", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input2Property =
            DependencyProperty.Register("Input2", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input3Property =
            DependencyProperty.Register("Input3", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input4Property =
            DependencyProperty.Register("Input4", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input5Property =
            DependencyProperty.Register("Input5", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input6Property =
            DependencyProperty.Register("Input6", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input7Property =
            DependencyProperty.Register("Input7", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input8Property =
            DependencyProperty.Register("Input8", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        public static readonly DependencyProperty Input9Property =
            DependencyProperty.Register("Input9", typeof(object), typeof(MultipleInputs10Converter),
                new PropertyMetadata(null, new PropertyChangedCallback(OnInputXPropertyChanged)));


        private static void OnInputXPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((MultipleInputs10Converter)sender).SetOutput();
        }

        private bool isUpdating;
        private ConvertInputs10EventHandler convert;
        private ConvertInputs10RefEventHandler convertRef;

        public ConvertInputs10EventHandler Convert
        {
            get { return convert; }
            set
            {
                if (value == convert) return;

                convert = value;
                SetOutput();
            }
        }

        public ConvertInputs10RefEventHandler ConvertRef
        {
            get { return convertRef; }
            set
            {
                if (value == convertRef) return;

                convertRef = value;
                SetOutput();
            }
        }

        public object Output
        {
            get { return GetValue(OutputProperty); }
            set { SetValue(OutputProperty, value); }
        }

        public object Input0
        {
            get { return GetValue(Input0Property); }
            set { SetValue(Input0Property, value); }
        }

        public object Input1
        {
            get { return GetValue(Input1Property); }
            set { SetValue(Input1Property, value); }
        }

        public object Input2
        {
            get { return GetValue(Input2Property); }
            set { SetValue(Input2Property, value); }
        }

        public object Input3
        {
            get { return GetValue(Input3Property); }
            set { SetValue(Input3Property, value); }
        }

        public object Input4
        {
            get { return GetValue(Input4Property); }
            set { SetValue(Input4Property, value); }
        }

        public object Input5
        {
            get { return GetValue(Input5Property); }
            set { SetValue(Input5Property, value); }
        }

        public object Input6
        {
            get { return GetValue(Input6Property); }
            set { SetValue(Input6Property, value); }
        }

        public object Input7
        {
            get { return GetValue(Input7Property); }
            set { SetValue(Input7Property, value); }
        }

        public object Input8
        {
            get { return GetValue(Input8Property); }
            set { SetValue(Input8Property, value); }
        }

        public object Input9
        {
            get { return GetValue(Input9Property); }
            set { SetValue(Input9Property, value); }
        }

        private void SetOutput()
        {
            if (Convert != null) SetOutputNonRef();
            else if (ConvertRef != null) SetOutputRef();
            else Output = null;
        }

        private void SetOutputNonRef()
        {
            Output = Convert(Input0, Input1, Input2, Input3, Input4, Input5, Input6, Input7, Input8, Input9);
        }

        private void SetOutputRef()
        {
            if (isUpdating) return;
            isUpdating = true;

            object input0 = Input0, input1 = Input1, input2 = Input2, input3 = Input3, input4 = Input4,
                input5 = Input5, input6 = Input6, input7 = Input7, input8 = Input8, input9 = Input9;

            Output = ConvertRef(ref input0, ref input1, ref input2, ref input3, 
                ref input4, ref input5, ref input6, ref input7, ref input8, ref input9);

            Input0 = input0;
            Input1 = input1;
            Input2 = input2;
            Input3 = input3;
            Input4 = input4;
            Input5 = input5;
            Input6 = input6;
            Input7 = input7;
            Input8 = input8;
            Input9 = input9;

            isUpdating = false;
        }
    }
}
