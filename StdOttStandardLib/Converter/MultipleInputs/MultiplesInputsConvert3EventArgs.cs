namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert3EventArgs : MultiplesInputsConvert2EventArgs
    {
        public object Input2 { get; set; }

        public MultiplesInputsConvert3EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
