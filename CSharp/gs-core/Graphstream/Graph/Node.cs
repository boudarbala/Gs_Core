using Java.Util;
using Java.Util.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface Node : Element, Iterable<Edge>
{
    Graph GetGraph();
    int GetDegree();
    int GetOutDegree();
    int GetInDegree();
    Edge GetEdgeToward(string id);
    Edge GetEdgeFrom(string id);
    Edge GetEdgeBetween(string id);
    Stream<Node> NeighborNodes()
    {
        return Edges().Map((edge) =>
        {
            return edge.GetOpposite(this);
        });
    }

    Edge GetEdge(int i);
    Edge GetEnteringEdge(int i);
    Edge GetLeavingEdge(int i);
    IEnumerator<Node> GetBreadthFirstIterator();
    IEnumerator<Node> GetBreadthFirstIterator(bool directed);
    IEnumerator<Node> GetDepthFirstIterator();
    IEnumerator<Node> GetDepthFirstIterator(bool directed);
    Stream<Edge> Edges();
    Stream<Edge> LeavingEdges()
    {
        return Edges().Filter((e) => (e.GetSourceNode() == this));
    }

    Stream<Edge> EnteringEdges()
    {
        return Edges().Filter((e) => (e.GetTargetNode() == this));
    }

    IEnumerator<Edge> Iterator()
    {
        return Edges().Iterator();
    }

    string ToString();
    bool HasEdgeToward(string id)
    {
        return GetEdgeToward(id) != null;
    }

    bool HasEdgeToward(Node node)
    {
        return GetEdgeToward(node) != null;
    }

    bool HasEdgeToward(int index)
    {
        return GetEdgeToward(index) != null;
    }

    bool HasEdgeFrom(string id)
    {
        return GetEdgeFrom(id) != null;
    }

    bool HasEdgeFrom(Node node)
    {
        return GetEdgeFrom(node) != null;
    }

    bool HasEdgeFrom(int index)
    {
        return GetEdgeFrom(index) != null;
    }

    bool HasEdgeBetween(string id)
    {
        return GetEdgeBetween(id) != null;
    }

    bool HasEdgeBetween(Node node)
    {
        return GetEdgeBetween(node) != null;
    }

    bool HasEdgeBetween(int index)
    {
        return GetEdgeBetween(index) != null;
    }

    Edge GetEdgeToward(Node node);
    Edge GetEdgeToward(int index);
    Edge GetEdgeFrom(Node node);
    Edge GetEdgeFrom(int index);
    Edge GetEdgeBetween(Node node);
    Edge GetEdgeBetween(int index);
}
