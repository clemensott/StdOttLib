using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace StdOttStandard
{
    public class OnRequestBuffer<T> : IEnumerable<T>
    {
        private IEnumerator<T> enumerator;

        public bool IsEnded { get; private set; }

        public IEnumerable<T> Source { get; }

        internal IEnumerator<T> Enumerator => enumerator ?? (enumerator = Source.GetEnumerator());

        internal IList<T> Buffer { get; }

        public IEnumerable<T> CurrentData => Buffer.Cast<T>();

        public OnRequestBuffer(IEnumerable<T> src)
        {
            Source = src;

            if (src is IList<T> list)
            {
                Buffer = list;
                Ended();
            }
            else Buffer = new List<T>();
        }

        public void Ended() => IsEnded = true;

        public IEnumerator<T> GetEnumerator()
        {
            return IsEnded ? Buffer.GetEnumerator() : new OnRequestBufferEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class OnRequestBufferEnumerator<T> : IEnumerator<T>
    {
        private int index;
        private readonly OnRequestBuffer<T> buffer;

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        internal OnRequestBufferEnumerator(OnRequestBuffer<T> buffer)
        {
            this.buffer = buffer;
            index = -1;
        }

        public bool MoveNext()
        {
            index++;

            if (index < buffer.Buffer.Count)
            {
                Current = buffer.Buffer[index];
                return true;
            }

            if (!buffer.IsEnded)
            {
                if (buffer.Enumerator.MoveNext())
                {
                    Current = buffer.Enumerator.Current;
                    buffer.Buffer.Add(Current);

                    return true;
                }

                buffer.Ended();
            }

            index--;

            Current = default(T);
            return false;
        }

        public void Reset()
        {
            index = -1;
        }

        public void Dispose()
        {
        }
    }
}
