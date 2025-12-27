using Java.Util;
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

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;

public class Style : StyleConstants
{
    protected Rule parent = null;
    protected HashMap<string, object> values = null;
    protected HashMap<string, Rule> alternates = null;
    public Style() : this(null)
    {
    }

    public Style(Rule parent)
    {
        this.parent = parent;
        this.values = new HashMap<string, object>();
    }

    public virtual Rule GetParent()
    {
        return parent;
    }

    public virtual object GetValue(string property, params string[] events)
    {
        if (events != null && events.Length > 0)
        {
            object o = null;
            int i = events.Length - 1;
            do
            {
                o = GetValueForEvent(property, events[i]);
                i--;
            }
            while (o == null && i >= 0);
            if (o != null)
                return o;
        }

        object value = values[property];
        if (value == null)
        {
            if (parent != null)
                return parent.style.GetValue(property, events);
        }

        return value;
    }

    protected virtual object GetValueForEvent(string property, string @event)
    {
        if (alternates != null)
        {
            Rule rule = alternates[@event];
            if (rule != null)
            {
                object o = rule.GetStyle().values[property];
                if (o != null)
                    return o;
            }
        }
        else if (parent != null)
        {
            return parent.style.GetValueForEvent(property, @event);
        }

        return null;
    }

    public virtual bool HasValue(string field, params string[] events)
    {
        bool hasValue = false;
        if (events != null && events.Length > 0 && alternates != null)
        {
            foreach (string event in events)
            {
                Rule rule = alternates[@event];
                if (rule != null)
                {
                    if (rule.GetStyle().HasValue(field))
                    {
                        hasValue = true;
                        break;
                    }
                }
            }
        }

        if (!hasValue)
        {
            hasValue = (values[field] != null);
        }

        return hasValue;
    }

    public virtual FillMode GetFillMode()
    {
        return (FillMode)GetValue("fill-mode");
    }

    public virtual Colors GetFillColors()
    {
        return (Colors)GetValue("fill-color");
    }

    public virtual int GetFillColorCount()
    {
        Colors colors = (Colors)GetValue("fill-color");
        if (colors != null)
            return colors.Count;
        return 0;
    }

