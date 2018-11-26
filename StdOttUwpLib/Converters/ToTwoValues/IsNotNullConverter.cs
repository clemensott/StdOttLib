namespace StdOttUwp.Converters
{
    public class IsNotNullConverter : IsNullToValueConverter
    {
        public IsNotNullConverter()
        {
            EqualsValue = false;
            NotEqualsValue = true;
        }
    }
}
