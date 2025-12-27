using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EventType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Mode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.What;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Option;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Balise;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.IDType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.FixType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GraphEvents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ThreadingModel;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.CloseFramePolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Measures;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Token;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Extension;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.DefaultEdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Resolutions;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PropertyType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Type;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Units;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.FillMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.StrokeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ShadowMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.VisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextVisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextStyle;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.IconMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SizeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextAlignment;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextBackgroundMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ShapeKind;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Shape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SpriteOrientation;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ArrowShape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.JComponents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.InteractiveElement;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.Parser;

public interface StyleSheetParserConstants
{
    int EOF = 0;
    int EOL = 5;
    int DIGIT = 6;
    int HEXDIGIT = 7;
    int UNITS = 8;
    int DOT = 9;
    int LBRACE = 10;
    int RBRACE = 11;
    int LPAREN = 12;
    int RPAREN = 13;
    int SHARP = 14;
    int COLON = 15;
    int SEMICOLON = 16;
    int COMA = 17;
    int RGBA = 18;
    int RGB = 19;
    int HTMLCOLOR = 20;
    int REAL = 21;
    int STRING = 22;
    int URL = 23;
    int GRAPH = 24;
    int EDGE = 25;
    int NODE = 26;
    int SPRITE = 27;
    int FILLMODE = 28;
    int FILLCOLOR = 29;
    int FILLIMAGE = 30;
    int STROKEMODE = 31;
    int STROKECOLOR = 32;
    int STROKEWIDTH = 33;
    int SHADOWMODE = 34;
    int SHADOWCOLOR = 35;
    int SHADOWWIDTH = 36;
    int SHADOWOFFSET = 37;
    int TEXTMODE = 38;
    int TEXTCOLOR = 39;
    int TEXTSTYLE = 40;
    int TEXTFONT = 41;
    int TEXTSIZE = 42;
    int TEXTVISIBILITYMODE = 43;
    int TEXTVISIBILITY = 44;
    int TEXTBACKGROUNDMODE = 45;
    int TEXTBACKGROUNDCOLOR = 46;
    int TEXTOFFSET = 47;
    int TEXTPADDING = 48;
    int ICONMODE = 49;
    int ICON = 50;
    int PADDING = 51;
    int ZINDEX = 52;
    int VISIBILITYMODE = 53;
    int VISIBILITY = 54;
    int SHAPE = 55;
    int SIZE = 56;
    int SIZEMODE = 57;
    int SHAPEPOINTS = 58;
    int TEXTALIGNMENT = 59;
    int JCOMPONENT = 60;
    int ARROWIMGURL = 61;
    int ARROWSIZE = 62;
    int ARROWSHAPE = 63;
    int SPRITEORIENT = 64;
    int CANVASCOLOR = 65;
    int PLAIN = 66;
    int DYNPLAIN = 67;
    int DYNSIZE = 68;
    int DYNICON = 69;
    int DASHES = 70;
    int DOTS = 71;
    int DOUBLE = 72;
    int GRADIENTDIAGONAL1 = 73;
    int GRADIENTDIAGONAL2 = 74;
    int GRADIENTHORIZONTAL = 75;
    int GRADIENTRADIAL = 76;
    int GRADIENTVERTICAL = 77;
    int HIDDEN = 78;
    int IMAGETILED = 79;
    int IMAGESCALED = 80;
    int IMAGESCALEDRATIOMAX = 81;
    int IMAGESCALEDRATIOMIN = 82;
    int NONE = 83;
    int NORMAL = 84;
    int TRUNCATED = 85;
    int ZOOMRANGE = 86;
    int ATZOOM = 87;
    int UNDERZOOM = 88;
    int OVERZOOM = 89;
    int ZOOMS = 90;
    int FIT = 91;
    int BOLD = 92;
    int BOLD_ITALIC = 93;
    int ITALIC = 94;
    int ALONG = 95;
    int ATLEFT = 96;
    int ATRIGHT = 97;
    int CENTER = 98;
    int LEFT = 99;
    int RIGHT = 100;
    int UNDER = 101;
    int ABOVE = 102;
    int JUSTIFY = 103;
    int CIRCLE = 104;
    int TRIANGLE = 105;
    int FREEPLANE = 106;
    int TEXTBOX = 107;
    int TEXTROUNDEDBOX = 108;
    int TEXTCIRCLE = 109;
    int TEXTDIAMOND = 110;
    int TEXTPARAGRAPH = 111;
    int BOX = 112;
    int ROUNDEDBOX = 113;
    int CROSS = 114;
    int DIAMOND = 115;
    int POLYGON = 116;
    int BUTTON = 117;
    int TEXTFIELD = 118;
    int PANEL = 119;
    int LINE = 120;
    int POLYLINE = 121;
    int POLYLINESCALED = 122;
    int ANGLE = 123;
    int CUBICCURVE = 124;
    int BLOB = 125;
    int SQUARELINE = 126;
    int LSQUARELINE = 127;
    int HSQUARELINE = 128;
    int VSQUARELINE = 129;
    int ARROW = 130;
    int FLOW = 131;
    int PIECHART = 132;
    int IMAGE = 133;
    int IMAGES = 134;
    int FROM = 135;
    int NODE0 = 136;
    int TO = 137;
    int NODE1 = 138;
    int PROJECTION = 139;
    int CLICKED = 140;
    int SELECTED = 141;
    int IDENTIFIER = 142;
    int COMMENT = 143;
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
        "<UNITS>",
        "\".\"",
        "\"{\"",
        "\"}\"",
        "\"(\"",
        "\")\"",
        "\"#\"",
        "\":\"",
        "\";\"",
        "\",\"",
        "\"rgba\"",
        "\"rgb\"",
        "<HTMLCOLOR>",
        "<REAL>",
        "<STRING>",
        "\"url\"",
        "\"graph\"",
        "\"edge\"",
        "\"node\"",
        "\"sprite\"",
        "\"fill-mode\"",
        "\"fill-color\"",
        "\"fill-image\"",
        "\"stroke-mode\"",
        "\"stroke-color\"",
        "\"stroke-width\"",
        "\"shadow-mode\"",
        "\"shadow-color\"",
        "\"shadow-width\"",
        "\"shadow-offset\"",
        "\"text-mode\"",
        "\"text-color\"",
        "\"text-style\"",
        "\"text-font\"",
        "\"text-size\"",
        "\"text-visibility-mode\"",
        "\"text-visibility\"",
        "\"text-background-mode\"",
        "\"text-background-color\"",
        "\"text-offset\"",
        "\"text-padding\"",
        "\"icon-mode\"",
        "\"icon\"",
        "\"padding\"",
        "\"z-index\"",
        "\"visibility-mode\"",
        "\"visibility\"",
        "\"shape\"",
        "\"size\"",
        "\"size-mode\"",
        "\"shape-points\"",
        "\"text-alignment\"",
        "\"jcomponent\"",
        "\"arrow-image\"",
        "\"arrow-size\"",
        "\"arrow-shape\"",
        "\"sprite-orientation\"",
        "\"canvas-color\"",
        "\"plain\"",
        "\"dyn-plain\"",
        "\"dyn-size\"",
        "\"dyn-icon\"",
        "\"dashes\"",
        "\"dots\"",
        "\"double\"",
        "\"gradient-diagonal1\"",
        "\"gradient-diagonal2\"",
        "\"gradient-horizontal\"",
        "\"gradient-radial\"",
        "\"gradient-vertical\"",
        "\"hidden\"",
        "\"image-tiled\"",
        "\"image-scaled\"",
        "\"image-scaled-ratio-max\"",
        "\"image-scaled-ratio-min\"",
        "\"none\"",
        "\"normal\"",
        "\"truncated\"",
        "\"zoom-range\"",
        "\"at-zoom\"",
        "\"under-zoom\"",
        "\"over-zoom\"",
        "\"zooms\"",
        "\"fit\"",
        "\"bold\"",
        "\"bold-italic\"",
        "\"italic\"",
        "\"along\"",
        "\"at-left\"",
        "\"at-right\"",
        "\"center\"",
        "\"left\"",
        "\"right\"",
        "\"under\"",
        "\"above\"",
        "\"justify\"",
        "\"circle\"",
        "\"triangle\"",
        "\"freeplane\"",
        "\"text-box\"",
        "\"text-rounded-box\"",
        "\"text-circle\"",
        "\"text-diamond\"",
        "\"text-paragraph\"",
        "\"box\"",
        "\"rounded-box\"",
        "\"cross\"",
        "\"diamond\"",
        "\"polygon\"",
        "\"button\"",
        "\"text-field\"",
        "\"panel\"",
        "\"line\"",
        "\"polyline\"",
        "\"polyline-scaled\"",
        "\"angle\"",
        "\"cubic-curve\"",
        "\"blob\"",
        "\"square-line\"",
        "\"L-square-line\"",
        "\"horizontal-square-line\"",
        "\"vertical-square-line\"",
        "\"arrow\"",
        "\"flow\"",
        "\"pie-chart\"",
        "\"image\"",
        "\"images\"",
        "\"from\"",
        "\"node0\"",
        "\"to\"",
        "\"node1\"",
        "\"projection\"",
        "\"clicked\"",
        "\"selected\"",
        "<IDENTIFIER>",
        "<COMMENT>"
    };
}
