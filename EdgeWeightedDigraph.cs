using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphDemo
{
        class EdgeWeightedDigraph : IGraph
        {
                public int V => adj.Length;
                private List<DirectedEdge>[] adj;
                public EdgeWeightedDigraph(int v)
                {
                        adj = new List<DirectedEdge>[v];
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<DirectedEdge>();
                        }
                }
                public EdgeWeightedDigraph(string fileName)
                {
                        //Build a graph by a txt file
                        //the fields should be separated by newlines or spaces
                        //the first field is a integer that describes the amount of vertices
                        //the second integer describes the amount of edges
                        //next lines: each line describes an edge
                        //the first and second fields indicates two vertices of that edge
                        //the third field indicates its weight
                        //see weighted1.txt as example
                        string fileContent = File.ReadAllText(fileName);
                        string[] Strings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] vs = new int[Strings.Length / 3];
                        int[] ws = new int[Strings.Length / 3];
                        double[] weights = new double[Strings.Length / 3];
                        for (int n = 0; n < Strings.Length - 2; n += 3)
                        {
                                vs[n / 3] = int.Parse(Strings[n + 2]);
                                ws[n / 3] = int.Parse(Strings[n + 3]);
                                weights[n / 3] = double.Parse(Strings[n + 4]);
                        }
                        int vertices = int.Parse(Strings[0]);
                        int edges = int.Parse(Strings[1]);
                        adj = new List<DirectedEdge>[vertices]; //initialize adjacent list
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<DirectedEdge>(); //initialize adjacent lists
                        }
                        for (int i = 0; i != edges; ++i)
                        {
                                AddEdge(vs[i], ws[i], weights[i]);
                        }
                }
                public void AddEdge(int v, int w, double weight)
                {
                        var e = new DirectedEdge(v, w, weight);
                        adj[v].Add(e);
                }
                public void AddEdge(DirectedEdge e)
                {
                        adj[e.From].Add(e);
                }

                public IEnumerable<DirectedEdge> Adj(int v)
                {
                        return adj[v];
                }

                public bool IsEdge(int v, int w)
                {
                       foreach(var e in adj[v])
                        {
                                if (e.To == w) return true;
                        }
                        return false;
                }

                IEnumerable<int> IGraph.AdjacentVertices(int v)
                {
                        Stack<int> vs = new Stack<int>();
                        foreach(var e in Adj(v))
                        {
                                vs.Push(e.To);
                        }
                        return vs;
                }
        }
}
