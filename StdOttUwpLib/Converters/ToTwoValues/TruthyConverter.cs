using StdOttStandard.Equal;

namespace StdOttUwp.Converters
{
    public class TruthyConverter : IsValueToTwoValueConverter
    {
        public TruthyConverter()
        {
            DecideType = TwoValueDecideType.Truthy;
            EqualsValue = true;
            NotEqualsValue = false;
        }
    }
}
