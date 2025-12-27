using Java.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph
{


    public class DepthFirstIterator : IEnumerator<Node>
    {
        bool directed;
        Graph graph;
        Node[] parent;
        IEnumerator<Edge>[] iterator;
        int[] depth;
        Node next;
        int maxDepth;
        public DepthFirstIterator(Node startNode, bool directed)
        {
            this.directed = directed;
            graph = startNode.GetGraph();
            int n = graph.GetNodeCount();
            parent = new Node[n];
            iterator = new IEnumerator[n];
            depth = new int[n];
            int s = startNode.GetIndex();
            for (int i = 0; i < n; i++)
                depth[i] = i == s ? 0 : -1;
            next = startNode;
        }

        protected virtual void GotoNext()
        {
            while (next != null)
            {
                int i = next.GetIndex();
                while (iterator[i].HasNext())
                {
                    Node neighbor = iterator[i].Next().GetOpposite(next);
                    int j = neighbor.GetIndex();
                    if (iterator[j] == null)
                    {
                        parent[j] = next;
                        iterator[j] = directed ? neighbor.LeavingEdges().Iterator() : neighbor.EnteringEdges().Iterator();
                        depth[j] = depth[i] + 1;
                        if (depth[j] > maxDepth)
                            maxDepth = depth[j];
                        next = neighbor;
                        return;
                    }
                }

                next = parent[i];
            }
        }

        public DepthFirstIterator(Node startNode) : this(startNode, true)
        {
        }

        public virtual bool HasNext()
        {
            return next != null;
        }

        public virtual Node Next()
        {
            if (next == null)
                throw new NoSuchElementException();
            iterator[next.GetIndex()] = directed ? next.LeavingEdges().Iterator() : next.EnteringEdges().Iterator();
            Node previous = next;
            GotoNext();
            return previous;
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
            return maxDepth;
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


}
