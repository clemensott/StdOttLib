using System;
using System.Collections;
using System.Collections.Generic;

namespace StdOttStandard.Linq.DataStructures.Enumerators
{
    public class EndedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> @base;

        public T Current => @base.Current;

        object IEnumerator.Current => ((IEnumerator)@base).Current;

        public bool Ended { get; private set; }

        public EndedEnumerator(IEnumerator<T> @base)
        {
            this.@base = @base ?? throw new ArgumentNullException(nameof(@base));
            Ended = false;
        }

        public void Dispose()
        {
            @base.Dispose();
        }

        public bool MoveNext()
        {
            if (@base.MoveNext()) return true;

            Ended = true;
            return false;
        }

        public void Reset()
        {
            @base.Reset();
        }
    }
}
