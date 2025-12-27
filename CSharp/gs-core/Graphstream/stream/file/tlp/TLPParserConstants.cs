using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Tlp.ElementType;
using static Org.Graphstream.Stream.File.Tlp.EventType;
using static Org.Graphstream.Stream.File.Tlp.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Tlp.Mode;
using static Org.Graphstream.Stream.File.Tlp.What;
using static Org.Graphstream.Stream.File.Tlp.TimeFormat;
using static Org.Graphstream.Stream.File.Tlp.OutputType;
using static Org.Graphstream.Stream.File.Tlp.OutputPolicy;
using static Org.Graphstream.Stream.File.Tlp.LayoutPolicy;
using static Org.Graphstream.Stream.File.Tlp.Quality;
using static Org.Graphstream.Stream.File.Tlp.Option;
using static Org.Graphstream.Stream.File.Tlp.AttributeType;
using static Org.Graphstream.Stream.File.Tlp.Balise;
using static Org.Graphstream.Stream.File.Tlp.GEXFAttribute;
using static Org.Graphstream.Stream.File.Tlp.METAAttribute;
using static Org.Graphstream.Stream.File.Tlp.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODEAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Tlp.PARENTAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESAttribute;
using static Org.Graphstream.Stream.File.Tlp.SPELLAttribute;
using static Org.Graphstream.Stream.File.Tlp.COLORAttribute;
using static Org.Graphstream.Stream.File.Tlp.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Tlp.SIZEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGEAttribute;
using static Org.Graphstream.Stream.File.Tlp.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.IDType;
using static Org.Graphstream.Stream.File.Tlp.ModeType;
using static Org.Graphstream.Stream.File.Tlp.WeightType;
using static Org.Graphstream.Stream.File.Tlp.EdgeType;
using static Org.Graphstream.Stream.File.Tlp.NodeShapeType;
using static Org.Graphstream.Stream.File.Tlp.EdgeShapeType;
using static Org.Graphstream.Stream.File.Tlp.ClassType;
using static Org.Graphstream.Stream.File.Tlp.TimeFormatType;
using static Org.Graphstream.Stream.File.Tlp.GPXAttribute;
using static Org.Graphstream.Stream.File.Tlp.WPTAttribute;
using static Org.Graphstream.Stream.File.Tlp.LINKAttribute;
using static Org.Graphstream.Stream.File.Tlp.EMAILAttribute;
using static Org.Graphstream.Stream.File.Tlp.PTAttribute;
using static Org.Graphstream.Stream.File.Tlp.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Tlp.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Tlp.FixType;
using static Org.Graphstream.Stream.File.Tlp.GraphAttribute;
using static Org.Graphstream.Stream.File.Tlp.LocatorAttribute;
using static Org.Graphstream.Stream.File.Tlp.NodeAttribute;
using static Org.Graphstream.Stream.File.Tlp.EdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.DataAttribute;
using static Org.Graphstream.Stream.File.Tlp.PortAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointType;
using static Org.Graphstream.Stream.File.Tlp.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyDomain;
using static Org.Graphstream.Stream.File.Tlp.KeyAttrType;
using static Org.Graphstream.Stream.File.Tlp.GraphEvents;
using static Org.Graphstream.Stream.File.Tlp.ThreadingModel;
using static Org.Graphstream.Stream.File.Tlp.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Tlp.Measures;
using static Org.Graphstream.Stream.File.Tlp.Token;
using static Org.Graphstream.Stream.File.Tlp.Extension;
using static Org.Graphstream.Stream.File.Tlp.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Tlp.AttrType;
using static Org.Graphstream.Stream.File.Tlp.Resolutions;
using static Org.Graphstream.Stream.File.Tlp.PropertyType;

namespace Gs_Core.Graphstream.Stream.File.Tlp;

public interface TLPParserConstants
{
    int EOF = 0;
    int EOL = 7;
    int DIGIT = 8;
    int HEXDIGIT = 9;
    int OBRACKET = 10;
    int CBRACKET = 11;
    int TLP = 12;
    int GRAPH = 13;
    int NODE = 14;
    int NODES = 15;
    int EDGE = 16;
    int EDGES = 17;
    int CLUSTER = 18;
    int AUTHOR = 19;
    int DATE = 20;
    int COMMENTS = 21;
    int PROPERTY = 22;
    int DEF = 23;
    int INTEGER = 24;
    int REAL = 25;
    int STRING = 26;
    int PTYPE = 27;
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
        "\"(\"",
        "\")\"",
        "\"tlp\"",
        "\"graph\"",
        "\"node\"",
        "\"nodes\"",
        "\"edge\"",
        "\"edges\"",
        "\"cluster\"",
        "\"author\"",
        "\"date\"",
        "\"comments\"",
        "\"property\"",
        "\"default\"",
        "<INTEGER>",
        "<REAL>",
        "<STRING>",
        "<PTYPE>"
    };
}
