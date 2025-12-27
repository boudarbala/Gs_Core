using Java.Rmi;
using Java.Rmi.Server;
using Java.Util.Concurrent;
using Gs_Core.Graphstream.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Rmi.ElementType;
using static Org.Graphstream.Stream.Rmi.EventType;
using static Org.Graphstream.Stream.Rmi.AttributeChangeEvent;
using static Org.Graphstream.Stream.Rmi.Mode;
using static Org.Graphstream.Stream.Rmi.What;
using static Org.Graphstream.Stream.Rmi.TimeFormat;
using static Org.Graphstream.Stream.Rmi.OutputType;
using static Org.Graphstream.Stream.Rmi.OutputPolicy;
using static Org.Graphstream.Stream.Rmi.LayoutPolicy;
using static Org.Graphstream.Stream.Rmi.Quality;
using static Org.Graphstream.Stream.Rmi.Option;
using static Org.Graphstream.Stream.Rmi.AttributeType;
using static Org.Graphstream.Stream.Rmi.Balise;
using static Org.Graphstream.Stream.Rmi.GEXFAttribute;
using static Org.Graphstream.Stream.Rmi.METAAttribute;
using static Org.Graphstream.Stream.Rmi.GRAPHAttribute;
using static Org.Graphstream.Stream.Rmi.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.Rmi.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.Rmi.NODESAttribute;
using static Org.Graphstream.Stream.Rmi.NODEAttribute;
using static Org.Graphstream.Stream.Rmi.ATTVALUEAttribute;
using static Org.Graphstream.Stream.Rmi.PARENTAttribute;
using static Org.Graphstream.Stream.Rmi.EDGESAttribute;
using static Org.Graphstream.Stream.Rmi.SPELLAttribute;
using static Org.Graphstream.Stream.Rmi.COLORAttribute;
using static Org.Graphstream.Stream.Rmi.POSITIONAttribute;
using static Org.Graphstream.Stream.Rmi.SIZEAttribute;
using static Org.Graphstream.Stream.Rmi.NODESHAPEAttribute;
using static Org.Graphstream.Stream.Rmi.EDGEAttribute;
using static Org.Graphstream.Stream.Rmi.THICKNESSAttribute;
using static Org.Graphstream.Stream.Rmi.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.Rmi.IDType;
using static Org.Graphstream.Stream.Rmi.ModeType;
using static Org.Graphstream.Stream.Rmi.WeightType;
using static Org.Graphstream.Stream.Rmi.EdgeType;
using static Org.Graphstream.Stream.Rmi.NodeShapeType;
using static Org.Graphstream.Stream.Rmi.EdgeShapeType;
using static Org.Graphstream.Stream.Rmi.ClassType;
using static Org.Graphstream.Stream.Rmi.TimeFormatType;
using static Org.Graphstream.Stream.Rmi.GPXAttribute;
using static Org.Graphstream.Stream.Rmi.WPTAttribute;
using static Org.Graphstream.Stream.Rmi.LINKAttribute;
using static Org.Graphstream.Stream.Rmi.EMAILAttribute;
using static Org.Graphstream.Stream.Rmi.PTAttribute;
using static Org.Graphstream.Stream.Rmi.BOUNDSAttribute;
using static Org.Graphstream.Stream.Rmi.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.Rmi.FixType;
using static Org.Graphstream.Stream.Rmi.GraphAttribute;
using static Org.Graphstream.Stream.Rmi.LocatorAttribute;
using static Org.Graphstream.Stream.Rmi.NodeAttribute;
using static Org.Graphstream.Stream.Rmi.EdgeAttribute;
using static Org.Graphstream.Stream.Rmi.DataAttribute;
using static Org.Graphstream.Stream.Rmi.PortAttribute;
using static Org.Graphstream.Stream.Rmi.EndPointAttribute;
using static Org.Graphstream.Stream.Rmi.EndPointType;
using static Org.Graphstream.Stream.Rmi.HyperEdgeAttribute;
using static Org.Graphstream.Stream.Rmi.KeyAttribute;
using static Org.Graphstream.Stream.Rmi.KeyDomain;
using static Org.Graphstream.Stream.Rmi.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.Rmi;

public class RMISource : UnicastRemoteObject, RMIAdapterIn, Source
{
    private static readonly long serialVersionUID = 6635146473737922832;
    ConcurrentLinkedQueue<AttributeSink> attributesListeners;
    ConcurrentLinkedQueue<ElementSink> elementsListeners;
    public RMISource() : base()
    {
        attributesListeners = new ConcurrentLinkedQueue<AttributeSink>();
        elementsListeners = new ConcurrentLinkedQueue<ElementSink>();
    }

    public RMISource(string name) : this()
    {
        Bind(name);
    }

    public virtual void Bind(string name)
    {
        try
        {
            Naming.Rebind(String.Format("//localhost/%s", name), this);
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
        }
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.EdgeAdded(graphId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.EdgeAttributeAdded(graphId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.EdgeAttributeChanged(graphId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.EdgeAttributeRemoved(graphId, timeId, edgeId, attribute);
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.EdgeRemoved(graphId, timeId, edgeId);
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.GraphAttributeAdded(graphId, timeId, attribute, value);
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.GraphAttributeChanged(graphId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.GraphAttributeRemoved(graphId, timeId, attribute);
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.GraphCleared(graphId, timeId);
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.NodeAdded(graphId, timeId, nodeId);
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.NodeAttributeAdded(graphId, timeId, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.NodeAttributeChanged(graphId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        foreach (AttributeSink gal in attributesListeners)
            gal.NodeAttributeRemoved(graphId, timeId, nodeId, attribute);
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.NodeRemoved(graphId, timeId, nodeId);
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
        foreach (ElementSink gel in elementsListeners)
            gel.StepBegins(graphId, timeId, step);
    }

    public virtual void AddAttributeSink(AttributeSink listener)
    {
        attributesListeners.Add(listener);
    }

    public virtual void AddElementSink(ElementSink listener)
    {
        elementsListeners.Add(listener);
    }

    public virtual void AddSink(Sink listener)
    {
        attributesListeners.Add(listener);
        elementsListeners.Add(listener);
    }

    public virtual void RemoveAttributeSink(AttributeSink listener)
    {
        attributesListeners.Remove(listener);
    }

    public virtual void RemoveElementSink(ElementSink listener)
    {
        elementsListeners.Remove(listener);
    }

    public virtual void RemoveSink(Sink listener)
    {
        attributesListeners.Remove(listener);
        elementsListeners.Remove(listener);
    }

    public virtual void ClearAttributeSinks()
    {
        attributesListeners.Clear();
        elementsListeners.Clear();
    }

    public virtual void ClearElementSinks()
    {
        elementsListeners.Clear();
    }

    public virtual void ClearSinks()
    {
        attributesListeners.Clear();
    }
}
