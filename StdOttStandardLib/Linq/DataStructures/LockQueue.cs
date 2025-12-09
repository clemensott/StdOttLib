using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace StdOttStandard.Linq.DataStructures
{
    public class LockQueue<T> : IEnumerable<T>
    {
        private readonly Queue<T> queue;

        public bool IsEnd { get; private set; }

        public int Count => queue.Count;

        public LockQueue()
        {
            queue = new Queue<T>();
        }

        public void End()
        {
            lock (queue)
            {
                IsEnd = true;
                Monitor.PulseAll(queue);
            }
        }

        public void Enqueue(T item)
        {
            lock (queue)
            {
                IsEnd = false;
                queue.Enqueue(item);
                Monitor.Pulse(queue);
            }
        }

        public void Enqueue(IEnumerable<T> items)
        {
            lock (queue)
            {
                IsEnd = false;
                foreach (T item in items)
                {
                    queue.Enqueue(item);
                    Monitor.Pulse(queue);
                }
            }
        }

        public bool TryDequeue(out T item)
        {
            lock (queue)
            {
                while (queue.Count == 0)
                {
                    if (IsEnd)
                    {
                        item = default(T);
                        return false;
                    }

                    Monitor.Wait(queue);
                }

                item = queue.Dequeue();
                return true;
            }
        }

        public (bool isEnd, T item) Dequeue()
        {
            T item;
            bool isEnd = !TryDequeue(out item);
            return (isEnd, item);
        }

        public (bool isEnd, T[] items) DequeueBatch()
        {
            lock (queue)
            {
                if (IsEnd && queue.Count == 0) return (true, new T[0]);

                while (queue.Count == 0)
                {
                    if (IsEnd) break;

                    Monitor.Wait(queue);
                }

                List<T> batch = new List<T>();
                while (queue.Count > 0) batch.Add(queue.Dequeue());
                return (IsEnd, batch.ToArray());
            }
        }

        public T[] ToArray()
        {
            lock (queue)
            {
                return queue.ToArray();
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
