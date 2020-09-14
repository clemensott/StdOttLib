using StdOttStandard.Linq.DataStructures.Enumerators;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StdOttStandard.Linq.DataStructures
{
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private SingleNode<T> first, last;

        public bool IsEmpty => last == null;

        public int Count { get; private set; }

        public LinkedQueue() { }

        public LinkedQueue(IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                Enqueue(item);
            }
        }

        public void Enqueue(T item)
        {
            if (last == null) first = last = new SingleNode<T>(item);
            else last = last.Next = new SingleNode<T>(item);

            Count++;
        }

        public T Dequeue()
        {
            if (first == null) throw new InvalidOperationException("Queue is empty");

            T value = first.Value;
            first = first.Next;
            Count--;

            return value;
        }

        public bool TryDequeue(out T item)
        {
            if (first == null)
            {
                item = default(T);
                return false;
            }

            item = first.Value;
            first = first.Next;
            Count--;

            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new SingleNodeEnummerator<T>(first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
