namespace StdOttFramework.Converters
{
    public class IsTrueToValueConverter : IsValueToTwoValueConverter
    {
        public IsTrueToValueConverter()
        {
            CompareValue = true;
        }
    }
}
