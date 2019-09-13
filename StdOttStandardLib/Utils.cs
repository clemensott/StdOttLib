using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StdOttStandard
{
    public static class Utils
    {
        public static string ToString(TimeSpan span, bool includeMillis = false)
        {
            string text = string.Empty;
            int hours = (int)Math.Floor(span.TotalHours);

            if (hours >= 1) text += string.Format("{0,2}:", hours);

            text += string.Format("{0,2}:{1,2}", span.Minutes, span.Seconds);

            if (includeMillis) text += string.Format(":{0,2}", span.Milliseconds);

            return text.Replace(' ', '0');
        }

        public static IEnumerable<string> Split(this string text, params string[] seperators)
        {
            string value = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                bool matched = false;

                foreach (string seperator in seperators)
                {
                    if (!ContinuesWith(text, i, seperator)) continue;

                    matched = true;
                    yield return value;

                    value = string.Empty;
                    i += seperator.Length - 1;

                    break;
                }

                if (!matched) value += text[i];
            }

            yield return value;
        }

        public static bool ContinuesWith(string text, int startIndex, string with)
        {
            if (text.Length < startIndex + with.Length) return false;

            for (int i = 0; i < with.Length; i++)
            {
                if (text[startIndex + i] != with[i]) return false;
            }

            return true;
        }

        public static (int index, bool overflow, bool underflow) OffsetIndex(int index, int count, int offset)
        {
            if (count <= 0) throw new ArgumentException("The count has to be greater than zero");

            bool overflow = false, underflow = false;
            index += offset;

            if (index < 0)
            {
                underflow = true;
                index = (index % count) + count;
            }
            else if (index >= count)
            {
                overflow = true;
                index = index % count;
            }

            return (index, overflow, underflow);
        }

        public static int CycleIndex(int index, int count, int begin = 0)
        {
            if (count == 0) return begin - 1;

            return ((index - begin) % count + count) % count + begin;
        }

        public static bool ReferenceEqualsOrEquals(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (!ReferenceEquals(obj1, null)) return obj1.Equals(obj2);
            if (!ReferenceEquals(obj2, null)) return obj2.Equals(obj1);

            throw new NotImplementedException();
        }

        public static async Task WaitAsync(object lockObj)
        {
            await Task.Run(() =>
            {
                lock (lockObj) Monitor.Wait(lockObj);
            });
        }

        public static async Task WaitAsync(object lockObj, Func<bool> whileFunc)
        {
            await Task.Run(() =>
            {
                lock (lockObj)
                {
                    while (whileFunc()) Monitor.Wait(lockObj);
                }
            });
        }

        public static string XmlSerialize(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, obj);

            return writer.ToString();
        }

        public static void XmlSerialize(string destFilePath, object obj)
        {
            File.WriteAllText(destFilePath, XmlSerialize(obj));
        }

        public static T XmlDeserializeText<T>(string xmlText)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(new StringReader(xmlText));
        }

        public static T XmlDeserializeFile<T>(string srcFilePath)
        {
            string xmlText = File.ReadAllText(srcFilePath);

            return XmlDeserializeText<T>(xmlText);
        }

        public static T DeserializeFile<T>( string srcFilePath, T defaultValue)
        {
            try
            {
                string xmlText = File.ReadAllText(srcFilePath);

                return XmlDeserializeText<T>(xmlText);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
