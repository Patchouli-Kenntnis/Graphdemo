using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class SPFA
        {
                private EdgeWeightedDigraph graph;
                private int source;
                public int Source { get => source; }
                private double[] distTo;
                private DirectedEdge[] edgeTo;

                private bool[] enqueued;
                private int[] enqueueTimes; //if a vertex is enqueued 
                private Queue<int> queue = new Queue<int>();
                public double DistTo(int v)
                {
                        return distTo[v];
                }
                public SPFA(EdgeWeightedDigraph ewd, int s)
                {
                        graph = ewd;
                        source = s;
                        distTo = new double[graph.V];
                        edgeTo = new DirectedEdge[graph.V];
                        enqueued = new bool[graph.V];
                        enqueueTimes = new int[graph.V];
                        for (int i = 0; i != distTo.Length; ++i)
                        {
                                distTo[i] = double.PositiveInfinity; //set initial values as infinity
                        }
                        distTo[Source] = 0; //the distance from source to source must be zero
                        Enqueue(0);
                        while(queue.Count != 0)
                        {
                                int v = Dequeue();
                                foreach(var e in graph.Adj(v))
                                {
                                        Relax(e);
                                }
                        }

                }
                private void Enqueue(int v)//enqueue a vertex ,set relevant marks,judge if negative cycle exists
                {
                        queue.Enqueue(v);
                        enqueued[v] = true;
                        ++enqueueTimes[v];
                        if(enqueueTimes[v] > graph.V)
                        {
                                throw new ArgumentException("Negative cycle exists, shortest path does not exist");
                        }
                }
                private int Dequeue()
                {
                        int v = queue.Dequeue();
                        enqueued[v] = false;
                        return v;
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
                        if(!enqueued[e.To])
                        Enqueue(e.To);
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
                        SPFA spfa = new SPFA(new EdgeWeightedDigraph("NegativeCycle.txt"), 1);
                        Console.WriteLine("------------SPFA------------");
                        for (int i = 1; i != spfa.distTo.Length; ++i)
                        {
                                Console.WriteLine(spfa.distTo[i] + " " + spfa.edgeTo[i].From);
                        }
                }
        }
}
