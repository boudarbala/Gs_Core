using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public class PipeAdapter : Pipe
{
    public virtual void AddAttributeSink(AttributeSink listener)
    {
    }

    public virtual void AddElementSink(ElementSink listener)
    {
    }

    public virtual void AddSink(Sink listener)
    {
    }

    public virtual void RemoveAttributeSink(AttributeSink listener)
    {
    }

    public virtual void RemoveElementSink(ElementSink listener)
    {
    }

    public virtual void RemoveSink(Sink listener)
    {
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
    }

    public virtual void ClearAttributeSinks()
    {
    }

    public virtual void ClearElementSinks()
    {
    }

    public virtual void ClearSinks()
    {
    }
}
