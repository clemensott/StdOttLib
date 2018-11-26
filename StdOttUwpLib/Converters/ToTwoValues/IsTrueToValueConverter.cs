namespace StdOttUwp.Converters
{
    class IsTrueToValueConverter : IsValueToTwoValueConverter
    {
        public IsTrueToValueConverter()
        {
            CompareValue = true;
        }
    }
}
