using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphDemo
{
        class EdgeWeightedGraph 
        {
                public int V => adj.Length;
                private List<Edge>[] adj;
                public EdgeWeightedGraph(int v)
                {
                        adj = new List<Edge>[v];
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<Edge>();
                        }
                }
                public EdgeWeightedGraph(string fileName)
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
                        double[] weights = new double[Strings.Length/3];
                        for (int n = 0; n < Strings.Length-2; n+=3)
                        {
                                vs[n / 3] = int.Parse(Strings[n + 2]);
                                ws[n / 3] = int.Parse(Strings[n + 3]);
                                weights[n / 3] = double.Parse(Strings[n + 4]);
                        }
                        int vertices = int.Parse(Strings[0]);
                        int edges = int.Parse(Strings[1]);
                        adj = new List<Edge>[vertices]; //initialize adjacent list
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<Edge>(); //initialize adjacent lists
                        }
                        for (int i = 0; i != edges; ++i)
                        {
                                AddEdge(vs[i],ws[i],weights[i]);
                        }
                }
                public void AddEdge(int v, int w,double weight)
                {
                        var e = new Edge(v, w, weight);
                        adj[v].Add(e);
                        adj[w].Add(e);
                }
                public void AddEdge(Edge e)
                {
                        int v = e.Either();
                        int w = e.Other(v);
                        adj[v].Add(e);
                        adj[w].Add(e);
                }

                public IEnumerable<Edge> Adj(int v)
                {
                        return adj[v];
                }
        }
}
