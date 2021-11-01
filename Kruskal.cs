using Algorithms;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class Kruskal
        {
                private UFSet uf;
                private Queue<Edge> result = new Queue<Edge>();
                public Queue<Edge> Result
                {
                        get => result;
                }
                public Kruskal(EdgeWeightedGraph graph)
                {
                        //initialization
                        uf = new UFSet(graph.V);
                        var edges = new List<Edge>();
                      
                        //add all the edges to a list
                        for (int i = 0; i != graph.V; ++i)
                        {
                                foreach(var e in graph.Adj(i))
                                {
                                        edges.Add(e);
                                }
                        }
                        edges.Sort();
                        int count = 0;
     
                        foreach(var e in edges)
                        {
                                int v = e.Either();
                                int w = e.Other(e.Either());
                                if(!uf.IsConnected(v,w))
                                {
                                        uf.Merge(v, w);
                                        result.Enqueue(e);
                                        ++count;
                                }
                                if (count == graph.V - 1) break;
                        }
                }
                public static void Main(string[] args)
                {
                        var k = new Kruskal(new EdgeWeightedGraph("weighted1.txt"));
                        double total = 0;
                        foreach(var e in k.result)
                        {
                                Console.WriteLine(e);
                                total += e.Weight;
                        }
                        Console.WriteLine(total);
                }
        }
}
