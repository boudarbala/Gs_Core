using Java.Lang.Reflect;
using Java.Util;
using Javax.Xml.Stream;
using Gs_Core.Graphstream.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Gexf.ElementType;
using static Org.Graphstream.Stream.File.Gexf.EventType;
using static Org.Graphstream.Stream.File.Gexf.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Gexf.Mode;
using static Org.Graphstream.Stream.File.Gexf.What;
using static Org.Graphstream.Stream.File.Gexf.TimeFormat;
using static Org.Graphstream.Stream.File.Gexf.OutputType;
using static Org.Graphstream.Stream.File.Gexf.OutputPolicy;
using static Org.Graphstream.Stream.File.Gexf.LayoutPolicy;
using static Org.Graphstream.Stream.File.Gexf.Quality;
using static Org.Graphstream.Stream.File.Gexf.Option;
using static Org.Graphstream.Stream.File.Gexf.AttributeType;
using static Org.Graphstream.Stream.File.Gexf.Balise;
using static Org.Graphstream.Stream.File.Gexf.GEXFAttribute;
using static Org.Graphstream.Stream.File.Gexf.METAAttribute;
using static Org.Graphstream.Stream.File.Gexf.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODESAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODEAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Gexf.PARENTAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGESAttribute;
using static Org.Graphstream.Stream.File.Gexf.SPELLAttribute;
using static Org.Graphstream.Stream.File.Gexf.COLORAttribute;
using static Org.Graphstream.Stream.File.Gexf.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Gexf.SIZEAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGEAttribute;
using static Org.Graphstream.Stream.File.Gexf.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gexf.IDType;
using static Org.Graphstream.Stream.File.Gexf.ModeType;
using static Org.Graphstream.Stream.File.Gexf.WeightType;
using static Org.Graphstream.Stream.File.Gexf.EdgeType;
using static Org.Graphstream.Stream.File.Gexf.NodeShapeType;
using static Org.Graphstream.Stream.File.Gexf.EdgeShapeType;
using static Org.Graphstream.Stream.File.Gexf.ClassType;
using static Org.Graphstream.Stream.File.Gexf.TimeFormatType;
using static Org.Graphstream.Stream.File.Gexf.GPXAttribute;
using static Org.Graphstream.Stream.File.Gexf.WPTAttribute;
using static Org.Graphstream.Stream.File.Gexf.LINKAttribute;
using static Org.Graphstream.Stream.File.Gexf.EMAILAttribute;
using static Org.Graphstream.Stream.File.Gexf.PTAttribute;
using static Org.Graphstream.Stream.File.Gexf.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Gexf.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Gexf.FixType;
using static Org.Graphstream.Stream.File.Gexf.GraphAttribute;
using static Org.Graphstream.Stream.File.Gexf.LocatorAttribute;
using static Org.Graphstream.Stream.File.Gexf.NodeAttribute;
using static Org.Graphstream.Stream.File.Gexf.EdgeAttribute;
using static Org.Graphstream.Stream.File.Gexf.DataAttribute;
using static Org.Graphstream.Stream.File.Gexf.PortAttribute;
using static Org.Graphstream.Stream.File.Gexf.EndPointAttribute;
using static Org.Graphstream.Stream.File.Gexf.EndPointType;
using static Org.Graphstream.Stream.File.Gexf.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Gexf.KeyAttribute;
using static Org.Graphstream.Stream.File.Gexf.KeyDomain;
using static Org.Graphstream.Stream.File.Gexf.KeyAttrType;
using static Org.Graphstream.Stream.File.Gexf.GraphEvents;
using static Org.Graphstream.Stream.File.Gexf.ThreadingModel;
using static Org.Graphstream.Stream.File.Gexf.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Gexf.Measures;
using static Org.Graphstream.Stream.File.Gexf.Token;
using static Org.Graphstream.Stream.File.Gexf.Extension;
using static Org.Graphstream.Stream.File.Gexf.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Gexf.AttrType;

namespace Gs_Core.Graphstream.Stream.File.Gexf;

public class GEXFNodes : SinkAdapter, GEXFElement
{
    GEXF root;
    HashMap<string, GEXFNode> nodes;
    public GEXFNodes(GEXF root)
    {
        this.root = root;
        this.nodes = new HashMap<string, GEXFNode>();
        root.AddSink(this);
    }

    private float[] ConvertToXYZ(object value)
    {
        if (value == null || !value.GetType().IsArray())
            return null;
        float[] xyz = new float[Array.GetLength(value)];
        for (int i = 0; i < xyz.Length; i++)
        {
            object o = Array.Get(value, i);
            if (o is Number)
                xyz[i] = ((Number)o).FloatValue();
            else
                return null;
        }

        return xyz;
    }

    public virtual void Export(SmartXMLWriter stream)
    {
        stream.StartElement("nodes");
        foreach (GEXFNode node in nodes.Values())
            node.Export(stream);
        stream.EndElement();
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        GEXFNode node = nodes[nodeId];
        if (node == null)
        {
            node = new GEXFNode(root, nodeId);
            nodes.Put(nodeId, node);
        }

        node.spells.Start();
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        GEXFNode node = nodes[nodeId];
        if (node == null)
        {
            System.err.Printf("node removed but not added\n");
            return;
        }

        node.spells.End();
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        GEXFNode node = nodes[nodeId];
        if (("ui.label".Equals(attribute) || "label".Equals(attribute)) && value != null)
            node.label = value.ToString();
        if ("xyz".Equals(attribute))
        {
            float[] xyz = ConvertToXYZ(value);
            switch (xyz.Length)
            {
                default:
                    node.z = xyz[2];
                    break;
                case 2:
                    node.y = xyz[1];
                case 1:
                    node.x = xyz[0];
                case 0:
                    break;
            }

            node.position = true;
        }

        node.attvalues.AttributeUpdated(root.GetNodeAttribute(attribute), value);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        NodeAttributeAdded(sourceId, timeId, nodeId, attribute, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        GEXFNode node = nodes[nodeId];
        node.attvalues.AttributeUpdated(root.GetNodeAttribute(attribute), null);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        foreach (GEXFNode node in nodes.Values())
            node.spells.End();
    }
}
