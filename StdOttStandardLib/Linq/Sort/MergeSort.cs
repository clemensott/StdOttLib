using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq.Sort
{
    public static class MergeSort
    {
        private const int defaultMergeInsertionSortCount = 20;
        
        public static IList<TSource> Sort<TSource>(IList<TSource> items, int begin, int end, Comparison<TSource> comparison = null)
        {
            return Sort(items, begin, end, defaultMergeInsertionSortCount, comparison.CreateComparer());
        }

        public static IList<TSource> Sort<TSource>(IList<TSource> items, int begin, int end, IComparer<TSource> comparer)
        {
            return Sort(items, begin, end, defaultMergeInsertionSortCount, comparer);
        }

        public static IList<TSource> Sort<TSource>(IList<TSource> items, int begin, int end, int insertionSortCount, IComparer<TSource> comparer)
        {
            for (int i = begin; i < end; i += insertionSortCount)
            {
                InsertionSort.Sort(items, i, Math.Min(i + insertionSortCount, end), comparer);
            }

            IList<TSource> src = items;
            IList<TSource> dest = new TSource[items.Count];

            for (int i = insertionSortCount; i < end - begin; i *= 2)
            {
                for (int j = begin; j < end; j += i * 2)
                {
                    Merge(src, dest, j, i, end, comparer);
                }

                StdUtils.Swap(ref src, ref dest);
            }

            return src;
        }

        internal static void Merge<TSource>(IList<TSource> src, IList<TSource> dest,
            int begin, int size, int end, IComparer<TSource> comparer)
        {
            int destIndex = begin;
            int begin1 = begin;
            int begin2 = Math.Min(begin1 + size, end);
            int end1 = begin2;
            int end2 = Math.Min(end1 + size, end);

            while (true)
            {
                if (begin1 < end1)
                {
                    if (begin2 >= end2 || comparer.Compare(src[begin1], src[begin2]) < 0) dest[destIndex++] = src[begin1++];
                    else dest[destIndex++] = src[begin2++];
                }
                else if (begin2 < end2)
                {
                    dest[destIndex++] = src[begin2++];
                }
                else return;
            }
        }



        public static IList<TSource> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort(items, begin, end, keySelector, comparison.CreateComparer());
        }

        public static IList<TSource> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort(items, begin, end, defaultMergeInsertionSortCount, keySelector, comparer);
        }

        public static IList<TSource> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int insertionSortCount, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort(items, begin, end, insertionSortCount, keySelector, comparison.CreateComparer());
        }

        public static IList<TSource> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int insertionSortCount, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TKey> keys = items.Select(keySelector).ToArray();

            return Sort(items, keys, begin, end, insertionSortCount, comparer);
        }

        private static IList<TSource> Sort<TSource, TKey>(IList<TSource> items, IList<TKey> keys,
           int begin, int end, int insertionSortCount, IComparer<TKey> comparer)
        {
            for (int i = begin; i < end; i += insertionSortCount)
            {
                InsertionSort.Sort(items, keys, i, Math.Min(i + insertionSortCount, end), comparer);
            }

            IList<TSource> srcValues = items;
            IList<TKey> srcKeys = keys;
            IList<TSource> destValues = new TSource[items.Count];
            IList<TKey> destKeys = new TKey[items.Count];

            for (int i = insertionSortCount; i < end - begin; i *= 2)
            {
                for (int j = begin; j < end; j += i * 2)
                {
                    Merge(srcValues, srcKeys, destValues, destKeys, j, i, end, comparer);
                }

                StdUtils.Swap(ref srcValues, ref destValues);
                StdUtils.Swap(ref srcKeys, ref destKeys);
            }

            return srcValues;
        }

        internal static void Merge<TSource, TKey>(IList<TSource> srcValues, IList<TKey> srcKeys,
            IList<TSource> destValues, IList<TKey> destKeys, int begin, int size, int end, IComparer<TKey> comparer)
        {
            int destIndex = begin;
            int begin1 = begin;
            int begin2 = Math.Min(begin1 + size, end);
            int end1 = begin2;
            int end2 = Math.Min(end1 + size, end);

            while (true)
            {
                if (begin1 < end1)
                {
                    if (begin2 >= end2 || comparer.Compare(srcKeys[begin1], srcKeys[begin2]) < 0)
                    {
                        destValues[destIndex] = srcValues[begin1];
                        destKeys[destIndex] = destKeys[begin1];

                        destIndex++;
                        begin1++;
                    }
                    else
                    {
                        destValues[destIndex] = srcValues[begin2];
                        destKeys[destIndex] = destKeys[begin2];

                        destIndex++;
                        begin2++;
                    }
                }
                else if (begin2 < end2)
                {
                    destValues[destIndex] = srcValues[begin2];
                    destKeys[destIndex] = destKeys[begin2];

                    destIndex++;
                    begin2++;
                }
                else return;
            }
        }
    }
}
