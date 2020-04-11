using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard.Linq
{
    public static class GenerateUtils
    {
        public static IEnumerable<TSource> ToIEnumerable<TSource>(IEnumerable<TSource> src)
        {
            return src.Select(i => i);
        }

        public static IList<TSource> ToIList<TSource>(this IEnumerable<TSource> source)
        {
            return source as IList<TSource> ?? source?.ToArray();
        }

        public static OnRequestBuffer<TSource> ToBuffer<TSource>(this IEnumerable<TSource> source)
        {
            switch (source)
            {
                case OnRequestBuffer<TSource> buffer:
                    return buffer;

                default:
                    return new OnRequestBuffer<TSource>(source);
            }
        }

        public static IEnumerable<TSource> ToNotNull<TSource>(this IEnumerable<TSource> enumerable)
        {
            return enumerable ?? Enumerable.Empty<TSource>();
        }

        public static IEnumerable ToNotNull(this IEnumerable enumerable)
        {
            return enumerable ?? Enumerable.Empty<object>();
        }

        public static IEnumerable<TResult> ConcatParams<TResult>(this IEnumerable<TResult> items, TResult item, params TResult[] more)
        {
            return Enumerable.Concat(items, ConcatParams(item, more));
        }

        public static IEnumerable<TResult> ConcatParams<TResult>(TResult first, params TResult[] more)
        {
            yield return first;

            foreach (TResult item in more) yield return item;
        }

        public static IEnumerable<TSource> Insert<TSource>(this IEnumerable<TSource> items, int index, TSource item)
        {
            int i = 0;

            using (IEnumerator<TSource> enumerator = items.GetEnumerator())
            {
                while (i++ < index)
                {
                    if (!enumerator.MoveNext()) yield break;

                    yield return enumerator.Current;
                }

                yield return item;

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> Remove<TSource>(this IEnumerable<TSource> src, TSource item, int max = 1)
        {
            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            using (IEnumerator<TSource> enumerator = src.GetEnumerator())
            {
                while (max > 0)
                {
                    if (!enumerator.MoveNext()) yield break;

                    TSource current = enumerator.Current;
                    if (!comparer.Equals(current, item)) yield return current;
                    else max--;
                }

                while (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
            }
        }

        public static IEnumerable<TSource> RemoveAll<TSource>(this IEnumerable<TSource> items, TSource item)
        {
            IEqualityComparer<TSource> comparer = EqualityComparer<TSource>.Default;
            return items.Where(i => !comparer.Equals(i, item));
        }

        public static IEnumerable<TSource> Extract<TSource>(this IEnumerable<TSource> items, out TSource first)
        {
            IEnumerable<TSource> list = items.ToBuffer();

            first = list.First();
            return list.Skip(1);
        }

        public static IEnumerable<Source> ExtractOrDefault<Source>(this IEnumerable<Source> items, out Source first)
        {
            IEnumerable<Source> list = items.ToBuffer();

            first = list.FirstOrDefault();
            return list.Skip(1);
        }

        public static IEnumerable<TSource> SelectRecursive<TSource>(TSource source, Func<TSource, IEnumerable<TSource>> selector)
        {
            Queue<TSource> queue = new Queue<TSource>();

            queue.Enqueue(source);

            while (queue.Count > 0)
            {
                source = queue.Dequeue();

                yield return source;

                foreach (TSource subItem in selector(source))
                {
                    queue.Enqueue(subItem);
                }
            }
        }
    }
}
