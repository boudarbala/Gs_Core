using Java.Io;
using Java.Net;
using Java.Util;
using Java.Util.Regex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EventType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Mode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.What;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Option;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Balise;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IDType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FixType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphEvents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ThreadingModel;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.CloseFramePolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Measures;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Token;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Extension;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DefaultEdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Resolutions;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PropertyType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Type;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Units;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FillMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.StrokeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShadowMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.VisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextVisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextStyle;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IconMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SizeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextAlignment;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextBackgroundMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShapeKind;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Shape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SpriteOrientation;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ArrowShape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.JComponents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;

public class StyleConstants
{
    public enum Units
    {
        PX,
        GU,
        PERCENTS
    }

    public enum FillMode
    {
        NONE,
        PLAIN,
        DYN_PLAIN,
        GRADIENT_RADIAL,
        GRADIENT_HORIZONTAL,
        GRADIENT_VERTICAL,
        GRADIENT_DIAGONAL1,
        GRADIENT_DIAGONAL2,
        IMAGE_TILED,
        IMAGE_SCALED,
        IMAGE_SCALED_RATIO_MAX,
        IMAGE_SCALED_RATIO_MIN
    }

    public enum StrokeMode
    {
        NONE,
        PLAIN,
        DASHES,
        DOTS,
        DOUBLE
    }

    public enum ShadowMode
    {
        NONE,
        PLAIN,
        GRADIENT_RADIAL,
        GRADIENT_HORIZONTAL,
        GRADIENT_VERTICAL,
        GRADIENT_DIAGONAL1,
        GRADIENT_DIAGONAL2
    }

    public enum VisibilityMode
    {
        NORMAL,
        HIDDEN,
        AT_ZOOM,
        UNDER_ZOOM,
        OVER_ZOOM,
        ZOOM_RANGE,
        ZOOMS
    }

    public enum TextMode
    {
        NORMAL,
        TRUNCATED,
        HIDDEN
    }

    public enum TextVisibilityMode
    {
        NORMAL,
        HIDDEN,
        AT_ZOOM,
        UNDER_ZOOM,
        OVER_ZOOM,
        ZOOM_RANGE,
        ZOOMS
    }

    public enum TextStyle
    {
        NORMAL,
        ITALIC,
        BOLD,
        BOLD_ITALIC
    }

    public enum IconMode
    {
        NONE,
        AT_LEFT,
        AT_RIGHT,
        UNDER,
        ABOVE
    }

    public enum SizeMode
    {
        NORMAL,
        FIT,
        DYN_SIZE
    }

    public enum TextAlignment
    {
        CENTER,
        LEFT,
        RIGHT,
        AT_LEFT,
        AT_RIGHT,
        UNDER,
        ABOVE,
        JUSTIFY,
        ALONG
    }

    public enum TextBackgroundMode
    {
        NONE,
        PLAIN,
        ROUNDEDBOX
    }

    public enum ShapeKind
    {
        ELLIPSOID,
        RECTANGULAR,
        LINEAR,
        CURVE
    }

    public enum Shape
    {
        CIRCLE,
        BOX,
        ROUNDED_BOX,
        DIAMOND,
        POLYGON,
        TRIANGLE,
        CROSS,
        FREEPLANE,
        TEXT_BOX,
        TEXT_ROUNDED_BOX,
        TEXT_PARAGRAPH,
        TEXT_CIRCLE,
        TEXT_DIAMOND,
        JCOMPONENT,
        PIE_CHART,
        FLOW,
        ARROW,
        IMAGES,
        LINE,
        ANGLE,
        CUBIC_CURVE,
        POLYLINE,
        POLYLINE_SCALED,
        SQUARELINE,
        LSQUARELINE,
        HSQUARELINE,
        VSQUARELINE,
        BLOB
    }

    public enum SpriteOrientation
    {
        NONE,
        FROM,
        NODE0,
        TO,
        NODE1,
        PROJECTION
    }

