using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StdOttStandard
{
    public static class StdUtils
    {
        public static string ToString(TimeSpan span, bool includeMillis = false)
        {
            string text = string.Empty;
            int hours = (int)Math.Floor(span.TotalHours);

            if (hours >= 1) text += $"{hours,2}:";

            text += $"{span.Minutes,2}:{span.Seconds,2}";

            if (includeMillis) text += $":{span.Milliseconds,2}";

            return text.Replace(' ', '0');
        }

        public static IEnumerable<string> Split(this string text, params string[] separators)
        {
            string value = string.Empty;

            for (int i = 0; i < text.Length; i++)
            {
                bool matched = false;

                foreach (string separator in separators)
                {
                    if (!ContinuesWith(text, i, separator)) continue;

                    matched = true;
                    yield return value;

                    value = string.Empty;
                    i += separator.Length - 1;

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

        public static T XmlDeserializeFileOrDefault<T>(string srcFilePath, T defaultValue = default(T))
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

        public static void Crop(uint srcWidth, uint srcHeight, uint destWidth, uint destHeight,
            out uint destX, out uint destY, out uint wouldWidth, out uint wouldHeight)
        {
            if (srcHeight * destHeight < destWidth * srcHeight)
            {
                wouldWidth = destWidth;
                wouldHeight = srcHeight * destWidth / srcWidth;
                destX = 0;
                destY = (wouldHeight - destHeight) / 2;
            }
            else
            {
                wouldWidth = srcWidth * destHeight / srcHeight;
                wouldHeight = destHeight;
                destX = (wouldWidth - destWidth) / 2;
                destY = 0;
            }
        }
    }
}
