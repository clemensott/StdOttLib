namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert6EventArgs : MultiplesInputsConvert5EventArgs
    {
        public object Input5 { get; set; }

        public MultiplesInputsConvert6EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
