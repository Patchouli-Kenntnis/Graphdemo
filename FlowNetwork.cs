using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GraphDemo
{
    class FlowNetwork
    {
        private List<FlowEdge>[] adj;

        public int V { get => adj.Length; }
        public double GetFlow(int from, int to) //NOT RECOMMENDED: NOT SUITABLE FOR PARALLEL EDGES!
        {
            for(int j = 0;j != adj[from].Count;++j)
            {
                if( adj[from][j].To == to)
                {
                    return adj[from][j].Flow;
                }
            }
            throw new ArgumentException("invalid vertices");
        }
        FlowNetwork(int v)
        {
            adj = new List<FlowEdge>[v];
            for (int i = 0; i != v; ++i)
            {
                adj[i] = new List<FlowEdge>();
            }
        }
        public void AddEdge(FlowEdge edge)
        {
            adj[edge.From].Add(edge);
            adj[edge.To].Add(edge);
        }
        public IEnumerable<FlowEdge> Adj(int v)
        {
            return adj[v];
        }
        public FlowNetwork(string fileName)
        {
            //Build a graph by a txt file
            //the fields should be separated by newlines or spaces
            //the first field is a integer that describes the amount of vertices
            //the second integer describes the amount of edges
            //next lines: each line describes an edge
            //the first and second fields indicates two vertices of that edge
            //the third field indicates its capacity
            //see weighted1.txt as example
            string fileContent = File.ReadAllText(fileName);
            string[] Strings = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] vs = new int[Strings.Length / 3];
            int[] ws = new int[Strings.Length / 3];
            double[] caps = new double[Strings.Length / 3];

            for (int n = 0; n < Strings.Length - 2; n += 3)
            {
                vs[n / 3] = int.Parse(Strings[n + 2]);
                ws[n / 3] = int.Parse(Strings[n + 3]);
                caps[n / 3] = double.Parse(Strings[n + 4]);
            }
            int vertices = int.Parse(Strings[0]);
            int edges = int.Parse(Strings[1]);
            adj = new List<FlowEdge>[vertices]; //initialize adjacent list
            for (int i = 0; i != adj.Length; ++i)
            {
                adj[i] = new List<FlowEdge>(); //initialize adjacent lists
            }
            for (int i = 0; i != edges; ++i)
            {
                AddEdge(new FlowEdge(vs[i], ws[i], caps[i]));
            }
        }
    }
}
