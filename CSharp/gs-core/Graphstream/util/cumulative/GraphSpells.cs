using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Stream;
using Java.Util;
using Java.Util.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Cumulative.ElementType;
using static Org.Graphstream.Util.Cumulative.EventType;
using static Org.Graphstream.Util.Cumulative.AttributeChangeEvent;
using static Org.Graphstream.Util.Cumulative.Mode;
using static Org.Graphstream.Util.Cumulative.What;
using static Org.Graphstream.Util.Cumulative.TimeFormat;
using static Org.Graphstream.Util.Cumulative.OutputType;
using static Org.Graphstream.Util.Cumulative.OutputPolicy;
using static Org.Graphstream.Util.Cumulative.LayoutPolicy;
using static Org.Graphstream.Util.Cumulative.Quality;
using static Org.Graphstream.Util.Cumulative.Option;
using static Org.Graphstream.Util.Cumulative.AttributeType;
using static Org.Graphstream.Util.Cumulative.Balise;
using static Org.Graphstream.Util.Cumulative.GEXFAttribute;
using static Org.Graphstream.Util.Cumulative.METAAttribute;
using static Org.Graphstream.Util.Cumulative.GRAPHAttribute;
using static Org.Graphstream.Util.Cumulative.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Cumulative.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Cumulative.NODESAttribute;
using static Org.Graphstream.Util.Cumulative.NODEAttribute;
using static Org.Graphstream.Util.Cumulative.ATTVALUEAttribute;
using static Org.Graphstream.Util.Cumulative.PARENTAttribute;
using static Org.Graphstream.Util.Cumulative.EDGESAttribute;
using static Org.Graphstream.Util.Cumulative.SPELLAttribute;
using static Org.Graphstream.Util.Cumulative.COLORAttribute;
using static Org.Graphstream.Util.Cumulative.POSITIONAttribute;
using static Org.Graphstream.Util.Cumulative.SIZEAttribute;
using static Org.Graphstream.Util.Cumulative.NODESHAPEAttribute;
using static Org.Graphstream.Util.Cumulative.EDGEAttribute;
using static Org.Graphstream.Util.Cumulative.THICKNESSAttribute;
using static Org.Graphstream.Util.Cumulative.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Cumulative.IDType;
using static Org.Graphstream.Util.Cumulative.ModeType;
using static Org.Graphstream.Util.Cumulative.WeightType;
using static Org.Graphstream.Util.Cumulative.EdgeType;
using static Org.Graphstream.Util.Cumulative.NodeShapeType;
using static Org.Graphstream.Util.Cumulative.EdgeShapeType;
using static Org.Graphstream.Util.Cumulative.ClassType;
using static Org.Graphstream.Util.Cumulative.TimeFormatType;
using static Org.Graphstream.Util.Cumulative.GPXAttribute;
using static Org.Graphstream.Util.Cumulative.WPTAttribute;
using static Org.Graphstream.Util.Cumulative.LINKAttribute;
using static Org.Graphstream.Util.Cumulative.EMAILAttribute;
using static Org.Graphstream.Util.Cumulative.PTAttribute;
using static Org.Graphstream.Util.Cumulative.BOUNDSAttribute;
using static Org.Graphstream.Util.Cumulative.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Cumulative.FixType;
using static Org.Graphstream.Util.Cumulative.GraphAttribute;
using static Org.Graphstream.Util.Cumulative.LocatorAttribute;
using static Org.Graphstream.Util.Cumulative.NodeAttribute;
using static Org.Graphstream.Util.Cumulative.EdgeAttribute;
using static Org.Graphstream.Util.Cumulative.DataAttribute;
using static Org.Graphstream.Util.Cumulative.PortAttribute;
using static Org.Graphstream.Util.Cumulative.EndPointAttribute;
using static Org.Graphstream.Util.Cumulative.EndPointType;
using static Org.Graphstream.Util.Cumulative.HyperEdgeAttribute;
using static Org.Graphstream.Util.Cumulative.KeyAttribute;
using static Org.Graphstream.Util.Cumulative.KeyDomain;
using static Org.Graphstream.Util.Cumulative.KeyAttrType;
using static Org.Graphstream.Util.Cumulative.GraphEvents;
using static Org.Graphstream.Util.Cumulative.ThreadingModel;
using static Org.Graphstream.Util.Cumulative.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Cumulative;

