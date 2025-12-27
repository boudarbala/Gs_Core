using Gs_Core.Graphstream.Graph;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class GraphReplay : SourceBase, Source
{
    public GraphReplay(string id) : base(id)
    {
    }

    public virtual void Replay(Graph graph)
    {
        graph.AttributeKeys().ForEach((key) => SendGraphAttributeAdded(sourceId, key, graph.GetAttribute(key)));
        graph.Nodes().ForEach((node) =>
        {
            string nodeId = node.GetId();
            SendNodeAdded(sourceId, nodeId);
            if (node.GetAttributeCount() > 0)
                node.AttributeKeys().ForEach((key) => SendNodeAttributeAdded(sourceId, nodeId, key, node.GetAttribute(key)));
        });
        graph.Edges().ForEach((edge) =>
        {
            string edgeId = edge.GetId();
            SendEdgeAdded(sourceId, edgeId, edge.GetNode0().GetId(), edge.GetNode1().GetId(), edge.IsDirected());
            if (edge.GetAttributeCount() > 0)
                edge.AttributeKeys().ForEach((key) => SendEdgeAttributeAdded(sourceId, edgeId, key, edge.GetAttribute(key)));
        });
    }
}
