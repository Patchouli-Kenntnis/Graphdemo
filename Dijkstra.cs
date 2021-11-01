using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace GraphDemo
{
        class Dijkstra 
        {
                private EdgeWeightedDigraph graph;
                private int source;
                public int Source { get => source; }

                private double[] distTo;
                private DirectedEdge[] edgeTo;
                private IndexedPQ<int> pq = new IndexedPQ<int>(); //priority is "the current distance from source"
                public double DistTo(int v)
                {
                        return distTo[v];
                }
                public Dijkstra(EdgeWeightedDigraph ewd, int s)
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
                        pq.Add(0.0, source); //add source vertex to the pq
                        while (!pq.IsEmpty())
                        {
                                int kebab = pq.Remove(); //remove the vertex from the pq which is "closest" to source
                                foreach (var e in graph.Adj(kebab))
                                {
                                        Relax(e);//relax all the edges from that vertex
                                }
                        }
                }
                public void DAGsp(EdgeWeightedDigraph ewd,int s)
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
                        foreach(var v in topo.Result)
                        {
                                foreach(var e in graph.Adj(v))
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
                        if (pq.Contains(e.To))
                        {
                                pq.ChangePriority(newDist, e.To); //if the vertex already exists in the pq, update its priority; or add it to the pq
                        }
                        else
                        {
                                pq.Add(newDist, e.To);
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
                        Dijkstra dijkstra = new Dijkstra(new EdgeWeightedDigraph("DijkstraTestFile.txt"), 0);
                        Console.WriteLine("------------dijkstra------------");
                        for (int i = 1; i != dijkstra.distTo.Length; ++i)
                        {
                                Console.WriteLine(dijkstra.distTo[i] + " " + dijkstra.edgeTo[i].From);
                        }
                }
        }
}
