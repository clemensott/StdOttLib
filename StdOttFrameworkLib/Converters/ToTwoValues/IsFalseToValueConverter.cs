namespace StdOttFramework.Converters
{
    class IsFalseToValueConverter : IsValueToTwoValueConverter
    {
        public IsFalseToValueConverter()
        {
            CompareValue = false;
        }
    }
}
