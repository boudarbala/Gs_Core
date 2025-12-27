using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Pajek.ElementType;
using static Org.Graphstream.Stream.File.Pajek.EventType;
using static Org.Graphstream.Stream.File.Pajek.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Pajek.Mode;
using static Org.Graphstream.Stream.File.Pajek.What;
using static Org.Graphstream.Stream.File.Pajek.TimeFormat;
using static Org.Graphstream.Stream.File.Pajek.OutputType;
using static Org.Graphstream.Stream.File.Pajek.OutputPolicy;
using static Org.Graphstream.Stream.File.Pajek.LayoutPolicy;
using static Org.Graphstream.Stream.File.Pajek.Quality;
using static Org.Graphstream.Stream.File.Pajek.Option;
using static Org.Graphstream.Stream.File.Pajek.AttributeType;
using static Org.Graphstream.Stream.File.Pajek.Balise;
using static Org.Graphstream.Stream.File.Pajek.GEXFAttribute;
using static Org.Graphstream.Stream.File.Pajek.METAAttribute;
using static Org.Graphstream.Stream.File.Pajek.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODEAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Pajek.PARENTAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESAttribute;
using static Org.Graphstream.Stream.File.Pajek.SPELLAttribute;
using static Org.Graphstream.Stream.File.Pajek.COLORAttribute;
using static Org.Graphstream.Stream.File.Pajek.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Pajek.SIZEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGEAttribute;
using static Org.Graphstream.Stream.File.Pajek.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.IDType;
using static Org.Graphstream.Stream.File.Pajek.ModeType;
using static Org.Graphstream.Stream.File.Pajek.WeightType;
using static Org.Graphstream.Stream.File.Pajek.EdgeType;
using static Org.Graphstream.Stream.File.Pajek.NodeShapeType;
using static Org.Graphstream.Stream.File.Pajek.EdgeShapeType;
using static Org.Graphstream.Stream.File.Pajek.ClassType;
using static Org.Graphstream.Stream.File.Pajek.TimeFormatType;
using static Org.Graphstream.Stream.File.Pajek.GPXAttribute;
using static Org.Graphstream.Stream.File.Pajek.WPTAttribute;
using static Org.Graphstream.Stream.File.Pajek.LINKAttribute;
using static Org.Graphstream.Stream.File.Pajek.EMAILAttribute;
using static Org.Graphstream.Stream.File.Pajek.PTAttribute;
using static Org.Graphstream.Stream.File.Pajek.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Pajek.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Pajek.FixType;
using static Org.Graphstream.Stream.File.Pajek.GraphAttribute;
using static Org.Graphstream.Stream.File.Pajek.LocatorAttribute;
using static Org.Graphstream.Stream.File.Pajek.NodeAttribute;
using static Org.Graphstream.Stream.File.Pajek.EdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.DataAttribute;
using static Org.Graphstream.Stream.File.Pajek.PortAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointType;
using static Org.Graphstream.Stream.File.Pajek.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyDomain;
using static Org.Graphstream.Stream.File.Pajek.KeyAttrType;
using static Org.Graphstream.Stream.File.Pajek.GraphEvents;
using static Org.Graphstream.Stream.File.Pajek.ThreadingModel;
using static Org.Graphstream.Stream.File.Pajek.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Pajek.Measures;
using static Org.Graphstream.Stream.File.Pajek.Token;
using static Org.Graphstream.Stream.File.Pajek.Extension;
using static Org.Graphstream.Stream.File.Pajek.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Pajek.AttrType;
using static Org.Graphstream.Stream.File.Pajek.Resolutions;

namespace Gs_Core.Graphstream.Stream.File.Pajek;

public interface PajekParserConstants
{
    int EOF = 0;
    int EOL = 3;
    int DIGIT = 4;
    int HEXDIGIT = 5;
    int INT = 6;
    int REAL = 7;
    int STRING = 8;
    int NETWORK = 9;
    int VERTICES = 10;
    int ARCS = 11;
    int EDGES = 12;
    int EDGESLIST = 13;
    int ARCSLIST = 14;
    int MATRIX = 15;
    int COMMENT = 16;
    int ELLIPSE = 17;
    int DIAMOND = 18;
    int CROSS = 19;
    int BOX = 20;
    int TRIANGLE = 21;
    int EMPTY = 22;
    int SIZE = 23;
    int XFACT = 24;
    int YFACT = 25;
    int PHI = 26;
    int R = 27;
    int Q = 28;
    int IC = 29;
    int BC = 30;
    int BW = 31;
    int LC = 32;
    int LA = 33;
    int LR = 34;
    int LPHI = 35;
    int FOS = 36;
    int FONT = 37;
    int C = 38;
    int P = 39;
    int W = 40;
    int S = 41;
    int A = 42;
    int AP = 43;
    int L = 44;
    int LP = 45;
    int H1 = 46;
    int H2 = 47;
    int K1 = 48;
    int K2 = 49;
    int A1 = 50;
    int A2 = 51;
    int B = 52;
    int KEY = 53;
    int DEFAULT = 0;
    string[] tokenImage = new[]
    {
        "<EOF>",
        "\" \"",
        "\"\\t\"",
        "<EOL>",
        "<DIGIT>",
        "<HEXDIGIT>",
        "<INT>",
        "<REAL>",
        "<STRING>",
        "\"*network\"",
        "\"*vertices\"",
        "\"*arcs\"",
        "\"*edges\"",
        "\"*edgeslist\"",
        "\"*arcslist\"",
        "\"*matrix\"",
        "<COMMENT>",
        "\"ellipse\"",
        "\"diamond\"",
        "\"cross\"",
        "\"box\"",
        "\"triangle\"",
        "\"empty\"",
        "<SIZE>",
        "\"x_fact\"",
        "\"y_fact\"",
        "\"phi\"",
        "\"r\"",
        "\"q\"",
        "\"ic\"",
        "\"bc\"",
        "\"bw\"",
        "\"lc\"",
        "\"la\"",
        "\"lr\"",
        "\"lphi\"",
        "\"fos\"",
        "\"font\"",
        "\"c\"",
        "\"p\"",
        "\"w\"",
        "\"s\"",
        "\"a\"",
        "\"ap\"",
        "\"l\"",
        "\"lp\"",
        "\"h1\"",
        "\"h2\"",
        "\"k1\"",
        "\"k2\"",
        "\"a1\"",
        "\"a2\"",
        "\"b\"",
        "<KEY>"
    };
}
