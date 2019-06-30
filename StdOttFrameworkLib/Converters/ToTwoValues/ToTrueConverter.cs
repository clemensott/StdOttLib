namespace StdOttFramework.Converters
{
    public class ToTrueConverter : IsValueToTwoValueConverter
    {
        public ToTrueConverter()
        {
            EqualsValue = true;
            NotEqualsValue = false;
        }
    }
}
