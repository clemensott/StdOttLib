namespace StdOttUwp.Converters
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
