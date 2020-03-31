using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StdOttStandard
{
    public static class Serializer
    {
        public const char DefaultAddChar = '&';

        public static IEnumerable<string> Deserialize(string dataString, char separator, char addChar = DefaultAddChar)
        {
            return Deserialize(new StringBuilder(dataString), separator, addChar);
        }

        public static IEnumerable<string> Deserialize(StringBuilder dataString, char separator, char addChar = DefaultAddChar)
        {
            while (dataString.Length > 0)
            {
                yield return GetUntil(ref dataString, separator, addChar);
            }
        }

        public static string GetUntil(ref StringBuilder text, char separator, char addChar = DefaultAddChar)
        {
            int index = 0;
            StringBuilder part = new StringBuilder(text.Length);

            while (index < text.Length && text[index] == addChar) index++;
            index /= 2;

            for (; index < text.Length; index++)
            {
                char c = text[index];

                if (c == separator && IsSeparator(text.ToString(), index++, addChar)) break;

                part.Append(c);
            }

            text.Remove(0, index);

            return part.ToString();
        }

        private static bool IsSeparator(string text, int index, char addChar)
        {
            return text.Skip(index + 1).TakeWhile(c => c == addChar).Count() % 2 == 0;
        }

        public static string Serialize(IEnumerable items, char separator, char addChar = DefaultAddChar)
        {
            return Serialize(items.Cast<object>().Select(o => o.ToString()), separator, addChar);
        }

        public static string Serialize(IEnumerable<string> items, char separator, char addChar = DefaultAddChar)
        {
            string spt = separator.ToString();

            return string.Join(spt, items.Select(i => AddAddChar(i, separator, addChar)));
        }

        private static string AddAddChar(string item, char separator, char addChar)
        {
            int addCharsAtBeginning = item.TakeWhile(c => c == addChar).Count();

            item = string.Concat(Enumerable.Repeat(addChar, addCharsAtBeginning)) + item;

            for (int i = item.Length - 1; i >= 0; i--)
            {
                if (item[i] == separator) item = item.Insert(i, addChar.ToString());
            }

            return item;
        }
    }
}
