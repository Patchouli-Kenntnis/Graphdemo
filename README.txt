-----------------------README-----------------------

===DirectedGraph & UndirectedGraph===
the syntax of input file to construct a graph is demonstrated below:

the integers can be separated by newlines or spaces. Empty lines are ok and will be ignored.

the first integer indicates the amount of vertices, and the second one indicates the amount of edges 
which will be added to the graph initially.

Then, each pair of integers " v w " represents an edge from v to w(since it's an undirected graph,
"w v" has an equivalent effect).

------------Sample Input File-----------

5 4

0 1
0 2
2 3
4 2
------------------Rules-----------------------
Parallel edges are not allowed. 
That means if the edge(v,w) already exists, 
reinsert that edge will result in an assertion failure.

Self loops are not allowed.
an edge such like (v,v) will cause an assertion failure.

===EdgeWeightedGraph===

 Build a graph by a txt file:
the fields should be separated by newlines or spaces.
the first field is a integer that describes the amount of vertices.
the second integer describes the amount of edges.
next lines: each line describes an edge.
the first and second fields indicates two vertices of that edge.
the third field indicates its weight.

------------Sample Input File-----------

8 16
4 5 0.35
4 7 0.37
5 7 0.28
0 7 0.16
1 5 0.32
0 4 0.38
2 3 0.17
1 7 0.19
0 2 0.26
1 2 0.36
1 3 0.29
2 7 0.34
6 2 0.40
3 6 0.52
6 0 0.58
6 4 0.93 