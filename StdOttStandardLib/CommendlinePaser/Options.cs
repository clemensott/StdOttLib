using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.CommendlinePaser
{
    public class Options : List<Option>
    {
        public const string ShortOptStart = "-", LongOptStart = "--";

        public Options() : base()
        {
        }

        public Options(params Option[] options) : base(options)
        {
        }

        public Options(IEnumerable<Option> collection) : base(collection)
        {
        }

        public OptionParseResult Parse(IEnumerable<string> args)
        {
            OptionParseResult result = new OptionParseResult();
            OptionParsed last = null;

            foreach (string arg in args)
            {
                if (arg.StartsWith(LongOptStart))
                {
                    string optString = arg.Substring(LongOptStart.Length);
                    Option opt = GetOptionWithLong(optString);

                    last = new OptionParsed(opt, optString, false, true);
                    result.Add(last);
                }
                else if (arg.StartsWith(ShortOptStart))
                {
                    string optString = arg.Substring(ShortOptStart.Length);
                    Option opt = GetOptionWithShort(optString);

                    last = new OptionParsed(opt, optString, true, false);
                    result.Add(last);
                }
                else 
                {
                    if (last == null || last.Opt == null || 
                        (last.Opt.MaxValuesCount > 0 && last.Values.Count >= last.Opt.MaxValuesCount))
                    {
                        last = new OptionParsed(null, string.Empty, false, false);
                        result.Add(last);
                    }

                    last.Values.Add(arg);
                }
            }

            return result;
        }

        public Option GetOptionWithShort(string shortOpt)
        {
            return this.FirstOrDefault(o => o.ShortOpts.Contains(shortOpt));
        }

        public Option GetOptionWithLong(string longOpt)
        {
            return this.FirstOrDefault(o => o.LongOpts.Contains(longOpt));
        }
    }
}
