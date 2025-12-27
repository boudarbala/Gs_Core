using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Dot.ElementType;
using static Org.Graphstream.Stream.File.Dot.EventType;
using static Org.Graphstream.Stream.File.Dot.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Dot.Mode;
using static Org.Graphstream.Stream.File.Dot.What;
using static Org.Graphstream.Stream.File.Dot.TimeFormat;
using static Org.Graphstream.Stream.File.Dot.OutputType;
using static Org.Graphstream.Stream.File.Dot.OutputPolicy;
using static Org.Graphstream.Stream.File.Dot.LayoutPolicy;
using static Org.Graphstream.Stream.File.Dot.Quality;
using static Org.Graphstream.Stream.File.Dot.Option;
using static Org.Graphstream.Stream.File.Dot.AttributeType;
using static Org.Graphstream.Stream.File.Dot.Balise;
using static Org.Graphstream.Stream.File.Dot.GEXFAttribute;
using static Org.Graphstream.Stream.File.Dot.METAAttribute;
using static Org.Graphstream.Stream.File.Dot.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESAttribute;
using static Org.Graphstream.Stream.File.Dot.NODEAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Dot.PARENTAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESAttribute;
using static Org.Graphstream.Stream.File.Dot.SPELLAttribute;
using static Org.Graphstream.Stream.File.Dot.COLORAttribute;
using static Org.Graphstream.Stream.File.Dot.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Dot.SIZEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGEAttribute;
using static Org.Graphstream.Stream.File.Dot.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.IDType;
using static Org.Graphstream.Stream.File.Dot.ModeType;
using static Org.Graphstream.Stream.File.Dot.WeightType;
using static Org.Graphstream.Stream.File.Dot.EdgeType;
using static Org.Graphstream.Stream.File.Dot.NodeShapeType;
using static Org.Graphstream.Stream.File.Dot.EdgeShapeType;
using static Org.Graphstream.Stream.File.Dot.ClassType;
using static Org.Graphstream.Stream.File.Dot.TimeFormatType;
using static Org.Graphstream.Stream.File.Dot.GPXAttribute;
using static Org.Graphstream.Stream.File.Dot.WPTAttribute;
using static Org.Graphstream.Stream.File.Dot.LINKAttribute;
using static Org.Graphstream.Stream.File.Dot.EMAILAttribute;
using static Org.Graphstream.Stream.File.Dot.PTAttribute;
using static Org.Graphstream.Stream.File.Dot.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Dot.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Dot.FixType;
using static Org.Graphstream.Stream.File.Dot.GraphAttribute;
using static Org.Graphstream.Stream.File.Dot.LocatorAttribute;
using static Org.Graphstream.Stream.File.Dot.NodeAttribute;
using static Org.Graphstream.Stream.File.Dot.EdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.DataAttribute;
using static Org.Graphstream.Stream.File.Dot.PortAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointType;
using static Org.Graphstream.Stream.File.Dot.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyDomain;
using static Org.Graphstream.Stream.File.Dot.KeyAttrType;
using static Org.Graphstream.Stream.File.Dot.GraphEvents;
using static Org.Graphstream.Stream.File.Dot.ThreadingModel;
using static Org.Graphstream.Stream.File.Dot.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Dot.Measures;
using static Org.Graphstream.Stream.File.Dot.Token;

namespace Gs_Core.Graphstream.Stream.File.Dot;

public interface DOTParserConstants
{
    int EOF = 0;
    int EOL = 7;
    int DIGIT = 8;
    int HEXDIGIT = 9;
    int LSQBR = 10;
    int RSQBR = 11;
    int LBRACE = 12;
    int RBRACE = 13;
    int COLON = 14;
    int COMMA = 15;
    int EQUALS = 16;
    int GRAPH = 17;
    int DIGRAPH = 18;
    int SUBGRAPH = 19;
    int NODE = 20;
    int EDGE = 21;
    int STRICT = 22;
    int EDGE_OP = 23;
    int REAL = 24;
    int STRING = 25;
    int WORD = 26;
    int DEFAULT = 0;
    string[] tokenImage = new[]
    {
        "<EOF>",
        "\" \"",
        "\"\\r\"",
        "\"\\t\"",
        "\"\\n\"",
        "<token of kind 5>",
        "<token of kind 6>",
        "<EOL>",
        "<DIGIT>",
        "<HEXDIGIT>",
        "\"[\"",
        "\"]\"",
        "\"{\"",
        "\"}\"",
        "\":\"",
        "\",\"",
        "\"=\"",
        "\"graph\"",
        "\"digraph\"",
        "\"subgraph\"",
        "\"node\"",
        "\"edge\"",
        "\"strict\"",
        "<EDGE_OP>",
        "<REAL>",
        "<STRING>",
        "<WORD>",
        "\";\"",
        "\"n\"",
        "\"ne\"",
        "\"e\"",
        "\"se\"",
        "\"s\"",
        "\"sw\"",
        "\"w\"",
        "\"nw\"",
        "\"c\"",
        "\"_\""
    };
}
