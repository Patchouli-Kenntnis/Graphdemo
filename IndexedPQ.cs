using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class IndexedPQ<T>
        {
                private SortedList<double, T> pq = new SortedList<double, T>(Comparer<double>.Create((double x,double y)=>x>y?1:-1)); //We define the compare result as 1 obtaining equal values,thus equal keys will  not cause an exception
                public void Add(double key, T value)
                {
                        pq.Add(key, value);
                }
                public T Remove()
                {
                        T val = pq.Values[0];
                        pq.RemoveAt(0);
                        return val;
                }
                public T Top()
                {
                        return pq[0];
                }
                public void ChangePriority(double newKey, T value)
                {
                        int position = pq.IndexOfValue(value);
                        pq.RemoveAt(position);
                        pq.Add(newKey, value);
                }
                public bool IsEmpty() => pq.Count == 0;
                public bool Contains(T value)
                {
                        return pq.ContainsValue(value);
                }
                public static void Main()
                {
                        var indexedpq = new IndexedPQ<int>();
                        indexedpq.Add( 1.9,0);
                        indexedpq.Add(1.5, 1);
                        indexedpq.Add(1.7, 2);
                        indexedpq.ChangePriority(-1.8, 0);
                        while(!indexedpq.IsEmpty())
                        {
                                Console.WriteLine(indexedpq.Remove());
                        }
                }
        }
}