    public enum ArrowShape
    {
        NONE,
        ARROW,
        CIRCLE,
        DIAMOND,
        IMAGE
    }

    public enum JComponents
    {
        BUTTON,
        TEXT_FIELD,
        PANEL
    }

    protected static HashMap<string, Color> colorMap;
    protected static Pattern sharpColor1, sharpColor2;
    protected static Pattern cssColor;
    protected static Pattern cssColorA;
    protected static Pattern awtColor;
    protected static Pattern hexaColor;
    protected static Pattern numberUnit, number;
    static StyleConstants()
    {
        number = Pattern.Compile("\\s*(\\p{Digit}+([.]\\p{Digit})?)\\s*");
        numberUnit = Pattern.Compile("\\s*(\\p{Digit}+(?:[.]\\p{Digit}+)?)\\s*(gu|px|%)\\s*");
        sharpColor1 = Pattern.Compile("#(\\p{XDigit}\\p{XDigit})(\\p{XDigit}\\p{XDigit})(\\p{XDigit}\\p{XDigit})((\\p{XDigit}\\p{XDigit})?)");
        sharpColor2 = Pattern.Compile("#(\\p{XDigit})(\\p{XDigit})(\\p{XDigit})((\\p{XDigit})?)");
        hexaColor = Pattern.Compile("0[xX](\\p{XDigit}\\p{XDigit})(\\p{XDigit}\\p{XDigit})(\\p{XDigit}\\p{XDigit})((\\p{XDigit}\\p{XDigit})?)");
        cssColor = Pattern.Compile("rgb\\s*\\(\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*\\)");
        cssColorA = Pattern.Compile("rgba\\s*\\(\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*,\\s*([0-9]+)\\s*\\)");
        awtColor = Pattern.Compile("java.awt.Color\\[r=([0-9]+),g=([0-9]+),b=([0-9]+)\\]");
        colorMap = new HashMap<string, Color>();
        URL url = typeof(StyleConstants).GetResource("rgb.properties");
        if (url == null)
            throw new Exception("corrupted graphstream.jar ? the org/miv/graphstream/ui/graphicGraph/rgb.properties file is not found");
        Properties p = new Properties();
        try
        {
            p.Load(url.OpenStream());
        }
        catch (IOException e)
        {
            e.PrintStackTrace();
        }

        foreach (object o in p.KeySet())
        {
            string key = (string)o;
            string val = p.GetProperty(key);
            Color col = Color.Decode(val);
            colorMap.Put(key.ToLowerCase(), col);
        }
    }

