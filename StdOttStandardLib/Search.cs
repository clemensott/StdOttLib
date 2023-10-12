using System;
using System.Collections.Generic;

namespace StdOttStandard
{
    public class Search
    {
        public static bool BinarySearch<T>(IList<T> items, T item, IComparer<T> comparer, out int index)
        {
            return BinarySearch(items, 0, items.Count, x => comparer.Compare(x, item), out index);
        }

        public static bool BinarySearch<T>(IList<T> items, Func<T, int> comparer, out int index)
        {
            return BinarySearch(items, 0, items.Count, comparer, out index);
        }

        /// <summary>
        /// Search for matching item in list with binary search.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">list of items to search in</param>
        /// <param name="begin">Inclusvie value of min index</param>
        /// <param name="end">Exclusive value of max index</param>
        /// <param name="comparer">Function that compares the current element with searched item</param>
        /// <param name="index">The index of the found item</param>
        /// <returns>Returns true if found and false if not</returns>
        public static bool BinarySearch<T>(IList<T> items, int begin, int end, Func<T, int> comparer, out int index)
        {
            while (true)
            {
                if (begin == end)
                {
                    index = begin;
                    return false;
                }

                int middle = (begin + end) / 2;
                int compareMiddle = comparer(items[middle]);

                if (compareMiddle == 0)
                {
                    index = middle;
                    return true;
                }

                if (begin == middle)
                {
                    index = begin + (compareMiddle < 0 ? 1 : 0);
                    return false;
                }

                int nextBegin, nextEnd;
                if (compareMiddle > 0)
                {
                    nextBegin = begin;
                    nextEnd = middle;
                }
                else
                {
                    nextBegin = middle + 1;
                    nextEnd = end;
                }

                begin = nextBegin;
                end = nextEnd;
            }
        }
    }
}
