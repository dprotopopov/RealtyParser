using System.Collections.Generic;

namespace RealtyParser
{
    public class QueueStack<T> : List<T>
    {
        public void Enqueue(T value)
        {
            Add(value);
        }

        public T Dequeue()
        {
            T value = this[0];
            RemoveAt(0);
            return value;
        }

        public void Push(T value)
        {
            Add(value);
        }

        public T Pop()
        {
            int index = Count;
            T value = this[--index];
            RemoveAt(index);
            return value;
        }
    }
}