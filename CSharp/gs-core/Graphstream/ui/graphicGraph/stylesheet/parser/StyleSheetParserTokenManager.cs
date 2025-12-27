using Gs_Core.Graphstream.Util.Parser;
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

public class StyleSheetParserTokenManager : StyleSheetParserConstants
{
    private int JjStopStringLiteralDfa_0(int pos, long active0, long active1, long active2)
    {
        switch (pos)
        {
            case 0:
                if ((active0 & 0xffffffffff8c0000) != 0 || (active1 & 0xffffffffffffffff) != 0 || (active2 & 0x3fff) != 0)
                {
                    jjmatchedKind = 142;
                    return 63;
                }

                if ((active0 & 0x4000) != 0)
                    return 1;
                return -1;
            case 1:
                if ((active2 & 0x200) != 0)
                    return 63;
                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                if ((active0 & 0xffefffffff8c0000) != 0 || (active1 & 0x7fffffffffffffff) != 0 || (active2 & 0x3dff) != 0)
                {
                    jjmatchedKind = 142;
                    jjmatchedPos = 1;
                    return 63;
                }

                return -1;
            case 2:
                if ((active0 & 0xffefffffff000000) != 0 || (active1 & 0x7ffefffcf77fffff) != 0 || (active2 & 0x3dff) != 0)
                {
                    if (jjmatchedPos != 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return 63;
                }

                if ((active0 & 0x8c0000) != 0 || (active1 & 0x1000008000000) != 0)
                    return 63;
                if ((active1 & 0x300800000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 3:
                if ((active0 & 0x306000006040000) != 0 || (active1 & 0x2100000830080080) != 0 || (active2 & 0x588) != 0)
                    return 63;
                if ((active1 & 0x38) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active0 & 0xfce9fffff9000000) != 0 || (active1 & 0x5efefff4c777ff47) != 0 || (active2 & 0x3867) != 0)
                {
                    if (jjmatchedPos != 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return 63;
                }

                if ((active1 & 0x300800000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 4:
                if ((active0 & 0xe480000001000000) != 0 || (active1 & 0x884007085078004) != 0 || (active2 & 0x564) != 0)
                    return 63;
                if ((active1 & 0x38) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active0 & 0x801ffc070000000) != 0 || (active1 & 0x40f80002400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x300800000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                if ((active0 & 0x1068003f88000000) != 0 || (active1 & 0x563a078440307f43) != 0 || (active2 & 0x3803) != 0)
                {
                    if (jjmatchedPos != 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return 63;
                }

                return -1;
            case 5:
                if ((active0 & 0x1068003f80000000) != 0 || (active1 & 0x461a068000203e02) != 0 || (active2 & 0x3803) != 0)
                {
                    if (jjmatchedPos != 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return 63;
                }

                if ((active0 & 0x8000000) != 0 || (active1 & 0x20010440104141) != 0 || (active2 & 0x40) != 0)
                    return 63;
                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active1 & 0x38) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active0 & 0x801ffc070000000) != 0 || (active1 & 0x40f80002400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x300800000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 6:
                if ((active0 & 0x8000000000000) != 0 || (active1 & 0x18008000000000) != 0 || (active2 & 0x1000) != 0)
                    return 63;
                if ((active0 & 0x1060000000000000) != 0 || (active1 & 0x602060000203e00) != 0 || (active2 & 0x2803) != 0)
                {
                    jjmatchedKind = 142;
                    jjmatchedPos = 6;
                    return 63;
                }

                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active1 & 0x38) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active0 & 0x3f80000000) != 0 || (active1 & 0x4000000000000002) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active0 & 0x801ffc070000000) != 0 || (active1 & 0x40f80002400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x300800000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active0 & 0x10000000000000) != 0 || (active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 7:
                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active0 & 0x801ffc070000000) != 0 || (active1 & 0x40f80002400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x600020000000000) != 0 || (active2 & 0x2000) != 0)
                    return 63;
                if ((active1 & 0x38) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active1 & 0x200000000) != 0)
                {
                    if (jjmatchedPos < 1)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 1;
                    }

                    return -1;
                }

                if ((active1 & 0x2000000000000) != 0)
                {
                    if (jjmatchedPos < 6)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 6;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                if ((active0 & 0x3f80000000) != 0 || (active1 & 0x4000000000000002) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active0 & 0x1060000000000000) != 0 || (active1 & 0x40000203e00) != 0 || (active2 & 0x803) != 0)
                {
                    if (jjmatchedPos != 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return 63;
                }

                return -1;
            case 8:
                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active0 & 0x801ffc070000000) != 0 || (active1 & 0x40f00002400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x40000200000) != 0)
                    return 63;
                if ((active1 & 0x8) != 0 || (active2 & 0x10) != 0)
                {
                    if (jjmatchedPos < 2)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 2;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x1060000000000000) != 0 || (active2 & 0x801) != 0)
                {
                    jjmatchedKind = 142;
                    jjmatchedPos = 8;
                    return 63;
                }

                if ((active1 & 0x2000000000000) != 0)
                {
                    if (jjmatchedPos < 6)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 6;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                if ((active0 & 0x3f80000000) != 0 || (active1 & 0x4000000000000002) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                return -1;
            case 9:
                if ((active0 & 0x1060000000000000) != 0 || (active2 & 0x800) != 0)
                    return 63;
                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos != 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return 63;
                }

                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active1 & 0x2000000000000) != 0)
                {
                    if (jjmatchedPos < 6)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 6;
                    }

