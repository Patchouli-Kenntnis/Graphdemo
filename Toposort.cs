using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        //The class is IMMUTABLE. 
        //If you change the graph which is used as a parameter in the constructor,
        //Any field will not be changed.
        class Toposort
        {

                public bool isDAG = true;
                private Stack<int> result = new Stack<int>();
                private int[] visited; //0: not processed
                public Stack<int> Result
                {
                        get
                        {
                                if(isDAG)
                                {
                                        return result;
                                }
                                else
                                {
                                        throw new InvalidOperationException("Not a DAG, cannot do Toposort");
                                }
                        }
                }

                //1: processed
                //-1:processing
                public Toposort(IGraph g)
                {
                        visited = new int[g.V];
                        for (int i = 0; i != g.V; ++i)
                        {
                                dfs(g, i);
                                if (!isDAG) break;
                        }
                }
                private void dfs(IGraph g, int v)
                {
                        if (visited[v] == 1) return; // processed
                        if (visited[v] == -1) // cycle detected
                        {
                                isDAG = false;
                                return;
                        } 
                        visited[v] = -1;
                        foreach (var i in g.AdjacentVertices(v))
                        {
                                dfs(g, i);
                        }
                        result.Push(v);
                        visited[v] = 1;
                }
                public static void Main(string[] args)
                {
                        DirectedGraph directedGraph = new DirectedGraph(args[0]);
                        //cycle.txt : testing cycles
                        //digraph1.txt: testing regular toposort
                        var topo = new Toposort(directedGraph);
                        while(topo.result.Count!=0)
                        {
                                Console.Write(topo.result.Pop() + " ");
                        }
                }
        }
}
