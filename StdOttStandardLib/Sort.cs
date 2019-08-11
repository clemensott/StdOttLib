using System;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard
{
    public static class Sort
    {
        private const int defaultMergeInsertionSortCount = 15;

        private static Comparer<T> CreateComparer<T>(Comparison<T> comparison)
        {
            return comparison != null ? Comparer<T>.Create(comparison) : Comparer<T>.Default;
        }

        public static TSource[] InsertionSort<TSource>(IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            TSource[] array = items.ToArray();
            InsertionSort(array, 0, array.Length, CreateComparer(comparison));

            return array;
        }

        public static void InsertionSort<TSource>(IList<TSource> items, Comparison<TSource> comparison = null)
        {
            InsertionSort(items, 0, items.Count, CreateComparer(comparison));
        }

        public static void InsertionSort<TSource>(IList<TSource> items, int begin, int end, Comparison<TSource> comparison = null)
        {
            InsertionSort(items, begin, end, CreateComparer(comparison));
        }

        public static void InsertionSort<TSource>(IList<TSource> items, int begin, int end, IComparer<TSource> comparer)
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
        }

        public static TSource[] InsertionSort<TSource, TKey>(IEnumerable<TSource> items,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            TSource[] array = items.ToArray();
            InsertionSort(array, 0, array.Length, keySelector, comparison);

            return array;
        }

        public static void InsertionSort<TSource, TKey>(IList<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            InsertionSort(items, 0, items.Count, keySelector, comparison);
        }

        public static void InsertionSort<TSource, TKey>(IList<TSource> items, int begin, int end,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            InsertionSort(items, begin, end, keySelector, CreateComparer(comparison));
        }

        public static void InsertionSort<TSource, TKey>(IList<TSource> items, int begin, int end,
        Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TKey> keys = items.Select(keySelector).ToArray();

            InsertionSort(items, keys, begin, end, comparer);
        }

        private static void InsertionSort<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int begin, int end, IComparer<TKey> comparer)
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

        public static TSource[] MergeInsertionSort<TSource>(IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            TSource[] array = items.ToArray();
            MergeInsertionSort(array, 0, array.Length, defaultMergeInsertionSortCount, CreateComparer(comparison));

            return array;
        }

        public static void MergeInsertionSort<TSource>(IList<TSource> items, Comparison<TSource> comparison = null)
        {
            MergeInsertionSort(items, 0, items.Count, defaultMergeInsertionSortCount, CreateComparer(comparison));
        }

        public static void MergeInsertionSort<TSource>(IList<TSource> items, int begin, int end, int insertionSortCount, Comparer<TSource> comparer)
        {
            if (end - begin - 1 <= insertionSortCount)
            {
                InsertionSort(items, begin, end, comparer);
                return;
            }

            int middle = (begin + end) / 2;

            MergeInsertionSort(items, begin, middle, insertionSortCount, comparer);
            MergeInsertionSort(items, middle, end, insertionSortCount, comparer);
            Merge(items, begin, middle, end, comparer);
        }

        private static void Merge<TSource>(IList<TSource> items, int begin, int middle, int end, Comparer<TSource> comparer)
        {
            IList<TSource> itemsA = new TSource[middle - begin];
            IList<TSource> itemsB = new TSource[end - middle];

            for (int i = 0; i < itemsA.Count; i++) itemsA[i] = items[begin + i];
            for (int i = 0; i < itemsB.Count; i++) itemsB[i] = items[middle + i];

            int indexA = 0, indexB = 0;

            for (int i = begin; i < end; i++)
            {
                if (indexB == itemsB.Count ||
                    (indexA != itemsA.Count && comparer.Compare(itemsA[indexA], itemsB[indexB]) < 0))
                {
                    items[i] = itemsA[indexA++];
                }
                else items[i] = itemsB[indexB++];
            }
        }

        public static TSource[] MergeInsertionSort<TSource, TKey>(IEnumerable<TSource> items,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            TSource[] array = items.ToArray();
            MergeInsertionSort(array, 0, array.Length, defaultMergeInsertionSortCount, keySelector, CreateComparer(comparison));

            return array;
        }

        public static void MergeInsertionSort<TSource, TKey>(IList<TSource> items,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            MergeInsertionSort(items, 0, items.Count, defaultMergeInsertionSortCount, keySelector, CreateComparer(comparison));
        }

        public static void MergeInsertionSort<TSource, TKey>(IList<TSource> items, int begin, int end, int insertionSortCount,
            Func<TSource, TKey> keySelector, Comparer<TKey> comparer)
        {
            IList<TKey> keys = items.Select(keySelector).ToArray();

            MergeInsertionSort(items, keys, begin, end, insertionSortCount, comparer);
        }

        public static void MergeInsertionSort<TSource, TKey>(IList<TSource> items, IList<TKey> keys,
            int begin, int end, int insertionSortCount, Comparer<TKey> comparer)
        {
            if (end - begin - 1 <= insertionSortCount)
            {
                InsertionSort(items, keys, begin, end, comparer);
                return;
            }

            int middle = (begin + end) / 2;

            MergeInsertionSort(items, keys, begin, middle, insertionSortCount, comparer);
            MergeInsertionSort(items, keys, middle, end, insertionSortCount, comparer);
            Merge(items, keys, begin, middle, end, comparer);
        }

        private static void Merge<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int begin, int middle, int end, Comparer<TKey> comparer)
        {
            IList<TSource> itemsA = new TSource[middle - begin];
            IList<TKey> keysA = new TKey[middle - begin];
            IList<TSource> itemsB = new TSource[end - middle];
            IList<TKey> keysB = new TKey[end - middle];

            for (int i = 0; i < itemsA.Count; i++)
            {
                itemsA[i] = items[begin + i];
                keysA[i] = keys[begin + i];
            }

            for (int i = 0; i < itemsB.Count; i++)
            {
                itemsB[i] = items[middle + i];
                keysB[i] = keys[middle + i];
            }

            int indexA = 0, indexB = 0;

            for (int i = begin; i < end; i++)
            {
                if (indexB == itemsB.Count ||
                    (indexA != itemsA.Count && comparer.Compare(keysA[indexA], keysB[indexB]) < 0))
                {
                    items[i] = itemsA[indexA];
                    keys[i] = keysA[indexA];

                    indexA++;
                }
                else
                {
                    items[i] = itemsB[indexB];
                    keys[i] = keysB[indexB];

                    indexB++;
                }
            }
        }

        public static IEnumerable<TSource> HeapSort<TSource>(IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return HeapSort(items.CastOrToArray(), CreateComparer(comparison));
        }

        public static IEnumerable<TSource> HeapSort<TSource>(IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return HeapSort(items.CastOrToArray(), comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource>(IList<TSource> items, IComparer<TSource> comparer)
        {
            int count = items.Count;

            for (int i = count / 2; i >= 0; i--)
            {
                Heapify(items, count, i, comparer);
            }

            if (count == 0) yield break;

            while (true)
            {
                yield return items[0];

                if (count == 1) yield break;

                items.Swap(0, count - 1);
                count--;

                Heapify(items, count, 0, comparer);
            }
        }

        private static void Heapify<TSource>(IList<TSource> items, int count, int i, IComparer<TSource> comparer)
        {
            int e = i;
            int l = Left(i);
            int r = Right(i);

            if (l < count && comparer.Compare(items[e], items[l]) > 0) e = l;
            if (r < count && comparer.Compare(items[e], items[r]) > 0) e = r;

            if (e == i) return;

            items.Swap(i, e);

            Heapify(items, count, e, comparer);
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSort(items, keySelector, CreateComparer(comparison));
        }

        public static IEnumerable<TSource> HeapSort<TSource, TKey>(IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TSource> array = items.CastOrToArray();
            TKey[] keys = array.Select(keySelector).ToArray();

            return HeapSort(array, keys, comparer);
        }

        private static IEnumerable<TSource> HeapSort<TSource, TKey>(IList<TSource> items, IList<TKey> keys, IComparer<TKey> comparer)
        {
            int count = items.Count;

            for (int i = count / 2; i >= 0; i--)
            {
                Heapify(items, keys, count, i, comparer);
            }

            if (count == 0) yield break;

            while (true)
            {
                yield return items[0];

                if (count == 1) yield break;

                items.Swap(0, count - 1);
                keys.Swap(0, count - 1);
                count--;

                Heapify(items, keys, count, 0, comparer);
            }
        }

        private static void Heapify<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int count, int i, IComparer<TKey> comparer)
        {
            int e = i;
            int l = Left(i);
            int r = Right(i);

            if (l < count && comparer.Compare(keys[e], keys[l]) > 0) e = l;
            if (r < count && comparer.Compare(keys[e], keys[r]) > 0) e = r;

            if (e == i) return;

            items.Swap(i, e);
            keys.Swap(i, e);

            Heapify(items, keys, count, e, comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(IEnumerable<TSource> items, Comparison<TSource> comparison = null)
        {
            return HeapSortDesc(items.CastOrToArray(), CreateComparer(comparison));
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(IEnumerable<TSource> items, IComparer<TSource> comparer)
        {
            return HeapSortDesc(items.CastOrToArray(), comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource>(IList<TSource> items, IComparer<TSource> comparer)
        {
            int count = items.Count;

            for (int i = count / 2; i >= 0; i--)
            {
                HeapifyDesc(items, count, i, comparer);
            }

            if (count == 0) yield break;

            while (true)
            {
                yield return items[0];

                if (count == 1) yield break;

                items.Swap(0, count - 1);
                count--;

                HeapifyDesc(items, count, 0, comparer);
            }
        }

        private static void HeapifyDesc<TSource>(IList<TSource> items, int count, int i, IComparer<TSource> comparer)
        {
            int e = i;
            int l = Left(i);
            int r = Right(i);

            if (l < count && comparer.Compare(items[e], items[l]) <= 0) e = l;
            if (r < count && comparer.Compare(items[e], items[r]) <= 0) e = r;

            if (e == i) return;

            items.Swap(i, e);

            HeapifyDesc(items, count, e, comparer);
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(IEnumerable<TSource> items, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return HeapSortDesc(items, keySelector, CreateComparer(comparison));
        }

        public static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(IEnumerable<TSource> items, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TSource> array = items.CastOrToArray();
            TKey[] keys = array.Select(keySelector).ToArray();

            return HeapSortDesc(array, keys, comparer);
        }

        private static IEnumerable<TSource> HeapSortDesc<TSource, TKey>(IList<TSource> items, IList<TKey> keys, IComparer<TKey> comparer)
        {
            int count = items.Count;

            for (int i = count / 2; i >= 0; i--)
            {
                HeapifyDesc(items, keys, count, i, comparer);
            }

            if (count == 0) yield break;

            while (true)
            {
                yield return items[0];

                if (count == 1) yield break;

                items.Swap(0, count - 1);
                keys.Swap(0, count - 1);
                count--;

                HeapifyDesc(items, keys, count, 0, comparer);
            }
        }

        private static void HeapifyDesc<TSource, TKey>(IList<TSource> items, IList<TKey> keys, int count, int i, IComparer<TKey> comparer)
        {
            int e = i;
            int l = Left(i);
            int r = Right(i);

            if (l < count && comparer.Compare(keys[e], keys[l]) <= 0) e = l;
            if (r < count && comparer.Compare(keys[e], keys[r]) <= 0) e = r;

            if (e == i) return;

            items.Swap(i, e);
            keys.Swap(i, e);

            HeapifyDesc(items, keys, count, e, comparer);
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
