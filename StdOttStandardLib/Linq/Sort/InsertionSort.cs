using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq.Sort
{
    public static class InsertionSort
    {
        public static void Sort<TSource>(IList<TSource> items, int begin, int end, Comparison<TSource> comparison = null)
        {
            Sort(items, begin, end, comparison.CreateComparer());
        }

        public static bool Sort<TSource>(IList<TSource> items, int begin, int end, IComparer<TSource> comparer)
        {
            for (int i = begin + 1; i < end; i++)
            {
                for (int j = i; j > begin; j--)
                {
                    if (comparer.Compare(items[j], items[j - 1]) < 0)
                    {
                        items.Swap(j, j - 1);
                    }
                    else break;
                }
            }

            return true;
        }

        public static void Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            Sort(items, begin, end, keySelector, comparison.CreateComparer());
        }

        public static void Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TKey> keys = items.Select(keySelector).ToArray();

            Sort(items, keys, begin, end, comparer);
        }

        internal static void Sort<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int begin, int end, IComparer<TKey> comparer)
        {

            for (int i = begin + 1; i < end; i++)
            {
                for (int j = i; j > begin; j--)
                {
                    if (comparer.Compare(keys[j], keys[j - 1]) < 0)
                    {
                        items.Swap(j, j - 1);
                        keys.Swap(j, j - 1);
                    }
                    else break;
                }
            }
        }
    }
}
