using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class Edge : IComparable
        {
                //IMMUTABLE
                private readonly int V;
                private readonly int W;
                public readonly double Weight;
                public Edge(int v, int w, double weight)
                {
                        V = v;
                        W = w;
                        Weight = weight;
                }
                public int Either()
                {
                        //return a vertex in the edge
                        return V;
                }
                public int Other(int i)
                {
                        //with a vertex on the edge given, return the other vertex
                        if (i == V) return W;
                        if (i == W) return V;
                        throw new ArgumentException("No such vertex on this edge");
                }
                public int CompareTo(Object  other)
                {
                        Edge edge = (Edge)other;
                        return this.Weight.CompareTo(edge.Weight);
                }
                public override string ToString()
                {
                        return V + "-" + W + " " + Weight;
                }


        }
}
