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
                        Utils.Swap(items, j, j - 1);
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
                        Utils.Swap(items, j, j - 1);
                        Utils.Swap(keys, j, j - 1);
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
    }
}
