namespace StdOttFramework.Converters
{
    class IsTrueToValueConverter : IsValueToTwoValueConverter
    {
        public IsTrueToValueConverter()
        {
            CompareValue = true;
        }
    }
}
