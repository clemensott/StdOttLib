using StdOttStandard.Equal;

namespace StdOttUwp.Converters
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
