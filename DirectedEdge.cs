using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{

        class DirectedEdge: IComparable
        {
                //IMMUTABLE
                public readonly int From;
                public readonly int To;
                public readonly double Weight;
                public DirectedEdge(int v, int w, double weight)
                {
                        From = v;
                        To = w;
                        Weight = weight;
                }
                public int CompareTo(Object other)
                {
                        Edge edge = (Edge)other;
                        return this.Weight.CompareTo(edge.Weight);
                }
                public override string ToString()
                {
                        return From + "->" + To + " " + Weight;
                }
        }


}
