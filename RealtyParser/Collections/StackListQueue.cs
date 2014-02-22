using System.Collections.Generic;

namespace RealtyParser.Collections
{
    public class StackListQueue<T> : List<T>
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
        public void Add(IEnumerable<T> value)
        {
            AddRange(value);
        }

        public T Pop()
        {
            int index = Count;
            T value = this[--index];
            RemoveAt(index);
            return value;
        }

        public static StackListQueue<T> IntersectSorted<T>(StackListQueue<T> array1, StackListQueue<T> array2, IComparer<T> comparer)
        {
            int i = 0;
            int j = 0;
            var stackListQueue = new StackListQueue<T>();
            while (i < array1.Count && j < array2.Count)
            {
                int value = comparer.Compare(array1[i], array2[j]);
                if (value == 0)
                {
                    stackListQueue.Add(array1[i]);
                    i++;
                    j++;
                }
                else if (value < 0) i++;
                else j++;
            }
            return stackListQueue;
        }

        public static StackListQueue<T> DistinctSorted<T>(StackListQueue<T> array1, StackListQueue<T> array2, IComparer<T> comparer)
        {
            int i = 0;
            int j = 0;
            var stackListQueue = new StackListQueue<T>();
            while (i < array1.Count && j < array2.Count)
            {
                int value = comparer.Compare(array1[i], array2[j]);
                if (value != 0)
                {
                    stackListQueue.Add(value < 0 ? array1[i++] : array2[j++]);
                }
                else
                {
                    stackListQueue.Add(array1[i++]);
                    j++;
                }
            }
            if (i < array1.Count)
                stackListQueue.AddRange(array1.GetRange(i, array1.Count - i));
            if (j < array2.Count)
                stackListQueue.AddRange(array2.GetRange(j, array2.Count - j));
            return stackListQueue;
        }
    }
}