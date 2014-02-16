using System.Collections.Generic;

namespace RealtyParser
{
    public static class Array<T>
    {
        public static T[] IntersectSorted(T[] array1, T[] array2, IComparer<T> comparer)
        {
            int i = 0;
            int j = 0;
            var list = new List<T>();
            while (i < array1.Length && j < array2.Length)
            {
                int value = comparer.Compare(array1[i], array2[j]);
                if (value == 0)
                {
                    list.Add(array1[i]);
                    i++;
                    j++;
                }
                else if (value < 0) i++;
                else j++;
            }
            return list.ToArray();
        }
    }
}