namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert7EventArgs : MultiplesInputsConvert6EventArgs
    {
        public object Input6 { get; set; }

        public MultiplesInputsConvert7EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
