using System.Collections;
using System.Collections.Generic;

namespace StdOttStandard
{
    public class OnRequestBufferEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> src;
        private OnRequestBuffer<T> buffer;

        public OnRequestBufferEnumerable(IEnumerable<T> src)
        {
            this.src = src;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (buffer == null) buffer = new OnRequestBuffer<T>(src.GetEnumerator());
            else if (buffer.IsEnded) return buffer.Buffer.GetEnumerator();

            return new OnRequestBufferEnumerator<T>(buffer);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class OnRequestBuffer<T>
    {
        public bool IsEnded { get; private set; }

        public IEnumerator<T> Enumerator { get; }

        public List<T> Buffer { get; }

        public OnRequestBuffer(IEnumerator<T> enumerator)
        {
            Enumerator = enumerator;
            Buffer = new List<T>();
        }

        public void Ended()
        {
            IsEnded = true;
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
            if (buffer.IsEnded)
            {
                Current = default(T);
                return false;
            }

            index++;

            if (index < buffer.Buffer.Count)
            {
                Current = buffer.Buffer[index];
                return true;
            }

            if (!buffer.Enumerator.MoveNext())
            {
                buffer.Ended();
                Current = default(T);
                return false;
            }

            Current = buffer.Enumerator.Current;
            buffer.Buffer.Add(Current);

            return true;
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
