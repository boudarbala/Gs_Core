using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Images.ElementType;
using static Org.Graphstream.Stream.File.Images.EventType;
using static Org.Graphstream.Stream.File.Images.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Images.Mode;
using static Org.Graphstream.Stream.File.Images.What;
using static Org.Graphstream.Stream.File.Images.TimeFormat;
using static Org.Graphstream.Stream.File.Images.OutputType;
using static Org.Graphstream.Stream.File.Images.OutputPolicy;
using static Org.Graphstream.Stream.File.Images.LayoutPolicy;
using static Org.Graphstream.Stream.File.Images.Quality;
using static Org.Graphstream.Stream.File.Images.Option;
using static Org.Graphstream.Stream.File.Images.AttributeType;
using static Org.Graphstream.Stream.File.Images.Balise;
using static Org.Graphstream.Stream.File.Images.GEXFAttribute;
using static Org.Graphstream.Stream.File.Images.METAAttribute;
using static Org.Graphstream.Stream.File.Images.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Images.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Images.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Images.NODESAttribute;
using static Org.Graphstream.Stream.File.Images.NODEAttribute;
using static Org.Graphstream.Stream.File.Images.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Images.PARENTAttribute;
using static Org.Graphstream.Stream.File.Images.EDGESAttribute;
using static Org.Graphstream.Stream.File.Images.SPELLAttribute;
using static Org.Graphstream.Stream.File.Images.COLORAttribute;
using static Org.Graphstream.Stream.File.Images.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Images.SIZEAttribute;
using static Org.Graphstream.Stream.File.Images.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Images.EDGEAttribute;
using static Org.Graphstream.Stream.File.Images.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Images.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Images.IDType;
using static Org.Graphstream.Stream.File.Images.ModeType;
using static Org.Graphstream.Stream.File.Images.WeightType;
using static Org.Graphstream.Stream.File.Images.EdgeType;
using static Org.Graphstream.Stream.File.Images.NodeShapeType;
using static Org.Graphstream.Stream.File.Images.EdgeShapeType;
using static Org.Graphstream.Stream.File.Images.ClassType;
using static Org.Graphstream.Stream.File.Images.TimeFormatType;
using static Org.Graphstream.Stream.File.Images.GPXAttribute;
using static Org.Graphstream.Stream.File.Images.WPTAttribute;
using static Org.Graphstream.Stream.File.Images.LINKAttribute;
using static Org.Graphstream.Stream.File.Images.EMAILAttribute;
using static Org.Graphstream.Stream.File.Images.PTAttribute;
using static Org.Graphstream.Stream.File.Images.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Images.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Images.FixType;
using static Org.Graphstream.Stream.File.Images.GraphAttribute;
using static Org.Graphstream.Stream.File.Images.LocatorAttribute;
using static Org.Graphstream.Stream.File.Images.NodeAttribute;
using static Org.Graphstream.Stream.File.Images.EdgeAttribute;
using static Org.Graphstream.Stream.File.Images.DataAttribute;
using static Org.Graphstream.Stream.File.Images.PortAttribute;
using static Org.Graphstream.Stream.File.Images.EndPointAttribute;
using static Org.Graphstream.Stream.File.Images.EndPointType;
using static Org.Graphstream.Stream.File.Images.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Images.KeyAttribute;
using static Org.Graphstream.Stream.File.Images.KeyDomain;
using static Org.Graphstream.Stream.File.Images.KeyAttrType;
using static Org.Graphstream.Stream.File.Images.GraphEvents;
using static Org.Graphstream.Stream.File.Images.ThreadingModel;
using static Org.Graphstream.Stream.File.Images.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Images.Measures;
using static Org.Graphstream.Stream.File.Images.Token;
using static Org.Graphstream.Stream.File.Images.Extension;
using static Org.Graphstream.Stream.File.Images.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Images.AttrType;
using static Org.Graphstream.Stream.File.Images.Resolutions;

namespace Gs_Core.Graphstream.Stream.File.Images;

public enum Resolutions
{
    QVGA,
    CGA,
    VGA,
    NTSC,
    PAL,
    WVGA_5by3,
    SVGA,
    WVGA_16by9,
    WSVGA,
    XGA,
    HD720,
    WXGA_5by3,
    WXGA_8by5,
    SXGA,
    FWXGA,
    SXGAp,
    WSXGAp,
    UXGA,
    HD1080,
    WUXGA,
    TwoK,
    QXGA,
    WQXGA,
    QSXGA,
    UHD_4K,
    UHD_8K_16by9,
    UHD_8K_17by8,
    UHD_8K_1by1
}
