using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq.Sort
{
    public static class HeapSort
    {
        public static IEnumerable<TSource> Sort<TSource>(IList<TSource> items, IComparer<TSource> comparer)
        {
            if (items.Count == 0) yield break;

            int index = 0;
            int size = items.Count;

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(items, size, i, comparer);
            }

            while (true)
            {
                items.Swap(items.Count - 1, index);

                yield return items[index];

                if (size == 1) yield break;

                index++;
                size--;

                Heapify(items, size, 0, comparer);
            }
        }

        private static void Heapify<TSource>(IList<TSource> items, int size, int indexFromBack, IComparer<TSource> comparer)
        {
            while (true)
            {
                int e = indexFromBack;
                int l = Left(indexFromBack);
                int r = Right(indexFromBack);

                if (l < size && comparer.Compare(items[items.Count - 1 - e], items[items.Count - 1 - l]) > 0) e = l;
                if (r < size && comparer.Compare(items[items.Count - 1 - e], items[items.Count - 1 - r]) > 0) e = r;

                if (e == indexFromBack) return;

                items.Swap(items.Count - 1 - indexFromBack, items.Count - 1 - e);

                indexFromBack = e;
            }
        }

        public static IEnumerable<TSource> Sort<TSource, TKey>(IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            TKey[] keys = items.Select(keySelector).ToArray();

            return Sort(items, keys, comparer);
        }

        private static IEnumerable<TSource> Sort<TSource, TKey>(IList<TSource> items, IList<TKey> keys, IComparer<TKey> comparer)
        {
            if (items.Count == 0) yield break;

            int index = 0;
            int size = items.Count;

            for (int i = size / 2; i >= 0; i--)
            {
                Heapify(items, keys, size, i, comparer);
            }

            while (true)
            {
                items.Swap(items.Count - 1, index);
                keys.Swap(items.Count - 1, index);

                yield return items[index];

                if (size == 1) yield break;

                index++;
                size--;

                Heapify(items, keys, size, 0, comparer);
            }
        }

        private static void Heapify<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int size, int indexFromBack, IComparer<TKey> comparer)
        {
            while (true)
            {
                int e = indexFromBack;
                int l = Left(indexFromBack);
                int r = Right(indexFromBack);

                if (l < size && comparer.Compare(keys[keys.Count - 1 - e], keys[keys.Count - 1 - l]) > 0) e = l;
                if (r < size && comparer.Compare(keys[keys.Count - 1 - e], keys[keys.Count - 1 - r]) > 0) e = r;

                if (e == indexFromBack) return;

                items.Swap(items.Count - 1 - indexFromBack, items.Count - 1 - e);
                keys.Swap(keys.Count - 1 - indexFromBack, keys.Count - 1 - e);

                indexFromBack = e;
            }
        }

        public static IEnumerable<TSource> SortDesc<TSource>(IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return SortDesc(items.ToIList(), comparison.CreateComparer());
        }

        public static IEnumerable<TSource> SortDesc<TSource>(IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return SortDesc(items.ToIList(), comparer);
        }

        public static IEnumerable<TSource> SortDesc<TSource>(IList<TSource> items, IComparer<TSource> comparer)
        {
            if (items.Count == 0) yield break;

            int index = 0;
            int size = items.Count;

            for (int i = size / 2; i >= 0; i--)
            {
                HeapifyDesc(items, size, i, comparer);
            }

            while (true)
            {
                items.Swap(items.Count - 1, index);

                yield return items[index];

                if (size == 1) yield break;

                index++;
                size--;

                HeapifyDesc(items, size, 0, comparer);
            }
        }

        private static void HeapifyDesc<TSource>(IList<TSource> items, int size, int indexFromBack, IComparer<TSource> comparer)
        {
            while (true)
            {
                int e = indexFromBack;
                int l = Left(indexFromBack);
                int r = Right(indexFromBack);

                if (l < size && comparer.Compare(items[items.Count - 1 - e], items[items.Count - 1 - l]) <= 0) e = l;
                if (r < size && comparer.Compare(items[items.Count - 1 - e], items[items.Count - 1 - r]) <= 0) e = r;

                if (e == indexFromBack) return;

                items.Swap(items.Count - 1 - indexFromBack, items.Count - 1 - e);

                indexFromBack = e;
            }
        }

        public static IEnumerable<TSource> SortDesc<TSource, TKey>(IList<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            TKey[] keys = items.Select(keySelector).ToArray();

            return SortDesc(items, keys, comparer);
        }

        private static IEnumerable<TSource> SortDesc<TSource, TKey>(IList<TSource> items, IList<TKey> keys, IComparer<TKey> comparer)
        {
            if (items.Count == 0) yield break;

            int index = 0;
            int size = items.Count;

            for (int i = size / 2; i >= 0; i--)
            {
                HeapifyDesc(items, keys, size, i, comparer);
            }

            while (true)
            {
                items.Swap(items.Count - 1, index);
                keys.Swap(items.Count - 1, index);

                yield return items[index];

                if (size == 1) yield break;

                index++;
                size--;

                HeapifyDesc(items, keys, size, 0, comparer);
            }
        }

        private static void HeapifyDesc<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int size, int indexFromBack, IComparer<TKey> comparer)
        {
            while (true)
            {
                int e = indexFromBack;
                int l = Left(indexFromBack);
                int r = Right(indexFromBack);

                if (l < size && comparer.Compare(keys[keys.Count - 1 - e], keys[keys.Count - 1 - l]) <= 0) e = l;
                if (r < size && comparer.Compare(keys[keys.Count - 1 - e], keys[keys.Count - 1 - r]) <= 0) e = r;

                if (e == indexFromBack) return;

                items.Swap(items.Count - 1 - indexFromBack, items.Count - 1 - e);
                keys.Swap(keys.Count - 1 - indexFromBack, keys.Count - 1 - e);

                indexFromBack = e;
            }
        }

        private static int Left(int i)
        {
            return 2 * i + 1;
        }

        private static int Right(int i)
        {
            return 2 * i + 2;
        }
    }
}
