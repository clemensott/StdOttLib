using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StdOttStandard
{
    public static class EnumerableUtils
    {
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
                if (Utils.ReferenzEqualOrEqual(item, searchItem)) return i;

                i++;
            }

            return -1;
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, params T[] concat)
        {
            return items.Concat(concat);
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
                (int index, bool overflow, bool underflow) = Utils.OffsetIndex(refIndex, list.Count, offset);

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

            (int index, bool overflow, bool underflow) = Utils.OffsetIndex(refIndex, list.Count, offset);

            return (list[index], overflow, underflow);
        }

    }
}
