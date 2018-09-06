namespace StdOttWpfLib.Converters
{
    class IsFalseToValueConverter : IsValueToTwoValueConverter
    {
        public IsFalseToValueConverter()
        {
            CompareValue = false;
        }
    }
}
