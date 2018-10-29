using System.Collections.Generic;

namespace StdOttWpfLib.CommendlinePaser
{
    public class OptionParsed
    {
        public bool UsedShortOpt { get; private set; }

        public bool UsedLongOpt { get; private set; }

        public string UsedOpt { get; private set; }

        public Option Opt { get; private set; }

        public List<string> Values { get; private set; }

        public OptionParsed(Option opt, string optString, bool isShortOpt, bool isLongOpt)
        {
            UsedShortOpt = isShortOpt;
            UsedLongOpt = isLongOpt;
            UsedOpt = optString;
            Opt = opt;
            Values = new List<string>();
        }

        public bool IsValid()
        {
            if (Opt == null) return false;

            return Values.Count >= Opt.MinValuesCount;
        }

        public string ToArgsString()
        {
            string args = string.Empty;

            if (UsedShortOpt) args += Options.ShortOptStart + UsedOpt + " ";
            else if (UsedLongOpt) args += Options.LongOptStart + UsedOpt + " ";

            args += string.Join(" ", Values);

            return args;
        }
    }
}
