namespace StdOttFramework.Converters
{
    class ToFalseConverter : IsValueToTwoValueConverter
    {
        public ToFalseConverter()
        {
            EqualsValue = false;
            NotEqualsValue = true;
        }
    }
}