public class GraphSpells : Sink
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphSpells).GetSimpleName());
    CumulativeSpells graph;
    CumulativeAttributes graphAttributes;
    HashMap<string, CumulativeSpells> nodes;
    HashMap<string, CumulativeAttributes> nodesAttributes;
    HashMap<string, CumulativeSpells> edges;
    HashMap<string, CumulativeAttributes> edgesAttributes;
    HashMap<string, EdgeData> edgesData;
    double date;
    public GraphSpells()
    {
        graph = new CumulativeSpells();
        graphAttributes = new CumulativeAttributes(0);
        nodes = new HashMap<string, CumulativeSpells>();
        nodesAttributes = new HashMap<string, CumulativeAttributes>();
        edges = new HashMap<string, CumulativeSpells>();
        edgesAttributes = new HashMap<string, CumulativeAttributes>();
        edgesData = new HashMap<string, EdgeData>();
        date = Double.NaN;
    }

    public class EdgeData
    {
        string source;
        string target;
        bool directed;
        public virtual string GetSource()
        {
            return source;
        }

        public virtual string GetTarget()
        {
            return target;
        }

        public virtual bool IsDirected()
        {
            return directed;
        }
    }

    public virtual Iterable<string> GetNodes()
    {
        return nodes.KeySet();
    }

    public virtual Iterable<string> GetEdges()
    {
        return edges.KeySet();
    }

    public virtual CumulativeSpells GetNodeSpells(string nodeId)
    {
        return nodes[nodeId];
    }

    public virtual CumulativeAttributes GetNodeAttributes(string nodeId)
    {
        return nodesAttributes[nodeId];
    }

    public virtual CumulativeSpells GetEdgeSpells(string edgeId)
    {
        return edges[edgeId];
    }

    public virtual CumulativeAttributes GetEdgeAttributes(string edgeId)
    {
        return edgesAttributes[edgeId];
    }

    public virtual EdgeData GetEdgeData(string edgeId)
    {
        return edgesData[edgeId];
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        this.date = step;
        graphAttributes.UpdateDate(step);
        graph.UpdateCurrentSpell(step);
        foreach (string id in nodes.KeySet())
        {
            nodes[id].UpdateCurrentSpell(step);
            nodesAttributes[id].UpdateDate(step);
        }

        foreach (string id in edges.KeySet())
        {
            edges[id].UpdateCurrentSpell(step);
            edgesAttributes[id].UpdateDate(step);
        }
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        if (!nodes.ContainsKey(nodeId))
        {
            nodes.Put(nodeId, new CumulativeSpells());
            nodesAttributes.Put(nodeId, new CumulativeAttributes(date));
        }

        nodes[nodeId].StartSpell(date);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        if (nodes.ContainsKey(nodeId))
        {
            nodes[nodeId].CloseSpell();
            nodesAttributes[nodeId].Remove();
        }
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        if (!edges.ContainsKey(edgeId))
        {
            edges.Put(edgeId, new CumulativeSpells());
            edgesAttributes.Put(edgeId, new CumulativeAttributes(date));
            EdgeData data = new EdgeData();
            data.source = fromNodeId;
            data.target = toNodeId;
            data.directed = directed;
            edgesData.Put(edgeId, data);
        }

        edges[edgeId].StartSpell(date);
        EdgeData data = edgesData[edgeId];
        if (!data.source.Equals(fromNodeId) || !data.target.Equals(toNodeId) || data.directed != directed)
            logger.Warning("An edge with this id but different properties" + " has already be created in the past.");
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        if (edges.ContainsKey(edgeId))
        {
            edges[edgeId].CloseSpell();
            edgesAttributes[edgeId].Remove();
        }
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        foreach (string id in nodes.KeySet())
        {
            nodes[id].CloseSpell();
            nodesAttributes[id].Remove();
        }

        foreach (string id in edges.KeySet())
        {
            edges[id].CloseSpell();
            edgesAttributes[id].Remove();
        }
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        graphAttributes[attribute] = value;
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        graphAttributes[attribute] = newValue;
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        graphAttributes.Remove(attribute);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        nodesAttributes[nodeId][attribute] = value;
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        nodesAttributes[nodeId][attribute] = newValue;
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        nodesAttributes[nodeId].Remove(attribute);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        edgesAttributes[edgeId][attribute] = value;
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        edgesAttributes[edgeId][attribute] = newValue;
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        edgesAttributes[edgeId].Remove(attribute);
    }

    public virtual string ToString()
    {
        StringBuilder buffer = new StringBuilder();
        foreach (string id in nodes.KeySet())
        {
            buffer.Append("node#\"").Append(id).Append("\" ").Append(nodes[id]).Append(" ").Append(nodesAttributes[id]).Append("\n");
        }

        foreach (string id in edges.KeySet())
        {
            buffer.Append("edge#\"").Append(id).Append("\" ").Append(edges[id]).Append("\n");
        }

        return buffer.ToString();
    }

    public static void Main(params string[] args)
    {
        GraphSpells graphSpells = new GraphSpells();
        Graph g = new AdjacencyListGraph("g");
        g.AddSink(graphSpells);
        g.AddNode("A");
        g.AddNode("B");
        g.AddNode("C");
        g.StepBegins(1);
        g.GetNode("A").SetAttribute("test1", 100);
        g.AddEdge("AB", "A", "B");
        g.AddEdge("AC", "A", "C");
        g.StepBegins(2);
        g.AddEdge("CB", "C", "B");
        g.RemoveNode("A");
        g.StepBegins(3);
        g.AddNode("A");
        g.AddEdge("AB", "A", "B");
        g.StepBegins(4);
        g.RemoveNode("C");
        g.StepBegins(5);
        System.@out.Println(graphSpells);
    }
}
