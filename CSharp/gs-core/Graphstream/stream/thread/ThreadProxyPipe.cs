using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.Replayable;
using Java.Util;
using Java.Util.Concurrent;
using Java.Util.Concurrent.Locks;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Thread.ElementType;
using static Org.Graphstream.Stream.Thread.EventType;
using static Org.Graphstream.Stream.Thread.AttributeChangeEvent;
using static Org.Graphstream.Stream.Thread.Mode;
using static Org.Graphstream.Stream.Thread.What;
using static Org.Graphstream.Stream.Thread.TimeFormat;
using static Org.Graphstream.Stream.Thread.OutputType;
using static Org.Graphstream.Stream.Thread.OutputPolicy;
using static Org.Graphstream.Stream.Thread.LayoutPolicy;
using static Org.Graphstream.Stream.Thread.Quality;
using static Org.Graphstream.Stream.Thread.Option;
using static Org.Graphstream.Stream.Thread.AttributeType;
using static Org.Graphstream.Stream.Thread.Balise;
using static Org.Graphstream.Stream.Thread.GEXFAttribute;
using static Org.Graphstream.Stream.Thread.METAAttribute;
using static Org.Graphstream.Stream.Thread.GRAPHAttribute;
using static Org.Graphstream.Stream.Thread.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.Thread.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.Thread.NODESAttribute;
using static Org.Graphstream.Stream.Thread.NODEAttribute;
using static Org.Graphstream.Stream.Thread.ATTVALUEAttribute;
using static Org.Graphstream.Stream.Thread.PARENTAttribute;
using static Org.Graphstream.Stream.Thread.EDGESAttribute;
using static Org.Graphstream.Stream.Thread.SPELLAttribute;
using static Org.Graphstream.Stream.Thread.COLORAttribute;
using static Org.Graphstream.Stream.Thread.POSITIONAttribute;
using static Org.Graphstream.Stream.Thread.SIZEAttribute;
using static Org.Graphstream.Stream.Thread.NODESHAPEAttribute;
using static Org.Graphstream.Stream.Thread.EDGEAttribute;
using static Org.Graphstream.Stream.Thread.THICKNESSAttribute;
using static Org.Graphstream.Stream.Thread.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.Thread.IDType;
using static Org.Graphstream.Stream.Thread.ModeType;
using static Org.Graphstream.Stream.Thread.WeightType;
using static Org.Graphstream.Stream.Thread.EdgeType;
using static Org.Graphstream.Stream.Thread.NodeShapeType;
using static Org.Graphstream.Stream.Thread.EdgeShapeType;
using static Org.Graphstream.Stream.Thread.ClassType;
using static Org.Graphstream.Stream.Thread.TimeFormatType;
using static Org.Graphstream.Stream.Thread.GPXAttribute;
using static Org.Graphstream.Stream.Thread.WPTAttribute;
using static Org.Graphstream.Stream.Thread.LINKAttribute;
using static Org.Graphstream.Stream.Thread.EMAILAttribute;
using static Org.Graphstream.Stream.Thread.PTAttribute;
using static Org.Graphstream.Stream.Thread.BOUNDSAttribute;
using static Org.Graphstream.Stream.Thread.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.Thread.FixType;
using static Org.Graphstream.Stream.Thread.GraphAttribute;
using static Org.Graphstream.Stream.Thread.LocatorAttribute;
using static Org.Graphstream.Stream.Thread.NodeAttribute;
using static Org.Graphstream.Stream.Thread.EdgeAttribute;
using static Org.Graphstream.Stream.Thread.DataAttribute;
using static Org.Graphstream.Stream.Thread.PortAttribute;
using static Org.Graphstream.Stream.Thread.EndPointAttribute;
using static Org.Graphstream.Stream.Thread.EndPointType;
using static Org.Graphstream.Stream.Thread.HyperEdgeAttribute;
using static Org.Graphstream.Stream.Thread.KeyAttribute;
using static Org.Graphstream.Stream.Thread.KeyDomain;
using static Org.Graphstream.Stream.Thread.KeyAttrType;
using static Org.Graphstream.Stream.Thread.GraphEvents;

