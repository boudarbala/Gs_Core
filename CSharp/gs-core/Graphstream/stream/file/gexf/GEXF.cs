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

public class GEXF : PipeBase, GEXFElement
{
    public static readonly string XMLNS = "http://www.gexf.net/1.2draft";
    public static readonly string XMLNS_XSI = "http://www.w3.org/2001/XMLSchema-instance";
    public static readonly string XMLNS_SL = "http://www.gexf.net/1.2draft http://www.gexf.net/1.2draft/gexf.xsd";
    public static readonly string XMLNS_VIZ = "http://www.gexf.net/1.2draft/viz";
    public static readonly string VERSION = "1.2";
    GEXFMeta meta;
    GEXFGraph graph;
    int currentAttributeIndex;
    double step;
    HashSet<Extension> extensions;
    TimeFormat timeFormat;
    public GEXF()
    {
        meta = new GEXFMeta();
        currentAttributeIndex = 0;
        step = 0;
        graph = new GEXFGraph(this);
        timeFormat = TimeFormat.DOUBLE;
        extensions = new HashSet<Extension>();
        extensions.Add(Extension.DATA);
        extensions.Add(Extension.DYNAMICS);
        extensions.Add(Extension.VIZ);
    }

    public virtual TimeFormat GetTimeFormat()
    {
        return timeFormat;
    }

    public virtual bool IsExtensionEnable(Extension ext)
    {
        return extensions.Contains(ext);
    }

    public virtual void Disable(Extension ext)
    {
        extensions.Remove(ext);
    }

    public virtual void Enable(Extension ext)
    {
        extensions.Add(ext);
    }

    public virtual void Export(SmartXMLWriter stream)
    {
        stream.StartElement("gexf");
        stream.stream.WriteAttribute("xmlns", XMLNS);
        stream.stream.WriteAttribute("xmlns:xsi", XMLNS_XSI);
        if (IsExtensionEnable(Extension.VIZ))
            stream.stream.WriteAttribute("xmlns:viz", XMLNS_VIZ);
        stream.stream.WriteAttribute("xsi:schemaLocation", XMLNS_SL);
        stream.stream.WriteAttribute("version", VERSION);
        meta.Export(stream);
        graph.Export(stream);
        stream.EndElement();
    }

    virtual int GetNewAttributeIndex()
    {
        return currentAttributeIndex++;
    }

    virtual GEXFAttribute GetNodeAttribute(string key)
    {
        return graph.nodesAttributes.attributes[key];
    }

    virtual GEXFAttribute GetEdgeAttribute(string key)
    {
        return graph.edgesAttributes.attributes[key];
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        this.step = step;
        base.StepBegins(sourceId, timeId, step);
    }
}