    public static Color ConvertColor(object anyValue)
    {
        if (anyValue == null)
            return null;
        if (anyValue is Color)
            return (Color)anyValue;
        if (anyValue is string)
        {
            Color c = null;
            string value = (string)anyValue;
            if (value.StartsWith("#"))
            {
                Matcher m = sharpColor1.Matcher(value);
                if (m.Matches())
                {
                    if (value.Length == 7)
                    {
                        try
                        {
                            c = Color.Decode(value);
                            return c;
                        }
                        catch (NumberFormatException e)
                        {
                            c = null;
                        }
                    }
                    else if (value.Length == 9)
                    {
                        int r = Integer.ParseInt(m.Group(1), 16);
                        int g = Integer.ParseInt(m.Group(2), 16);
                        int b = Integer.ParseInt(m.Group(3), 16);
                        int a = Integer.ParseInt(m.Group(4), 16);
                        return new Color(r, g, b, a);
                    }
                }

                m = sharpColor2.Matcher(value);
                if (m.Matches())
                {
                    if (value.Length >= 4)
                    {
                        int r = Integer.ParseInt(m.Group(1), 16) * 16;
                        int g = Integer.ParseInt(m.Group(2), 16) * 16;
                        int b = Integer.ParseInt(m.Group(3), 16) * 16;
                        int a = 255;
                        if (value.Length == 5)
                            a = Integer.ParseInt(m.Group(4), 16) * 16;
                        return new Color(r, g, b, a);
                    }
                }
            }
            else if (value.StartsWith("rgb"))
            {
                Matcher m = cssColorA.Matcher(value);
                if (m.Matches())
                {
                    int r = Integer.ParseInt(m.Group(1));
                    int g = Integer.ParseInt(m.Group(2));
                    int b = Integer.ParseInt(m.Group(3));
                    int a = Integer.ParseInt(m.Group(4));
                    return new Color(r, g, b, a);
                }

                m = cssColor.Matcher(value);
                if (m.Matches())
                {
                    int r = Integer.ParseInt(m.Group(1));
                    int g = Integer.ParseInt(m.Group(2));
                    int b = Integer.ParseInt(m.Group(3));
                    return new Color(r, g, b);
                }
            }
            else if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                Matcher m = hexaColor.Matcher(value);
                if (m.Matches())
                {
                    if (value.Length == 8)
                    {
                        try
                        {
                            return Color.Decode(value);
                        }
                        catch (NumberFormatException e)
                        {
                            c = null;
                        }
                    }
                    else if (value.Length == 10)
                    {
                        string r = m.Group(1);
                        string g = m.Group(2);
                        string b = m.Group(3);
                        string a = m.Group(4);
                        return new Color(Integer.ParseInt(r, 16), Integer.ParseInt(g, 16), Integer.ParseInt(b, 16), Integer.ParseInt(a, 16));
                    }
                }
            }
            else if (value.StartsWith("java.awt.Color["))
            {
                Matcher m = awtColor.Matcher(value);
                if (m.Matches())
                {
                    int r = Integer.ParseInt(m.Group(1));
                    int g = Integer.ParseInt(m.Group(2));
                    int b = Integer.ParseInt(m.Group(3));
                    return new Color(r, g, b);
                }
            }

            return colorMap[value.ToLowerCase()];
        }

        return null;
    }

    public static string ConvertLabel(object value)
    {
        string label = null;
        if (value != null)
        {
            if (value is CharSequence)
                label = ((CharSequence)value).ToString();
            else
                label = value.ToString();
            if (label.Length > 128)
                label = String.Format("%s...", label.Substring(0, 128));
        }

        return label;
    }

    public static float ConvertWidth(object value)
    {
        if (value is CharSequence)
        {
            try
            {
                float val = Float.ParseFloat(((CharSequence)value).ToString());
                return val;
            }
            catch (NumberFormatException e)
            {
                return -1;
            }
        }
        else if (value is Number)
        {
            return ((Number)value).FloatValue();
        }

        return -1;
    }

    public static Value ConvertValue(object value)
    {
        if (value is CharSequence)
        {
            CharSequence string = (CharSequence)value;
            if (@string.Length < 0)
                throw new Exception("empty size string ...");
            Matcher m = numberUnit.Matcher(@string);
            if (m.Matches())
                return new Value(ConvertUnit(m.Group(2)), Float.ParseFloat(m.Group(1)));
            m = number.Matcher(@string);
            if (m.Matches())
                return new Value(Units.PX, Float.ParseFloat(m.Group(1)));
            throw new Exception(String.Format("string is not convertible to a value (%s)", @string));
        }
        else if (value is Number)
        {
            return new Value(Units.PX, ((Number)value).FloatValue());
        }

        if (value == null)
            throw new Exception("cannot convert null value");
        throw new Exception(String.Format("value is of class %s%n", value.GetType().GetName()));
    }

    protected static Units ConvertUnit(string unit)
    {
        if (unit.Equals("gu"))
            return Units.GU;
        else if (unit.Equals("px"))
            return Units.PX;
        else if (unit.Equals("%"))
            return Units.PERCENTS;
        return Units.PX;
    }
}
