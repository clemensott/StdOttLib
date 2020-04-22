namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert4EventArgs : MultiplesInputsConvert3EventArgs
    {
        public object Input3 { get; set; }

        public MultiplesInputsConvert4EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
