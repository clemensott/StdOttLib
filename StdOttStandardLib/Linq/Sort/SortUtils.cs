using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq.Sort
{
    public static class SortUtils
    {
        public static Comparer<T> CreateComparer<T>(this Comparison<T> comparison)
        {
            return comparison != null ? Comparer<T>.Create(comparison) : Comparer<T>.Default;
        }

        public static IList<TSource> InsertionSort<TSource>(this IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            IList<TSource> array = items.ToArray();
            Sort.InsertionSort.Sort(array, 0, array.Count, comparison);

            return array;
        }

        public static IList<TSource> InsertionSort<TSource>(this IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            IList<TSource> array = items.ToArray();
            Sort.InsertionSort.Sort(array, 0, array.Count, comparer);

            return array;
        }

        public static void InsertionSort<TSource>(this IList<TSource> items, Comparison<TSource> comparison = null)
        {
            Sort.InsertionSort.Sort(items, 0, items.Count, comparison);
        }

        public static void InsertionSort<TSource>(this IList<TSource> items, IComparer<TSource> comparer)
        {
            Sort.InsertionSort.Sort(items, 0, items.Count, comparer);
        }

        public static IList<TSource> InsertionSort<TSource, TKey>(this IEnumerable<TSource> items,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            IList<TSource> array = items.ToArray();
            Sort.InsertionSort.Sort(array, 0, array.Count, keySelector, comparison);

            return array;
        }

        public static IList<TSource> InsertionSort<TSource, TKey>(this IEnumerable<TSource> items,
            Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TSource> array = items.ToArray();
            Sort.InsertionSort.Sort(array, 0, array.Count, keySelector, comparer);

            return array;
        }

        public static void InsertionSort<TSource, TKey>(IList<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            Sort.InsertionSort.Sort(items, 0, items.Count, keySelector, comparison);
        }

        public static void InsertionSort<TSource, TKey>(IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            Sort.InsertionSort.Sort(items, 0, items.Count, keySelector, comparer);
        }

        public static IList<TSource> MergeSort<TSource>(this IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return MergeSort(items.ToArray(), comparison);
        }

        public static IList<TSource> MergeSort<TSource>(this IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return MergeSort(items.ToArray(), comparer);
        }

        public static IList<TSource> MergeSort<TSource>(this IList<TSource> items, Comparison<TSource> comparison = null)
        {
            return Sort.MergeSort.Sort(items, 0, items.Count, comparison);
        }

        public static IList<TSource> MergeSort<TSource>(this IList<TSource> items, IComparer<TSource> comparer)
        {
            return Sort.MergeSort.Sort(items, 0, items.Count, comparer);
        }

        public static IList<TSource> MergeSort<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return MergeSort(items.ToArray(), keySelector, comparison);
        }

        public static IList<TSource> MergeSort<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return MergeSort(items.ToArray(), keySelector, comparer);
        }

        public static IList<TSource> MergeSort<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort.MergeSort.Sort(items, 0, items.Count, keySelector, comparison);
        }

        public static IList<TSource> MergeSort<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort.MergeSort.Sort(items, 0, items.Count, keySelector, comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource>(this IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return Sort.HeapSort.Sort(items.ToArray(), comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSort<TSource>(this IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return Sort.HeapSort.Sort(items.ToArray(), comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource>(this IList<TSource> items, Comparison<TSource> comparison = null)
        {
            return Sort.HeapSort.Sort(items, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSort<TSource>(this IList<TSource> items, IComparer<TSource> comparer)
        {
            return Sort.HeapSort.Sort(items, comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSort(items, keySelector, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort.HeapSort.Sort(items.ToArray(), keySelector, comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSort(items, keySelector, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort.HeapSort.Sort(items, keySelector, comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(this IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return Sort.HeapSort.SortDesc(items.ToArray(), comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(this IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return Sort.HeapSort.SortDesc(items.ToArray(), comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(this IList<TSource> items, Comparison<TSource> comparison = null)
        {
            return Sort.HeapSort.SortDesc(items, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(this IList<TSource> items, IComparer<TSource> comparer)
        {
            return Sort.HeapSort.SortDesc(items, comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSortDesc(items, keySelector, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(this IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort.HeapSort.SortDesc(items.ToArray(), keySelector, comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSortDesc(items, keySelector, comparison.CreateComparer());
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(this IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort.HeapSort.SortDesc(items, keySelector, comparer);
        }

    }
}
