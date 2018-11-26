namespace StdOttUwp.Converters
{
    class IsFalseToValueConverter : IsValueToTwoValueConverter
    {
        public IsFalseToValueConverter()
        {
            CompareValue = false;
        }
    }
}
