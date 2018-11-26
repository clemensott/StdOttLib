namespace StdOttUwp.Converters
{
    public class IsNullConverter : IsNullToValueConverter
    {
        public IsNullConverter()
        {
            EqualsValue = true;
            NotEqualsValue = false;
        }
    }
}
