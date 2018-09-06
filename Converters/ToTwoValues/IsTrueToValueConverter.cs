namespace StdOttWpfLib.Converters
{
    class IsTrueToValueConverter : IsValueToTwoValueConverter
    {
        public IsTrueToValueConverter()
        {
            CompareValue = true;
        }
    }
}
