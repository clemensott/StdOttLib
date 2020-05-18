﻿using StdOttStandard.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StdOttStandard
{
    public static class StdUtils
    {
        private static Random rnd;

        public static Random Random => rnd ?? (rnd = new Random());

        public static string ToString(TimeSpan span, bool includeMillis = false)
        {
            string text = string.Empty;
            int hours = (int)Math.Floor(span.TotalHours);

            if (hours >= 1) text += $"{hours,2}:";

            text += $"{span.Minutes,2}:{span.Seconds,2}";

            if (includeMillis) text += $":{span.Milliseconds,2}";

            return text.Replace(' ', '0');
        }

        public static string GetFormattedFileSize(long totalSize, int digits = 4, int maxValue = 1024)
        {
            string[] endings = new string[] {"B", "kB", "MB", "GB", "TB", "PB", "EB"};
            double unitSize = Convert.ToDouble(totalSize);
            string ending = endings.Last();

            foreach (string end in endings)
            {
                if (unitSize < maxValue)
                {
                    ending = end;
                    break;
                }

                unitSize /= 1024.0;
            }

            int beforeDigits = (int)Math.Ceiling(Math.Log10(unitSize));
            double roundedSize = Math.Round(unitSize, beforeDigits < digits ? digits - beforeDigits : 0);

            return $"{roundedSize} {ending}";
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

        /// <summary>
        /// Normalizes the given YouTube URL to the format http://youtube.com/watch?v={youtube-id}
        /// and returns whether the normalization was successful or not.
        /// Copied from https://github.com/flagbug/YoutubeExtractor
        /// </summary>
        /// <param name="url">The YouTube URL to normalize.</param>
        /// <param name="normalizedUrl">The normalized YouTube URL.</param>
        /// <returns>
        /// <c>true</c>, if the normalization was successful; <c>false</c>, if the URL is invalid.
        /// </returns>
        public static bool TryNormalizeYoutubeUrl(string url, out string normalizedUrl)
        {
            url = url.Trim();

            url = url.Replace("youtu.be/", "youtube.com/watch?v=");
            url = url.Replace("www.youtube", "youtube");
            url = url.Replace("youtube.com/embed/", "youtube.com/watch?v=");

            if (url.Contains("/v/"))
            {
                url = "http://youtube.com" + new Uri(url).AbsolutePath.Replace("/v/", "/watch?v=");
            }

            url = url.Replace("/watch#", "/watch?");

            IDictionary<string, string> query = ParseQueryString(url);

            string v;

            if (!query.TryGetValue("v", out v))
            {
                normalizedUrl = null;
                return false;
            }

            normalizedUrl = "http://youtube.com/watch?v=" + v;

            return true;
        }

        public static IDictionary<string, string> ParseQueryString(string url)
        {
            int index;
            if (!url.TryIndexOf('?', out index) ||
                index + 1 == url.Length) return new Dictionary<string, string>(0);

            string query = url.Substring(index + 1);
            string[] rawParts = query.Split('&');

            return rawParts.Select(p =>
            {
                string[] pairParts = p.Split('=');
                if (pairParts.Length != 2) throw new ArgumentException("Query part is not valid", nameof(url));

                string key = Uri.UnescapeDataString(pairParts[0]);
                string value = Uri.UnescapeDataString(pairParts[1]);

                return (key: key, value: value);
            }).ToDictionary(p => p.key, p => p.value);
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }
    }
}