namespace Gs_Core.Graphstream.Stream.Thread;

public class ThreadProxyPipe : SourceBase, ProxyPipe
{
    private static readonly Logger logger = Logger.GetLogger(typeof(ThreadProxyPipe).GetSimpleName());
    protected string id;
    protected string from;
    protected LinkedList<GraphEvents> events;
    protected LinkedList<Object[]> eventsData;
    protected ReentrantLock lock;
    protected Condition notEmpty;
    protected Source input;
    protected bool unregisterWhenPossible = false;
    public ThreadProxyPipe()
    {
        this.events = new LinkedList<GraphEvents>();
        this.eventsData = new LinkedList<Object[]>();
        this.@lock = new ReentrantLock();
        this.notEmpty = this.@lock.NewCondition();
        this.from = "<in>";
        this.input = null;
    }

    public ThreadProxyPipe(Source input) : this(input, null, input is Replayable)
    {
    }

    public ThreadProxyPipe(Source input, bool replay) : this(input, null, replay)
    {
    }

    public ThreadProxyPipe(Source input, Sink initialListener, bool replay) : this()
    {
        if (initialListener != null)
            AddSink(initialListener);
        Init(input, replay);
    }

    public virtual void Init()
    {
        Init(null, false);
    }

    public virtual void Init(Source source)
    {
        Init(source, source is Replayable);
    }

    public virtual void Init(Source source, bool replay)
    {
        @lock.Lock();
        try
        {
            if (this.input != null)
                this.input.RemoveSink(this);
            this.input = source;
            this.events.Clear();
            this.eventsData.Clear();
        }
        finally
        {
            @lock.Unlock();
        }

        if (source != null)
        {
            if (source is Graph)
                this.from = ((Graph)source).GetId();
            this.input.AddSink(this);
            if (replay && source is Replayable)
            {
                Replayable r = (Replayable)source;
                Controller rc = r.GetReplayController();
                rc.AddSink(this);
                rc.Replay();
            }
        }
    }

    public override string ToString()
    {
        string dest = "nil";
        if (attrSinks.Count > 0)
            dest = attrSinks[0].ToString();
        return String.Format("thread-proxy(from %s to %s)", from, dest);
    }

    public virtual void UnregisterFromSource()
    {
        unregisterWhenPossible = true;
    }

    public virtual void Pump()
    {
        GraphEvents e = null;
        object[] data = null;
        do
        {
            @lock.Lock();
            try
            {
                e = events.Poll();
                data = eventsData.Poll();
            }
            finally
            {
                @lock.Unlock();
            }

            if (e != null)
                ProcessMessage(e, data);
        }
        while (e != null);
    }

    public virtual void BlockingPump()
    {
        BlockingPump(0);
    }

    public virtual void BlockingPump(long timeout)
    {
        GraphEvents e;
        object[] data;
        @lock.Lock();
        try
        {
            if (timeout > 0)
                while (events.Count == 0)
                    notEmpty.Await(timeout, TimeUnit.MILLISECONDS);
            else
                while (events.Count == 0)
                    notEmpty.Await();
        }
        finally
        {
            @lock.Unlock();
        }

        do
        {
            @lock.Lock();
            try
            {
                e = events.Poll();
                data = eventsData.Poll();
            }
            finally
            {
                @lock.Unlock();
            }

            if (e != null)
                ProcessMessage(e, data);
        }
        while (e != null);
    }

    public virtual bool HasPostRemaining()
    {
        bool r = true;
        @lock.Lock();
        try
        {
            r = events.Count > 0;
        }
        finally
        {
            @lock.Unlock();
        }

        return r;
    }

    protected enum GraphEvents
    {
        ADD_NODE,
        DEL_NODE,
        ADD_EDGE,
        DEL_EDGE,
        STEP,
        CLEARED,
        ADD_GRAPH_ATTR,
        CHG_GRAPH_ATTR,
        DEL_GRAPH_ATTR,
        ADD_NODE_ATTR,
        CHG_NODE_ATTR,
        DEL_NODE_ATTR,
        ADD_EDGE_ATTR,
        CHG_EDGE_ATTR,
        DEL_EDGE_ATTR
    }

