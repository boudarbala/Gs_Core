using Java.Net;
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

namespace Gs_Core.Graphstream.Stream.File.Gexf;

public class GEXFAttributes : GEXFElement, AttributeSink
{
    GEXF root;
    ClassType type;
    Mode mode;
    HashMap<string, GEXFAttribute> attributes;
    public GEXFAttributes(GEXF root, ClassType type)
    {
        this.root = root;
        this.type = type;
        this.mode = Mode.STATIC;
        this.attributes = new HashMap<string, GEXFAttribute>();
        root.AddAttributeSink(this);
    }

    protected virtual void CheckAttribute(string key, object value)
    {
        AttrType type = DetectType(value);
        if (!attributes.ContainsKey(key))
            attributes.Put(key, new GEXFAttribute(root, key, type));
        else
        {
            GEXFAttribute a = attributes[key];
            if (a.type != type && value != null)
                a.type = AttrType.STRING;
        }
    }

    protected virtual AttrType DetectType(object value)
    {
        if (value == null)
            return AttrType.STRING;
        if (value is int || value is Short)
            return AttrType.INTEGER;
        else if (value is long)
            return AttrType.LONG;
        else if (value is float)
            return AttrType.FLOAT;
        else if (value is Double)
            return AttrType.DOUBLE;
        else if (value is bool)
            return AttrType.BOOLEAN;
        else if (value is URL || value is URI)
            return AttrType.ANYURI;
        else if (value.GetType().IsArray() || value is Collection)
            return AttrType.LISTSTRING;
        return AttrType.STRING;
    }

    public virtual void Export(SmartXMLWriter stream)
    {
        if (attributes.Count == 0)
            return;
        stream.StartElement("attributes");
        stream.stream.WriteAttribute("class", type.qname);
        foreach (GEXFAttribute attribute in attributes.Values())
            attribute.Export(stream);
        stream.EndElement();
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        if (type == ClassType.NODE)
            CheckAttribute(attribute, value);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (type == ClassType.NODE)
            CheckAttribute(attribute, newValue);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        if (type == ClassType.EDGE)
            CheckAttribute(attribute, value);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (type == ClassType.EDGE)
            CheckAttribute(attribute, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
    }
}
