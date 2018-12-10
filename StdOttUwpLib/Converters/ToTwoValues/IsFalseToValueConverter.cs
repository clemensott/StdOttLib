namespace StdOttUwp.Converters
{
    public class IsFalseToValueConverter : IsValueToTwoValueConverter
    {
        public IsFalseToValueConverter()
        {
            CompareValue = false;
        }
    }
}
