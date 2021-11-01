using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class FordFulkerson
        {
                private bool[] marked;
                private FlowEdge[] edgeTo; //last edge on s->v path
                private double value;

                public double Value { get => value;  }

                public FordFulkerson(FlowNetwork G, int s, int t) //s:start t:terminate
                {
                        value = 0;
                        while (hasAugmentingPath(G, s, t))
                        {
                                // the bottleneck which represents the min flow value along the path
                                double bottleneck = double.PositiveInfinity;
                                //update bottleneck: the min flow on the augmenting path
                                for (int v = t; v != s; v = edgeTo[v].other(v))
                                {
                                        if (edgeTo[v].residualCapacityTo(v) < bottleneck) 
                                        {
                                                bottleneck = (edgeTo[v].residualCapacityTo(v));
                                        }
                                }
                                //add the bottleneck flow to the entire path
                                for (int v = t; v != s; v = edgeTo[v].other(v))
                                {
                                        edgeTo[v].addResidualTo(v, bottleneck);
                                }
                                //the flow is added to the result
                                value += bottleneck;
                        }
                }
                private bool hasAugmentingPath(FlowNetwork G, int s, int t)
                {
                        edgeTo = new FlowEdge[G.V];
                        marked = new bool[G.V];
                        var queue = new Queue<int>();
                        queue.Enqueue(s);
                        marked[s] = true;
                        while(queue.Count != 0)
                        {
                                int v = queue.Dequeue();
                                foreach(var e in G.Adj(v)) //BFS the edges adjacent to v
                                {
                                        int w = e.other(v); //the next vertex on the search route
                                        if(e.residualCapacityTo(w) > 0 && !marked[w]) //have residual flow; hasn't searched yet
                                        {
                                                edgeTo[w] = e;
                                                marked[w] = true;
                                                queue.Enqueue(w);//enqueue the useable route
                                        }
                                }
                        }
                        return marked[t]; // marked[t] == true indicates the final destination is already searched,thus a possible path exists; if marked[t] == false, it's impossible to find a new path
                }
                 public static void Main(string[] args)
                {
                        var g = new FlowNetwork("flowSample.txt");
                        var ff = new FordFulkerson(g, 0, 6);
                        Console.WriteLine(ff.Value);
                }
        }
}
