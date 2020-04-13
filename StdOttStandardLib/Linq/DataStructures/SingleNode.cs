using System;
using System.Collections.Generic;
using System.Text;

namespace StdOttStandard.Linq.DataStructures
{
    class SingleNode<T>
    {
        public T Value { get; set; }

        public SingleNode<T> Next { get; internal set; }

        public SingleNode(T value)
        {
            Value = value;
        }

        public SingleNode(T value, SingleNode<T> next) : this(value)
        {
            Next = next;
        }
    }
}
