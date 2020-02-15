using StdOttStandard.Equal;

namespace StdOttFramework.Converters
{
    class FalsyConverter : IsValueToTwoValueConverter
    {
        public FalsyConverter()
        {
            DecideType = TwoValueDecideType.Falsy;
            EqualsValue = false;
            NotEqualsValue = true;
        }
    }
}
