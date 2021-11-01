using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GraphDemo
{
        class UndirectedGraph : IGraph
        {
                //Implemented by adjacent list
                int vertices = 0;
                private List<int>[] adj;
                public int V { get => vertices; }
                public UndirectedGraph(int v) //vertices
                {
                        vertices = v;
                        adj = new List<int>[vertices];
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<int>();
                        }
                }
                public UndirectedGraph(string fileName)
                {
                        string fileContent = File.ReadAllText(fileName);
                        string[] integerStrings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        int[] integers = new int[integerStrings.Length];
                        for (int n = 0; n < integerStrings.Length; n++)
                                integers[n] = int.Parse(integerStrings[n]);
                        vertices = integers[0];
                        adj = new List<int>[vertices]; //initialize adjacent list
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                adj[i] = new List<int>(); //initialize adjacent lists
                        }
                        int edges = integers[1];
                        for (int i = 0; i != edges; ++i)
                        {
                                AddEdge(integers[2 * i + 2], integers[2 * i + 3]);
                        }
                }
                public void AddEdge(int v, int w)
                {
                        Debug.Assert(v != w, "Self loop is not allowed");// No self-loop
                        Debug.Assert(!IsEdge(v, w), "Parallel edges are not allowed");//No parallel edges
                        adj[v].Add(w);
                        adj[w].Add(v);
                }
                public IEnumerable<int> AdjacentVertices(int v) //adjacent vertices to v
                {
                        return adj[v];
                }
                public void ShowAllEdges() //TODO:debug
                {
                        for (int i = 0; i != adj.Length; ++i)
                        {
                                foreach (var j in adj[i])
                                {
                                        if (i < j)
                                        {
                                                Console.WriteLine(i + " " + j);
                                        }
                                }
                        }
                }
                public bool IsEdge(int v, int w)
                {
                        if (adj[v].Count < adj[w].Count)
                        {
                                foreach (var i in adj[v])
                                {
                                        if (i == w) return true;
                                }
                                return false;
                        }
                        else
                        {
                                foreach (var i in adj[w])
                                {
                                        if (i == v) return true;
                                }
                                return false;
                        }
                }



        }
}