    public virtual Color GetFillColor(int i)
    {
        Colors colors = (Colors)GetValue("fill-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual string GetFillImage()
    {
        return (string)GetValue("fill-image");
    }

    public virtual StrokeMode GetStrokeMode()
    {
        return (StrokeMode)GetValue("stroke-mode");
    }

    public virtual Colors GetStrokeColor()
    {
        return (Colors)GetValue("stroke-color");
    }

    public virtual int GetStrokeColorCount()
    {
        Colors colors = (Colors)GetValue("stroke-color");
        if (colors != null)
            return colors.Count;
        return 0;
    }

    public virtual Color GetStrokeColor(int i)
    {
        Colors colors = (Colors)GetValue("stroke-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual Value GetStrokeWidth()
    {
        return (Value)GetValue("stroke-width");
    }

    public virtual ShadowMode GetShadowMode()
    {
        return (ShadowMode)GetValue("shadow-mode");
    }

    public virtual Colors GetShadowColors()
    {
        return (Colors)GetValue("shadow-color");
    }

    public virtual int GetShadowColorCount()
    {
        Colors colors = (Colors)GetValue("shadow-color");
        if (colors != null)
            return colors.Count;
        return 0;
    }

    public virtual Color GetShadowColor(int i)
    {
        Colors colors = (Colors)GetValue("shadow-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual Value GetShadowWidth()
    {
        return (Value)GetValue("shadow-width");
    }

    public virtual Values GetShadowOffset()
    {
        return (Values)GetValue("shadow-offset");
    }

    public virtual Values GetPadding()
    {
        return (Values)GetValue("padding");
    }

    public virtual TextMode GetTextMode()
    {
        return (TextMode)GetValue("text-mode");
    }

    public virtual TextVisibilityMode GetTextVisibilityMode()
    {
        return (TextVisibilityMode)GetValue("text-visibility-mode");
    }

    public virtual Values GetTextVisibility()
    {
        return (Values)GetValue("text-visibility");
    }

    public virtual Colors GetTextColor()
    {
        return (Colors)GetValue("text-color");
    }

    public virtual int GetTextColorCount()
    {
        Colors colors = (Colors)GetValue("text-color");
        if (colors != null)
            return colors.Count;
        return 0;
    }

    public virtual Color GetTextColor(int i)
    {
        Colors colors = (Colors)GetValue("text-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual TextStyle GetTextStyle()
    {
        return (TextStyle)GetValue("text-style");
    }

    public virtual string GetTextFont()
    {
        return (string)GetValue("text-font");
    }

    public virtual Value GetTextSize()
    {
        return (Value)GetValue("text-size");
    }

    public virtual IconMode GetIconMode()
    {
        return (IconMode)GetValue("icon-mode");
    }

    public virtual string GetIcon()
    {
        return (string)GetValue("icon");
    }

    public virtual VisibilityMode GetVisibilityMode()
    {
        return (VisibilityMode)GetValue("visibility-mode");
    }

    public virtual Values GetVisibility()
    {
        return (Values)GetValue("visibility");
    }

    public virtual SizeMode GetSizeMode()
    {
        return (SizeMode)GetValue("size-mode");
    }

    public virtual Values GetSize()
    {
        return (Values)GetValue("size");
    }

    public virtual Values GetShapePoints()
    {
        return (Values)GetValue("shape-points");
    }

    public virtual TextAlignment GetTextAlignment()
    {
        return (TextAlignment)GetValue("text-alignment");
    }

    public virtual TextBackgroundMode GetTextBackgroundMode()
    {
        return (TextBackgroundMode)GetValue("text-background-mode");
    }

    public virtual Colors GetTextBackgroundColor()
    {
        return (Colors)GetValue("text-background-color");
    }

    public virtual Color GetTextBackgroundColor(int i)
    {
        Colors colors = (Colors)GetValue("text-background-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual Values GetTextOffset()
    {
        return (Values)GetValue("text-offset");
    }

    public virtual Values GetTextPadding()
    {
        return (Values)GetValue("text-padding");
    }

    public virtual Shape GetShape()
    {
        return (Shape)GetValue("shape");
    }

    public virtual JComponents GetJComponent()
    {
        return (JComponents)GetValue("jcomponent");
    }

    public virtual SpriteOrientation GetSpriteOrientation()
    {
        return (SpriteOrientation)GetValue("sprite-orientation");
    }

    public virtual ArrowShape GetArrowShape()
    {
        return (ArrowShape)GetValue("arrow-shape");
    }

    public virtual string GetArrowImage()
    {
        return (string)GetValue("arrow-image");
    }

    public virtual Values GetArrowSize()
    {
        return (Values)GetValue("arrow-size");
    }

    public virtual Colors GetCanvasColor()
    {
        return (Colors)GetValue("canvas-color");
    }

    public virtual int GetCanvasColorCount()
    {
        Colors colors = (Colors)GetValue("canvas-color");
        if (colors != null)
            return colors.Count;
        return 0;
    }

    public virtual Color GetCanvasColor(int i)
    {
        Colors colors = (Colors)GetValue("canvas-color");
        if (colors != null)
            return colors[i];
        return null;
    }

    public virtual int GetZIndex()
    {
        return (int)GetValue("z-index");
    }

    public virtual void SetDefaults()
    {
        Colors fillColor = new Colors();
        Colors strokeColor = new Colors();
        Colors shadowColor = new Colors();
        Colors textColor = new Colors();
        Colors canvasColor = new Colors();
        Colors textBgColor = new Colors();
        fillColor.Add(Color.BLACK);
        strokeColor.Add(Color.BLACK);
        shadowColor.Add(Color.GRAY);
        textColor.Add(Color.BLACK);
        canvasColor.Add(Color.WHITE);
        textBgColor.Add(Color.WHITE);
        values.Put("z-index", new int (0));
        values.Put("fill-mode", FillMode.PLAIN);
        values.Put("fill-color", fillColor);
        values.Put("fill-image", null);
        values.Put("stroke-mode", StrokeMode.NONE);
        values.Put("stroke-color", strokeColor);
        values.Put("stroke-width", new Value(Units.PX, 1));
        values.Put("shadow-mode", ShadowMode.NONE);
        values.Put("shadow-color", shadowColor);
        values.Put("shadow-width", new Value(Units.PX, 3));
        values.Put("shadow-offset", new Values(Units.PX, 3, 3));
        values.Put("padding", new Values(Units.PX, 0, 0, 0));
        values.Put("text-mode", TextMode.NORMAL);
        values.Put("text-visibility-mode", TextVisibilityMode.NORMAL);
        values.Put("text-visibility", null);
        values.Put("text-color", textColor);
        values.Put("text-style", TextStyle.NORMAL);
        values.Put("text-font", "default");
        values.Put("text-size", new Value(Units.PX, 10));
        values.Put("text-alignment", TextAlignment.CENTER);
        values.Put("text-background-mode", TextBackgroundMode.NONE);
        values.Put("text-background-color", textBgColor);
        values.Put("text-offset", new Values(Units.PX, 0, 0));
        values.Put("text-padding", new Values(Units.PX, 0, 0));
        values.Put("icon-mode", IconMode.NONE);
        values.Put("icon", null);
        values.Put("visibility-mode", VisibilityMode.NORMAL);
        values.Put("visibility", null);
        values.Put("size-mode", SizeMode.NORMAL);
        values.Put("size", new Values(Units.PX, 10, 10, 10));
        values.Put("shape", Shape.CIRCLE);
        values.Put("shape-points", null);
        values.Put("jcomponent", null);
        values.Put("sprite-orientation", SpriteOrientation.NONE);
        values.Put("arrow-shape", ArrowShape.ARROW);
        values.Put("arrow-size", new Values(Units.PX, 8, 4));
        values.Put("arrow-image", null);
        values.Put("canvas-color", canvasColor);
    }

    public virtual void Augment(Style other)
    {
        if (other != this)
        {
            AugmentField("z-index", other);
            AugmentField("fill-mode", other);
            AugmentField("fill-color", other);
            AugmentField("fill-image", other);
            AugmentField("stroke-mode", other);
            AugmentField("stroke-color", other);
            AugmentField("stroke-width", other);
            AugmentField("shadow-mode", other);
            AugmentField("shadow-color", other);
            AugmentField("shadow-width", other);
            AugmentField("shadow-offset", other);
            AugmentField("padding", other);
            AugmentField("text-mode", other);
            AugmentField("text-visibility-mode", other);
            AugmentField("text-visibility", other);
            AugmentField("text-color", other);
            AugmentField("text-style", other);
            AugmentField("text-font", other);
            AugmentField("text-size", other);
            AugmentField("text-alignment", other);
            AugmentField("text-background-mode", other);
            AugmentField("text-background-color", other);
            AugmentField("text-offset", other);
            AugmentField("text-padding", other);
            AugmentField("icon-mode", other);
            AugmentField("icon", other);
            AugmentField("visibility-mode", other);
            AugmentField("visibility", other);
            AugmentField("size-mode", other);
            AugmentField("size", other);
            AugmentField("shape", other);
            AugmentField("shape-points", other);
            AugmentField("jcomponent", other);
            AugmentField("sprite-orientation", other);
            AugmentField("arrow-shape", other);
            AugmentField("arrow-size", other);
            AugmentField("arrow-image", other);
            AugmentField("canvas-color", other);
        }
    }

    protected virtual void AugmentField(string field, Style other)
    {
        object value = other.values[field];
        if (value != null)
        {
            if (value is Value)
                SetValue(field, new Value((Value)value));
            else if (value is Values)
                SetValue(field, new Values((Values)value));
            else if (value is Colors)
                SetValue(field, new Colors((Colors)value));
            else
                SetValue(field, value);
        }
    }

    public virtual void Reparent(Rule parent)
    {
        this.parent = parent;
    }

    public virtual void AddAlternateStyle(string @event, Rule alternateStyle)
    {
        if (alternates == null)
            alternates = new HashMap<string, Rule>();
        alternates.Put(@event, alternateStyle);
    }

    public virtual void SetValue(string field, object value)
    {
        values.Put(field, value);
    }

    public override string ToString()
    {
        return ToString(-1);
    }

    public virtual string ToString(int level)
    {
        StringBuilder builder = new StringBuilder();
        string prefix = "";
        string sprefix = "    ";
        if (level > 0)
        {
            for (int i = 0; i < level; i++)
                prefix += "    ";
        }

        if (parent != null)
        {
            Rule p = parent;
            while (!(p == null))
            {
                builder.Append(String.Format(" -> %s", p.selector.ToString()));
                p = p.GetStyle().GetParent();
            }
        }

        builder.Append(String.Format("%n"));
        IEnumerator<string> i = values.KeySet().Iterator();
        while (i.HasNext())
        {
            string key = i.Next();
            object o = values[key];
            if (o is List<TWildcardTodo>)
            {
                List<TWildcardTodo> array = (List<TWildcardTodo>)o;
                if (array.Count > 0)
                {
                    builder.Append(String.Format("%s%s%s%s: ", prefix, sprefix, sprefix, key));
                    foreach (object p in array)
                        builder.Append(String.Format("%s ", p.ToString()));
                    builder.Append(String.Format("%n"));
                }
                else
                {
                    builder.Append(String.Format("%s%s%s%s: <empty>%n", prefix, sprefix, sprefix, key));
                }
            }
            else
            {
                builder.Append(String.Format("%s%s%s%s: %s%n", prefix, sprefix, sprefix, key, o != null ? o.ToString() : "<null>"));
            }
        }

        if (alternates != null && alternates.Count > 0)
        {
            foreach (Rule rule in alternates.Values())
            {
                builder.Append(rule.ToString(level - 1));
            }
        }

        string res = builder.ToString();
        if (res.Length == 0)
            return String.Format("%s%s<empty>%n", prefix, prefix);
        return res;
    }
}