    protected virtual bool MaybeUnregister()
    {
        if (unregisterWhenPossible)
        {
            if (input != null)
                input.RemoveSink(this);
            return true;
        }

        return false;
    }

    protected virtual void Post(GraphEvents e, params object[] data)
    {
        @lock.Lock();
        try
        {
            events.Add(e);
            eventsData.Add(data);
            notEmpty.Signal();
        }
        finally
        {
            @lock.Unlock();
        }
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.ADD_EDGE_ATTR, graphId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.CHG_EDGE_ATTR, graphId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.DEL_EDGE_ATTR, graphId, timeId, edgeId, attribute);
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.ADD_GRAPH_ATTR, graphId, timeId, attribute, value);
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.CHG_GRAPH_ATTR, graphId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.DEL_GRAPH_ATTR, graphId, timeId, attribute);
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.ADD_NODE_ATTR, graphId, timeId, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.CHG_NODE_ATTR, graphId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.DEL_NODE_ATTR, graphId, timeId, nodeId, attribute);
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.ADD_EDGE, graphId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.DEL_EDGE, graphId, timeId, edgeId);
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.CLEARED, graphId, timeId);
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.ADD_NODE, graphId, timeId, nodeId);
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.DEL_NODE, graphId, timeId, nodeId);
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
        if (MaybeUnregister())
            return;
        Post(GraphEvents.STEP, graphId, timeId, step);
    }

    protected virtual void ProcessMessage(GraphEvents e, object[] data)
    {
        string graphId, elementId, attribute;
        long timeId;
        object newValue, oldValue;
        switch (e)
        {
            case ADD_NODE:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                SendNodeAdded(graphId, timeId, elementId);
                break;
            case DEL_NODE:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                SendNodeRemoved(graphId, timeId, elementId);
                break;
            case ADD_EDGE:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                string fromId = (string)data[3];
                string toId = (string)data[4];
                bool directed = (bool)data[5];
                SendEdgeAdded(graphId, timeId, elementId, fromId, toId, directed);
                break;
            case DEL_EDGE:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                SendEdgeRemoved(graphId, timeId, elementId);
                break;
            case STEP:
                graphId = (string)data[0];
                timeId = (long)data[1];
                double step = (Double)data[2];
                SendStepBegins(graphId, timeId, step);
                break;
            case ADD_GRAPH_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                attribute = (string)data[2];
                newValue = data[3];
                SendGraphAttributeAdded(graphId, timeId, attribute, newValue);
                break;
            case CHG_GRAPH_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                attribute = (string)data[2];
                oldValue = data[3];
                newValue = data[4];
                SendGraphAttributeChanged(graphId, timeId, attribute, oldValue, newValue);
                break;
            case DEL_GRAPH_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                attribute = (string)data[2];
                SendGraphAttributeRemoved(graphId, timeId, attribute);
                break;
            case ADD_EDGE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                newValue = data[4];
                SendEdgeAttributeAdded(graphId, timeId, elementId, attribute, newValue);
                break;
            case CHG_EDGE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                oldValue = data[4];
                newValue = data[5];
                SendEdgeAttributeChanged(graphId, timeId, elementId, attribute, oldValue, newValue);
                break;
            case DEL_EDGE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                SendEdgeAttributeRemoved(graphId, timeId, elementId, attribute);
                break;
            case ADD_NODE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                newValue = data[4];
                SendNodeAttributeAdded(graphId, timeId, elementId, attribute, newValue);
                break;
            case CHG_NODE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                oldValue = data[4];
                newValue = data[5];
                SendNodeAttributeChanged(graphId, timeId, elementId, attribute, oldValue, newValue);
                break;
            case DEL_NODE_ATTR:
                graphId = (string)data[0];
                timeId = (long)data[1];
                elementId = (string)data[2];
                attribute = (string)data[3];
                SendNodeAttributeRemoved(graphId, timeId, elementId, attribute);
                break;
            case CLEARED:
                graphId = (string)data[0];
                timeId = (long)data[1];
                SendGraphCleared(graphId, timeId);
                break;
            default:
                logger.Warning(String.Format("Unknown message %s.", e));
                break;
        }
    }
}
