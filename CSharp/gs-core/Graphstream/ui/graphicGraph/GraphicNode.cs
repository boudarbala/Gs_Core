using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Ui.GraphicGraph.GraphPosLengthUtils;
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

public class GraphicNode : GraphicElement, Node
{
    public double x, y, z;
    public bool positionned = false;
    public GraphicNode(GraphicGraph graph, string id, HashMap<string, object> attributes) : base(id, graph)
    {
        if (attributes != null)
            SetAttributes(attributes);
    }

    public override Selector.Type GetSelectorType()
    {
        return Selector.Type.NODE;
    }

    public override double GetX()
    {
        return x;
    }

    public override double GetY()
    {
        return y;
    }

    public override double GetZ()
    {
        return z;
    }

    protected virtual Point3 GetPosition()
    {
        return new Point3(x, y, z);
    }

    protected virtual void MoveFromEvent(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        if (!positionned)
        {
            positionned = true;
        }

        mygraph.graphChanged = true;
        mygraph.boundsChanged = true;
    }

    public override void Move(double x, double y, double z)
    {
        MoveFromEvent(x, y, z);
        if (mygraph.feedbackXYZ)
            SetAttribute("xyz", x, y, z);
    }

    protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
    {
        base.AttributeChanged(@event, attribute, oldValue, newValue);
        char c = attribute.CharAt(0);
        if (attribute.Length > 2 && c == 'u' && attribute.CharAt(1) == 'i' && attribute.StartsWith("ui.sprite."))
        {
            mygraph.SpriteAttribute(@event, this, attribute, newValue);
        }
        else if ((@event == AttributeChangeEvent.ADD || @event == AttributeChangeEvent.CHANGE))
        {
            if (attribute.Length == 1)
            {
                switch (c)
                {
                    case 'x':
                        MoveFromEvent(NumberAttribute(newValue), y, z);
                        break;
                    case 'y':
                        MoveFromEvent(x, NumberAttribute(newValue), z);
                        break;
                    case 'z':
                        MoveFromEvent(x, y, NumberAttribute(newValue));
                        break;
                    default:
                        break;
                }
            }
            else if (c == 'x' && attribute.Length > 1 && attribute.CharAt(1) == 'y' && (attribute.Length == 2 || (attribute.Length == 3 && attribute.CharAt(2) == 'z')))
            {
                double[] pos = NodePosition(this);
                MoveFromEvent(pos[0], pos[1], pos[2]);
            }
        }

        mygraph.listeners.SendAttributeChangedEvent(GetId(), ElementType.NODE, attribute, @event, oldValue, newValue);
    }

    protected virtual double NumberAttribute(object value)
    {
        if (value is Number)
        {
            return ((Number)value).DoubleValue();
        }
        else if (value is string)
        {
            try
            {
                return Double.ParseDouble((string)value);
            }
            catch (NumberFormatException e)
            {
            }
        }
        else if (value is CharSequence)
        {
            try
            {
                return Double.ParseDouble(((CharSequence)value).ToString());
            }
            catch (NumberFormatException e)
            {
            }
        }

        return 0;
    }

    protected override void Removed()
    {
    }

    public override IEnumerator<Node> GetBreadthFirstIterator()
    {
        throw new Exception("not implemented !");
    }

    public override IEnumerator<Node> GetBreadthFirstIterator(bool directed)
    {
        throw new Exception("not implemented !");
    }

    public override IEnumerator<Node> GetDepthFirstIterator()
    {
        throw new Exception("not implemented !");
    }

    public override IEnumerator<Node> GetDepthFirstIterator(bool directed)
    {
        throw new Exception("not implemented !");
    }

    public override int GetDegree()
    {
        IList<GraphicEdge> edges = mygraph.connectivity[this ];
        if (edges != null)
            return edges.Count;
        return 0;
    }

    public override Edge GetEdge(int i)
    {
        IList<GraphicEdge> edges = mygraph.connectivity[this ];
        if (edges != null && i >= 0 && i < edges.Count)
            return edges[i];
        return null;
    }

    public override Edge GetEdgeBetween(string id)
    {
        if (HasEdgeToward(id))
            return GetEdgeToward(id);
        else
            return GetEdgeFrom(id);
    }

    public override Edge GetEdgeFrom(string id)
    {
        return null;
    }

    public override Stream<Edge> Edges()
    {
        return mygraph.connectivity[this ].Stream().Map((ge) => (Edge)ge);
    }

    public override IEnumerator<Edge> Iterator()
    {
        return Edges().Iterator();
    }

    public override Edge GetEdgeToward(string id)
    {
        IList<TWildcardTodoEdge> edges = mygraph.connectivity[this ];
        foreach (Edge edge in edges)
        {
            if (edge.GetOpposite(this).GetId().Equals(id))
                return edge;
        }

        return null;
    }

    public override Graph GetGraph()
    {
        return mygraph;
    }

    public virtual string GetGraphName()
    {
        throw new Exception("impossible with GraphicGraph");
    }

    public virtual string GetHost()
    {
        throw new Exception("impossible with GraphicGraph");
    }

    public override int GetInDegree()
    {
        return GetDegree();
    }

    public override int GetOutDegree()
    {
        return GetDegree();
    }

    public override bool HasEdgeBetween(string id)
    {
        return (HasEdgeToward(id) || HasEdgeFrom(id));
    }

    public override bool HasEdgeFrom(string id)
    {
        return false;
    }

    public override bool HasEdgeToward(string id)
    {
        return false;
    }

    public virtual bool IsDistributed()
    {
        return false;
    }

    public virtual void SetGraph(Graph graph)
    {
        throw new Exception("impossible with GraphicGraph");
    }

    public virtual void SetGraphName(string newHost)
    {
        throw new Exception("impossible with GraphicGraph");
    }

    public virtual void SetHost(string newHost)
    {
        throw new Exception("impossible with GraphicGraph");
    }

    public override Edge GetEdgeBetween(Node Node)
    {
        return null;
    }

    public override Edge GetEdgeBetween(int index)
    {
        return null;
    }

    public override Edge GetEdgeFrom(Node Node)
    {
        return null;
    }

    public override Edge GetEdgeFrom(int index)
    {
        return null;
    }

    public override Edge GetEdgeToward(Node Node)
    {
        return null;
    }

    public override Edge GetEdgeToward(int index)
    {
        return null;
    }

    public override Edge GetEnteringEdge(int i)
    {
        return null;
    }

    public override Edge GetLeavingEdge(int i)
    {
        return null;
    }
}
