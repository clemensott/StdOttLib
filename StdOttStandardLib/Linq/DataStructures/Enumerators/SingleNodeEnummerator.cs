using System.Collections;
using System.Collections.Generic;

namespace StdOttStandard.Linq.DataStructures.Enumerators
{
    internal class SingleNodeEnummerator<T> : IEnumerator<T>
    {
        private SingleNode<T> firstNode, currentNode;

        public T Current { get; private set; }

        object IEnumerator.Current => Current;

        public SingleNodeEnummerator(SingleNode<T> firstNode)
        {
            this.firstNode = currentNode = firstNode;
        }

        public void Dispose()
        {
            currentNode = null;
        }

        public bool MoveNext()
        {
            if (currentNode == null)
            {
                Current = default(T);
                return false;
            }

            Current = currentNode.Value;
            return true;
        }

        public void Reset()
        {
            currentNode = firstNode;
        }
    }
}
