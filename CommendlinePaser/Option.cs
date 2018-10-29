using System.Collections.Generic;
using System.Linq;

namespace StdOttWpfLib.CommendlinePaser
{
    public class Option
    {
        public bool Required { get; set; }

        public int MinValuesCount { get; set; }

        public int MaxValuesCount { get; set; }

        public string Distription { get; set; }

        public List<string> ShortOpts { get; private set; }

        public List<string> LongOpts { get; private set; }

        public Option()
        {
            ShortOpts = new List<string>();
            LongOpts = new List<string>();
        }

        public Option(string shortOpt, string longOpt, string discription = "",
            bool required = false, int maxValuesCount = -1, int minValuesCount = 0) : this()
        {
            ShortOpts.Add(shortOpt);
            LongOpts.Add(longOpt);
            Distription = discription;
            Required = required;
            MaxValuesCount = maxValuesCount;
            MinValuesCount = minValuesCount;
        }

        public static Option GetLongOnly(string longOpt, string discription = "",
            bool required = false, int maxValuesCount = -1, int minValuesCount = 0)
        {
            Option option = new Option();

            option.LongOpts.Add(longOpt);
            option.Distription = discription;
            option.Required = required;
            option.MaxValuesCount = maxValuesCount;
            option.MinValuesCount = minValuesCount;

            return option;
        }

        public static Option GetHelpOption()
        {
            Option option = new Option("h", "help", "Prints out all options.", false, 0, 0);
            option.ShortOpts.Add("?");

            return option;
        }
    }
}
