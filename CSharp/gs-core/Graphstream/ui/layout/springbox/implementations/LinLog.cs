using Java.Util;
using Gs_Core.Graphstream.Ui.Layout.Springbox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ElementType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EventType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttributeChangeEvent;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Mode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.What;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TimeFormat;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.OutputType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.OutputPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LayoutPolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Quality;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Option;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttributeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Balise;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GEXFAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.METAAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GRAPHAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ATTVALUEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PARENTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGESAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SPELLAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.COLORAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.POSITIONAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SIZEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NODESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.THICKNESSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.IDType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ModeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.WeightType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NodeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeShapeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ClassType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TimeFormatType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GPXAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.WPTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LINKAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EMAILAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.BOUNDSAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.FixType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GraphAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.LocatorAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.NodeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.DataAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PortAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EndPointAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.EndPointType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.HyperEdgeAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyAttribute;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyDomain;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.KeyAttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.GraphEvents;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ThreadingModel;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.CloseFramePolicy;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Measures;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Token;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Extension;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.DefaultEdgeType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.AttrType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Resolutions;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.PropertyType;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Type;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Units;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.FillMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.StrokeMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ShadowMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.VisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextVisibilityMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextStyle;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.IconMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SizeMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextAlignment;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.TextBackgroundMode;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ShapeKind;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.Shape;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.SpriteOrientation;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.ArrowShape;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.JComponents;
using static Org.Graphstream.Ui.Layout.Springbox.Implementations.InteractiveElement;

namespace Gs_Core.Graphstream.Ui.Layout.Springbox.Implementations;

public class LinLog : BarnesHutLayout
{
    protected double k = 1;
    protected double aFactor = 1F;
    protected double rFactor = 1F;
    protected bool edgeBased = true;
    protected double maxR = 0.5;
    protected double a = 0;
    protected double r = -1.2;
    public LinLog() : this(false)
    {
    }

    public LinLog(bool is3D) : this(is3D, new Random(System.CurrentTimeMillis()))
    {
    }

    public LinLog(bool is3D, Random randomNumberGenerator) : base(is3D, randomNumberGenerator)
    {
        SetQuality(1);
        force = 3;
    }

    public virtual void Configure(double a, double r, bool edgeBased, double force)
    {
        this.a = a;
        this.r = r;
        this.edgeBased = edgeBased;
        this.force = force;
    }

    public override string GetLayoutAlgorithmName()
    {
        return "LinLog";
    }

    public override void SetQuality(double qualityLevel)
    {
        base.SetQuality(qualityLevel);
        if (quality >= 1)
        {
            viewZone = -1;
        }
        else
        {
            viewZone = k;
        }
    }

    public override void Compute()
    {
        if (viewZone > 0)
            viewZone = area / 1.5;
        base.Compute();
    }

    protected override void ChooseNodePosition(NodeParticle n0, NodeParticle n1)
    {
    }

    public override NodeParticle NewNodeParticle(string id)
    {
        return new LinLogNodeParticle(this, id);
    }
}
