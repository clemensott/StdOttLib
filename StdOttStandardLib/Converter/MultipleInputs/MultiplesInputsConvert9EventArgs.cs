namespace StdOttStandard.Converter.MultipleInputs
{
    public class MultiplesInputsConvert9EventArgs : MultiplesInputsConvert8EventArgs
    {
        public object Input8 { get; set; }

        public MultiplesInputsConvert9EventArgs(int changedValueIndex, object oldValue) : base(changedValueIndex, oldValue)
        {
        }
    }
}
