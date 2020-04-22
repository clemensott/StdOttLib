namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert5EventArgs : MultiplesInputsConvert4EventArgs
    {
        public object Input4 { get; set; }

        public MultiplesInputsConvert5EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
