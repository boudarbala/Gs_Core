using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class PipeBase : SourceBase, Pipe
{
    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        SendEdgeAttributeAdded(graphId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        SendEdgeAttributeChanged(graphId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        SendEdgeAttributeRemoved(graphId, timeId, edgeId, attribute);
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        SendGraphAttributeAdded(graphId, timeId, attribute, value);
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        SendGraphAttributeChanged(graphId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        SendGraphAttributeRemoved(graphId, timeId, attribute);
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        SendNodeAttributeAdded(graphId, timeId, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        SendNodeAttributeChanged(graphId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        SendNodeAttributeRemoved(graphId, timeId, nodeId, attribute);
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        SendEdgeAdded(graphId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
        SendEdgeRemoved(graphId, timeId, edgeId);
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
        SendGraphCleared(graphId, timeId);
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        SendNodeAdded(graphId, timeId, nodeId);
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        SendNodeRemoved(graphId, timeId, nodeId);
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
        SendStepBegins(graphId, timeId, step);
    }
}
