using Java.Util;
using Java.Util.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public class BreadthFirstIterator : IEnumerator<Node>
{
    protected bool directed;
    protected Graph graph;
    protected Node[] queue;
    protected int[] depth;
    protected int qHead, qTail;
    public BreadthFirstIterator(Node startNode, bool directed)
    {
        this.directed = directed;
        graph = startNode.GetGraph();
        int n = graph.GetNodeCount();
        queue = new Node[n];
        depth = new int[n];
        int s = startNode.GetIndex();
        for (int i = 0; i < n; i++)
            depth[i] = i == s ? 0 : -1;
        queue[0] = startNode;
        qHead = 0;
        qTail = 1;
    }

    public BreadthFirstIterator(Node startNode) : this(startNode, true)
    {
    }

    public virtual bool HasNext()
    {
        return qHead < qTail;
    }

    public virtual Node Next()
    {
        if (qHead >= qTail)
            throw new NoSuchElementException();
        Node current = queue[qHead++];
        int level = depth[current.GetIndex()] + 1;
        Stream<Edge> edges = directed ? current.LeavingEdges() : current.Edges();
        edges.ForEach((e) =>
        {
            Node node = e.GetOpposite(current);
            int j = node.GetIndex();
            if (depth[j] == -1)
            {
                queue[qTail++] = node;
                depth[j] = level;
            }
        });
        return current;
    }

    public virtual void Remove()
    {
        throw new NotSupportedException("This iterator does not support remove");
    }

    public virtual int GetDepthOf(Node node)
    {
        return depth[node.GetIndex()];
    }

    public virtual int GetDepthMax()
    {
        return depth[queue[qTail - 1].GetIndex()];
    }

    public virtual bool Tabu(Node node)
    {
        return depth[node.GetIndex()] != -1;
    }

    public virtual bool IsDirected()
    {
        return directed;
    }
}
