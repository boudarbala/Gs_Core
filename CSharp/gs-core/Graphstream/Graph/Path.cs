using Java.Util;
using Java.Util.Logging;
using Java.Util.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public class Path : Structure
{
    private static readonly Logger logger = Logger.GetLogger(typeof(Path).GetSimpleName());
    private Node root = null;
    Stack<Edge> edgePath;
    Stack<Node> nodePath;
    public Path()
    {
        edgePath = new Stack<Edge>();
        nodePath = new Stack<Node>();
    }

    public virtual Node GetRoot()
    {
        return this.root;
    }

    public virtual void SetRoot(Node root)
    {
        if (this.root == null)
        {
            this.root = root;
            nodePath.Push(root);
        }
        else
        {
            logger.Warning("Root node is not null - first use the clear method.");
        }
    }

    public virtual bool Contains(Node node)
    {
        return nodePath.Contains(node);
    }

    public virtual bool Contains(Edge edge)
    {
        return edgePath.Contains(edge);
    }

    public virtual bool Empty()
    {
        return nodePath.Empty();
    }

    public virtual int Size()
    {
        return nodePath.Count;
    }

    public virtual Double GetPathWeight(string characteristic)
    {
        double d = 0;
        foreach (Edge l in edgePath)
        {
            d += (Double)l.GetAttribute(characteristic, typeof(Number));
        }

        return d;
    }

    public virtual IList<Edge> GetEdgePath()
    {
        return edgePath;
    }

    public virtual IList<Node> GetNodePath()
    {
        return nodePath;
    }

    public virtual void Add(Node from, Edge edge)
    {
        if (root == null)
        {
            if (from == null)
            {
                throw new ArgumentException("From node cannot be null.");
            }
            else
            {
                SetRoot(from);
            }
        }

        if (from == null)
        {
            from = nodePath.Peek();
        }

        if (!nodePath.Peek().Equals(from))
        {
            throw new ArgumentException("From node must be at the head of the path");
        }

        if (!edge.GetSourceNode().Equals(from) && !edge.GetTargetNode().Equals(from))
        {
            throw new ArgumentException("From node must be part of the edge");
        }

        nodePath.Push(edge.GetOpposite(from));
        edgePath.Push(edge);
    }

    public virtual void Add(Edge edge)
    {
        if (nodePath.IsEmpty())
        {
            Add(null, edge);
        }
        else
        {
            Add(nodePath.Peek(), edge);
        }
    }

    public virtual void Push(Node from, Edge edge)
    {
        Add(from, edge);
    }

    public virtual void Push(Edge edge)
    {
        Add(edge);
    }

    public virtual Edge PopEdge()
    {
        nodePath.Pop();
        return edgePath.Pop();
    }

    public virtual Node PopNode()
    {
        edgePath.Pop();
        return nodePath.Pop();
    }

    public virtual Node PeekNode()
    {
        return nodePath.Peek();
    }

    public virtual Edge PeekEdge()
    {
        return edgePath.Peek();
    }

    public virtual void Clear()
    {
        nodePath.Clear();
        edgePath.Clear();
        root = null;
    }

    public virtual Path GetACopy()
    {
        Path newPath = new Path();
        newPath.root = this.root;
        newPath.edgePath = (Stack<Edge>)edgePath.Clone();
        newPath.nodePath = (Stack<Node>)nodePath.Clone();
        return newPath;
    }

    public virtual void RemoveLoops()
    {
        int n = nodePath.Count;
        for (int i = 0; i < n; i++)
        {
            for (int j = n - 1; j > i; j--)
            {
                if (nodePath[i] == nodePath[j])
                {
                    for (int k = i + 1; k <= j; k++)
                    {
                        nodePath.Remove(i + 1);
                        edgePath.Remove(i);
                    }

                    n -= (j - i);
                    j = i;
                }
            }
        }
    }

    public virtual bool Equals(Path p)
    {
        if (nodePath.Count != p.nodePath.Count)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < nodePath.Count; i++)
            {
                if (nodePath[i] != p.nodePath[i])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public virtual string ToString()
    {
        return nodePath.ToString();
    }

    public virtual int GetNodeCount()
    {
        return nodePath.Count;
    }

    public virtual int GetEdgeCount()
    {
        return edgePath.Count;
    }

    public virtual Stream<Node> Nodes()
    {
        return nodePath.Stream();
    }

    public virtual Stream<Edge> Edges()
    {
        return edgePath.Stream();
    }

    public virtual Collection<T> GetNodeSet<T extends Node>()
    {
        return (Collection<T>)nodePath;
    }

    public virtual Collection<T> GetEdgeSet<T extends Edge>()
    {
        return (Collection<T>)edgePath;
    }
}
