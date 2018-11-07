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
            int index;
            StringBuilder part = new StringBuilder(text.Length);

            for (index = 0; index < text.Length; index++)
            {
                char c = text[index];

                if (c == seperator && IsSeperator(text.ToString(), index++, addChar)) break;

                part.Append(c);
            }

            text.Remove(0, index);

            return part.ToString();
        }

        private static bool IsSeperator(string text, int index, char addChar)
        {
            return text.Skip(index + 1).TakeWhile(c => c == addChar).Count() % 2 == 0;
        }

        public static string Serialize(IEnumerable items, char seperator, char addChar = DefaultAddChar)
        {
            return Serialize(items.Cast<object>().Select(o => o.ToString()), seperator, addChar);
        }

        public static string Serialize(IEnumerable<string> items, char seperator, char addChar = DefaultAddChar)
        {
            string spt = seperator.ToString();

            return string.Join(spt, items.Select(i => AddAddChar(i, seperator, addChar)));
        }

        private static string AddAddChar(string item, char seperator, char addChar)
        {
            int addCharsAtBeginning = item.TakeWhile(c => c == addChar).Count();

            item = string.Concat(Enumerable.Repeat(addChar, addCharsAtBeginning)) + item;

            for (int i = item.Length - 1; i >= 0; i--)
            {
                if (item[i] == seperator) item = item.Insert(i, addChar.ToString());
            }

            return item;
        }
    }
}
