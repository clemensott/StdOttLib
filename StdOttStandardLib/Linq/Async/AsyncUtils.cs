using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StdOttStandard.Linq.Async
{
    public static class AsyncUtils
    {
        public static Task ParallelForEach<TSource>(this IEnumerable<TSource> src, int threadCount, Action<TSource> body)
        {
            return ParallelForEach(new Queue<TSource>(src), threadCount, body);
        }

        public static Task ParallelForEach<TSource>(this Queue<TSource> src, int threadCount, Action<TSource> body)
        {
            return Task.WhenAll(Enumerable.Range(0, threadCount).Select(_ => Task.Run((Action)Thread)));

            void Thread()
            {
                while (true)
                {
                    TSource param;

                    lock (src)
                    {
                        if (src.Count == 0) return;

                        param = src.Dequeue();
                    }

                    body(param);
                }
            }
        }

        public static Task ParallelForEach(this Queue<Action> src, int threadCount)
        {
            return Task.WhenAll(Enumerable.Range(0, threadCount).Select(_ => Task.Run((Action)Thread)));

            void Thread()
            {
                while (true)
                {
                    Action action;

                    lock (src)
                    {
                        if (src.Count == 0) return;

                        action = src.Dequeue();
                    }

                    action();
                }
            }
        }
    }
}
