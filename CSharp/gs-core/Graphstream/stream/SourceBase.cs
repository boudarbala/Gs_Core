using Java.Util;
using Gs_Core.Graphstream.Graph.Implementations.AbstractElement;
using Gs_Core.Graphstream.Stream.Sync;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.ElementType;

namespace Gs_Core.Graphstream.Stream;

public abstract class SourceBase : Source
{
    public enum ElementType
    {
        NODE,
        EDGE,
        GRAPH
    }

    protected List<AttributeSink> attrSinks = new List<AttributeSink>();
    protected List<ElementSink> eltsSinks = new List<ElementSink>();
    protected LinkedList<GraphEvent> eventQueue = new LinkedList<GraphEvent>();
    protected bool eventProcessing = false;
    protected string sourceId;
    protected SourceTime sourceTime;
    protected SourceBase() : this(String.Format("sourceOnThread#%d_%d", Thread.CurrentThread().GetId(), System.CurrentTimeMillis() + ((int)(Math.Random() * 1000))))
    {
    }

    protected SourceBase(string sourceId)
    {
        this.sourceId = sourceId;
        this.sourceTime = new SourceTime(sourceId);
    }

    public virtual Iterable<AttributeSink> AttributeSinks()
    {
        return attrSinks;
    }

    public virtual Iterable<ElementSink> ElementSinks()
    {
        return eltsSinks;
    }

    public virtual void AddSink(Sink sink)
    {
        AddAttributeSink(sink);
        AddElementSink(sink);
    }

