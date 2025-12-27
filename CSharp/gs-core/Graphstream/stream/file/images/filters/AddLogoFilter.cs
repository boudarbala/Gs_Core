using Gs_Core.Graphstream.Stream.File.Images;
using Javax.Imageio;
using Java;
using Java.Awt.Image;
using Java.Io;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Images.Filters.ElementType;
using static Org.Graphstream.Stream.File.Images.Filters.EventType;
using static Org.Graphstream.Stream.File.Images.Filters.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Images.Filters.Mode;
using static Org.Graphstream.Stream.File.Images.Filters.What;
using static Org.Graphstream.Stream.File.Images.Filters.TimeFormat;
using static Org.Graphstream.Stream.File.Images.Filters.OutputType;
using static Org.Graphstream.Stream.File.Images.Filters.OutputPolicy;
using static Org.Graphstream.Stream.File.Images.Filters.LayoutPolicy;
using static Org.Graphstream.Stream.File.Images.Filters.Quality;
using static Org.Graphstream.Stream.File.Images.Filters.Option;
using static Org.Graphstream.Stream.File.Images.Filters.AttributeType;
using static Org.Graphstream.Stream.File.Images.Filters.Balise;
using static Org.Graphstream.Stream.File.Images.Filters.GEXFAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.METAAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.NODESAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.NODEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.PARENTAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EDGESAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.SPELLAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.COLORAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.SIZEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EDGEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.IDType;
using static Org.Graphstream.Stream.File.Images.Filters.ModeType;
using static Org.Graphstream.Stream.File.Images.Filters.WeightType;
using static Org.Graphstream.Stream.File.Images.Filters.EdgeType;
using static Org.Graphstream.Stream.File.Images.Filters.NodeShapeType;
using static Org.Graphstream.Stream.File.Images.Filters.EdgeShapeType;
using static Org.Graphstream.Stream.File.Images.Filters.ClassType;
using static Org.Graphstream.Stream.File.Images.Filters.TimeFormatType;
using static Org.Graphstream.Stream.File.Images.Filters.GPXAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.WPTAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.LINKAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EMAILAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.PTAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.FixType;
using static Org.Graphstream.Stream.File.Images.Filters.GraphAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.LocatorAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.NodeAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EdgeAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.DataAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.PortAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EndPointAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.EndPointType;
using static Org.Graphstream.Stream.File.Images.Filters.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.KeyAttribute;
using static Org.Graphstream.Stream.File.Images.Filters.KeyDomain;
using static Org.Graphstream.Stream.File.Images.Filters.KeyAttrType;
using static Org.Graphstream.Stream.File.Images.Filters.GraphEvents;
using static Org.Graphstream.Stream.File.Images.Filters.ThreadingModel;
using static Org.Graphstream.Stream.File.Images.Filters.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Images.Filters.Measures;
using static Org.Graphstream.Stream.File.Images.Filters.Token;
using static Org.Graphstream.Stream.File.Images.Filters.Extension;
using static Org.Graphstream.Stream.File.Images.Filters.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Images.Filters.AttrType;
using static Org.Graphstream.Stream.File.Images.Filters.Resolutions;
using static Org.Graphstream.Stream.File.Images.Filters.PropertyType;
using static Org.Graphstream.Stream.File.Images.Filters.Type;
using static Org.Graphstream.Stream.File.Images.Filters.Units;
using static Org.Graphstream.Stream.File.Images.Filters.FillMode;
using static Org.Graphstream.Stream.File.Images.Filters.StrokeMode;
using static Org.Graphstream.Stream.File.Images.Filters.ShadowMode;
using static Org.Graphstream.Stream.File.Images.Filters.VisibilityMode;
using static Org.Graphstream.Stream.File.Images.Filters.TextMode;
using static Org.Graphstream.Stream.File.Images.Filters.TextVisibilityMode;
using static Org.Graphstream.Stream.File.Images.Filters.TextStyle;
using static Org.Graphstream.Stream.File.Images.Filters.IconMode;
using static Org.Graphstream.Stream.File.Images.Filters.SizeMode;
using static Org.Graphstream.Stream.File.Images.Filters.TextAlignment;
using static Org.Graphstream.Stream.File.Images.Filters.TextBackgroundMode;
using static Org.Graphstream.Stream.File.Images.Filters.ShapeKind;
using static Org.Graphstream.Stream.File.Images.Filters.Shape;
using static Org.Graphstream.Stream.File.Images.Filters.SpriteOrientation;
using static Org.Graphstream.Stream.File.Images.Filters.ArrowShape;
using static Org.Graphstream.Stream.File.Images.Filters.JComponents;
using static Org.Graphstream.Stream.File.Images.Filters.InteractiveElement;

namespace Gs_Core.Graphstream.Stream.File.Images.Filters;

public class AddLogoFilter : Filter
{
    private BufferedImage logo;
    private int x, y;
    public AddLogoFilter(string logoFile, int x, int y)
    {
        File f = new File(logoFile);
        if (f.Exists())
            this.logo = ImageIO.Read(f);
        else
            this.logo = ImageIO.Read(ClassLoader.GetSystemResource(logoFile));
        this.x = x;
        this.y = y;
    }

    public virtual void Apply(BufferedImage image)
    {
        Graphics2D g = image.CreateGraphics();
        g.DrawImage(logo, x, y, null);
    }
}
