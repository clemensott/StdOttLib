using System.Collections.Generic;
using System.Text;

namespace StdOttWpfLib
{
    public class Serializer
    {
        public const char DefaultAddChar = '&';

        public char AddChar { get; private set; }

        public Serializer() : this(DefaultAddChar)
        {
        }

        public Serializer(char addChar)
        {
            AddChar = addChar;
        }

        public IEnumerable<string> Split(StringBuilder dataString, char seperator)
        {
            return Split(dataString, seperator, AddChar);
        }

        public static IEnumerable<string> Split(StringBuilder dataString, char seperator, char addChar = DefaultAddChar)
        {
            while (dataString.Length > 0)
            {
                yield return GetUntil(ref dataString, seperator, addChar);
            }
        }

        public string GetUntil(ref StringBuilder text, char seperator)
        {
            return GetUntil(ref text, seperator, AddChar);
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
    }
}
