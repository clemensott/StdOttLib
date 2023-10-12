using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StdOttStandard.Linq.DataStructures
{
    public class AsyncQueue<T> : IEnumerable<T>
    {
        private readonly SemaphoreSlim semSem, semQueue;
        private readonly Queue<T> queue;

        public bool IsEnd { get; private set; }

        public int Count => queue.Count;

        public AsyncQueue()
        {
            semSem = new SemaphoreSlim(1);
            semQueue = new SemaphoreSlim(0);
            queue = new Queue<T>();
        }

        public async Task End()
        {
            await semSem.WaitAsync();

            if (!IsEnd)
            {
                IsEnd = true;
                semQueue.Release();
            }

            semSem.Release();
        }

        public async Task Enqueue(T item)
        {
            await semSem.WaitAsync();

            IsEnd = false;
            queue.Enqueue(item);
            if (semQueue.CurrentCount == 0) semQueue.Release();

            semSem.Release();
        }

        public async Task Enqueue(IEnumerable<T> items)
        {
            await semSem.WaitAsync().ConfigureAwait(false);

            IsEnd = false;
            foreach (T item in items) queue.Enqueue(item);
            if (semQueue.CurrentCount == 0) semQueue.Release();

            semSem.Release();
        }

        public async Task<T> DequeueOrDefault()
        {
            while (true)
            {
                try
                {
                    await semSem.WaitAsync().ConfigureAwait(false);

                    if (queue.Count > 0) return queue.Dequeue();
                    if (IsEnd) return default(T);
                }
                finally
                {
                    semSem.Release();
                }

                await semQueue.WaitAsync().ConfigureAwait(false);
            }
        }

        public Task<(bool isEnd, T item)> Dequeue()
        {
            return Dequeue(CancellationToken.None);
        }

        public async Task<(bool isEnd, T item)> Dequeue(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    await semSem.WaitAsync(cancellationToken).ConfigureAwait(false);

                    if (queue.Count > 0) return (false, queue.Dequeue());
                    if (IsEnd) return (true, default(T));
                }
                finally
                {
                    semSem.Release();
                }

                await semQueue.WaitAsync().ConfigureAwait(false);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
