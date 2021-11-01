using System;
using System.Collections.Generic;
using System.Text;
using Algorithms;
namespace GraphDemo
{
        class Prim
        {
                //CAN ONLY BE USED ON CONNECTED GRAPHS
                private List<Edge> result = new List<Edge>();
                private bool[] marked; //vertices already added to the MST
                private EdgeWeightedGraph graph;
                public List<Edge> Result { get => result; }
                private PriorityQueue<Edge> pq = new PriorityQueue<Edge>((Edge x, Edge y) => x.CompareTo(y));

                public Prim(EdgeWeightedGraph graph, bool isLazy)
                {
                        if (isLazy) LazyPrim(graph);
                        else EagerPrim(graph);
                }
                private void Raid(int v)
                {
                        marked[v] = true;
                        foreach(var e in graph.Adj(v))
                        {
                                int w = e.Other(v);
                                if (!marked[w])
                                pq.Add(e);
                        }
                }
                private void LazyPrim(EdgeWeightedGraph g)
                {
                        this.graph = g;
                        marked = new bool[graph.V];
                        marked[0] = true;
                        int count = 0;
                        Raid(0);
                        while (count < graph.V - 1)
                        {
                                var e = pq.Remove();
                                result.Add(e);
                                int v = e.Either(), w = e.Other(v);
                                if (marked[v] && marked[w]) continue;
                                if(!marked[v]) Raid(v);
                                if (!marked[w]) Raid(w);
                                ++count;
                        }
                }
                private void EagerPrim(EdgeWeightedGraph graph)
                {

                }
                public static void Main(string[] args)
                {
                        var k = new Prim(new EdgeWeightedGraph("weighted1.txt"), true);
                        double total = 0;
                        foreach (var e in k.result)
                        {
                                Console.WriteLine(e);
                                total += e.Weight;
                        }
                        Console.WriteLine(total);
                }
        }
}