    public virtual void AddAttributeSink(AttributeSink sink)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            attrSinks.Add(sink);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new AddToListEvent<AttributeSink>(attrSinks, sink));
        }
    }

    public virtual void AddElementSink(ElementSink sink)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            eltsSinks.Add(sink);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new AddToListEvent<ElementSink>(eltsSinks, sink));
        }
    }

    public virtual void ClearSinks()
    {
        ClearElementSinks();
        ClearAttributeSinks();
    }

    public virtual void ClearElementSinks()
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            eltsSinks.Clear();
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new ClearListEvent<ElementSink>(eltsSinks));
        }
    }

    public virtual void ClearAttributeSinks()
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            attrSinks.Clear();
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new ClearListEvent<AttributeSink>(attrSinks));
        }
    }

    public virtual void RemoveSink(Sink sink)
    {
        RemoveAttributeSink(sink);
        RemoveElementSink(sink);
    }

    public virtual void RemoveAttributeSink(AttributeSink sink)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            attrSinks.Remove(sink);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new RemoveFromListEvent<AttributeSink>(attrSinks, sink));
        }
    }

    public virtual void RemoveElementSink(ElementSink sink)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            eltsSinks.Remove(sink);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new RemoveFromListEvent<ElementSink>(eltsSinks, sink));
        }
    }

    public virtual void SendGraphCleared(string sourceId)
    {
        SendGraphCleared(sourceId, sourceTime.NewEvent());
    }

    public virtual void SendGraphCleared(string sourceId, long timeId)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].GraphCleared(sourceId, timeId);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new BeforeGraphClearEvent(sourceId, timeId));
        }
    }

    public virtual void SendStepBegins(string sourceId, double step)
    {
        SendStepBegins(sourceId, sourceTime.NewEvent(), step);
    }

    public virtual void SendStepBegins(string sourceId, long timeId, double step)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].StepBegins(sourceId, timeId, step);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new StepBeginsEvent(sourceId, timeId, step));
        }
    }

    public virtual void SendNodeAdded(string sourceId, string nodeId)
    {
        SendNodeAdded(sourceId, sourceTime.NewEvent(), nodeId);
    }

    public virtual void SendNodeAdded(string sourceId, long timeId, string nodeId)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].NodeAdded(sourceId, timeId, nodeId);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new AfterNodeAddEvent(sourceId, timeId, nodeId));
        }
    }

    public virtual void SendNodeRemoved(string sourceId, string nodeId)
    {
        SendNodeRemoved(sourceId, sourceTime.NewEvent(), nodeId);
    }

    public virtual void SendNodeRemoved(string sourceId, long timeId, string nodeId)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].NodeRemoved(sourceId, timeId, nodeId);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new BeforeNodeRemoveEvent(sourceId, timeId, nodeId));
        }
    }

    public virtual void SendEdgeAdded(string sourceId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        SendEdgeAdded(sourceId, sourceTime.NewEvent(), edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void SendEdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new AfterEdgeAddEvent(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed));
        }
    }

    public virtual void SendEdgeRemoved(string sourceId, string edgeId)
    {
        SendEdgeRemoved(sourceId, sourceTime.NewEvent(), edgeId);
    }

    public virtual void SendEdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].EdgeRemoved(sourceId, timeId, edgeId);
            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new BeforeEdgeRemoveEvent(sourceId, timeId, edgeId));
        }
    }

    public virtual void SendEdgeAttributeAdded(string sourceId, string edgeId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendEdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, timeId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendEdgeAttributeChanged(string sourceId, string edgeId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendEdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, timeId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendEdgeAttributeRemoved(string sourceId, string edgeId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendEdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, timeId, edgeId, ElementType.EDGE, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendGraphAttributeAdded(string sourceId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendGraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, timeId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendGraphAttributeChanged(string sourceId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendGraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, timeId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendGraphAttributeRemoved(string sourceId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendGraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, timeId, null, ElementType.GRAPH, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendNodeAttributeAdded(string sourceId, string nodeId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendNodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        SendAttributeChangedEvent(sourceId, timeId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.ADD, null, value);
    }

    public virtual void SendNodeAttributeChanged(string sourceId, string nodeId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendNodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, timeId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.CHANGE, oldValue, newValue);
    }

    public virtual void SendNodeAttributeRemoved(string sourceId, string nodeId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendNodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        SendAttributeChangedEvent(sourceId, timeId, nodeId, ElementType.NODE, attribute, AttributeChangeEvent.REMOVE, null, null);
    }

    public virtual void SendAttributeChangedEvent(string sourceId, string eltId, ElementType eltType, string attribute, AttributeChangeEvent @event, object oldValue, object newValue)
    {
        SendAttributeChangedEvent(sourceId, sourceTime.NewEvent(), eltId, eltType, attribute, @event, oldValue, newValue);
    }

    public virtual void SendAttributeChangedEvent(string sourceId, long timeId, string eltId, ElementType eltType, string attribute, AttributeChangeEvent @event, object oldValue, object newValue)
    {
        if (!eventProcessing)
        {
            eventProcessing = true;
            ManageEvents();
            if (@event == AttributeChangeEvent.ADD)
            {
                if (eltType == ElementType.NODE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].NodeAttributeAdded(sourceId, timeId, eltId, attribute, newValue);
                }
                else if (eltType == ElementType.EDGE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].EdgeAttributeAdded(sourceId, timeId, eltId, attribute, newValue);
                }
                else
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].GraphAttributeAdded(sourceId, timeId, attribute, newValue);
                }
            }
            else if (@event == AttributeChangeEvent.REMOVE)
            {
                if (eltType == ElementType.NODE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].NodeAttributeRemoved(sourceId, timeId, eltId, attribute);
                }
                else if (eltType == ElementType.EDGE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].EdgeAttributeRemoved(sourceId, timeId, eltId, attribute);
                }
                else
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].GraphAttributeRemoved(sourceId, timeId, attribute);
                }
            }
            else
            {
                if (eltType == ElementType.NODE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].NodeAttributeChanged(sourceId, timeId, eltId, attribute, oldValue, newValue);
                }
                else if (eltType == ElementType.EDGE)
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].EdgeAttributeChanged(sourceId, timeId, eltId, attribute, oldValue, newValue);
                }
                else
                {
                    for (int i = 0; i < attrSinks.Count; i++)
                        attrSinks[i].GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
                }
            }

            ManageEvents();
            eventProcessing = false;
        }
        else
        {
            eventQueue.Add(new AttributeChangedEvent(sourceId, timeId, eltId, eltType, attribute, @event, oldValue, newValue));
        }
    }

    protected virtual void ManageEvents()
    {
        if (eventProcessing)
        {
            while (!eventQueue.IsEmpty())
                eventQueue.Remove().Trigger();
        }
    }

    abstract class GraphEvent
    {
        string sourceId;
        long timeId;
        GraphEvent(string sourceId, long timeId)
        {
            this.sourceId = sourceId;
            this.timeId = timeId;
        }

        abstract void Trigger();
    }

    class AfterEdgeAddEvent : GraphEvent
    {
        string edgeId;
        string fromNodeId;
        string toNodeId;
        bool directed;
        AfterEdgeAddEvent(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed) : base(sourceId, timeId)
        {
            this.edgeId = edgeId;
            this.fromNodeId = fromNodeId;
            this.toNodeId = toNodeId;
            this.directed = directed;
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
        }
    }

    class BeforeEdgeRemoveEvent : GraphEvent
    {
        string edgeId;
        BeforeEdgeRemoveEvent(string sourceId, long timeId, string edgeId) : base(sourceId, timeId)
        {
            this.edgeId = edgeId;
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].EdgeRemoved(sourceId, timeId, edgeId);
        }
    }

    class AfterNodeAddEvent : GraphEvent
    {
        string nodeId;
        AfterNodeAddEvent(string sourceId, long timeId, string nodeId) : base(sourceId, timeId)
        {
            this.nodeId = nodeId;
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].NodeAdded(sourceId, timeId, nodeId);
        }
    }

    class BeforeNodeRemoveEvent : GraphEvent
    {
        string nodeId;
        BeforeNodeRemoveEvent(string sourceId, long timeId, string nodeId) : base(sourceId, timeId)
        {
            this.nodeId = nodeId;
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].NodeRemoved(sourceId, timeId, nodeId);
        }
    }

    class BeforeGraphClearEvent : GraphEvent
    {
        BeforeGraphClearEvent(string sourceId, long timeId) : base(sourceId, timeId)
        {
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].GraphCleared(sourceId, timeId);
        }
    }

    class StepBeginsEvent : GraphEvent
    {
        double step;
        StepBeginsEvent(string sourceId, long timeId, double step) : base(sourceId, timeId)
        {
            this.step = step;
        }

        virtual void Trigger()
        {
            for (int i = 0; i < eltsSinks.Count; i++)
                eltsSinks[i].StepBegins(sourceId, timeId, step);
        }
    }

    class AttributeChangedEvent : GraphEvent
    {
        ElementType eltType;
        string eltId;
        string attribute;
        AttributeChangeEvent event;
        object oldValue;
        object newValue;
        AttributeChangedEvent(string sourceId, long timeId, string eltId, ElementType eltType, string attribute, AttributeChangeEvent @event, object oldValue, object newValue) : base(sourceId, timeId)
        {
            this.eltType = eltType;
            this.eltId = eltId;
            this.attribute = attribute;
            this.@event = @event;
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        virtual void Trigger()
        {
            switch (@event)
            {
                case ADD:
                    switch (eltType)
                    {
                        case NODE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].NodeAttributeAdded(sourceId, timeId, eltId, attribute, newValue);
                            break;
                        case EDGE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].EdgeAttributeAdded(sourceId, timeId, eltId, attribute, newValue);
                            break;
                        default:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].GraphAttributeAdded(sourceId, timeId, attribute, newValue);
                            break;
                    }

                    break;
                case REMOVE:
                    switch (eltType)
                    {
                        case NODE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].NodeAttributeRemoved(sourceId, timeId, eltId, attribute);
                            break;
                        case EDGE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].EdgeAttributeRemoved(sourceId, timeId, eltId, attribute);
                            break;
                        default:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].GraphAttributeRemoved(sourceId, timeId, attribute);
                            break;
                    }

                    break;
                default:
                    switch (eltType)
                    {
                        case NODE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].NodeAttributeChanged(sourceId, timeId, eltId, attribute, oldValue, newValue);
                            break;
                        case EDGE:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].EdgeAttributeChanged(sourceId, timeId, eltId, attribute, oldValue, newValue);
                            break;
                        default:
                            for (int i = 0; i < attrSinks.Count; i++)
                                attrSinks[i].GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
                            break;
                    }

                    break;
            }
        }
    }

    class AddToListEvent<T> : GraphEvent
    {
        IList<T> l;
        T obj;
        AddToListEvent(IList<T> l, T obj) : base(null, -1)
        {
            this.l = l;
            this.obj = obj;
        }

        virtual void Trigger()
        {
            l.Add(obj);
        }
    }

    class RemoveFromListEvent<T> : GraphEvent
    {
        IList<T> l;
        T obj;
        RemoveFromListEvent(IList<T> l, T obj) : base(null, -1)
        {
            this.l = l;
            this.obj = obj;
        }

        virtual void Trigger()
        {
            l.Remove(obj);
        }
    }

    class ClearListEvent<T> : GraphEvent
    {
        IList<T> l;
        ClearListEvent(IList<T> l) : base(null, -1)
        {
            this.l = l;
        }

        virtual void Trigger()
        {
            l.Clear();
        }
    }
}
