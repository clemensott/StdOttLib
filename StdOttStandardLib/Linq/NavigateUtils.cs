using System;
using System.Collections.Generic;

namespace StdOttStandard.Linq
{
    public static class NavigateUtils
    {
        public static (T next, bool overflow) Next<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool overflow, _) = OffsetElement(items, refItem, 1);

            return (next, overflow);
        }

        public static (T next, bool found, bool overflow) NextOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool found, bool overflow, _) = OffsetElementOrDefault(items, refItem, 1);

            return (next, found, overflow);
        }

        public static (T previous, bool overflow) Previous<T>(this IEnumerable<T> items, T refItem)
        {
            (T previous, _, bool underflow) = OffsetElement(items, refItem, -1);

            return (previous, underflow);
        }

        public static (T next, bool found, bool underflow) PreviousOrDefault<T>(this IEnumerable<T> items, T refItem)
        {
            (T next, bool found, _, bool underflow) = OffsetElementOrDefault(items, refItem, -1);

            return (next, found, underflow);
        }

        public static (T item, bool found, bool overflow, bool underflow) OffsetElementOrDefault<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items.ToIList();
            int refIndex = list.IndexOf(refItem);

            try
            {
                (int index, bool overflow, bool underflow) = StdUtils.OffsetIndex(refIndex, list.Count, offset);

                return (list[index], true, overflow, underflow);
            }
            catch
            {
                return (default(T), false, false, false);
            }
        }

        public static (T item, bool overflow, bool underflow) OffsetElement<T>(this IEnumerable<T> items, T refItem, int offset)
        {
            IList<T> list = items.ToIList();
            int refIndex = list.IndexOf(refItem);

            if (refIndex == -1) throw new ArgumentException("The refItem has to be in items");

            (int index, bool overflow, bool underflow) = StdUtils.OffsetIndex(refIndex, list.Count, offset);

            return (list[index], overflow, underflow);
        }

    }
}
