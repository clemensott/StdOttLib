using StdOttStandard.Equal;

namespace StdOttFramework.Converters
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
