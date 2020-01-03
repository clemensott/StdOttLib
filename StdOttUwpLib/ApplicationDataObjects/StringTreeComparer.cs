using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttUwp.ApplicationDataObjects
{
    public class StringTreeComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (string.IsNullOrWhiteSpace(x))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(x));
            }
            if (string.IsNullOrWhiteSpace(y))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(y));
            }

            if (x[0] != '-') x = '+' + x;
            if (y[0] != '-') y = '+' + y;

            x = x.PadRight(y.Length, '0');
            y = y.PadRight(x.Length, '0');

            return x.Select((t, i) => t.CompareTo(y[i])).FirstOrDefault(compare => compare != 0);
        }
    }
}
