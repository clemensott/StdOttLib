using System;
using System.Collections.Generic;
using System.Text;

namespace StdOttStandard.CSV
{
    public class CsvColumn
    {
        public string Header { get; set; }

        public List<string> Items { get; }

        public CsvColumn() : this(string.Empty)
        {
            Items = new List<string>();
        }

        public CsvColumn(string header) : this(header, new string[0])
        {
        }

        public CsvColumn(string header, IEnumerable<string> items)
        {
            Header = header;
            Items = new List<string>(items);
        }
    }
}
