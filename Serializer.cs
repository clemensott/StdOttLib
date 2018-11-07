using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StdOttWpfLib
{
    public static class Serializer
    {
        public const char DefaultAddChar = '&';

        public static IEnumerable<string> Deserialize(string dataString, char seperator, char addChar = DefaultAddChar)
        {
            return Deserialize(new StringBuilder(dataString), seperator, addChar);
        }

        public static IEnumerable<string> Deserialize(StringBuilder dataString, char seperator, char addChar = DefaultAddChar)
        {
            while (dataString.Length > 0)
            {
                yield return GetUntil(ref dataString, seperator, addChar);
            }
        }

        public static string GetUntil(ref StringBuilder text, char seperator, char addChar = DefaultAddChar)
        {
            int length;
            StringBuilder part = new StringBuilder(text.Length);

            for (length = 0; length < text.Length; length++)
            {
                char c = text[length];

                if (text[length] == seperator)
                {
                    length++;

                    if (length >= text.Length || text[length] != addChar) break;
                }

                part.Append(c);
            }

            text.Remove(0, length);

            return part.ToString();
        }

        public static string Serialize(IEnumerable items, char seperator, char addChar = DefaultAddChar)
        {
            return Serialize(items.Cast<object>().Select(o => o.ToString()), seperator, addChar);
        }

        public static string Serialize(IEnumerable<string> items, char seperator, char addChar = DefaultAddChar)
        {
            string spt = seperator.ToString();

            return string.Join(spt, items.Select(i => i.Replace(spt, spt + addChar)));
        }
    }
}
