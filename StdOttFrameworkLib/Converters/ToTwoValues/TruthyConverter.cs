using StdOttStandard.Equal;

namespace StdOttFramework.Converters
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
