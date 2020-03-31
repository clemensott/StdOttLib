using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.CommandlineParser
{
    public class OptionParseResult : List<OptionParsed>
    {
        public OptionParseResult()
        {
        }

        public bool HasOptionParseds(Option opt)
        {
            return GetOptionParseds(opt).Any();
        }

        public IEnumerable<OptionParsed> GetOptionParseds(Option opt)
        {
            return GetKownOptionParsed().Where(o => o.Opt == opt);
        }

        public bool HasValidOptionParseds(Option opt)
        {
            return GetValidOptionParseds(opt).Any();
        }

        public IEnumerable<OptionParsed> GetValidOptionParseds(Option opt)
        {
            return GetValidKownOptionParsed().Where(o => o.Opt == opt);
        }

        public bool TryGetFirstValidOptionParseds(Option opt, out OptionParsed parsed)
        {
            parsed = GetValidKownOptionParsed().FirstOrDefault(p => p.Opt == opt);

            return parsed != null;
        }

        public bool HasOptionParseds(string shortOrLongOpt)
        {
            return GetOptionParseds(shortOrLongOpt).Any();
        }

        public IEnumerable<OptionParsed> GetOptionParseds(string shortOrLongOpt)
        {
            return this.Where(o => o.UsedOpt == shortOrLongOpt);
        }

        public bool HasOptionParsedsWithShort(string shortOpt)
        {
            return GetOptionParsedsWithShort(shortOpt).Any();
        }

        public IEnumerable<OptionParsed> GetOptionParsedsWithShort(string shortOpt)
        {
            return this.Where(o => o.UsedShortOpt && o.UsedOpt == shortOpt);
        }

        public bool HasOptionParsedsWithLong(string longOpt)
        {
            return GetOptionParsedsWithLong(longOpt).Any();
        }

        public IEnumerable<OptionParsed> GetOptionParsedsWithLong(string longOpt)
        {
            return this.Where(o => o.UsedShortOpt && o.UsedOpt == longOpt);
        }

        public IEnumerable<OptionParsed> GetArgsWithoutOption()
        {
            return this.Where(o => string.IsNullOrWhiteSpace(o.UsedOpt));
        }

        public IEnumerable<OptionParsed> GetArgsWithoutKownOption()
        {
            return this.Where(o => o.Opt == null && !string.IsNullOrWhiteSpace(o.UsedOpt));
        }

        public IEnumerable<OptionParsed> GetKownOptionParsed()
        {
            return this.Where(o => o.Opt != null);
        }

        public IEnumerable<OptionParsed> GetValidKownOptionParsed()
        {
            return GetKownOptionParsed().Where(o => o.IsValid());
        }

        public IEnumerable<string> GetUnknownArgs()
        {
            return this.Where(o => o.Opt == null).Select(o => o.ToArgsString());
        }
    }
}
