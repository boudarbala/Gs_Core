using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Java.Util;
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

public class GraphicEdge : GraphicElement, Edge
{
    public GraphicNode from;
    public GraphicNode to;
    public bool directed;
    public int multi;
    public EdgeGroup group;
    public double[] ctrl;
    public GraphicEdge(string id, GraphicNode from, GraphicNode to, bool dir, HashMap<string, object> attributes) : base(id, from.mygraph)
    {
        this.from = from;
        this.to = to;
        this.directed = dir;
        if (this.attributes == null)
            this.attributes = new HashMap<string, object>();
        if (attributes != null)
            SetAttributes(attributes);
    }

    public override Selector.Type GetSelectorType()
    {
        return Selector.Type.EDGE;
    }

    public virtual GraphicNode OtherNode(GraphicNode n)
    {
        return (GraphicNode)GetOpposite(n);
    }

    public override double GetX()
    {
        return from.x + ((to.x - from.x) / 2);
    }

    public override double GetY()
    {
        return from.y + ((to.y - from.y) / 2);
    }

    public override double GetZ()
    {
        return from.z + ((to.z - from.z) / 2);
    }

    public virtual double[] GetControlPoints()
    {
        return ctrl;
    }

    public virtual bool IsCurve()
    {
        return ctrl != null;
    }

    public virtual void SetControlPoints(double[] points)
    {
        ctrl = points;
    }

    public virtual int GetMultiIndex()
    {
        return multi;
    }

    public override void Move(double x, double y, double z)
    {
    }

    protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
    {
        base.AttributeChanged(@event, attribute, oldValue, newValue);
        if (attribute.StartsWith("ui.sprite."))
        {
            mygraph.SpriteAttribute(@event, this, attribute, newValue);
        }

        mygraph.listeners.SendAttributeChangedEvent(GetId(), ElementType.EDGE, attribute, @event, oldValue, newValue);
    }

    protected virtual void CountSameEdges(Iterable<GraphicEdge> edgeList)
    {
        foreach (GraphicEdge other in edgeList)
        {
            if (other != this)
            {
                if ((other.from == from && other.to == to) || (other.to == from && other.from == to))
                {
                    group = other.group;
                    if (group == null)
                        group = new EdgeGroup(other, this);
                    else
                        group.Increment(this);
                    break;
                }
            }
        }
    }

    public override void Removed()
    {
        if (group != null)
        {
            group.Decrement(this);
            if (group.GetCount() == 1)
                group = null;
        }
    }

    public override Node GetNode0()
    {
        return from;
    }

    public override Node GetNode1()
    {
        return to;
    }

    public virtual EdgeGroup GetGroup()
    {
        return group;
    }

    public override Node GetOpposite(Node node)
    {
        if (node == from)
            return to;
        return from;
    }

    public override Node GetSourceNode()
    {
        return from;
    }

    public override Node GetTargetNode()
    {
        return to;
    }

    public virtual bool IsDirected()
    {
        return directed;
    }

    public virtual bool IsLoop()
    {
        return (from == to);
    }

    public virtual void SetDirected(bool on)
    {
        directed = on;
    }

    public virtual void SwitchDirection()
    {
        GraphicNode tmp;
        tmp = from;
        from = to;
        to = tmp;
    }

    public class EdgeGroup
    {
        public List<GraphicEdge> edges;
        public EdgeGroup(GraphicEdge first, GraphicEdge second)
        {
            edges = new List<GraphicEdge>();
            first.group = this;
            second.group = this;
            edges.Add(first);
            edges.Add(second);
            first.multi = 0;
            second.multi = 1;
        }

        public virtual GraphicEdge GetEdge(int i)
        {
            return edges[i];
        }

        public virtual int GetCount()
        {
            return edges.Count;
        }

        public virtual void Increment(GraphicEdge edge)
        {
            edge.multi = GetCount();
            edges.Add(edge);
        }

        public virtual void Decrement(GraphicEdge edge)
        {
            edges.Remove(edges.IndexOf(edge));
            for (int i = 0; i < edges.Count; i++)
                edges[i].multi = i;
        }
    }
}
