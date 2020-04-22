using System;

namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert2EventArgs : EventArgs
    {
        public int ChangedValueIndex { get; }

        public object OldValue { get; }

        public object Input0 { get; set; }

        public object Input1 { get; set; }

        public MultiplesInputsConvert2EventArgs(int changedValueIndex, object oldValue)
        {
            ChangedValueIndex = changedValueIndex;
            OldValue = oldValue;
        }
    }
}
