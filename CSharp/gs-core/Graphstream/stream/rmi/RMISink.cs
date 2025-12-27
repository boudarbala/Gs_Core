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

public class RMISink : UnicastRemoteObject, RMIAdapterOut, Sink
{
    private static readonly long serialVersionUID = 23444722897331612;
    ConcurrentHashMap<string, RMIAdapterIn> inputs;
    public RMISink() : base()
    {
        inputs = new ConcurrentHashMap<string, RMIAdapterIn>();
    }

    public RMISink(string name) : this()
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

    public virtual void Register(string url)
    {
        try
        {
            RMIAdapterIn in = (RMIAdapterIn)Naming.Lookup(url);
            if (@in != null)
                inputs.Put(url, @in);
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
        }
    }

    public virtual void Unregister(string url)
    {
        if (inputs.ContainsKey(url))
            inputs.Remove(url);
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.EdgeAttributeAdded(graphId, timeId, edgeId, attribute, value);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.EdgeAttributeChanged(graphId, timeId, edgeId, attribute, oldValue, newValue);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.EdgeAttributeRemoved(graphId, timeId, edgeId, attribute);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.GraphAttributeAdded(graphId, timeId, attribute, value);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.GraphAttributeChanged(graphId, timeId, attribute, oldValue, newValue);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.GraphAttributeRemoved(graphId, timeId, attribute);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.NodeAttributeAdded(graphId, timeId, nodeId, attribute, value);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.NodeAttributeChanged(graphId, timeId, nodeId, attribute, oldValue, newValue);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.NodeAttributeRemoved(graphId, timeId, nodeId, attribute);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.EdgeAdded(graphId, timeId, edgeId, fromNodeId, toNodeId, directed);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.EdgeRemoved(graphId, timeId, edgeId);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.GraphCleared(graphId, timeId);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.NodeAdded(graphId, timeId, nodeId);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.NodeRemoved(graphId, timeId, nodeId);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
        foreach (RMIAdapterIn in in inputs.Values())
        {
            try
            {
                @in.StepBegins(graphId, timeId, step);
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }
}
