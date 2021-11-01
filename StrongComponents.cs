using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class StrongComponents
        {
                //IMMUTABLE!
                //If u do any change to the graph, RECONSTRUCT THE INSTANCE!
                //This algorithm is based on Kosaraju algorithm, with a time complexity of O(E+V)
                private int[] scc;
                private Stack<int> firstDFSResult = new Stack<int>();
                private int count;
                private bool[] visited;

                public int[] ComponentId { get => scc; }
                StrongComponents(IGraph graph)
                {
                        //initialize scc array: -1 = unmarked vertexes
                        scc = new int[graph.V];
                        for (int i = 0; i != graph.V; ++i)
                        {
                                scc[i] = -1;
                        }
                        //initialize visited array
                        visited = new bool[graph.V];
                        //create reversed graph
                        DirectedGraph rev = new DirectedGraph(graph.V);
                        for (int i = 0; i != graph.V; ++i)
                        {
                                foreach (int j in graph.AdjacentVertices(i))
                                {
                                        rev.AddEdge(j, i);
                                }
                        }
                        //first DFS: DFS postorder the reversed graph
                        for (int i = 0; i != graph.V; ++i)
                        {
                                FirstDFS(rev, i);
                        }
                        //second DFS: pop a vertex from the stack,
                        //if the vertex does not have a component id,
                        //DFS the ORIGINAL graph from there and mark their component id,
                        //then increment current component id"count"
                        while (firstDFSResult.Count != 0)
                        {
                                int v = firstDFSResult.Pop();
                                if (scc[v] == -1)
                                {
                                        SecondDFS(graph, v);
                                        ++count;
                                }
                        }
                }
                private void FirstDFS(IGraph g, int v)
                {
                        if (visited[v] == true) return; // processed
                        visited[v] = true;
                        foreach (var i in g.AdjacentVertices(v))
                        {
                                FirstDFS(g, i);
                        }
                        firstDFSResult.Push(v);
                }
                void SecondDFS(IGraph g, int v)
                {
                        if (scc[v] != -1) return;
                        scc[v] = count;
                        foreach (var i in g.AdjacentVertices(v))
                        {
                                SecondDFS(g, i);
                        }
                }
                public static void Main(string[] args)
                {
                        DirectedGraph digraph = new DirectedGraph("SCC.txt");
                        StrongComponents strongComponents = new StrongComponents(digraph);
                        for (int i = 0; i != strongComponents.ComponentId.Length; ++i)
                        {
                                Console.Write(strongComponents.ComponentId[i] + " ");
                        }
                }
        }

}
