using StdOttStandard.Linq.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.Linq.Sort
{
    public static class MergeParallelSort
    {
        private const int defaultMergeInsertionSortCount = 30;

        public static Task<IList<TSource>> Sort<TSource>(IList<TSource> items, int threadCount, Comparison<TSource> comparison = null)
        {
            return Sort(items, 0, items.Count, defaultMergeInsertionSortCount, threadCount, comparison.CreateComparer());
        }

        public static Task<IList<TSource>> Sort<TSource>(IList<TSource> items, int begin, int end, int threadCount, Comparison<TSource> comparison = null)
        {
            return Sort(items, begin, end, defaultMergeInsertionSortCount, threadCount, comparison.CreateComparer());
        }

        public static async Task<IList<TSource>> Sort<TSource>(IList<TSource> items,
            int begin, int end, int insertionSortCount, int threadCount, IComparer<TSource> comparer)
        {
            int insertionSortActionsCount = (int)Math.Ceiling((end - begin) / (double)insertionSortCount);
            int depth = (int)Math.Ceiling(Math.Log(insertionSortActionsCount, 2));
            int donesCount = 0;
            IList<bool> dones = new bool[2 * insertionSortActionsCount + depth];
            Queue<Action> queue = new Queue<Action>();

            for (int i = begin; i < end; i += insertionSortCount)
            {
                int index = donesCount++;
                int insertionBegin = i;
                int insertionEnd = Math.Min(i + insertionSortCount, end);

                queue.Enqueue(() => InsertionSort(index, insertionBegin, insertionEnd));
            }

            IList<TSource> items1 = items;
            IList<TSource> items2 = new TSource[items.Count];

            for (int range = insertionSortCount; range < end - begin; range *= 2)
            {
                int lastActionsCount = (int)Math.Ceiling((end - begin) / (double)range);
                int lastActionsBegin = queue.Count - lastActionsCount;
                int currentRange = range;
                IList<TSource> src = items1, dest = items2;

                for (int j = 0, rangeBegin = begin; rangeBegin < end; j++, rangeBegin += range * 2)
                {
                    int leftNodeIndex = lastActionsBegin + j * 2;
                    int rightNodeIndex = lastActionsBegin + Math.Min(j * 2 + 1, lastActionsCount);
                    int index = donesCount++;
                    int currentBegin = rangeBegin;

                    queue.Enqueue(() => Merge(leftNodeIndex, rightNodeIndex, index, src, dest, currentBegin, currentRange));
                }

                StdUtils.Swap(ref items1, ref items2);
            }

            await AsyncUtils.ParallelForEach(queue, threadCount);

            return items1;

            void InsertionSort(int thisIndex, int insertionSortBegin, int insertionSortEnd)
            {
                Linq.Sort.InsertionSort.Sort(items, insertionSortBegin, insertionSortEnd, comparer);

                lock (dones)
                {
                    dones[thisIndex] = true;
                    Monitor.PulseAll(dones);
                }
            }

            void Merge(int leftIndex, int rightIndex, int thisIndex,
                IList<TSource> mergeSrc, IList<TSource> mergeDest, int mergeBegin, int size)
            {
                lock (dones)
                {
                    while (!dones[leftIndex]) Monitor.Wait(dones);
                    while (!dones[rightIndex]) Monitor.Wait(dones);
                }

                MergeSort.Merge(mergeSrc, mergeDest, mergeBegin, size, end, comparer);
                lock (dones)
                {
                    dones[thisIndex] = true;
                    Monitor.PulseAll(dones);
                }
            }
        }



        public static Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, int threadCount,
            Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort(items, 0, items.Count, threadCount, keySelector, comparison);
        }

        public static Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int threadCount, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort(items, begin, end, threadCount, keySelector, comparison.CreateComparer());
        }

        public static Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int threadCount, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            return Sort(items, begin, end, defaultMergeInsertionSortCount, threadCount, keySelector, comparer);
        }

        public static Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int insertionSortCount, int threadCount, Func<TSource, TKey> keySelector, Comparison<TKey> comparison = null)
        {
            return Sort(items, begin, end, insertionSortCount, threadCount, keySelector, comparison.CreateComparer());
        }

        public static Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, int begin, int end,
            int insertionSortCount, int threadCount, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            IList<TKey> keys = items.Select(keySelector).ToArray();

            return Sort(items, keys, begin, end, threadCount, insertionSortCount, comparer);
        }

        private static async Task<IList<TSource>> Sort<TSource, TKey>(IList<TSource> items, IList<TKey> keys,
           int begin, int end, int insertionSortCount, int threadCount, IComparer<TKey> comparer)
        {
            int insertionSortActionsCount = (int)Math.Ceiling((end - begin) / (double)insertionSortCount);
            int depth = (int)Math.Ceiling(Math.Log(insertionSortActionsCount, 2));
            int donesCount = 0;
            IList<bool> dones = new bool[2 * (insertionSortActionsCount - 1) + depth];
            Queue<Action> queue = new Queue<Action>();

            for (int i = begin; i < end; i += insertionSortCount)
            {
                int index = donesCount++;
                int insertionBegin = i;
                int insertionEnd = Math.Min(i + insertionSortCount, end);

                queue.Enqueue(() => InsertionSort(index, insertionBegin, insertionEnd));
            }

            IList<TSource> values1 = items;
            IList<TKey> keys1 = keys;
            IList<TSource> values2 = new TSource[items.Count];
            IList<TKey> keys2 = new TKey[items.Count];

            for (int range = insertionSortCount; range < end - begin; range *= 2)
            {
                int lastActionsCount = (int)Math.Ceiling((end - begin) / (double)range);
                int lastActionsBegin = queue.Count - lastActionsCount;
                int currentRange = range;
                IList<TSource> srcValues = values1, destValues = values2;
                IList<TKey> srcKeys = keys1, destKeys = keys2;

                for (int j = 0, rangeBegin = begin; rangeBegin < end; j++, rangeBegin += range * 2)
                {
                    int leftNodeIndex = lastActionsBegin + j * 2;
                    int rightNodeIndex = lastActionsBegin + Math.Min(j * 2 + 1, lastActionsCount);
                    int index = donesCount++;
                    int currentBegin = rangeBegin;

                    queue.Enqueue(() => Merge(leftNodeIndex, rightNodeIndex, index, srcValues, srcKeys, destValues, destKeys, currentBegin, currentRange));
                }

                StdUtils.Swap(ref values1, ref values2);
            }

            await AsyncUtils.ParallelForEach(queue, threadCount);

            return values1;

            void InsertionSort(int thisIndex, int insertionSortBegin, int insertionSortEnd)
            {
                Linq.Sort.InsertionSort.Sort(items, keys, insertionSortBegin, insertionSortEnd, comparer);

                lock (dones)
                {
                    dones[thisIndex] = true;
                    Monitor.PulseAll(dones);
                }
            }

            void Merge(int leftIndex, int rightIndex, int thisIndex, IList<TSource> srcValues, IList<TKey> srcKeys,
                IList<TSource> destValues, IList<TKey> destKeys, int mergeBegin, int size)
            {
                lock (dones)
                {
                    while (!dones[leftIndex]) Monitor.Wait(dones);
                    while (!dones[rightIndex]) Monitor.Wait(dones);
                }

                MergeSort.Merge(srcValues, srcKeys, destValues, destKeys, mergeBegin, size, end, comparer);
                lock (dones)
                {
                    dones[thisIndex] = true;
                    Monitor.PulseAll(dones);
                }
            }
        }
    }
}
