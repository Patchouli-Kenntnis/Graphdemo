using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
namespace GraphDemo
{
        class SampleClient
        {
                private IGraph graph;
                SampleClient(IGraph graph)
                {
                        this.graph = graph;
                }
                public void DFS(int source)
                {
                        bool[] visited = new bool[graph.V];
                        DFSFrom(source, ref visited);
                }
                void DFSFrom(int source, ref bool[] vs)
                {
                        vs[source] = true;
                        Console.Write(source + " ");
                        foreach (var i in graph.AdjacentVertices(source))
                        {
                                if (!vs[i])
                                {
                                        DFSFrom(i, ref vs);
                                }
                        }
                }
                public void BFS(int source)
                {
                        bool[] visited = new bool[graph.V];
                        Queue<int> queue = new Queue<int>();
                        visited[source] = true;
                        queue.Enqueue(source);
                        while (queue.Count != 0)
                        {
                                int s = queue.Dequeue();
                                Console.Write(s + " ");
                                foreach (var i in graph.AdjacentVertices(s))
                                {
                                        if (!visited[i])
                                        {
                                                visited[i] = true; //HINT:the node must be marked before enqueue
                                                queue.Enqueue(i);
                                        }
                                }
                        }
                }
                static void Main(string[] args)
                {
                        var indexedpq = new IndexedPQ<int>();
                        indexedpq.Add(0.2, 0);
                        indexedpq.Add(1.5, 1);
                        indexedpq.Add(1.7, 2);
                        indexedpq.Add(2.1, 3);
                        indexedpq.Add(2.5, 4);
                        indexedpq.Add(2.7, 5);
                        indexedpq.ChangePriority(1.8, 0);
                        while (!indexedpq.IsEmpty())
                        {
                                Console.WriteLine( indexedpq.Remove());
                        }
                }
        }
}
