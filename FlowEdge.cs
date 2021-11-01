using System;
using System.Collections.Generic;
using System.Text;

namespace GraphDemo
{
        class FlowEdge
        {
                private int from;
                private int to;
                private double capacity;
                private double flow = 0;

                public int From { get => from; set => from = value; }
                public int To { get => to; set => to = value; }
                public double Capacity { get => capacity; set => capacity = value; }
                public double Flow { get => flow; }

                public FlowEdge(int from, int to, double capacity)
                {
                        this.from = from;
                        this.to = to;
                        this.capacity = capacity;
                }

                public int other(int v)
                {
                        if (v == from) return to;
                        if (v == to) return from;
                        throw new ArgumentException("The provided vertex is not on the edge");
                }

                public void addResidualTo(int v, double delta)
                {
                        if (v == to)
                        {
                                flow += delta;
                                return;
                        }
                        if (v == from)
                        {
                                flow -= delta;
                                return;
                        }
                        throw new ArgumentException("The provided vertex  is not on the edge");
                }
                public double residualCapacityTo(int v)
                {
                        if (v == to)
                        {
                                return capacity - flow;
                        }
                        if (v == from)
                        {
                                return flow;
                        }
                        throw new ArgumentException("The provided vertex  is not on the edge");
                }
        }
}
