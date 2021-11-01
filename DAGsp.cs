using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        //DAG shortest path algorithm
        //Toposort the vertices, then relax them in that order
        class DAGsp 
        {
                private EdgeWeightedDigraph graph;
                private int source;
                public int Source { get => source; }
                private double[] distTo;
                private DirectedEdge[] edgeTo;
                public double DistTo(int v)
                {
                        return distTo[v];
                }
                public DAGsp(EdgeWeightedDigraph ewd, int s)
                {
                        graph = ewd;
                        source = s;
                        distTo = new double[graph.V];
                        edgeTo = new DirectedEdge[graph.V];
                        for (int i = 0; i != distTo.Length; ++i)
                        {
                                distTo[i] = double.PositiveInfinity; //set initial values as infinity
                        }
                        distTo[Source] = 0; //the distance from source to source must be zero
                        var topo = new Toposort(ewd);
                        foreach (var v in topo.Result)
                        {
                                foreach (var e in graph.Adj(v))
                                {
                                        Relax(e);
                                }
                        }
                }
                private void Relax(DirectedEdge e)
                {
                        //Console.WriteLine(e);
                        double newDist = distTo[e.From] + e.Weight;
                        if (distTo[e.To] > newDist)
                        {
                                //Console.WriteLine("Relaxed " + distTo[e.To] + " to " + newDist);
                                distTo[e.To] = newDist; //Update distance if the new distance is shorter
                                edgeTo[e.To] = e;
                        }
                }
                public IEnumerable<DirectedEdge> PathTo(int v)
                {
                        if (v == source) throw new ArgumentException("The destination cannot be the same as the source");
                        Stack<DirectedEdge> stack = new Stack<DirectedEdge>();
                        for (var e = edgeTo[v]; e != null; e = edgeTo[e.From])
                        {
                                stack.Push(e);
                        }
                        return stack;
                }
                public static void Main(string[] args)
                {
                        DAGsp dagsp = new DAGsp(new EdgeWeightedDigraph("DijkstraTestFile.txt"), 0);
                        Console.WriteLine("------------DAGsp------------");
                        for (int i = 1; i != dagsp.distTo.Length; ++i)
                        {
                                Console.WriteLine(dagsp.distTo[i] + " " + dagsp.edgeTo[i].From);
                        }
                }
        }
}
