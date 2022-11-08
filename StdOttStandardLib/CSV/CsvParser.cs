using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.CSV
{
    public static class CsvParser
    {
        public static CsvColumn[] GetColumns(IEnumerable<string> lines, char delimiter = ',')
        {
            using (IEnumerator<string> enumerator = lines.GetEnumerator())
            {
                if (!enumerator.MoveNext()) return new CsvColumn[0];
                CsvColumn[] columns = Split(enumerator.Current, delimiter).Select(h => new CsvColumn(h)).ToArray();

                while (enumerator.MoveNext())
                {
                    string[] values = Split(enumerator.Current, delimiter).ToArray();

                    for (int i = 0; i < columns.Length; i++)
                    {
                        columns[i].Items.Add(values.ElementAtOrDefault(i));
                    }
                }

                return columns;
            }
        }

        public static Dictionary<string, List<string>> GetDict(IEnumerable<string> lines, char delimiter = ',')
        {
            using (IEnumerator<string> enumerator = lines.GetEnumerator())
            {
                if (!enumerator.MoveNext()) return new Dictionary<string, List<string>>();
                string[] headers = Split(enumerator.Current, delimiter).ToArray();
                Dictionary<string, List<string>> dict = headers.ToDictionary(h => h, h => new List<string>());

                while (enumerator.MoveNext())
                {
                    string[] values = Split(enumerator.Current, delimiter).ToArray();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        dict[headers[i]].Add(values.ElementAtOrDefault(i));
                    }
                }

                return dict;
            }
        }

        public static IEnumerable<Dictionary<string, string>> GetRows(string[] lines, char delimiter = ',')
        {
            string[] headers = Split(lines[0], delimiter).ToArray();

            for (int i = 1; i < lines.Length; i++)
            {
                int headerIndex = 0;
                Dictionary<string, string> dict = new Dictionary<string, string>();

                foreach (string value in Split(lines[i], delimiter))
                {
                    dict.Add(headers[headerIndex++], value);
                }

                yield return dict;
            }
        }

        private static IEnumerable<string> Split(string line, char delimiter)
        {
            int index = 0;

            while (true)
            {
                bool withQuotation;
                string value = string.Empty;

                if (index < line.Length && line[index] == '\"')
                {
                    withQuotation = true;
                    index++;
                }
                else withQuotation = false;

                while (index < line.Length)
                {
                    if (withQuotation && line[index] == '"' &&
                        (index + 2 > line.Length || line[index + 1] == delimiter))
                    {
                        index++;
                        break;
                    }
                    else if (!withQuotation && line[index] == delimiter) break;

                    value += line[index++];
                }

                yield return value;
                if (value == "8101898") { }

                if (index >= line.Length) break;

                if (line[index] != delimiter) throw new Exception();
                else index++;
            }
        }
    }
}
