using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

        public static (int index, bool overflow, bool underflow) OffsetIndex(int index, int count, int offset)
        {
            if (count <= 0) throw new ArgumentException("The count has to be greater than zero");

            bool overflow = false, underflow = false;
            index += offset % count;

            if (index < 0)
            {
                underflow = true;
                index += count;
            }
            else if (index >= count)
            {
                overflow = true;
                index -= count;
            }

            return (index, overflow, underflow);
        }

        public static bool ReferenzEqualOrEqual(object obj1, object obj2)
        {
            if (ReferenceEquals(obj1, obj2)) return true;
            if (!ReferenceEquals(obj1, null)) return obj1.Equals(obj2);
            if (!ReferenceEquals(obj2, null)) return obj2.Equals(obj2);

            throw new NotImplementedException();
        }

        public async static Task WaitAsync(object lockObj)
        {
            await Task.Run(() =>
            {
                lock (lockObj) Monitor.Wait(lockObj);
            });
        }

        public async static Task WaitAsync(object lockObj, Func<bool> whileFunc)
        {
            await Task.Run(() =>
            {
                lock (lockObj)
                {
                    while (whileFunc()) Monitor.Wait(lockObj);
                }
            });
        }
    }
}
