using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.EventType;
using static Org.Graphstream.Ui.GraphicGraph.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Mode;
using static Org.Graphstream.Ui.GraphicGraph.What;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Option;
using static Org.Graphstream.Ui.GraphicGraph.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Balise;
using static Org.Graphstream.Ui.GraphicGraph.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.IDType;
using static Org.Graphstream.Ui.GraphicGraph.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.FixType;
using static Org.Graphstream.Ui.GraphicGraph.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.GraphEvents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph;

public class GraphicSprite : GraphicElement
{
    protected GraphicNode node;
    protected GraphicEdge edge;
    public Values position = new Values(StyleConstants.Units.GU, 0, 0, 0);
    public GraphicSprite(string id, GraphicGraph graph) : base(id, graph)
    {
        if (graph.GetNodeCount() > 0)
        {
            GraphicNode node = (GraphicNode)graph.Nodes().FindFirst().Get();
            position.SetValue(0, node.x);
            position.SetValue(1, node.y);
            position.SetValue(2, node.z);
        }

        string myPrefix = String.Format("ui.sprite.%s", id);
        if (mygraph.GetAttribute(myPrefix) == null)
            mygraph.SetAttribute(myPrefix, position);
    }

    public virtual GraphicNode GetNodeAttachment()
    {
        return node;
    }

    public virtual GraphicEdge GetEdgeAttachment()
    {
        return edge;
    }

    public virtual GraphicElement GetAttachment()
    {
        GraphicNode n = GetNodeAttachment();
        if (n != null)
            return n;
        return GetEdgeAttachment();
    }

    public virtual bool IsAttached()
    {
        return (edge != null || node != null);
    }

    public virtual bool IsAttachedToNode()
    {
        return node != null;
    }

    public virtual bool IsAttachedToEdge()
    {
        return edge != null;
    }

    public override Selector.Type GetSelectorType()
    {
        return Selector.Type.SPRITE;
    }

    public override double GetX()
    {
        return position[0];
    }

    public override double GetY()
    {
        return position[1];
    }

    public override double GetZ()
    {
        return position[2];
    }

    public virtual Style.Units GetUnits()
    {
        return position.GetUnits();
    }

    public override void Move(double x, double y, double z)
    {
        if (IsAttachedToNode())
        {
            GraphicNode n = GetNodeAttachment();
            x -= n.x;
            y -= n.y;
            z -= n.z;
            SetPosition(x, y, z, Style.Units.GU);
        }
        else if (IsAttachedToEdge())
        {
            GraphicEdge e = GetEdgeAttachment();
            double len = e.to.x - e.from.x;
            double diff = x - e.from.x;
            x = diff / len;
            SetPosition(x);
        }
        else
        {
            SetPosition(x, y, z, Style.Units.GU);
        }
    }

    public virtual void AttachToNode(GraphicNode node)
    {
        this.edge = null;
        this.node = node;
        string prefix = String.Format("ui.sprite.%s", GetId());
        if (this.node.GetAttribute(prefix) == null)
            this.node.SetAttribute(prefix);
        mygraph.graphChanged = true;
    }

    public virtual void AttachToEdge(GraphicEdge edge)
    {
        this.node = null;
        this.edge = edge;
        string prefix = String.Format("ui.sprite.%s", GetId());
        if (this.edge.GetAttribute(prefix) == null)
            this.edge.SetAttribute(prefix);
        mygraph.graphChanged = true;
    }

    public virtual void Detach()
    {
        string prefix = String.Format("ui.sprite.%s", GetId());
        if (this.node != null)
            this.node.RemoveAttribute(prefix);
        else if (this.edge != null)
            this.edge.RemoveAttribute(prefix);
        this.edge = null;
        this.node = null;
        mygraph.graphChanged = true;
    }

    public virtual void SetPosition(double value)
    {
        SetPosition(value, 0, 0, GetUnits());
    }

    public virtual void SetPosition(double x, double y, double z, Style.Units units)
    {
        if (edge != null)
        {
            if (x < 0)
                x = 0;
            else if (x > 1)
                x = 1;
        }

        bool changed = false;
        if (GetX() != x)
        {
            changed = true;
            position.SetValue(0, x);
        }

        if (GetY() != y)
        {
            changed = true;
            position.SetValue(1, y);
        }

        if (GetZ() != z)
        {
            changed = true;
            position.SetValue(2, z);
        }

        if (GetUnits() != units)
        {
            changed = true;
            position.SetUnits(units);
        }

        if (changed)
        {
            mygraph.graphChanged = true;
            mygraph.boundsChanged = true;
            string prefix = String.Format("ui.sprite.%s", GetId());
            mygraph.SetAttribute(prefix, position);
        }
    }

    public virtual void SetPosition(Values values)
    {
        double x = 0;
        double y = 0;
        double z = 0;
        if (values.GetValueCount() > 0)
            x = values[0];
        if (values.GetValueCount() > 1)
            y = values[1];
        if (values.GetValueCount() > 2)
            z = values[2];
        if (x == 1 && y == 1 && z == 1)
            throw new Exception("WTF !!!");
        SetPosition(x, y, z, values.units);
    }

    protected virtual double CheckAngle(double angle)
    {
        if (angle > Math.PI * 2)
            angle = angle % (Math.PI * 2);
        else if (angle < 0)
            angle = (Math.PI * 2) - (angle % (Math.PI * 2));
        return angle;
    }

    protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
    {
        base.AttributeChanged(@event, attribute, oldValue, newValue);
        string completeAttr = String.Format("ui.sprite.%s.%s", GetId(), attribute);
        mygraph.listeners.SendAttributeChangedEvent(mygraph.GetId(), ElementType.GRAPH, completeAttr, @event, oldValue, newValue);
    }

    protected override void Removed()
    {
    }
}
