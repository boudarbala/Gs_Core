using Gs_Core.Graphstream.Ui.Geom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.Springbox.ElementType;
using static Org.Graphstream.Ui.Layout.Springbox.EventType;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Springbox.Mode;
using static Org.Graphstream.Ui.Layout.Springbox.What;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormat;
using static Org.Graphstream.Ui.Layout.Springbox.OutputType;
using static Org.Graphstream.Ui.Layout.Springbox.OutputPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Quality;
using static Org.Graphstream.Ui.Layout.Springbox.Option;
using static Org.Graphstream.Ui.Layout.Springbox.AttributeType;
using static Org.Graphstream.Ui.Layout.Springbox.Balise;
using static Org.Graphstream.Ui.Layout.Springbox.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.METAAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COLORAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.IDType;
using static Org.Graphstream.Ui.Layout.Springbox.ModeType;
using static Org.Graphstream.Ui.Layout.Springbox.WeightType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.NodeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.ClassType;
using static Org.Graphstream.Ui.Layout.Springbox.TimeFormatType;
using static Org.Graphstream.Ui.Layout.Springbox.GPXAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.WPTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LINKAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.FixType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.NodeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.DataAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.PortAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.EndPointType;
using static Org.Graphstream.Ui.Layout.Springbox.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.KeyDomain;
using static Org.Graphstream.Ui.Layout.Springbox.KeyAttrType;
using static Org.Graphstream.Ui.Layout.Springbox.GraphEvents;
using static Org.Graphstream.Ui.Layout.Springbox.ThreadingModel;
using static Org.Graphstream.Ui.Layout.Springbox.CloseFramePolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Measures;
using static Org.Graphstream.Ui.Layout.Springbox.Token;
using static Org.Graphstream.Ui.Layout.Springbox.Extension;
using static Org.Graphstream.Ui.Layout.Springbox.DefaultEdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.AttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Resolutions;
using static Org.Graphstream.Ui.Layout.Springbox.PropertyType;
using static Org.Graphstream.Ui.Layout.Springbox.Type;
using static Org.Graphstream.Ui.Layout.Springbox.Units;
using static Org.Graphstream.Ui.Layout.Springbox.FillMode;
using static Org.Graphstream.Ui.Layout.Springbox.StrokeMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShadowMode;
using static Org.Graphstream.Ui.Layout.Springbox.VisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextVisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextStyle;
using static Org.Graphstream.Ui.Layout.Springbox.IconMode;
using static Org.Graphstream.Ui.Layout.Springbox.SizeMode;
using static Org.Graphstream.Ui.Layout.Springbox.TextAlignment;
using static Org.Graphstream.Ui.Layout.Springbox.TextBackgroundMode;
using static Org.Graphstream.Ui.Layout.Springbox.ShapeKind;
using static Org.Graphstream.Ui.Layout.Springbox.Shape;
using static Org.Graphstream.Ui.Layout.Springbox.SpriteOrientation;
using static Org.Graphstream.Ui.Layout.Springbox.ArrowShape;
using static Org.Graphstream.Ui.Layout.Springbox.JComponents;

namespace Gs_Core.Graphstream.Ui.Layout.Springbox;

public class EdgeSpring
{
    public string id;
    public NodeParticle node0;
    public NodeParticle node1;
    public double weight = 1F;
    public Point3 spring = new Point3();
    public bool ignored = false;
    public double attE;
    public EdgeSpring(string id, NodeParticle n0, NodeParticle n1)
    {
        this.id = id;
        this.node0 = n0;
        this.node1 = n1;
    }

    public virtual NodeParticle GetOpposite(NodeParticle node)
    {
        return node0 == node ? node1 : node0;
    }
}
