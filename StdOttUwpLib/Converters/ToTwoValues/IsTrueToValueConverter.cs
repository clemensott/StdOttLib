namespace StdOttUwp.Converters
{
    public class IsTrueToValueConverter : IsValueToTwoValueConverter
    {
        public IsTrueToValueConverter()
        {
            CompareValue = true;
        }
    }
}
