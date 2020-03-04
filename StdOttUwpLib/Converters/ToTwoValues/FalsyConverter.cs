using StdOttStandard.Equal;

namespace StdOttUwp.Converters
{
    public class FalsyConverter : IsValueToTwoValueConverter
    {
        public FalsyConverter()
        {
            DecideType = TwoValueDecideType.Truthy;
            EqualsValue = false;
            NotEqualsValue = true;
        }
    }
}
