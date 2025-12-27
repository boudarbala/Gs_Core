using Java.Io;
using Java.Util;
using Java.Util.Concurrent.Atomic;
using Java.Util.Logging;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Stream.SourceBase;
using Gs_Core.Graphstream.Stream.File;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Gs_Core.Graphstream.Ui.View;
using Gs_Core.Graphstream.Util;
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

public class GraphicGraph : AbstractElement, Graph, StyleGroupListener
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphicGraph).GetSimpleName());
    protected StyleSheet styleSheet;
    protected StyleGroupSet styleGroups;
    protected readonly Dictionary<GraphicNode, IList<GraphicEdge>> connectivity;
    public StyleGroup style;
    public double step = 0;
    public bool graphChanged;
    protected bool boundsChanged = true;
    protected Point3 hi = new Point3();
    protected Point3 lo = new Point3();
    protected GraphListeners listeners;
    protected bool feedbackXYZ = true;
    public GraphicGraph(string id) : base(id)
    {
        listeners = new GraphListeners(this);
        styleSheet = new StyleSheet();
        styleGroups = new StyleGroupSet(styleSheet);
        connectivity = new HashMap<GraphicNode, IList<GraphicEdge>>();
        styleGroups.AddListener(this);
        styleGroups.AddElement(this);
        style = styleGroups.GetStyleFor(this);
    }

    public virtual bool GraphChangedFlag()
    {
        return graphChanged;
    }

    public virtual void ResetGraphChangedFlag()
    {
        graphChanged = false;
    }

    public virtual StyleSheet GetStyleSheet()
    {
        return styleSheet;
    }

    public virtual StyleGroup GetStyle()
    {
        return style;
    }

    public virtual StyleGroupSet GetStyleGroups()
    {
        return styleGroups;
    }

    public override string ToString()
    {
        return String.Format("[%s %d nodes %d edges]", GetId(), GetNodeCount(), GetEdgeCount());
    }

    public virtual double GetStep()
    {
        return step;
    }

    public virtual Point3 GetMaxPos()
    {
        return hi;
    }

    public virtual Point3 GetMinPos()
    {
        return lo;
    }

    public virtual bool FeedbackXYZ()
    {
        return feedbackXYZ;
    }

    public virtual void FeedbackXYZ(bool on)
    {
        feedbackXYZ = on;
    }

    public virtual void ComputeBounds()
    {
        if (boundsChanged)
        {
            AtomicBoolean effectiveChange = new AtomicBoolean(false);
            lo.x = lo.y = lo.z = Double.MAX_VALUE;
            hi.x = hi.y = hi.z = -Double.MAX_VALUE;
            Nodes().ForEach((n) =>
            {
                GraphicNode node = (GraphicNode)n;
                if (!node.hidden && node.positionned)
                {
                    effectiveChange.Set(true);
                    if (node.x < lo.x)
                        lo.x = node.x;
                    if (node.x > hi.x)
                        hi.x = node.x;
                    if (node.y < lo.y)
                        lo.y = node.y;
                    if (node.y > hi.y)
                        hi.y = node.y;
                    if (node.z < lo.z)
                        lo.z = node.z;
                    if (node.z > hi.z)
                        hi.z = node.z;
                }
            });
            Sprites().ForEach((sprite) =>
            {
                if (!sprite.IsAttached() && sprite.GetUnits() == StyleConstants.Units.GU)
                {
                    double x = sprite.GetX();
                    double y = sprite.GetY();
                    double z = sprite.GetZ();
                    if (!sprite.hidden)
                    {
                        effectiveChange.Set(true);
                        if (x < lo.x)
                            lo.x = x;
                        if (x > hi.x)
                            hi.x = x;
                        if (y < lo.y)
                            lo.y = y;
                        if (y > hi.y)
                            hi.y = y;
                        if (z < lo.z)
                            lo.z = z;
                        if (z > hi.z)
                            hi.z = z;
                    }
                }
            });
            if (hi.x - lo.x < 1E-06)
            {
                hi.x = hi.x + 1;
                lo.x = lo.x - 1;
            }

            if (hi.y - lo.y < 1E-06)
            {
                hi.y = hi.y + 1;
                lo.y = lo.y - 1;
            }

            if (hi.z - lo.z < 1E-06)
            {
                hi.z = hi.z + 1;
                lo.z = lo.z - 1;
            }

            if (effectiveChange.Get())
                boundsChanged = false;
            else
            {
                lo.x = lo.y = lo.z = -1;
                hi.x = hi.y = hi.z = 1;
            }
        }
    }

    protected virtual void MoveNode(string id, double x, double y, double z)
    {
        GraphicNode node = (GraphicNode)styleGroups.GetNode(id);
        if (node != null)
        {
            node.x = x;
            node.y = y;
            node.z = z;
            node.SetAttribute("x", x);
            node.SetAttribute("y", y);
            node.SetAttribute("z", z);
            graphChanged = true;
        }
    }

    public override Node GetNode(string id)
    {
        return styleGroups.GetNode(id);
    }

    public override Edge GetEdge(string id)
    {
        return styleGroups.GetEdge(id);
    }

    public virtual GraphicSprite GetSprite(string id)
    {
        return styleGroups.GetSprite(id);
    }

    protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
    {
        if (attribute.Equals("ui.repaint"))
        {
            graphChanged = true;
        }
        else if (attribute.Equals("ui.stylesheet") || attribute.Equals("stylesheet"))
        {
            if (@event == AttributeChangeEvent.ADD || @event == AttributeChangeEvent.CHANGE)
            {
                if (newValue is string)
                {
                    try
                    {
                        styleSheet.Load((string)newValue);
                        graphChanged = true;
                    }
                    catch (Exception e)
                    {
                        logger.Log(Level.WARNING, String.Format("Error while parsing style sheet for graph '%s'.", GetId()), e);
                    }
                }
                else
                {
                    logger.Warning(String.Format("Error with stylesheet specification what to do with '%s'.", newValue));
                }
            }
            else
            {
                styleSheet.Clear();
                graphChanged = true;
            }
        }
        else if (attribute.StartsWith("ui.sprite."))
        {
            SpriteAttribute(@event, null, attribute, newValue);
            graphChanged = true;
        }

        listeners.SendAttributeChangedEvent(GetId(), ElementType.GRAPH, attribute, @event, oldValue, newValue);
    }

    public virtual void PrintConnectivity()
    {
        IEnumerator<GraphicNode> keys = connectivity.KeySet().Iterator();
        System.err.Printf("Graphic graph connectivity:%n");
        while (keys.HasNext())
        {
            GraphicNode node = keys.Next();
            System.err.Printf("    [%s] -> ", node.GetId());
            Iterable<GraphicEdge> edges = connectivity[node];
            foreach (GraphicEdge edge in edges)
                System.err.Printf(" (%s %d)", edge.GetId(), edge.GetMultiIndex());
            System.err.Printf("%n");
        }
    }

    public virtual void ElementStyleChanged(Element element, StyleGroup oldStyle, StyleGroup style)
    {
        if (element is GraphicElement)
        {
            GraphicElement ge = (GraphicElement)element;
            ge.style = style;
            graphChanged = true;
        }
        else if (element is GraphicGraph)
        {
            GraphicGraph gg = (GraphicGraph)element;
            gg.style = style;
            graphChanged = true;
        }
        else
        {
            throw new Exception("WTF ?");
        }
    }

    public virtual void StyleChanged(StyleGroup style)
    {
    }

    public override Stream<Node> Nodes()
    {
        return styleGroups.Nodes();
    }

    public override Stream<Edge> Edges()
    {
        return styleGroups.Edges();
    }

    public virtual Stream<GraphicSprite> Sprites()
    {
        return styleGroups.Sprites();
    }

    public virtual IEnumerator<Node> Iterator()
    {
        return (IEnumerator<Node>)styleGroups.GetNodeIterator();
    }

    public virtual void AddSink(Sink listener)
    {
        listeners.AddSink(listener);
    }

    public virtual void RemoveSink(Sink listener)
    {
        listeners.RemoveSink(listener);
    }

    public virtual void AddAttributeSink(AttributeSink listener)
    {
        listeners.AddAttributeSink(listener);
    }

    public virtual void RemoveAttributeSink(AttributeSink listener)
    {
        listeners.RemoveAttributeSink(listener);
    }

    public virtual void AddElementSink(ElementSink listener)
    {
        listeners.AddElementSink(listener);
    }

    public virtual void RemoveElementSink(ElementSink listener)
    {
        listeners.RemoveElementSink(listener);
    }

    public virtual Iterable<AttributeSink> AttributeSinks()
    {
        return listeners.AttributeSinks();
    }

    public virtual Iterable<ElementSink> ElementSinks()
    {
        return listeners.ElementSinks();
    }

    public override Edge AddEdge(string id, string from, string to, bool directed)
    {
        GraphicEdge edge = (GraphicEdge)styleGroups.GetEdge(id);
        if (edge == null)
        {
            GraphicNode n1 = (GraphicNode)styleGroups.GetNode(from);
            GraphicNode n2 = (GraphicNode)styleGroups.GetNode(to);
            if (n1 == null)
                throw new ElementNotFoundException("node \"%s\"", from);
            if (n2 == null)
                throw new ElementNotFoundException("node \"%s\"", to);
            edge = new GraphicEdge(id, n1, n2, directed, null);
            styleGroups.AddElement(edge);
            IList<GraphicEdge> l1 = connectivity[n1];
            IList<GraphicEdge> l2 = connectivity[n2];
            if (l1 == null)
            {
                l1 = new List<GraphicEdge>();
                connectivity.Put(n1, l1);
            }

            if (l2 == null)
            {
                l2 = new List<GraphicEdge>();
                connectivity.Put(n2, l2);
            }

            l1.Add(edge);
            l2.Add(edge);
            edge.CountSameEdges(l1);
            graphChanged = true;
            listeners.SendEdgeAdded(id, from, to, directed);
        }

        return edge;
    }

    public override Node AddNode(string id)
    {
        GraphicNode node = (GraphicNode)styleGroups.GetNode(id);
        if (node == null)
        {
            node = new GraphicNode(this, id, null);
            styleGroups.AddElement(node);
            graphChanged = true;
            listeners.SendNodeAdded(id);
        }

        return node;
    }

    public override void Clear()
    {
        listeners.SendGraphCleared();
        ClearAttributesWithNoEvent();
        connectivity.Clear();
        styleGroups.Clear();
        styleSheet.Clear();
        step = 0;
        graphChanged = true;
        styleGroups.AddElement(this);
        style = styleGroups.GetStyleFor(this);
    }

    public override Edge RemoveEdge(string id)
    {
        GraphicEdge edge = (GraphicEdge)styleGroups.GetEdge(id);
        if (edge != null)
        {
            listeners.SendEdgeRemoved(id);
            if (connectivity[edge.from] != null)
                connectivity[edge.from].Remove(edge);
            if (connectivity[edge.to] != null)
                connectivity[edge.to].Remove(edge);
            styleGroups.RemoveElement(edge);
            edge.Removed();
            graphChanged = true;
        }

        return edge;
    }

    public override Edge RemoveEdge(string from, string to)
    {
        GraphicNode node0 = (GraphicNode)styleGroups.GetNode(from);
        GraphicNode node1 = (GraphicNode)styleGroups.GetNode(to);
        if (node0 != null && node1 != null)
        {
            Collection<GraphicEdge> edges0 = connectivity[node0];
            Collection<GraphicEdge> edges1 = connectivity[node1];
            foreach (GraphicEdge edge0 in edges0)
            {
                foreach (GraphicEdge edge1 in edges1)
                {
                    if (edge0 == edge1)
                    {
                        RemoveEdge(edge0.GetId());
                        return edge0;
                    }
                }
            }
        }

        return null;
    }

    public override Node RemoveNode(string id)
    {
        GraphicNode node = (GraphicNode)styleGroups.GetNode(id);
        if (node != null)
        {
            listeners.SendNodeRemoved(id);
            if (connectivity[node] != null)
            {
                IList<GraphicEdge> l = new List<GraphicEdge>(connectivity[node]);
                foreach (GraphicEdge edge in l)
                    RemoveEdge(edge.GetId());
                connectivity.Remove(node);
            }

            styleGroups.RemoveElement(node);
            node.Removed();
            graphChanged = true;
        }

        return node;
    }

    public virtual Viewer Display()
    {
        throw new Exception("GraphicGraph is used by display() and cannot recursively define display()");
    }

    public virtual Viewer Display(bool autoLayout)
    {
        throw new Exception("GraphicGraph is used by display() and cannot recursively define display()");
    }

    public override void StepBegins(double step)
    {
        listeners.SendStepBegins(step);
        this.step = step;
    }

    public override EdgeFactory<TWildcardTodoEdge> EdgeFactory()
    {
        throw new Exception("GraphicGraph does not support EdgeFactory");
    }

    public override int GetEdgeCount()
    {
        return styleGroups.GetEdgeCount();
    }

    public override int GetNodeCount()
    {
        return styleGroups.GetNodeCount();
    }

    public virtual int GetSpriteCount()
    {
        return styleGroups.GetSpriteCount();
    }

    public override bool IsAutoCreationEnabled()
    {
        return false;
    }

    public override NodeFactory<TWildcardTodoNode> NodeFactory()
    {
        throw new Exception("GraphicGraph does not support NodeFactory");
    }

    public override void SetAutoCreate(bool on)
    {
        throw new Exception("GraphicGraph does not support auto-creation");
    }

    public override bool IsStrict()
    {
        return false;
    }

    public override void SetStrict(bool on)
    {
        throw new Exception("GraphicGraph does not support strict checking");
    }

    public override void SetEdgeFactory(EdgeFactory<TWildcardTodoEdge> ef)
    {
        throw new Exception("you cannot change the edge factory for graphic graphs !");
    }

    public override void SetNodeFactory(NodeFactory<TWildcardTodoNode> nf)
    {
        throw new Exception("you cannot change the node factory for graphic graphs !");
    }

    public override void Read(string filename)
    {
        throw new Exception("GraphicGraph does not support I/O");
    }

    public override void Read(FileSource input, string filename)
    {
        throw new Exception("GraphicGraph does not support I/O");
    }

    public override void Write(FileSink output, string filename)
    {
        throw new Exception("GraphicGraph does not support I/O");
    }

    public override void Write(string filename)
    {
        throw new Exception("GraphicGraph does not support I/O");
    }

    public override void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        listeners.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
    }

    public override void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        listeners.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public override void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        listeners.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
    }

    public override void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        listeners.GraphAttributeAdded(sourceId, timeId, attribute, value);
    }

    public override void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        listeners.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
    }

    public override void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        listeners.GraphAttributeRemoved(sourceId, timeId, attribute);
    }

    public override void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        listeners.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
    }

    public override void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        listeners.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public override void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        listeners.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
    }

    public override void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        listeners.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public override void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        listeners.EdgeRemoved(sourceId, timeId, edgeId);
    }

    public override void GraphCleared(string sourceId, long timeId)
    {
        listeners.GraphCleared(sourceId, timeId);
    }

    public override void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        listeners.NodeAdded(sourceId, timeId, nodeId);
    }

    public override void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        listeners.NodeRemoved(sourceId, timeId, nodeId);
    }

    public override void StepBegins(string sourceId, long timeId, double time)
    {
        listeners.SendStepBegins(sourceId, timeId, time);
        StepBegins(time);
    }

    protected virtual void SpriteAttribute(AttributeChangeEvent @event, Element element, string attribute, object value)
    {
        string spriteId = attribute.Substring(10);
        int pos = spriteId.IndexOf('.');
        string attr = null;
        if (pos > 0)
        {
            attr = spriteId.Substring(pos + 1);
            spriteId = spriteId.Substring(0, pos);
        }

        if (attr == null)
        {
            AddOrChangeSprite(@event, element, spriteId, value);
        }
        else
        {
            if (@event == AttributeChangeEvent.ADD)
            {
                GraphicSprite sprite = styleGroups.GetSprite(spriteId);
                if (sprite == null)
                {
                    AddOrChangeSprite(AttributeChangeEvent.ADD, element, spriteId, null);
                    sprite = styleGroups.GetSprite(spriteId);
                }

                sprite.SetAttribute(attr, value);
            }
            else if (@event == AttributeChangeEvent.CHANGE)
            {
                GraphicSprite sprite = styleGroups.GetSprite(spriteId);
                if (sprite == null)
                {
                    AddOrChangeSprite(AttributeChangeEvent.ADD, element, spriteId, null);
                    sprite = styleGroups.GetSprite(spriteId);
                }

                sprite.SetAttribute(attr, value);
            }
            else if (@event == AttributeChangeEvent.REMOVE)
            {
                GraphicSprite sprite = styleGroups.GetSprite(spriteId);
                if (sprite != null)
                    sprite.RemoveAttribute(attr);
            }
        }
    }

    protected virtual void AddOrChangeSprite(AttributeChangeEvent @event, Element element, string spriteId, object value)
    {
        if (@event == AttributeChangeEvent.ADD || @event == AttributeChangeEvent.CHANGE)
        {
            GraphicSprite sprite = styleGroups.GetSprite(spriteId);
            if (sprite == null)
                sprite = AddSprite_(spriteId);
            if (element != null)
            {
                if (element is GraphicNode)
                    sprite.AttachToNode((GraphicNode)element);
                else if (element is GraphicEdge)
                    sprite.AttachToEdge((GraphicEdge)element);
            }

            if (value != null && (!(value is bool)))
                PositionSprite(sprite, value);
        }
        else if (@event == AttributeChangeEvent.REMOVE)
        {
            if (element == null)
            {
                if (styleGroups.GetSprite(spriteId) != null)
                {
                    RemoveSprite_(spriteId);
                }
            }
            else
            {
                GraphicSprite sprite = styleGroups.GetSprite(spriteId);
                if (sprite != null)
                    sprite.Detach();
            }
        }
    }

    public virtual GraphicSprite AddSprite(string id)
    {
        string prefix = String.Format("ui.sprite.%s", id);
        logger.Info(String.Format("Added sprite %s.", id));
        SetAttribute(prefix, 0, 0, 0);
        GraphicSprite s = styleGroups.GetSprite(id);
        return s;
    }

    protected virtual GraphicSprite AddSprite_(string id)
    {
        GraphicSprite s = new GraphicSprite(id, this);
        styleGroups.AddElement(s);
        graphChanged = true;
        return s;
    }

    public virtual void RemoveSprite(string id)
    {
        string prefix = String.Format("ui.sprite.%s", id);
        RemoveAttribute(prefix);
    }

    protected virtual GraphicSprite RemoveSprite_(string id)
    {
        GraphicSprite sprite = (GraphicSprite)styleGroups.GetSprite(id);
        if (sprite != null)
        {
            sprite.Detach();
            styleGroups.RemoveElement(sprite);
            sprite.Removed();
            graphChanged = true;
        }

        return sprite;
    }

    protected virtual void PositionSprite(GraphicSprite sprite, object value)
    {
        if (value is Object[])
        {
            object[] values = (Object[])value;
            if (values.Length == 4)
            {
                if (values[0] is Number && values[1] is Number && values[2] is Number && values[3] is Style.Units)
                {
                    sprite.SetPosition(((Number)values[0]).DoubleValue(), ((Number)values[1]).DoubleValue(), ((Number)values[2]).DoubleValue(), (Style.Units)values[3]);
                }
                else
                {
                    logger.Warning("Cannot parse values[4] for sprite position.");
                }
            }
            else if (values.Length == 3)
            {
                if (values[0] is Number && values[1] is Number && values[2] is Number)
                {
                    sprite.SetPosition(((Number)values[0]).DoubleValue(), ((Number)values[1]).DoubleValue(), ((Number)values[2]).DoubleValue(), Units.GU);
                }
                else
                {
                    logger.Warning("Cannot parse values[3] for sprite position.");
                }
            }
            else if (values.Length == 1)
            {
                if (values[0] is Number)
                {
                    sprite.SetPosition(((Number)values[0]).DoubleValue());
                }
                else
                {
                    logger.Warning("Sprite position percent is not a number.");
                }
            }
            else
            {
                logger.Warning(String.Format("Cannot transform value '%s' (length=%d) into a position%n", Arrays.ToString(values), values.Length));
            }
        }
        else if (value is Number)
        {
            sprite.SetPosition(((Number)value).DoubleValue());
        }
        else if (value is Value)
        {
            sprite.SetPosition(((Value)value).value);
        }
        else if (value is Values)
        {
            sprite.SetPosition((Values)value);
        }
        else if (value == null)
        {
            throw new Exception("What do you expect with a null value ?");
        }
        else
        {
            logger.Warning(String.Format("Cannot place sprite with posiiton '%s' (instance of %s)%n", value, value.GetType().GetName()));
        }
    }

    public override void ClearAttributeSinks()
    {
        listeners.ClearAttributeSinks();
    }

    public override void ClearElementSinks()
    {
        listeners.ClearElementSinks();
    }

    public override void ClearSinks()
    {
        listeners.ClearSinks();
    }

    public override Edge AddEdge(string id, int index1, int index2)
    {
        throw new Exception("not implemented !");
    }

    public override Edge AddEdge(string id, int fromIndex, int toIndex, bool directed)
    {
        throw new Exception("not implemented !");
    }

    public override Edge AddEdge(string id, Node node1, Node node2)
    {
        throw new Exception("not implemented !");
    }

    public override Edge AddEdge(string id, Node from, Node to, bool directed)
    {
        throw new Exception("not implemented !");
    }

    public override Edge GetEdge(int index)
    {
        throw new Exception("not implemented !");
    }

    public override Node GetNode(int index)
    {
        throw new Exception("not implemented !");
    }

    public override Edge RemoveEdge(int index)
    {
        throw new Exception("not implemented !");
    }

    public override Edge RemoveEdge(int fromIndex, int toIndex)
    {
        throw new Exception("not implemented !");
    }

    public override Edge RemoveEdge(Node node1, Node node2)
    {
        throw new Exception("not implemented !");
    }

    public override Edge RemoveEdge(Edge edge)
    {
        throw new Exception("not implemented !");
    }

    public override Node RemoveNode(int index)
    {
        throw new Exception("not implemented !");
    }

    public override Node RemoveNode(Node node)
    {
        throw new Exception("not implemented !");
    }

    public virtual void Replay()
    {
        AttributeKeys().ForEach((key) =>
        {
            listeners.SendGraphAttributeAdded(id, key, GetAttribute(key));
        });
        Nodes().ForEach((node) =>
        {
            listeners.SendNodeAdded(id, node.GetId());
            node.AttributeKeys().ForEach((key) =>
            {
                listeners.SendNodeAttributeAdded(id, node.GetId(), key, node.GetAttribute(key));
            });
        });
        Edges().ForEach((edge) =>
        {
            listeners.SendEdgeAdded(id, edge.GetId(), edge.GetSourceNode().GetId(), edge.GetTargetNode().GetId(), edge.IsDirected());
            edge.AttributeKeys().ForEach((key) =>
            {
                listeners.SendEdgeAttributeAdded(id, edge.GetId(), key, edge.GetAttribute(key));
            });
        });
    }
}
