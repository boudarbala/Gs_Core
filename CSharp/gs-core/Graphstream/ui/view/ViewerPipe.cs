using Gs_Core.Graphstream.Stream;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.View.ElementType;
using static Org.Graphstream.Ui.View.EventType;
using static Org.Graphstream.Ui.View.AttributeChangeEvent;
using static Org.Graphstream.Ui.View.Mode;
using static Org.Graphstream.Ui.View.What;
using static Org.Graphstream.Ui.View.TimeFormat;
using static Org.Graphstream.Ui.View.OutputType;
using static Org.Graphstream.Ui.View.OutputPolicy;
using static Org.Graphstream.Ui.View.LayoutPolicy;
using static Org.Graphstream.Ui.View.Quality;
using static Org.Graphstream.Ui.View.Option;
using static Org.Graphstream.Ui.View.AttributeType;
using static Org.Graphstream.Ui.View.Balise;
using static Org.Graphstream.Ui.View.GEXFAttribute;
using static Org.Graphstream.Ui.View.METAAttribute;
using static Org.Graphstream.Ui.View.GRAPHAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.View.NODESAttribute;
using static Org.Graphstream.Ui.View.NODEAttribute;
using static Org.Graphstream.Ui.View.ATTVALUEAttribute;
using static Org.Graphstream.Ui.View.PARENTAttribute;
using static Org.Graphstream.Ui.View.EDGESAttribute;
using static Org.Graphstream.Ui.View.SPELLAttribute;
using static Org.Graphstream.Ui.View.COLORAttribute;
using static Org.Graphstream.Ui.View.POSITIONAttribute;
using static Org.Graphstream.Ui.View.SIZEAttribute;
using static Org.Graphstream.Ui.View.NODESHAPEAttribute;
using static Org.Graphstream.Ui.View.EDGEAttribute;
using static Org.Graphstream.Ui.View.THICKNESSAttribute;
using static Org.Graphstream.Ui.View.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.View.IDType;
using static Org.Graphstream.Ui.View.ModeType;
using static Org.Graphstream.Ui.View.WeightType;
using static Org.Graphstream.Ui.View.EdgeType;
using static Org.Graphstream.Ui.View.NodeShapeType;
using static Org.Graphstream.Ui.View.EdgeShapeType;
using static Org.Graphstream.Ui.View.ClassType;
using static Org.Graphstream.Ui.View.TimeFormatType;
using static Org.Graphstream.Ui.View.GPXAttribute;
using static Org.Graphstream.Ui.View.WPTAttribute;
using static Org.Graphstream.Ui.View.LINKAttribute;
using static Org.Graphstream.Ui.View.EMAILAttribute;
using static Org.Graphstream.Ui.View.PTAttribute;
using static Org.Graphstream.Ui.View.BOUNDSAttribute;
using static Org.Graphstream.Ui.View.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.View.FixType;
using static Org.Graphstream.Ui.View.GraphAttribute;
using static Org.Graphstream.Ui.View.LocatorAttribute;
using static Org.Graphstream.Ui.View.NodeAttribute;
using static Org.Graphstream.Ui.View.EdgeAttribute;
using static Org.Graphstream.Ui.View.DataAttribute;
using static Org.Graphstream.Ui.View.PortAttribute;
using static Org.Graphstream.Ui.View.EndPointAttribute;
using static Org.Graphstream.Ui.View.EndPointType;
using static Org.Graphstream.Ui.View.HyperEdgeAttribute;
using static Org.Graphstream.Ui.View.KeyAttribute;
using static Org.Graphstream.Ui.View.KeyDomain;
using static Org.Graphstream.Ui.View.KeyAttrType;
using static Org.Graphstream.Ui.View.GraphEvents;
using static Org.Graphstream.Ui.View.ThreadingModel;
using static Org.Graphstream.Ui.View.CloseFramePolicy;

namespace Gs_Core.Graphstream.Ui.View;

public class ViewerPipe : SourceBase, ProxyPipe
{
    private string id;
    protected ProxyPipe pipeIn;
    protected HashSet<ViewerListener> viewerListeners = new HashSet<ViewerListener>();
    public ViewerPipe(string id, ProxyPipe pipeIn)
    {
        this.id = id;
        this.pipeIn = pipeIn;
        pipeIn.AddSink(this);
    }

    public virtual string GetId()
    {
        return id;
    }

    public virtual void Pump()
    {
        pipeIn.Pump();
    }

    public virtual void BlockingPump()
    {
        pipeIn.BlockingPump();
    }

    public virtual void BlockingPump(long timeout)
    {
        pipeIn.BlockingPump(timeout);
    }

    public virtual void AddViewerListener(ViewerListener listener)
    {
        viewerListeners.Add(listener);
    }

    public virtual void RemoveViewerListener(ViewerListener listener)
    {
        viewerListeners.Remove(listener);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        SendEdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        SendEdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        SendEdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        SendGraphAttributeAdded(sourceId, timeId, attribute, value);
        if (attribute.Equals("ui.viewClosed") && value is string)
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.ViewClosed((string)value);
            SendGraphAttributeRemoved(id, attribute);
        }
        else if (attribute.Equals("ui.clicked") && value is string)
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.ButtonPushed((string)value);
            SendGraphAttributeRemoved(id, attribute);
        }
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        SendGraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        SendGraphAttributeRemoved(sourceId, timeId, attribute);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        SendNodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
        if (attribute.Equals("ui.clicked"))
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.ButtonPushed(nodeId);
        }

        if (attribute.Equals("ui.mouseOver"))
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.MouseOver(nodeId);
        }
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        SendNodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        SendNodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
        if (attribute.Equals("ui.clicked"))
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.ButtonReleased(nodeId);
        }

        if (attribute.Equals("ui.mouseOver"))
        {
            foreach (ViewerListener listener in viewerListeners)
                listener.MouseLeft(nodeId);
        }
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        SendEdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        SendEdgeRemoved(sourceId, timeId, edgeId);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        SendGraphCleared(sourceId, timeId);
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        SendNodeAdded(sourceId, timeId, nodeId);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        SendNodeRemoved(sourceId, timeId, nodeId);
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        SendStepBegins(sourceId, timeId, step);
    }
}