                    return -1;
                }

                if ((active0 & 0x3f80000000) != 0 || (active1 & 0x4000000000000002) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active0 & 0x801f98060000000) != 0 || (active1 & 0x40f00000400000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 10:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active1 & 0x1000000000000000) != 0)
                {
                    if (jjmatchedPos < 4)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 4;
                    }

                    return -1;
                }

                if ((active1 & 0x2000000000000) != 0)
                {
                    if (jjmatchedPos < 6)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 6;
                    }

                    return -1;
                }

                if ((active0 & 0x801f80000000000) != 0 || (active1 & 0xf00000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active0 & 0x3f80000000) != 0 || (active1 & 0x4000000000000002) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 11:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x801780000000000) != 0 || (active1 & 0xd00000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active0 & 0x3b00000000) != 0 || (active1 & 0x2) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 12:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x800780000000000) != 0 || (active1 & 0x900000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                if ((active0 & 0x2000000000) != 0)
                {
                    if (jjmatchedPos < 5)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 5;
                    }

                    return -1;
                }

                if ((active1 & 0x8000000000000000) != 0)
                {
                    if (jjmatchedPos == 0)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 0;
                    }

                    return -1;
                }

                return -1;
            case 13:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x800780000000000) != 0 || (active1 & 0x900000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 14:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x3e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x780000000000) != 0 || (active1 & 0x100000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 15:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x2e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x680000000000) != 0 || (active1 & 0x100000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 16:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x2e00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x680000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 17:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0xe00) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x680000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 18:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active1 & 0x800) != 0 || (active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active0 & 0x680000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 19:
                if ((active2 & 0x2) != 0)
                {
                    if (jjmatchedPos < 7)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 7;
                    }

                    return -1;
                }

                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active0 & 0x680000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            case 20:
                if ((active2 & 0x1) != 0)
                {
                    if (jjmatchedPos < 9)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 9;
                    }

                    return -1;
                }

                if ((active0 & 0x400000000000) != 0)
                {
                    if (jjmatchedPos < 3)
                    {
                        jjmatchedKind = 142;
                        jjmatchedPos = 3;
                    }

                    return -1;
                }

                return -1;
            default:
                return -1;
                break;
        }
    }

    private int JjStartNfa_0(int pos, long active0, long active1, long active2)
    {
        return JjMoveNfa_0(JjStopStringLiteralDfa_0(pos, active0, active1, active2), pos + 1);
    }

    private int JjStopAtPos(int pos, int kind)
    {
        jjmatchedKind = kind;
        jjmatchedPos = pos;
        return pos + 1;
    }

    private int JjMoveStringLiteralDfa0_0()
    {
        switch (curChar)
        {
            case 35:
                return JjStartNfaWithStates_0(0, 14, 1);
            case 40:
                return JjStopAtPos(0, 12);
            case 41:
                return JjStopAtPos(0, 13);
            case 44:
                return JjStopAtPos(0, 17);
            case 46:
                return JjStopAtPos(0, 9);
            case 58:
                return JjStopAtPos(0, 15);
            case 59:
                return JjStopAtPos(0, 16);
            case 76:
                return JjMoveStringLiteralDfa1_0(0x0, 0x8000000000000000, 0x0);
            case 97:
                return JjMoveStringLiteralDfa1_0(0xe000000000000000, 0x800004380800000, 0x4);
            case 98:
                return JjMoveStringLiteralDfa1_0(0x0, 0x2021000030000000, 0x0);
            case 99:
                return JjMoveStringLiteralDfa1_0(0x0, 0x1004010400000002, 0x1000);
            case 100:
                return JjMoveStringLiteralDfa1_0(0x0, 0x80000000001f8, 0x0);
            case 101:
                return JjMoveStringLiteralDfa1_0(0x2000000, 0x0, 0x0);
            case 102:
                return JjMoveStringLiteralDfa1_0(0x70000000, 0x40008000000, 0x88);
            case 103:
                return JjMoveStringLiteralDfa1_0(0x1000000, 0x3e00, 0x0);
            case 104:
                return JjMoveStringLiteralDfa1_0(0x0, 0x4000, 0x1);
            case 105:
                return JjMoveStringLiteralDfa1_0(0x6000000000000, 0x40078000, 0x60);
            case 106:
                return JjMoveStringLiteralDfa1_0(0x1000000000000000, 0x8000000000, 0x0);
            case 108:
                return JjMoveStringLiteralDfa1_0(0x0, 0x100000800000000, 0x0);
            case 110:
                return JjMoveStringLiteralDfa1_0(0x4000000, 0x180000, 0x500);
            case 111:
                return JjMoveStringLiteralDfa1_0(0x0, 0x2000000, 0x0);
            case 112:
                return JjMoveStringLiteralDfa1_0(0x8000000000000, 0x690000000000004, 0x810);
            case 114:
                return JjMoveStringLiteralDfa1_0(0xc0000, 0x2001000000000, 0x0);
            case 115:
                return JjMoveStringLiteralDfa1_0(0x780003f88000000, 0x4000000000000001, 0x2000);
            case 116:
                return JjMoveStringLiteralDfa1_0(0x801ffc000000000, 0x40fa0000200000, 0x200);
            case 117:
                return JjMoveStringLiteralDfa1_0(0x800000, 0x2001000000, 0x0);
            case 118:
                return JjMoveStringLiteralDfa1_0(0x60000000000000, 0x0, 0x2);
            case 122:
                return JjMoveStringLiteralDfa1_0(0x10000000000000, 0x4400000, 0x0);
            case 123:
                return JjStopAtPos(0, 10);
            case 125:
                return JjStopAtPos(0, 11);
            default:
                return JjMoveNfa_0(0, 0);
                break;
        }
    }

    private int JjMoveStringLiteralDfa1_0(long active0, long active1, long active2)
    {
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(0, active0, active1, active2);
            return 1;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa2_0(active0, 0x10000000000000, active1, 0x8000000000000000, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa2_0(active0, 0x8000000000000, active1, 0x80000000000042, active2, 0);
            case 98:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x4000000000, active2, 0);
            case 99:
                return JjMoveStringLiteralDfa2_0(active0, 0x1006000000000000, active1, 0, active2, 0);
            case 100:
                return JjMoveStringLiteralDfa2_0(active0, 0x2000000, active1, 0, active2, 0);
            case 101:
                return JjMoveStringLiteralDfa2_0(active0, 0x801ffc000000000, active1, 0x40f80c00000000, active2, 0x2002);
            case 103:
                return JjMoveStringLiteralDfa2_0(active0, 0xc0000, active1, 0, active2, 0);
            case 104:
                return JjMoveStringLiteralDfa2_0(active0, 0x480003c00000000, active1, 0, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa2_0(active0, 0x360000070000000, active1, 0x108011008004000, active2, 0x10);
            case 108:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x2000000080000004, active2, 0x1008);
            case 109:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x78000, active2, 0x60);
            case 110:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x800002001000000, active2, 0);
            case 111:
                if ((active2 & 0x200) != 0)
                    return JjStartNfaWithStates_0(1, 137, 63);
                return JjMoveStringLiteralDfa2_0(active0, 0x4000000, active1, 0x613000034580180, active2, 0x501);
            case 112:
                return JjMoveStringLiteralDfa2_0(active0, 0x8000000, active1, 0x1, active2, 0);
            case 113:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x4000000000000000, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa2_0(active0, 0xe000000001800000, active1, 0x4060000203e00, active2, 0x884);
            case 116:
                return JjMoveStringLiteralDfa2_0(active0, 0x380000000, active1, 0x340800000, active2, 0);
            case 117:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x1020008000000000, active2, 0);
            case 118:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x2000000, active2, 0);
            case 121:
                return JjMoveStringLiteralDfa2_0(active0, 0, active1, 0x38, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(0, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa2_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(0, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(1, active0, active1, active2);
            return 2;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x300800000, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa3_0(active0, 0x480003c01000000, active1, 0x800004007be04, active2, 0x60);
            case 98:
                if ((active0 & 0x80000) != 0)
                {
                    jjmatchedKind = 19;
                    jjmatchedPos = 2;
                }

                return JjMoveStringLiteralDfa3_0(active0, 0x40000, active1, 0x1000000000000000, active2, 0);
            case 100:
                return JjMoveStringLiteralDfa3_0(active0, 0x8000004000000, active1, 0x2001004000, active2, 0x500);
            case 101:
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x40002000000, active2, 0x10);
            case 102:
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x800000000, active2, 0);
            case 103:
                return JjMoveStringLiteralDfa3_0(active0, 0x2000000, active1, 0x800001000000000, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa3_0(active0, 0x10000000000000, active1, 0x20000000000, active2, 0x1000);
            case 108:
                if ((active0 & 0x800000) != 0)
                    return JjStartNfaWithStates_0(2, 23, 63);
                return JjMoveStringLiteralDfa3_0(active0, 0x70000000, active1, 0x610000030000000, active2, 0x2000);
            case 110:
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x18000040008003a, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa3_0(active0, 0x1006000000000000, active1, 0x2004004084400000, active2, 0x888);
            case 114:
                return JjMoveStringLiteralDfa3_0(active0, 0xe000000388000000, active1, 0x10000100001, active2, 0x7);
            case 115:
                return JjMoveStringLiteralDfa3_0(active0, 0x60000000000000, active1, 0x8000008000000040, active2, 0);
            case 116:
                if ((active1 & 0x8000000) != 0)
                    return JjStartNfaWithStates_0(2, 91, 63);
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x20000000000080, active2, 0);
            case 117:
                return JjMoveStringLiteralDfa3_0(active0, 0, active1, 0x4002000000200100, active2, 0);
            case 120:
                if ((active1 & 0x1000000000000) != 0)
                    return JjStartNfaWithStates_0(2, 112, 63);
                return JjMoveStringLiteralDfa3_0(active0, 0x801ffc000000000, active1, 0x40f80000000000, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa3_0(active0, 0x300000000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(1, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa3_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(1, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(2, active0, active1, active2);
            return 3;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x38, active2, 0x10);
            case 97:
                if ((active0 & 0x40000) != 0)
                    return JjStartNfaWithStates_0(3, 18, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x4000020000000000, active2, 0);
            case 98:
                if ((active1 & 0x2000000000000000) != 0)
                    return JjStartNfaWithStates_0(3, 125, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x100, active2, 0);
            case 99:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x10000000000, active2, 0x1000);
            case 100:
                if ((active1 & 0x10000000) != 0)
                {
                    jjmatchedKind = 92;
                    jjmatchedPos = 3;
                }

                return JjMoveStringLiteralDfa4_0(active0, 0x8003c00000000, active1, 0x20007e00, active2, 0);
            case 101:
                if ((active0 & 0x2000000) != 0)
                    return JjStartNfaWithStates_0(3, 25, 63);
                else if ((active0 & 0x4000000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 3;
                }
                else if ((active0 & 0x100000000000000) != 0)
                {
                    jjmatchedKind = 56;
                    jjmatchedPos = 3;
                }
                else if ((active1 & 0x80000) != 0)
                    return JjStartNfaWithStates_0(3, 83, 63);
                else if ((active1 & 0x100000000000000) != 0)
                    return JjStartNfaWithStates_0(3, 120, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0x200000000000000, active1, 0x80042001000000, active2, 0x2500);
            case 103:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x78000, active2, 0x60);
            case 104:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x1000000040, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa4_0(active0, 0x60000008000000, active1, 0x1000000000000005, active2, 0x1);
            case 106:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0, active2, 0x800);
            case 108:
                return JjMoveStringLiteralDfa4_0(active0, 0x70000000, active1, 0x800000140000000, active2, 0);
            case 109:
                if ((active2 & 0x80) != 0)
                    return JjStartNfaWithStates_0(3, 135, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0x1000000000000000, active1, 0x8000004500000, active2, 0);
            case 110:
                if ((active0 & 0x4000000000000) != 0)
                {
                    jjmatchedKind = 50;
                    jjmatchedPos = 3;
                }

                return JjMoveStringLiteralDfa4_0(active0, 0x12000000000000, active1, 0x2000080200000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa4_0(active0, 0xe000000380000000, active1, 0, active2, 0x4);
            case 112:
                return JjMoveStringLiteralDfa4_0(active0, 0x480000001000000, active1, 0, active2, 0);
            case 113:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x8000000000000000, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x202000000, active2, 0);
            case 115:
                if ((active1 & 0x80) != 0)
                    return JjStartNfaWithStates_0(3, 71, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x4000000000000, active2, 0);
            case 116:
                if ((active1 & 0x800000000) != 0)
                    return JjStartNfaWithStates_0(3, 99, 63);
                return JjMoveStringLiteralDfa4_0(active0, 0x801ffc000000000, active1, 0x60f88400000000, active2, 0x2);
            case 118:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x4000000002, active2, 0);
            case 119:
                if ((active2 & 0x8) != 0)
                    return JjStartNfaWithStates_0(3, 131, 63);
                break;
            case 121:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x610000000000000, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa4_0(active0, 0, active1, 0x800000, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(2, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa4_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(2, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(3, active0, active1, active2);
            return 4;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa5_0(active0, 0xa03ffc070000000, active1, 0x40f80022400000, active2, 0);
            case 48:
                if ((active2 & 0x100) != 0)
                    return JjStartNfaWithStates_0(4, 136, 63);
                break;
            case 49:
                if ((active2 & 0x400) != 0)
                    return JjStartNfaWithStates_0(4, 138, 63);
                break;
            case 97:
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x100002, active2, 0);
            case 98:
                return JjMoveStringLiteralDfa5_0(active0, 0x60000000000000, active1, 0, active2, 0);
            case 99:
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x1000000000200000, active2, 0x2010);
            case 100:
                return JjMoveStringLiteralDfa5_0(active0, 0x10000000000000, active1, 0x2000000000000, active2, 0);
            case 101:
                if ((active0 & 0x80000000000000) != 0)
                {
                    jjmatchedKind = 55;
                    jjmatchedPos = 4;
                }
                else if ((active1 & 0x4000000000) != 0)
                    return JjStartNfaWithStates_0(4, 102, 63);
                else if ((active1 & 0x800000000000000) != 0)
                    return JjStartNfaWithStates_0(4, 123, 63);
                else if ((active2 & 0x20) != 0)
                {
                    jjmatchedKind = 133;
                    jjmatchedPos = 4;
                }

                return JjMoveStringLiteralDfa5_0(active0, 0x400000000000000, active1, 0x50007c040, active2, 0x840);
            case 103:
                if ((active1 & 0x80000000) != 0)
                    return JjStartNfaWithStates_0(4, 95, 63);
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x10000000000000, active2, 0);
            case 104:
                if ((active0 & 0x1000000) != 0)
                    return JjStartNfaWithStates_0(4, 24, 63);
                break;
            case 105:
                return JjMoveStringLiteralDfa5_0(active0, 0x8000000000000, active1, 0x8240003e20, active2, 0x2);
            case 107:
                return JjMoveStringLiteralDfa5_0(active0, 0x380000000, active1, 0, active2, 0x1000);
            case 108:
                if ((active1 & 0x80000000000000) != 0)
                    return JjStartNfaWithStates_0(4, 119, 63);
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x600010000000100, active2, 0);
            case 110:
                if ((active1 & 0x4) != 0)
                    return JjStartNfaWithStates_0(4, 66, 63);
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x20000000000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa5_0(active0, 0x3c00000000, active1, 0x28000000800000, active2, 0);
            case 112:
                return JjMoveStringLiteralDfa5_0(active0, 0x1000000000000000, active1, 0x40000000008, active2, 0);
            case 114:
                if ((active1 & 0x2000000000) != 0)
                {
                    jjmatchedKind = 101;
                    jjmatchedPos = 4;
                }

                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x4000000001000000, active2, 0);
            case 115:
                if ((active1 & 0x4000000) != 0)
                    return JjStartNfaWithStates_0(4, 90, 63);
                else if ((active1 & 0x4000000000000) != 0)
                    return JjStartNfaWithStates_0(4, 114, 63);
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x10, active2, 0);
            case 116:
                if ((active1 & 0x1000000000) != 0)
                    return JjStartNfaWithStates_0(4, 100, 63);
                return JjMoveStringLiteralDfa5_0(active0, 0x8000000, active1, 0x1, active2, 0);
            case 117:
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0x8000000000000000, active2, 0);
            case 119:
                if ((active2 & 0x4) != 0)
                {
                    jjmatchedKind = 130;
                    jjmatchedPos = 4;
                }

                return JjMoveStringLiteralDfa5_0(active0, 0xe000000000000000, active1, 0, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa5_0(active0, 0, active1, 0, active2, 0x1);
            default:
                break;
        }

        return JjStartNfa_0(3, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa5_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(3, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(4, active0, active1, active2);
            return 5;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa6_0(active0, 0xe400000000000000, active1, 0x1000000001078000, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa6_0(active0, 0x800000000000000, active1, 0x8000000000200000, active2, 0);
            case 98:
                return JjMoveStringLiteralDfa6_0(active0, 0x600000000000, active1, 0x80000000000, active2, 0);
            case 99:
                if ((active1 & 0x40000000) != 0)
                    return JjStartNfaWithStates_0(5, 94, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0x8020000000, active1, 0x200000000020, active2, 0x802);
            case 100:
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0x400000000000, active2, 0);
            case 101:
                if ((active0 & 0x8000000) != 0)
                {
                    jjmatchedKind = 27;
                    jjmatchedPos = 5;
                }
                else if ((active1 & 0x100) != 0)
                    return JjStartNfaWithStates_0(5, 72, 63);
                else if ((active1 & 0x10000000000) != 0)
                    return JjStartNfaWithStates_0(5, 104, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0x10000380000000, active1, 0x4002000000003e01, active2, 0x1000);
            case 102:
                return JjMoveStringLiteralDfa6_0(active0, 0x20000000000, active1, 0x40008100000000, active2, 0);
            case 103:
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0x20200000000, active2, 0);
            case 104:
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0, active2, 0x10);
            case 105:
                return JjMoveStringLiteralDfa6_0(active0, 0x60000040000000, active1, 0x600000020000010, active2, 0);
            case 108:
                if ((active1 & 0x100000) != 0)
                    return JjStartNfaWithStates_0(5, 84, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0x40000000008, active2, 0);
            case 109:
                return JjMoveStringLiteralDfa6_0(active0, 0x202004010000000, active1, 0, active2, 0);
            case 110:
                if ((active1 & 0x4000) != 0)
                    return JjStartNfaWithStates_0(5, 78, 63);
                else if ((active1 & 0x20000000000000) != 0)
                    return JjStartNfaWithStates_0(5, 117, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0x8000000000000, active1, 0x8000000000000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa6_0(active0, 0x1000800000000000, active1, 0x10000000800000, active2, 0x1);
            case 112:
                return JjMoveStringLiteralDfa6_0(active0, 0x1000000000000, active1, 0x800000000000, active2, 0);
            case 114:
                if ((active1 & 0x400000000) != 0)
                    return JjStartNfaWithStates_0(5, 98, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0x100000400000, active2, 0);
            case 115:
                if ((active1 & 0x40) != 0)
                    return JjStartNfaWithStates_0(5, 70, 63);
                else if ((active2 & 0x40) != 0)
                    return JjStartNfaWithStates_0(5, 134, 63);
                return JjMoveStringLiteralDfa6_0(active0, 0x50000000000, active1, 0x2, active2, 0);
            case 116:
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0, active2, 0x2000);
            case 118:
                return JjMoveStringLiteralDfa6_0(active0, 0x180000000000, active1, 0, active2, 0);
            case 119:
                return JjMoveStringLiteralDfa6_0(active0, 0x3c00000000, active1, 0, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa6_0(active0, 0, active1, 0x2000000, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(4, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa6_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(4, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(5, active0, active1, active2);
            return 6;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa7_0(active0, 0x3f80000000, active1, 0x4000000000000003, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa7_0(active0, 0x1600000000000, active1, 0x840000400008, active2, 0x12);
            case 99:
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0x1000000000000000, active2, 0);
            case 100:
                if ((active1 & 0x8000000000000) != 0)
                    return JjStartNfaWithStates_0(6, 115, 63);
                else if ((active2 & 0x1000) != 0)
                    return JjStartNfaWithStates_0(6, 140, 63);
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0x2000000000000, active2, 0);
            case 101:
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0, active2, 0x2000);
            case 102:
                return JjMoveStringLiteralDfa7_0(active0, 0x800000000000, active1, 0, active2, 0);
            case 103:
                if ((active0 & 0x8000000000000) != 0)
                    return JjStartNfaWithStates_0(6, 51, 63);
                break;
            case 104:
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0x200000000, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa7_0(active0, 0x20001c0000000000, active1, 0x40600000000000, active2, 0);
            case 108:
                return JjMoveStringLiteralDfa7_0(active0, 0x860000000000000, active1, 0x20000000000, active2, 0);
            case 109:
                if ((active1 & 0x800000) != 0)
                    return JjStopAtPos(6, 87);
                return JjMoveStringLiteralDfa7_0(active0, 0x40000000, active1, 0, active2, 0);
            case 110:
                if ((active1 & 0x10000000000000) != 0)
                    return JjStartNfaWithStates_0(6, 116, 63);
                return JjMoveStringLiteralDfa7_0(active0, 0x1000000000000000, active1, 0x600000000003e00, active2, 0x1);
            case 111:
                return JjMoveStringLiteralDfa7_0(active0, 0x20202c030000000, active1, 0x180002000020, active2, 0);
            case 112:
                return JjMoveStringLiteralDfa7_0(active0, 0x400000000000000, active1, 0, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0x8000000000000000, active2, 0);
            case 115:
                return JjMoveStringLiteralDfa7_0(active0, 0xc000000000000000, active1, 0x70000, active2, 0);
            case 116:
                if ((active1 & 0x100000000) != 0)
                    return JjStopAtPos(6, 96);
                return JjMoveStringLiteralDfa7_0(active0, 0x10000000000, active1, 0x20208000, active2, 0x800);
            case 120:
                if ((active0 & 0x10000000000000) != 0)
                    return JjStopAtPos(6, 52);
                break;
            case 121:
                if ((active1 & 0x8000000000) != 0)
                    return JjStartNfaWithStates_0(6, 103, 63);
                break;
            case 122:
                return JjMoveStringLiteralDfa7_0(active0, 0, active1, 0x1000010, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(5, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa7_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(5, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(6, active0, active1, active2);
            return 7;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa8_0(active0, 0, active1, 0x2000000000000, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa8_0(active0, 0x40000000, active1, 0x400020000000, active2, 0);
            case 99:
                return JjMoveStringLiteralDfa8_0(active0, 0x600900000000, active1, 0x70002, active2, 0);
            case 100:
                if ((active2 & 0x2000) != 0)
                    return JjStartNfaWithStates_0(7, 141, 63);
                return JjMoveStringLiteralDfa8_0(active0, 0x203004010000000, active1, 0, active2, 0);
            case 101:
                if ((active1 & 0x10) != 0)
                    return JjStopAtPos(7, 68);
                else if ((active1 & 0x20000000000) != 0)
                    return JjStartNfaWithStates_0(7, 105, 63);
                else if ((active1 & 0x200000000000000) != 0)
                {
                    jjmatchedKind = 121;
                    jjmatchedPos = 7;
                }

                return JjMoveStringLiteralDfa8_0(active0, 0x1000000000000000, active1, 0x8440000000200000, active2, 0);
            case 102:
                return JjMoveStringLiteralDfa8_0(active0, 0x800000000000, active1, 0, active2, 0);
            case 104:
                return JjMoveStringLiteralDfa8_0(active0, 0x8000000000000000, active1, 0, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa8_0(active0, 0x4860000000000000, active1, 0x8008, active2, 0x800);
            case 108:
                return JjMoveStringLiteralDfa8_0(active0, 0x8020000000, active1, 0x4000000000000000, active2, 0x2);
            case 109:
                return JjMoveStringLiteralDfa8_0(active0, 0x2000000480000000, active1, 0, active2, 0);
            case 110:
                if ((active1 & 0x20) != 0)
                    return JjStopAtPos(7, 69);
                return JjMoveStringLiteralDfa8_0(active0, 0x20000000000, active1, 0x40000400000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa8_0(active0, 0x400002000000000, active1, 0x3000001, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa8_0(active0, 0, active1, 0xa00000000000, active2, 0x10);
            case 115:
                return JjMoveStringLiteralDfa8_0(active0, 0x180000000000, active1, 0, active2, 0);
            case 116:
                if ((active1 & 0x200000000) != 0)
                    return JjStopAtPos(7, 97);
                return JjMoveStringLiteralDfa8_0(active0, 0, active1, 0x3e00, active2, 0x1);
            case 117:
                return JjMoveStringLiteralDfa8_0(active0, 0, active1, 0x1000100000000000, active2, 0);
            case 119:
                return JjMoveStringLiteralDfa8_0(active0, 0x1200000000, active1, 0, active2, 0);
            case 120:
                if ((active1 & 0x80000000000) != 0)
                    return JjStopAtPos(7, 107);
                break;
            case 121:
                return JjMoveStringLiteralDfa8_0(active0, 0x10000000000, active1, 0, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa8_0(active0, 0x40000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(6, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa8_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(6, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(7, active0, active1, active2);
            return 8;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa9_0(active0, 0, active1, 0x8400000000003e00, active2, 0x2);
            case 97:
                return JjMoveStringLiteralDfa9_0(active0, 0xa000000000000000, active1, 0x800000070000, active2, 0x1);
            case 98:
                return JjMoveStringLiteralDfa9_0(active0, 0, active1, 0x2000000000000, active2, 0);
            case 99:
                return JjMoveStringLiteralDfa9_0(active0, 0, active1, 0x200000000000, active2, 0);
            case 100:
                if ((active1 & 0x200000) != 0)
                    return JjStartNfaWithStates_0(8, 85, 63);
                return JjMoveStringLiteralDfa9_0(active0, 0x1000000000000, active1, 0, active2, 0);
            case 101:
                if ((active0 & 0x10000000) != 0)
                    return JjStopAtPos(8, 28);
                else if ((active0 & 0x4000000000) != 0)
                    return JjStopAtPos(8, 38);
                else if ((active0 & 0x40000000000) != 0)
                    return JjStopAtPos(8, 42);
                else if ((active0 & 0x2000000000000) != 0)
                    return JjStopAtPos(8, 49);
                else if ((active0 & 0x200000000000000) != 0)
                    return JjStopAtPos(8, 57);
                else if ((active1 & 0x40000000000) != 0)
                    return JjStartNfaWithStates_0(8, 106, 63);
                break;
            case 102:
                return JjMoveStringLiteralDfa9_0(active0, 0x2000000000, active1, 0, active2, 0);
            case 103:
                return JjMoveStringLiteralDfa9_0(active0, 0x800000040000000, active1, 0x400000, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa9_0(active0, 0x400181200000000, active1, 0x4000000000000000, active2, 0);
            case 107:
                return JjMoveStringLiteralDfa9_0(active0, 0x600000000000, active1, 0, active2, 0);
            case 108:
                return JjMoveStringLiteralDfa9_0(active0, 0x10000000000, active1, 0x40000020008000, active2, 0);
            case 109:
                if ((active1 & 0x2000000) != 0)
                    return JjStopAtPos(8, 89);
                return JjMoveStringLiteralDfa9_0(active0, 0, active1, 0x400000000000, active2, 0);
            case 110:
                if ((active1 & 0x8) != 0)
                    return JjStopAtPos(8, 67);
                return JjMoveStringLiteralDfa9_0(active0, 0x1000000000000000, active1, 0x100000000000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa9_0(active0, 0x8da0000000, active1, 0x1000002, active2, 0x800);
            case 114:
                return JjMoveStringLiteralDfa9_0(active0, 0, active1, 0x1000000000000001, active2, 0);
            case 115:
                return JjMoveStringLiteralDfa9_0(active0, 0x800000000000, active1, 0, active2, 0);
            case 116:
                if ((active0 & 0x20000000000) != 0)
                    return JjStopAtPos(8, 41);
                else if ((active2 & 0x10) != 0)
                    return JjStopAtPos(8, 132);
                return JjMoveStringLiteralDfa9_0(active0, 0x60000000000000, active1, 0, active2, 0);
            case 122:
                return JjMoveStringLiteralDfa9_0(active0, 0x4000000000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(7, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa9_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(7, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(8, active0, active1, active2);
            return 9;
        }

        switch (curChar)
        {
            case 98:
                return JjMoveStringLiteralDfa10_0(active0, 0x180000000000, active1, 0, active2, 0);
            case 100:
                if ((active1 & 0x40000000000000) != 0)
                    return JjStopAtPos(9, 118);
                return JjMoveStringLiteralDfa10_0(active0, 0x1680000000, active1, 0x100000000600, active2, 0);
            case 101:
                if ((active0 & 0x40000000) != 0)
                    return JjStopAtPos(9, 30);
                else if ((active0 & 0x10000000000) != 0)
                    return JjStopAtPos(9, 40);
                else if ((active0 & 0x4000000000000000) != 0)
                    return JjStopAtPos(9, 62);
                else if ((active1 & 0x400000) != 0)
                    return JjStopAtPos(9, 86);
                return JjMoveStringLiteralDfa10_0(active0, 0x800000000000, active1, 0x8000, active2, 0);
            case 102:
                return JjMoveStringLiteralDfa10_0(active0, 0x2000000000, active1, 0, active2, 0);
            case 103:
                return JjMoveStringLiteralDfa10_0(active0, 0x2000600000000000, active1, 0x800000000000, active2, 0);
            case 104:
                return JjMoveStringLiteralDfa10_0(active0, 0, active1, 0x800, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa10_0(active0, 0x1000000000000, active1, 0x20000001, active2, 0);
            case 108:
                return JjMoveStringLiteralDfa10_0(active0, 0x900000000, active1, 0x8000200000070002, active2, 0x1);
            case 109:
                if ((active1 & 0x1000000) != 0)
                    return JjStopAtPos(9, 88);
                break;
            case 110:
                if ((active2 & 0x800) != 0)
                    return JjStartNfaWithStates_0(9, 139, 63);
                return JjMoveStringLiteralDfa10_0(active0, 0xc00000000000000, active1, 0x4000000000000000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa10_0(active0, 0, active1, 0x2400000000000, active2, 0);
            case 112:
                return JjMoveStringLiteralDfa10_0(active0, 0x8000000000000000, active1, 0, active2, 0);
            case 114:
                if ((active0 & 0x20000000) != 0)
                    return JjStopAtPos(9, 29);
                else if ((active0 & 0x8000000000) != 0)
                    return JjStopAtPos(9, 39);
                return JjMoveStringLiteralDfa10_0(active0, 0, active1, 0x1000, active2, 0);
            case 115:
                return JjMoveStringLiteralDfa10_0(active0, 0, active1, 0x400000000000000, active2, 0x2);
            case 116:
                if ((active0 & 0x1000000000000000) != 0)
                    return JjStartNfaWithStates_0(9, 60, 63);
                break;
            case 118:
                return JjMoveStringLiteralDfa10_0(active0, 0, active1, 0x1000000000002000, active2, 0);
            case 121:
                if ((active0 & 0x40000000000000) != 0)
                {
                    jjmatchedKind = 54;
                    jjmatchedPos = 9;
                }

                return JjMoveStringLiteralDfa10_0(active0, 0x20000000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(8, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa10_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(8, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(9, active0, active1, active2);
            return 10;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa11_0(active0, 0x20000000000000, active1, 0, active2, 0x1);
            case 97:
                return JjMoveStringLiteralDfa11_0(active0, 0, active1, 0x1000, active2, 0);
            case 99:
                if ((active1 & 0x20000000) != 0)
                    return JjStopAtPos(10, 93);
                return JjMoveStringLiteralDfa11_0(active0, 0, active1, 0x400000000000000, active2, 0);
            case 100:
                if ((active1 & 0x8000) != 0)
                    return JjStopAtPos(10, 79);
                break;
            case 101:
                if ((active0 & 0x80000000) != 0)
                    return JjStopAtPos(10, 31);
                else if ((active0 & 0x400000000) != 0)
                    return JjStopAtPos(10, 34);
                else if ((active0 & 0x2000000000000000) != 0)
                    return JjStopAtPos(10, 61);
                else if ((active0 & 0x8000000000000000) != 0)
                    return JjStopAtPos(10, 63);
                else if ((active1 & 0x200000000000) != 0)
                    return JjStopAtPos(10, 109);
                else if ((active1 & 0x1000000000000000) != 0)
                    return JjStopAtPos(10, 124);
                else if ((active1 & 0x4000000000000000) != 0)
                    return JjStopAtPos(10, 126);
                return JjMoveStringLiteralDfa11_0(active0, 0, active1, 0x100000072001, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa11_0(active0, 0x180000000000, active1, 0x8000000000000600, active2, 0);
            case 109:
                return JjMoveStringLiteralDfa11_0(active0, 0x800000000000000, active1, 0, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa11_0(active0, 0x1000000000000, active1, 0x400000000000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa11_0(active0, 0x900000000, active1, 0x802, active2, 0);
            case 113:
                return JjMoveStringLiteralDfa11_0(active0, 0, active1, 0, active2, 0x2);
            case 114:
                return JjMoveStringLiteralDfa11_0(active0, 0x600000000000, active1, 0x800000000000, active2, 0);
            case 115:
                return JjMoveStringLiteralDfa11_0(active0, 0x2000000000, active1, 0, active2, 0);
            case 116:
                if ((active0 & 0x800000000000) != 0)
                    return JjStopAtPos(10, 47);
                return JjMoveStringLiteralDfa11_0(active0, 0x400001200000000, active1, 0, active2, 0);
            case 120:
                if ((active1 & 0x2000000000000) != 0)
                    return JjStopAtPos(10, 113);
                break;
            default:
                break;
        }

        return JjStartNfa_0(9, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa11_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(9, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(10, active0, active1, active2);
            return 11;
        }

        switch (curChar)
        {
            case 97:
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0x400800000000600, active2, 0);
            case 100:
                if ((active1 & 0x10000) != 0)
                {
                    jjmatchedKind = 80;
                    jjmatchedPos = 11;
                }
                else if ((active1 & 0x400000000000) != 0)
                    return JjStopAtPos(11, 110);
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0x100000061000, active2, 0);
            case 101:
                return JjMoveStringLiteralDfa12_0(active0, 0x800002000000000, active1, 0, active2, 0);
            case 103:
                if ((active0 & 0x1000000000000) != 0)
                    return JjStopAtPos(11, 48);
                break;
            case 104:
                if ((active0 & 0x200000000) != 0)
                    return JjStopAtPos(11, 33);
                else if ((active0 & 0x1000000000) != 0)
                    return JjStopAtPos(11, 36);
                break;
            case 108:
                return JjMoveStringLiteralDfa12_0(active0, 0x180000000000, active1, 0, active2, 0);
            case 109:
                return JjMoveStringLiteralDfa12_0(active0, 0x20000000000000, active1, 0, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0x8000000000000001, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa12_0(active0, 0x600000000000, active1, 0, active2, 0);
            case 114:
                if ((active0 & 0x100000000) != 0)
                    return JjStopAtPos(11, 32);
                else if ((active0 & 0x800000000) != 0)
                    return JjStopAtPos(11, 35);
                else if ((active1 & 0x2) != 0)
                    return JjStopAtPos(11, 65);
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0x2800, active2, 0);
            case 115:
                if ((active0 & 0x400000000000000) != 0)
                    return JjStopAtPos(11, 58);
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0, active2, 0x1);
            case 117:
                return JjMoveStringLiteralDfa12_0(active0, 0, active1, 0, active2, 0x2);
            default:
                break;
        }

        return JjStartNfa_0(10, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa12_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(10, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(11, active0, active1, active2);
            return 12;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0x100000060000, active2, 0);
            case 97:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0, active2, 0x2);
            case 101:
                if ((active1 & 0x8000000000000000) != 0)
                    return JjStopAtPos(12, 127);
                break;
            case 103:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0x600, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa13_0(active0, 0x180000000000, active1, 0x1800, active2, 0);
            case 108:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0x400000000000000, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa13_0(active0, 0x800000000000000, active1, 0, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa13_0(active0, 0x20000000000000, active1, 0, active2, 0);
            case 112:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0x800000000000, active2, 0);
            case 113:
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0, active2, 0x1);
            case 116:
                if ((active0 & 0x2000000000) != 0)
                    return JjStopAtPos(12, 37);
                return JjMoveStringLiteralDfa13_0(active0, 0, active1, 0x2001, active2, 0);
            case 117:
                return JjMoveStringLiteralDfa13_0(active0, 0x600000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(11, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa13_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(11, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(12, active0, active1, active2);
            return 13;
        }

        switch (curChar)
        {
            case 97:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x1001, active2, 0);
            case 98:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x100000000000, active2, 0);
            case 100:
                return JjMoveStringLiteralDfa14_0(active0, 0x20000000000000, active1, 0, active2, 0);
            case 101:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x400000000000000, active2, 0);
            case 104:
                if ((active1 & 0x800000000000) != 0)
                    return JjStopAtPos(13, 111);
                break;
            case 105:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x2000, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa14_0(active0, 0x600000000000, active1, 0, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x600, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x60000, active2, 0x2);
            case 116:
                if ((active0 & 0x800000000000000) != 0)
                    return JjStopAtPos(13, 59);
                return JjMoveStringLiteralDfa14_0(active0, 0x180000000000, active1, 0, active2, 0);
            case 117:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0, active2, 0x1);
            case 122:
                return JjMoveStringLiteralDfa14_0(active0, 0, active1, 0x800, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(12, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa14_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(12, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(13, active0, active1, active2);
            return 14;
        }

        switch (curChar)
        {
            case 97:
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0x60000, active2, 0x1);
            case 99:
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0x2000, active2, 0);
            case 100:
                if ((active1 & 0x400000000000000) != 0)
                    return JjStopAtPos(14, 122);
                return JjMoveStringLiteralDfa15_0(active0, 0x600000000000, active1, 0, active2, 0);
            case 101:
                if ((active0 & 0x20000000000000) != 0)
                    return JjStopAtPos(14, 53);
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0, active2, 0x2);
            case 108:
                if ((active1 & 0x1000) != 0)
                    return JjStopAtPos(14, 76);
                break;
            case 110:
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0x600, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0x100000000800, active2, 0);
            case 116:
                return JjMoveStringLiteralDfa15_0(active0, 0, active1, 0x1, active2, 0);
            case 121:
                if ((active0 & 0x100000000000) != 0)
                {
                    jjmatchedKind = 44;
                    jjmatchedPos = 14;
                }

                return JjMoveStringLiteralDfa15_0(active0, 0x80000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(13, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa15_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(13, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(14, active0, active1, active2);
            return 15;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa16_0(active0, 0x680000000000, active1, 0, active2, 0x2);
            case 97:
                return JjMoveStringLiteralDfa16_0(active0, 0, active1, 0x2600, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa16_0(active0, 0, active1, 0x1, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa16_0(active0, 0, active1, 0x800, active2, 0);
            case 114:
                return JjMoveStringLiteralDfa16_0(active0, 0, active1, 0, active2, 0x1);
            case 116:
                return JjMoveStringLiteralDfa16_0(active0, 0, active1, 0x60000, active2, 0);
            case 120:
                if ((active1 & 0x100000000000) != 0)
                    return JjStopAtPos(15, 108);
                break;
            default:
                break;
        }

        return JjStartNfa_0(14, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa16_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(14, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(15, active0, active1, active2);
            return 16;
        }

        switch (curChar)
        {
            case 99:
                return JjMoveStringLiteralDfa17_0(active0, 0x400000000000, active1, 0, active2, 0);
            case 101:
                return JjMoveStringLiteralDfa17_0(active0, 0, active1, 0, active2, 0x1);
            case 105:
                return JjMoveStringLiteralDfa17_0(active0, 0, active1, 0x60000, active2, 0);
            case 108:
                if ((active1 & 0x2000) != 0)
                    return JjStopAtPos(16, 77);
                return JjMoveStringLiteralDfa17_0(active0, 0, active1, 0x600, active2, 0x2);
            case 109:
                return JjMoveStringLiteralDfa17_0(active0, 0x280000000000, active1, 0, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa17_0(active0, 0, active1, 0x1, active2, 0);
            case 116:
                return JjMoveStringLiteralDfa17_0(active0, 0, active1, 0x800, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(15, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa17_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(15, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(16, active0, active1, active2);
            return 17;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa18_0(active0, 0, active1, 0, active2, 0x1);
            case 49:
                if ((active1 & 0x200) != 0)
                    return JjStopAtPos(17, 73);
                break;
            case 50:
                if ((active1 & 0x400) != 0)
                    return JjStopAtPos(17, 74);
                break;
            case 97:
                return JjMoveStringLiteralDfa18_0(active0, 0, active1, 0x800, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa18_0(active0, 0, active1, 0, active2, 0x2);
            case 110:
                if ((active1 & 0x1) != 0)
                    return JjStopAtPos(17, 64);
                break;
            case 111:
                return JjMoveStringLiteralDfa18_0(active0, 0x680000000000, active1, 0x60000, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(16, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa18_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(16, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(17, active0, active1, active2);
            return 18;
        }

        switch (curChar)
        {
            case 45:
                return JjMoveStringLiteralDfa19_0(active0, 0, active1, 0x60000, active2, 0);
            case 100:
                return JjMoveStringLiteralDfa19_0(active0, 0x280000000000, active1, 0, active2, 0);
            case 108:
                if ((active1 & 0x800) != 0)
                    return JjStopAtPos(18, 75);
                return JjMoveStringLiteralDfa19_0(active0, 0x400000000000, active1, 0, active2, 0x1);
            case 110:
                return JjMoveStringLiteralDfa19_0(active0, 0, active1, 0, active2, 0x2);
            default:
                break;
        }

        return JjStartNfa_0(17, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa19_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(17, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(18, active0, active1, active2);
            return 19;
        }

        switch (curChar)
        {
            case 101:
                if ((active0 & 0x80000000000) != 0)
                    return JjStopAtPos(19, 43);
                else if ((active0 & 0x200000000000) != 0)
                    return JjStopAtPos(19, 45);
                else if ((active2 & 0x2) != 0)
                    return JjStopAtPos(19, 129);
                break;
            case 105:
                return JjMoveStringLiteralDfa20_0(active0, 0, active1, 0, active2, 0x1);
            case 109:
                return JjMoveStringLiteralDfa20_0(active0, 0, active1, 0x60000, active2, 0);
            case 111:
                return JjMoveStringLiteralDfa20_0(active0, 0x400000000000, active1, 0, active2, 0);
            default:
                break;
        }

        return JjStartNfa_0(18, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa20_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(18, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(19, active0, active1, active2);
            return 20;
        }

        switch (curChar)
        {
            case 97:
                return JjMoveStringLiteralDfa21_0(active0, 0, active1, 0x20000, active2, 0);
            case 105:
                return JjMoveStringLiteralDfa21_0(active0, 0, active1, 0x40000, active2, 0);
            case 110:
                return JjMoveStringLiteralDfa21_0(active0, 0, active1, 0, active2, 0x1);
            case 114:
                if ((active0 & 0x400000000000) != 0)
                    return JjStopAtPos(20, 46);
                break;
            default:
                break;
        }

        return JjStartNfa_0(19, active0, active1, active2);
    }

    private int JjMoveStringLiteralDfa21_0(long old0, long active0, long old1, long active1, long old2, long active2)
    {
        if (((active0 &= old0) | (active1 &= old1) | (active2 &= old2)) == 0)
            return JjStartNfa_0(19, old0, old1, old2);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(20, 0, active1, active2);
            return 21;
        }

        switch (curChar)
        {
            case 101:
                if ((active2 & 0x1) != 0)
                    return JjStopAtPos(21, 128);
                break;
            case 110:
                if ((active1 & 0x40000) != 0)
                    return JjStopAtPos(21, 82);
                break;
            case 120:
                if ((active1 & 0x20000) != 0)
                    return JjStopAtPos(21, 81);
                break;
            default:
                break;
        }

        return JjStartNfa_0(20, 0, active1, active2);
    }

    private int JjStartNfaWithStates_0(int pos, int kind, int state)
    {
        jjmatchedKind = kind;
        jjmatchedPos = pos;
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            return pos + 1;
        }

        return JjMoveNfa_0(state, pos + 1);
    }

    static readonly long[] jjbitVec0 = new[]
    {
        0x0,
        0x0,
        0xffffffffffffffff,
        0xffffffffffffffff
    };
    private int JjMoveNfa_0(int startState, int curPos)
    {
        int startsAt = 0;
        jjnewStateCnt = 72;
        int i = 1;
        jjstateSet[0] = startState;
        int kind = 0x7fffffff;
        for (;;)
        {
            if (++jjround == 0x7fffffff)
                ReInitRounds();
            if (curChar < 64)
            {
                long l = 1 << curChar;
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 0:
                            if ((0x3ff000000000000 & l) != 0)
                            {
                                if (kind > 21)
                                    kind = 21;
                                JjCheckNAddStates(0, 4);
                            }
                            else if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(48);
                            else if (curChar == 47)
                                JjAddStates(5, 6);
                            else if (curChar == 39)
                                JjCheckNAddTwoStates(60, 61);
                            else if (curChar == 34)
                                JjCheckNAddTwoStates(57, 58);
                            else if (curChar == 35)
                                jjstateSet[jjnewStateCnt++] = 1;
                            break;
                        case 1:
                            if ((0x3ff000000000000 & l) != 0)
                                JjAddStates(7, 13);
                            break;
                        case 2:
                        case 5:
                        case 9:
                        case 15:
                        case 22:
                        case 32:
                        case 46:
                            if ((0x3ff000000000000 & l) != 0)
                                JjCheckNAdd(3);
                            break;
                        case 3:
                            if ((0x3ff000000000000 & l) != 0 && kind > 20)
                                kind = 20;
                            break;
                        case 4:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 5;
                            break;
                        case 6:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 7;
                            break;
                        case 7:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 8;
                            break;
                        case 8:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 9;
                            break;
                        case 10:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 11;
                            break;
                        case 11:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 12;
                            break;
                        case 12:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 13;
                            break;
                        case 13:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 14;
                            break;
                        case 14:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 15;
                            break;
                        case 16:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 17;
                            break;
                        case 17:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 18;
                            break;
                        case 18:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 19;
                            break;
                        case 19:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 20;
                            break;
                        case 20:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 21;
                            break;
                        case 21:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 22;
                            break;
                        case 23:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 24;
                            break;
                        case 24:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 25;
                            break;
                        case 25:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 26;
                            break;
                        case 26:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 27;
                            break;
                        case 27:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 28;
                            break;
                        case 28:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 29;
                            break;
                        case 29:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 30;
                            break;
                        case 30:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 31;
                            break;
                        case 31:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 32;
                            break;
                        case 33:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 34;
                            break;
                        case 34:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 35;
                            break;
                        case 35:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 36;
                            break;
                        case 36:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 37;
                            break;
                        case 37:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 38;
                            break;
                        case 38:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 39;
                            break;
                        case 39:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 40;
                            break;
                        case 40:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 41;
                            break;
                        case 41:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 42;
                            break;
                        case 42:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 43;
                            break;
                        case 43:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 44;
                            break;
                        case 44:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 45;
                            break;
                        case 45:
                            if ((0x3ff000000000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 46;
                            break;
                        case 47:
                            if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(48);
                            break;
                        case 48:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 21)
                                kind = 21;
                            JjCheckNAddStates(0, 4);
                            break;
                        case 49:
                            if (curChar == 46)
                                JjCheckNAdd(50);
                            break;
                        case 50:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 21)
                                kind = 21;
                            JjCheckNAddStates(14, 17);
                            break;
                        case 55:
                            if (curChar == 37 && kind > 21)
                                kind = 21;
                            break;
                        case 56:
                            if (curChar == 34)
                                JjCheckNAddTwoStates(57, 58);
                            break;
                        case 57:
                            if ((0xfffffffbffffffff & l) != 0)
                                JjCheckNAddTwoStates(57, 58);
                            break;
                        case 58:
                            if (curChar == 34 && kind > 22)
                                kind = 22;
                            break;
                        case 59:
                            if (curChar == 39)
                                JjCheckNAddTwoStates(60, 61);
                            break;
                        case 60:
                            if ((0xffffff7fffffffff & l) != 0)
                                JjCheckNAddTwoStates(60, 61);
                            break;
                        case 61:
                            if (curChar == 39 && kind > 22)
                                kind = 22;
                            break;
                        case 63:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 142)
                                kind = 142;
                            jjstateSet[jjnewStateCnt++] = 63;
                            break;
                        case 64:
                            if (curChar == 47)
                                JjAddStates(5, 6);
                            break;
                        case 65:
                            if (curChar == 42)
                                JjCheckNAddTwoStates(66, 68);
                            break;
                        case 66:
                            if ((0xfffffbffffffffff & l) != 0)
                                JjCheckNAddTwoStates(66, 68);
                            break;
                        case 67:
                            if (curChar == 47 && kind > 143)
                                kind = 143;
                            break;
                        case 68:
                            if (curChar == 42)
                                jjstateSet[jjnewStateCnt++] = 67;
                            break;
                        case 69:
                            if (curChar == 47)
                                JjCheckNAddTwoStates(70, 71);
                            break;
                        case 70:
                            if ((0xffffffffffffdbff & l) != 0)
                                JjCheckNAddTwoStates(70, 71);
                            break;
                        case 71:
                            if ((0x2400 & l) != 0 && kind > 143)
                                kind = 143;
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }
            else if (curChar < 128)
            {
                long l = 1 << (curChar & 63);
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 0:
                            if ((0x7fffffe07fffffe & l) == 0)
                                break;
                            if (kind > 142)
                                kind = 142;
                            JjCheckNAdd(63);
                            break;
                        case 1:
                            if ((0x7e0000007e & l) != 0)
                                JjAddStates(7, 13);
                            break;
                        case 2:
                        case 5:
                        case 9:
                        case 15:
                        case 22:
                        case 32:
                        case 46:
                            if ((0x7e0000007e & l) != 0)
                                JjCheckNAdd(3);
                            break;
                        case 3:
                            if ((0x7e0000007e & l) != 0 && kind > 20)
                                kind = 20;
                            break;
                        case 4:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 5;
                            break;
                        case 6:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 7;
                            break;
                        case 7:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 8;
                            break;
                        case 8:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 9;
                            break;
                        case 10:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 11;
                            break;
                        case 11:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 12;
                            break;
                        case 12:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 13;
                            break;
                        case 13:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 14;
                            break;
                        case 14:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 15;
                            break;
                        case 16:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 17;
                            break;
                        case 17:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 18;
                            break;
                        case 18:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 19;
                            break;
                        case 19:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 20;
                            break;
                        case 20:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 21;
                            break;
                        case 21:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 22;
                            break;
                        case 23:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 24;
                            break;
                        case 24:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 25;
                            break;
                        case 25:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 26;
                            break;
                        case 26:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 27;
                            break;
                        case 27:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 28;
                            break;
                        case 28:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 29;
                            break;
                        case 29:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 30;
                            break;
                        case 30:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 31;
                            break;
                        case 31:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 32;
                            break;
                        case 33:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 34;
                            break;
                        case 34:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 35;
                            break;
                        case 35:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 36;
                            break;
                        case 36:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 37;
                            break;
                        case 37:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 38;
                            break;
                        case 38:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 39;
                            break;
                        case 39:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 40;
                            break;
                        case 40:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 41;
                            break;
                        case 41:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 42;
                            break;
                        case 42:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 43;
                            break;
                        case 43:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 44;
                            break;
                        case 44:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 45;
                            break;
                        case 45:
                            if ((0x7e0000007e & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 46;
                            break;
                        case 51:
                            if (curChar == 117 && kind > 21)
                                kind = 21;
                            break;
                        case 52:
                            if (curChar == 103)
                                jjstateSet[jjnewStateCnt++] = 51;
                            break;
                        case 53:
                            if (curChar == 120 && kind > 21)
                                kind = 21;
                            break;
                        case 54:
                            if (curChar == 112)
                                jjstateSet[jjnewStateCnt++] = 53;
                            break;
                        case 57:
                            JjAddStates(18, 19);
                            break;
                        case 60:
                            JjAddStates(20, 21);
                            break;
                        case 63:
                            if ((0x7fffffe87fffffe & l) == 0)
                                break;
                            if (kind > 142)
                                kind = 142;
                            JjCheckNAdd(63);
                            break;
                        case 66:
                            JjAddStates(22, 23);
                            break;
                        case 70:
                            JjAddStates(24, 25);
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }
            else
            {
                int i2 = (curChar & 0xff) >> 6;
                long l2 = 1 << (curChar & 63);
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 57:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(18, 19);
                            break;
                        case 60:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(20, 21);
                            break;
                        case 66:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(22, 23);
                            break;
                        case 70:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(24, 25);
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }

            if (kind != 0x7fffffff)
            {
                jjmatchedKind = kind;
                jjmatchedPos = curPos;
                kind = 0x7fffffff;
            }

            ++curPos;
            if ((i = jjnewStateCnt) == (startsAt = 72 - (jjnewStateCnt = startsAt)))
                return curPos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (java.io.IOException e)
            {
                return curPos;
            }
        }
    }

    static readonly int[] jjnextStates = new[]
    {
        48,
        49,
        52,
        54,
        55,
        65,
        69,
        2,
        4,
        6,
        10,
        16,
        23,
        33,
        50,
        52,
        54,
        55,
        57,
        58,
        60,
        61,
        66,
        68,
        70,
        71
    };
    public static readonly string[] jjstrLiteralImages = new[]
    {
        "",
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        ".",
        "{",
        "}",
        "(",
        ")",
        "#",
        ":",
        ";",
        ",",
        "rgba",
        "rgb",
        null,
        null,
        null,
        "url",
        "graph",
        "edge",
        "node",
        "sprite",
        "fill-mode",
        "fill-color",
        "fill-image",
        "stroke-mode",
        "stroke-color",
        "stroke-width",
        "shadow-mode",
        "shadow-color",
        "shadow-width",
        "shadow-offset",
        "text-mode",
        "text-color",
        "text-style",
        "text-font",
        "text-size",
        "text-visibility-mode",
        "text-visibility",
        "text-background-mode",
        "text-background-color",
        "text-offset",
        "text-padding",
        "icon-mode",
        "icon",
        "padding",
        "z-index",
        "visibility-mode",
        "visibility",
        "shape",
        "size",
        "size-mode",
        "shape-points",
        "text-alignment",
        "jcomponent",
        "arrow-image",
        "arrow-size",
        "arrow-shape",
        "sprite-orientation",
        "canvas-color",
        "plain",
        "dyn-plain",
        "dyn-size",
        "dyn-icon",
        "dashes",
        "dots",
        "double",
        "gradient-diagonal1",
        "gradient-diagonal2",
        "gradient-horizontal",
        "gradient-radial",
        "gradient-vertical",
        "hidden",
        "image-tiled",
        "image-scaled",
        "image-scaled-ratio-max",
        "image-scaled-ratio-min",
        "none",
        "normal",
        "truncated",
        "zoom-range",
        "at-zoom",
        "under-zoom",
        "over-zoom",
        "zooms",
        "fit",
        "bold",
        "bold-italic",
        "italic",
        "along",
        "at-left",
        "at-right",
        "center",
        "left",
        "right",
        "under",
        "above",
        "justify",
        "circle",
        "triangle",
        "freeplane",
        "text-box",
        "text-rounded-box",
        "text-circle",
        "text-diamond",
        "text-paragraph",
        "box",
        "rounded-box",
        "cross",
        "diamond",
        "polygon",
        "button",
        "text-field",
        "panel",
        "line",
        "polyline",
        "polyline-scaled",
        "angle",
        "cubic-curve",
        "blob",
        "square-line",
        "L-square-line",
        "horizontal-square-line",
        "vertical-square-line",
        "arrow",
        "flow",
        "pie-chart",
        "image",
        "images",
        "from",
        "node0",
        "to",
        "node1",
        "projection",
        "clicked",
        "selected",
        null,
        null
    };
    public static readonly string[] lexStateNames = new[]
    {
        "DEFAULT"
    };
    static readonly long[] jjtoToken = new[]
    {
        0xfffffffffffffe01,
        0xffffffffffffffff,
        0xffff
    };
    static readonly long[] jjtoSkip = new[]
    {
        0x1e,
        0x0,
        0x0
    };
    protected SimpleCharStream input_stream;
    private readonly int[] jjrounds = new int[72];
    private readonly int[] jjstateSet = new int[144];
    protected char curChar;
    public StyleSheetParserTokenManager(SimpleCharStream stream)
    {
        if (SimpleCharStream.staticFlag)
            throw new Exception("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
        input_stream = stream;
    }

    public StyleSheetParserTokenManager(SimpleCharStream stream, int lexState) : this(stream)
    {
        SwitchTo(lexState);
    }

    public virtual void ReInit(SimpleCharStream stream)
    {
        jjmatchedPos = jjnewStateCnt = 0;
        curLexState = defaultLexState;
        input_stream = stream;
        ReInitRounds();
    }

    private void ReInitRounds()
    {
        int i;
        jjround = 0x80000001;
        for (i = 72; i-- > 0;)
            jjrounds[i] = 0x80000000;
    }

    public virtual void ReInit(SimpleCharStream stream, int lexState)
    {
        ReInit(stream);
        SwitchTo(lexState);
    }

    public virtual void SwitchTo(int lexState)
    {
        if (lexState >= 1 || lexState < 0)
            throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.INVALID_LEXICAL_STATE);
        else
            curLexState = lexState;
    }

    protected virtual Token JjFillToken()
    {
        Token t;
        string curTokenImage;
        int beginLine;
        int endLine;
        int beginColumn;
        int endColumn;
        string im = jjstrLiteralImages[jjmatchedKind];
        curTokenImage = (im == null) ? input_stream.GetImage() : im;
        beginLine = input_stream.GetBeginLine();
        beginColumn = input_stream.GetBeginColumn();
        endLine = input_stream.GetEndLine();
        endColumn = input_stream.GetEndColumn();
        t = Token.NewToken(jjmatchedKind, curTokenImage);
        t.beginLine = beginLine;
        t.endLine = endLine;
        t.beginColumn = beginColumn;
        t.endColumn = endColumn;
        return t;
    }

    int curLexState = 0;
    int defaultLexState = 0;
    int jjnewStateCnt;
    int jjround;
    int jjmatchedPos;
    int jjmatchedKind;
    public virtual Token GetNextToken()
    {
        Token matchedToken;
        int curPos = 0;
        EOFLoop:
            for (;;)
            {
                try
                {
                    curChar = input_stream.BeginToken();
                }
                catch (java.io.IOException e)
                {
                    jjmatchedKind = 0;
                    matchedToken = JjFillToken();
                    return matchedToken;
                }

                try
                {
                    input_stream.Backup(0);
                    while (curChar <= 32 && (0x100002600 & (1 << curChar)) != 0)
                        curChar = input_stream.BeginToken();
                }
                catch (java.io.IOException e1)
                {
                    continue;
                }

                jjmatchedKind = 0x7fffffff;
                jjmatchedPos = 0;
                curPos = JjMoveStringLiteralDfa0_0();
                if (jjmatchedKind != 0x7fffffff)
                {
                    if (jjmatchedPos + 1 < curPos)
                        input_stream.Backup(curPos - jjmatchedPos - 1);
                    if ((jjtoToken[jjmatchedKind >> 6] & (1 << (jjmatchedKind & 63))) != 0)
                    {
                        matchedToken = JjFillToken();
                        return matchedToken;
                    }
                    else
                    {
                        continue;
                    }
                }

                int error_line = input_stream.GetEndLine();
                int error_column = input_stream.GetEndColumn();
                string error_after = null;
                bool EOFSeen = false;
                try
                {
                    input_stream.ReadChar();
                    input_stream.Backup(1);
                }
                catch (java.io.IOException e1)
                {
                    EOFSeen = true;
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                    if (curChar == '\n' || curChar == '\r')
                    {
                        error_line++;
                        error_column = 0;
                    }
                    else
                        error_column++;
                }

                if (!EOFSeen)
                {
                    input_stream.Backup(1);
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                }

                throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LEXICAL_ERROR);
            }
    }

    private void JjCheckNAdd(int state)
    {
        if (jjrounds[state] != jjround)
        {
            jjstateSet[jjnewStateCnt++] = state;
            jjrounds[state] = jjround;
        }
    }

    private void JjAddStates(int start, int end)
    {
        do
        {
            jjstateSet[jjnewStateCnt++] = jjnextStates[start];
        }
        while (start++ != end);
    }

    private void JjCheckNAddTwoStates(int state1, int state2)
    {
        JjCheckNAdd(state1);
        JjCheckNAdd(state2);
    }

    private void JjCheckNAddStates(int start, int end)
    {
        do
        {
            JjCheckNAdd(jjnextStates[start]);
        }
        while (start++ != end);
    }
}
