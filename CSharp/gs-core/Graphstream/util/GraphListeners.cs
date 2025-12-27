using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;

namespace Gs_Core.Graphstream.Util;

public class GraphListeners : SourceBase, Pipe
{
    SinkTime sinkTime;
    bool passYourWay, passYourWayAE;
    string dnSourceId;
    long dnTimeId;
    Graph g;
    public GraphListeners(Graph g) : base(g.GetId())
    {
        this.sinkTime = new SinkTime();
        this.sourceTime.SetSinkTime(sinkTime);
        this.passYourWay = false;
        this.passYourWayAE = false;
        this.dnSourceId = null;
        this.dnTimeId = Long.MIN_VALUE;
        this.g = g;
    }

    public virtual long NewEvent()
    {
        return sourceTime.NewEvent();
    }

    public virtual void SendAttributeChangedEvent(string eltId, ElementType eltType, string attribute, AttributeChangeEvent @event, object oldValue, object newValue)
    {
        if (passYourWay || attribute.CharAt(0) == '.')
            return;
        SendAttributeChangedEvent(sourceId, NewEvent(), eltId, eltType, attribute, @event, oldValue, newValue);
    }

    public virtual void SendNodeAdded(string nodeId)
    {
        if (passYourWay)
            return;
        SendNodeAdded(sourceId, NewEvent(), nodeId);
    }

    public virtual void SendNodeRemoved(string nodeId)
    {
        if (dnSourceId != null)
        {
            SendNodeRemoved(dnSourceId, dnTimeId, nodeId);
        }
        else
        {
            SendNodeRemoved(sourceId, NewEvent(), nodeId);
        }
    }

    public virtual void SendEdgeAdded(string edgeId, string source, string target, bool directed)
    {
        if (passYourWayAE)
            return;
        SendEdgeAdded(sourceId, NewEvent(), edgeId, source, target, directed);
    }

    public virtual void SendEdgeRemoved(string edgeId)
    {
        if (passYourWay)
            return;
        SendEdgeRemoved(sourceId, NewEvent(), edgeId);
    }

    public virtual void SendGraphCleared()
    {
        if (passYourWay)
            return;
        SendGraphCleared(sourceId, NewEvent());
    }

    public virtual void SendStepBegins(double step)
    {
        if (passYourWay)
            return;
        SendStepBegins(sourceId, NewEvent(), step);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Edge edge = g.GetEdge(edgeId);
            if (edge != null)
            {
                passYourWay = true;
                try
                {
                    edge.SetAttribute(attribute, value);
                }
                finally
                {
                    passYourWay = false;
                }

                SendEdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
            }
        }
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Edge edge = g.GetEdge(edgeId);
            if (edge != null)
            {
                passYourWay = true;
                if (oldValue == null)
                    oldValue = edge.GetAttribute(attribute);
                try
                {
                    edge.SetAttribute(attribute, newValue);
                }
                finally
                {
                    passYourWay = false;
                }

                SendEdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
            }
        }
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Edge edge = g.GetEdge(edgeId);
            if (edge != null)
            {
                SendEdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
                passYourWay = true;
                try
                {
                    edge.RemoveAttribute(attribute);
                }
                finally
                {
                    passYourWay = false;
                }
            }
        }
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            passYourWay = true;
            try
            {
                g.SetAttribute(attribute, value);
            }
            finally
            {
                passYourWay = false;
            }

            SendGraphAttributeAdded(sourceId, timeId, attribute, value);
        }
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            passYourWay = true;
            if (oldValue == null)
                oldValue = g.GetAttribute(attribute);
            try
            {
                g.SetAttribute(attribute, newValue);
            }
            finally
            {
                passYourWay = false;
            }

            SendGraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
        }
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            SendGraphAttributeRemoved(sourceId, timeId, attribute);
            passYourWay = true;
            try
            {
                g.RemoveAttribute(attribute);
            }
            finally
            {
                passYourWay = false;
            }
        }
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Node node = g.GetNode(nodeId);
            if (node != null)
            {
                passYourWay = true;
                try
                {
                    node.SetAttribute(attribute, value);
                }
                finally
                {
                    passYourWay = false;
                }

                SendNodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
            }
        }
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Node node = g.GetNode(nodeId);
            if (node != null)
            {
                passYourWay = true;
                if (oldValue == null)
                    oldValue = node.GetAttribute(attribute);
                try
                {
                    node.SetAttribute(attribute, newValue);
                }
                finally
                {
                    passYourWay = false;
                }

                SendNodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
            }
        }
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            Node node = g.GetNode(nodeId);
            if (node != null)
            {
                SendNodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
                passYourWay = true;
                try
                {
                    node.RemoveAttribute(attribute);
                }
                finally
                {
                    passYourWay = false;
                }
            }
        }
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            passYourWayAE = true;
            try
            {
                g.AddEdge(edgeId, fromNodeId, toNodeId, directed);
            }
            finally
            {
                passYourWayAE = false;
            }

            SendEdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
        }
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            SendEdgeRemoved(sourceId, timeId, edgeId);
            passYourWay = true;
            try
            {
                g.RemoveEdge(edgeId);
            }
            finally
            {
                passYourWay = false;
            }
        }
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            SendGraphCleared(sourceId, timeId);
            passYourWay = true;
            try
            {
                g.Clear();
            }
            finally
            {
                passYourWay = false;
            }
        }
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            passYourWay = true;
            try
            {
                g.AddNode(nodeId);
            }
            finally
            {
                passYourWay = false;
            }

            SendNodeAdded(sourceId, timeId, nodeId);
        }
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            dnSourceId = sourceId;
            dnTimeId = timeId;
            try
            {
                g.RemoveNode(nodeId);
            }
            finally
            {
                dnSourceId = null;
                dnTimeId = Long.MIN_VALUE;
            }
        }
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        if (sinkTime.IsNewEvent(sourceId, timeId))
        {
            passYourWay = true;
            try
            {
                g.StepBegins(step);
            }
            finally
            {
                passYourWay = false;
            }

            SendStepBegins(sourceId, timeId, step);
        }
    }

    public override string ToString()
    {
        return String.Format("GraphListeners of %s.%s", g.GetType().GetSimpleName(), g.GetId());
    }
}
