namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert8EventArgs : MultiplesInputsConvert7EventArgs
    {
        public object Input7 { get; set; }

        public MultiplesInputsConvert8EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
