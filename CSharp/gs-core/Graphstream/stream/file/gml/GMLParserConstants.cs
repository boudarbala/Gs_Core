using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Gml.ElementType;
using static Org.Graphstream.Stream.File.Gml.EventType;
using static Org.Graphstream.Stream.File.Gml.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Gml.Mode;
using static Org.Graphstream.Stream.File.Gml.What;
using static Org.Graphstream.Stream.File.Gml.TimeFormat;
using static Org.Graphstream.Stream.File.Gml.OutputType;
using static Org.Graphstream.Stream.File.Gml.OutputPolicy;
using static Org.Graphstream.Stream.File.Gml.LayoutPolicy;
using static Org.Graphstream.Stream.File.Gml.Quality;
using static Org.Graphstream.Stream.File.Gml.Option;
using static Org.Graphstream.Stream.File.Gml.AttributeType;
using static Org.Graphstream.Stream.File.Gml.Balise;
using static Org.Graphstream.Stream.File.Gml.GEXFAttribute;
using static Org.Graphstream.Stream.File.Gml.METAAttribute;
using static Org.Graphstream.Stream.File.Gml.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESAttribute;
using static Org.Graphstream.Stream.File.Gml.NODEAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Gml.PARENTAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESAttribute;
using static Org.Graphstream.Stream.File.Gml.SPELLAttribute;
using static Org.Graphstream.Stream.File.Gml.COLORAttribute;
using static Org.Graphstream.Stream.File.Gml.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Gml.SIZEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGEAttribute;
using static Org.Graphstream.Stream.File.Gml.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.IDType;
using static Org.Graphstream.Stream.File.Gml.ModeType;
using static Org.Graphstream.Stream.File.Gml.WeightType;
using static Org.Graphstream.Stream.File.Gml.EdgeType;
using static Org.Graphstream.Stream.File.Gml.NodeShapeType;
using static Org.Graphstream.Stream.File.Gml.EdgeShapeType;
using static Org.Graphstream.Stream.File.Gml.ClassType;
using static Org.Graphstream.Stream.File.Gml.TimeFormatType;
using static Org.Graphstream.Stream.File.Gml.GPXAttribute;
using static Org.Graphstream.Stream.File.Gml.WPTAttribute;
using static Org.Graphstream.Stream.File.Gml.LINKAttribute;
using static Org.Graphstream.Stream.File.Gml.EMAILAttribute;
using static Org.Graphstream.Stream.File.Gml.PTAttribute;
using static Org.Graphstream.Stream.File.Gml.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Gml.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Gml.FixType;
using static Org.Graphstream.Stream.File.Gml.GraphAttribute;
using static Org.Graphstream.Stream.File.Gml.LocatorAttribute;
using static Org.Graphstream.Stream.File.Gml.NodeAttribute;
using static Org.Graphstream.Stream.File.Gml.EdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.DataAttribute;
using static Org.Graphstream.Stream.File.Gml.PortAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointType;
using static Org.Graphstream.Stream.File.Gml.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyDomain;
using static Org.Graphstream.Stream.File.Gml.KeyAttrType;
using static Org.Graphstream.Stream.File.Gml.GraphEvents;
using static Org.Graphstream.Stream.File.Gml.ThreadingModel;
using static Org.Graphstream.Stream.File.Gml.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Gml.Measures;
using static Org.Graphstream.Stream.File.Gml.Token;
using static Org.Graphstream.Stream.File.Gml.Extension;
using static Org.Graphstream.Stream.File.Gml.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Gml.AttrType;

namespace Gs_Core.Graphstream.Stream.File.Gml;

public interface GMLParserConstants
{
    int EOF = 0;
    int EOL = 5;
    int DIGIT = 6;
    int HEXDIGIT = 7;
    int LSQBR = 8;
    int RSQBR = 9;
    int REAL = 10;
    int STRING = 11;
    int GRAPH = 12;
    int DIGRAPH = 13;
    int KEY = 14;
    int COMMENT = 15;
    int DEFAULT = 0;
    string[] tokenImage = new[]
    {
        "<EOF>",
        "\" \"",
        "\"\\r\"",
        "\"\\t\"",
        "\"\\n\"",
        "<EOL>",
        "<DIGIT>",
        "<HEXDIGIT>",
        "\"[\"",
        "\"]\"",
        "<REAL>",
        "<STRING>",
        "\"graph\"",
        "\"digraph\"",
        "<KEY>",
        "<COMMENT>"
    };
}
