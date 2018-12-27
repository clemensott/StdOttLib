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

        public static string GetTypeMessageAndStack(this Exception exception)
        {
            IEnumerable<string> exceptions = ToEnumerable(exception).Select(e => e.GetType().Name + ":\r\n" + e.Message);

            return string.Join("\r\n\r\n", exceptions) + "\r\n\r\n" + exception.StackTrace;
        }

        public static IEnumerable<Exception> ToEnumerable(Exception e)
        {
            while (e != null)
            {
                yield return e;

                e = e.InnerException;
            }
        }

        public static bool BothNullOrSequenceEqual<T>(this IEnumerable<T> enum1, IEnumerable<T> enum2)
        {
            if (enum1 == enum2) return true;
            if (enum1 == null || enum2 == null) return false;

            return enum1.SequenceEqual(enum2);
        }

        public static IEnumerable<T> ToNotNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        public static int IndexOf<T>(this IEnumerable<T> enumerable, T searchItem)
        {
            if (enumerable is IList<T> list) return list.IndexOf(searchItem);

            int i = 0;

            foreach (T item in enumerable)
            {
                if (ReferenzEqualOrEqual(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static (T next, bool overflow) Next<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool overflow, _) = OffsetElement(items, refItem, 1);

            return (next, overflow);
        }

        public static (T next, bool overflow) NextOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool overflow, _) = OffsetElementOrDefault(items, refItem, 1);

            return (next, overflow);
        }

        public static (T next, bool overflow) Previous<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, _, bool underflow) = OffsetElement(items, refItem, -1);

            return (next, underflow);
        }

        public static (T next, bool overflow) PreviousOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, _, bool underflow) = OffsetElementOrDefault(items, refItem, -1);

            return (next, underflow);
        }

        public static (T item, bool overflow, bool underflow) OffsetElementOrDefault<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items as IList<T> ?? items.ToArray();

            int refIndex = list.IndexOf(refItem);

            try
            {
                (int index, bool overflow, bool underflow) = OffsetIndex(refIndex, list.Count, offset);

                return (list[index], overflow, underflow);
            }
            catch
            {
                return (default(T), false, false);
            }
        }

        public static (T item, bool overflow, bool underflow) OffsetElement<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items as IList<T> ?? items.ToArray();

            int refIndex = list.IndexOf(refItem);

            if (refIndex == -1) throw new ArgumentException("The refItem has to be in items");

            (int index, bool overflow, bool underflow) = OffsetIndex(refIndex, list.Count, offset);

            return (list[index], overflow, underflow);
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
