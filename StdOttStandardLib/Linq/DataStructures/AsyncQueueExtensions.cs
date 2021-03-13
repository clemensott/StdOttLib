using System;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.Linq.DataStructures
{
    public static class AsyncQueueExtensions
    {
        public static async Task Consume<TSource>(this AsyncQueue<TSource> queue, CancellationToken cancellationToken, Func<TSource, Task> consumer)
        {
            while (true)
            {
                (bool isEnd, TSource item) = await queue.Dequeue(cancellationToken);
                if (isEnd) return;

                await consumer(item);
            }
        }

        public static async Task Consume<TSource>(this AsyncQueue<TSource> queue, Func<TSource, Task> consumer)
        {
            while (true)
            {
                (bool isEnd, TSource item) = await queue.Dequeue();
                if (isEnd) return;

                await consumer(item);
            }
        }

        public static async Task Consume<TSource>(this AsyncQueue<TSource> queue, CancellationToken cancellationToken, Action<TSource> consumer)
        {
            while (true)
            {
                (bool isEnd, TSource item) = await queue.Dequeue(cancellationToken);
                if (isEnd) return;

                consumer(item);
            }
        }

        public static async Task Consume<TSource>(this AsyncQueue<TSource> queue, Action<TSource> consumer)
        {
            while (true)
            {
                (bool isEnd, TSource item) = await queue.Dequeue();
                if (isEnd) return;

                consumer(item);
            }
        }
    }
}
