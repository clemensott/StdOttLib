using System;

namespace StdOttStandard.Converter.MultipleInputs
{
    public class SingleInputsConvertEventArgs : EventArgs
    {
        public int ChangedValueIndex { get; }

        public object OldValue { get; }

        public object Input { get; set; }

        public SingleInputsConvertEventArgs(int changedValueIndex, object oldValue)
        {
            ChangedValueIndex = changedValueIndex;
            OldValue = oldValue;
        }
    }
}
