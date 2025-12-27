using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Java.Io;
using Java.Lang.Reflect;
using Java.Util;
using Java.Util.Logging;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Gs_Core.Graphstream.Ui.Layout.Springbox.Implementations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;
using static Org.Graphstream.Stream.File.TimeFormat;
using static Org.Graphstream.Stream.File.OutputType;
using static Org.Graphstream.Stream.File.OutputPolicy;
using static Org.Graphstream.Stream.File.LayoutPolicy;
using static Org.Graphstream.Stream.File.Quality;
using static Org.Graphstream.Stream.File.Option;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkTikZ : FileSinkBase
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(FileSinkTikZ).GetName());
    public static readonly string XYZ_ATTR = "xyz";
    public static readonly string WIDTH_ATTR = "ui.tikz.width";
    public static readonly string HEIGHT_ATTR = "ui.tikz.height";
    public static readonly double DEFAULT_WIDTH = 10;
    public static readonly double DEFAULT_HEIGHT = 10;
    public static readonly double DISPLAY_MIN_SIZE_IN_MM = 2;
    public static readonly double DISPLAY_MAX_SIZE_IN_MM = 10;
    protected PrintWriter out;
    protected HashMap<string, string> colors = new HashMap<string, string>();
    protected HashMap<string, string> classes = new HashMap<string, string>();
    protected HashMap<string, string> classNames = new HashMap<string, string>();
    protected int classIndex = 0;
    protected int colorIndex = 0;
    protected double width = Double.NaN;
    protected double height = Double.NaN;
    protected bool layout = false;
    protected GraphicGraph buffer;
    protected string css = null;
    protected double minSize = 0;
    protected double maxSize = 0;
    protected double displayMinSize = DISPLAY_MIN_SIZE_IN_MM;
    protected double displayMaxSize = DISPLAY_MAX_SIZE_IN_MM;
    private double xmin, ymin, xmax, ymax;
    private PointsWrapper points;
    private Locale l = Locale.ROOT;
    protected static string FormatId(string id)
    {
        return "node" + id.ReplaceAll("\\W", "_");
    }

    public FileSinkTikZ()
    {
        buffer = new GraphicGraph("tikz-buffer");
    }

    public virtual double GetWidth()
    {
        return width;
    }

    public virtual void SetWidth(double width)
    {
        this.width = width;
    }

    public virtual double GetHeight()
    {
        return height;
    }

    public virtual void SetHeight(double height)
    {
        this.height = height;
    }

    public virtual void SetDisplaySize(double min, double max)
    {
        this.displayMinSize = min;
        this.displayMaxSize = max;
    }

    public virtual void SetCSS(string css)
    {
        this.css = css;
    }

    public virtual void SetLayout(bool layout)
    {
        this.layout = layout;
    }

    protected virtual double GetNodeX(Node n)
    {
        if (n.HasAttribute(XYZ_ATTR))
            return ((Number)(n.GetArray(XYZ_ATTR)[0])).DoubleValue();
        if (n.HasAttribute("x"))
            return n.GetNumber("x");
        return Double.NaN;
    }

    protected virtual double GetNodeY(Node n)
    {
        if (n.HasAttribute(XYZ_ATTR))
            return ((Number)(n.GetArray(XYZ_ATTR)[1])).DoubleValue();
        if (n.HasAttribute("y"))
            return n.GetNumber("y");
        return Double.NaN;
    }

    protected virtual string GetNodeStyle(Node n)
    {
        string style = "tikzgsnode";
        if (n is GraphicNode)
        {
            GraphicNode gn = (GraphicNode)n;
            style = classNames[gn.style.GetId()];
            if (gn.style.GetFillMode() == FillMode.DYN_PLAIN)
            {
                double uicolor = gn.GetNumber("ui.color");
                if (Double.IsNaN(uicolor))
                    uicolor = 0;
                int c = gn.style.GetFillColorCount();
                int s = 1;
                double d = 1 / (c - 1);
                while (s * d < uicolor && s < c)
                    s++;
                uicolor -= (s - 1) * d;
                uicolor *= c;
                style += String.Format(Locale.ROOT, ", fill=%s!%d!%s", CheckColor(gn.style.GetFillColor(0)), (int)(uicolor * 100), CheckColor(gn.style.GetFillColor(1)));
            }

            if (gn.style.GetSizeMode() == SizeMode.DYN_SIZE)
            {
                double uisize = gn.GetNumber("ui.size");
                if (Double.IsNaN(uisize))
                    uisize = minSize;
                uisize = (uisize - minSize) / (maxSize - minSize);
                uisize = uisize * (displayMaxSize - displayMinSize) + displayMinSize;
                style += String.Format(Locale.ROOT, ", minimum size=%fmm", uisize);
            }
        }

        return style;
    }

    protected virtual string GetEdgeStyle(Edge e)
    {
        string style = "tikzgsnode";
        if (e is GraphicEdge)
        {
            GraphicEdge ge = (GraphicEdge)e;
            style = classNames[ge.style.GetId()];
            if (ge.style.GetFillMode() == FillMode.DYN_PLAIN)
            {
                double uicolor = ge.GetNumber("ui.color");
                if (Double.IsNaN(uicolor))
                    uicolor = 0;
                int c = ge.style.GetFillColorCount();
                int s = 1;
                double d = 1 / (c - 1);
                while (s * d < uicolor && s < c)
                    s++;
                uicolor -= (s - 1) * d;
                uicolor *= c;
                style += String.Format(Locale.ROOT, ", draw=%s!%d!%s", CheckColor(ge.style.GetFillColor(s - 1)), (int)(uicolor * 100), CheckColor(ge.style.GetFillColor(s)));
            }

            if (ge.style.GetSizeMode() == SizeMode.DYN_SIZE)
            {
                double uisize = ge.GetNumber("ui.size");
                if (Double.IsNaN(uisize) || uisize < 0.01)
                    uisize = 1;
                style += String.Format(Locale.ROOT, ", line width=%fpt", uisize);
            }
        }

        return style;
    }

    protected virtual string CheckColor(Color c)
    {
        string rgb = String.Format(Locale.ROOT, "%.3f,%.3f,%.3f", c.GetRed() / 255F, c.GetGreen() / 255F, c.GetBlue() / 255F);
        if (colors.ContainsKey(rgb))
            return colors[rgb];
        string key = String.Format("tikzC%02d", colorIndex++);
        colors.Put(rgb, key);
        return key;
    }

    protected virtual string GetTikzStyle(StyleGroup group)
    {
        StringBuilder buffer = new StringBuilder();
        LinkedList<string> style = new LinkedList<string>();
        for (int i = 0; i < group.GetFillColorCount(); i++)
            CheckColor(group.GetFillColor(i));
        switch (group.GetType())
        {
            case NODE:
            {
                if (group.GetFillMode() != FillMode.DYN_PLAIN)
                {
                    string fill = CheckColor(group.GetFillColor(0));
                    style.Add("fill=" + fill);
                }

                if (group.GetFillColor(0).GetAlpha() < 255)
                    style.Add(String.Format(Locale.ROOT, "fill opacity=%.2f", group.GetFillColor(0).GetAlpha() / 255F));
                switch (group.GetStrokeMode())
                {
                    case DOTS:
                    case DASHES:
                    case PLAIN:
                        string stroke = CheckColor(group.GetStrokeColor(0));
                        style.Add("draw=" + stroke);
                        style.Add("line width=" + String.Format(Locale.ROOT, "%.1fpt", group.GetStrokeWidth().value));
                        if (group.GetStrokeColor(0).GetAlpha() < 255)
                            style.Add(String.Format(Locale.ROOT, "draw opacity=%.2f", group.GetStrokeColor(0).GetAlpha() / 255F));
                        break;
                    default:
                        LOGGER.Warning(String.Format("unhandled stroke mode : %s%n", group.GetStrokeMode()));
                        break;
                }

                switch (group.GetShape())
                {
                    case CIRCLE:
                        style.Add("circle");
                        break;
                    case ROUNDED_BOX:
                        style.Add("rounded corners=2pt");
                    case BOX:
                        style.Add("rectangle");
                        break;
                    case TRIANGLE:
                        style.Add("isosceles triangle");
                        break;
                    case DIAMOND:
                        style.Add("diamond");
                        break;
                    default:
                        LOGGER.Warning(String.Format("unhandled shape : %s%n", group.GetShape()));
                        break;
                }

                string text = CheckColor(group.GetTextColor(0));
                style.Add("text=" + text);
                switch (group.GetSize().units)
                {
                    case GU:
                        style.Add("minimum size=" + String.Format(Locale.ROOT, "%.1fcm", group.GetSize().values[0]));
                        break;
                    case PX:
                        style.Add("minimum size=" + String.Format(Locale.ROOT, "%.1fpt", group.GetSize().values[0]));
                        break;
                    default:
                        LOGGER.Warning(String.Format("%% [warning] units %s are not compatible with TikZ.%n", group.GetSize().units));
                        break;
                }

                style.Add("inner sep=0pt");
            }

                break;
            case EDGE:
            {
                if (group.GetFillMode() != FillMode.DYN_PLAIN)
                {
                    string fill = CheckColor(group.GetFillColor(0));
                    style.Add("draw=" + fill);
                }

                if (group.GetFillColor(0).GetAlpha() < 255)
                    style.Add(String.Format(Locale.ROOT, "draw opacity=%.2f", group.GetFillColor(0).GetAlpha() / 255F));
                switch (group.GetSize().units)
                {
                    case PX:
                    case GU:
                        style.Add("line width=" + String.Format(Locale.ROOT, "%.1fpt", group.GetSize().values[0]));
                        break;
                    default:
                        LOGGER.Warning(String.Format("%% [warning] units %s are not compatible with TikZ.%n", group.GetSize().units));
                        break;
                }
            }

                break;
            default:
                LOGGER.Warning(String.Format("unhandled group type : %s%n", group.GetType()));
                break;
        }

        for (int i = 0; i < style.Count; i++)
        {
            if (i > 0)
                buffer.Append(",");
            buffer.Append(style[i]);
        }

        return buffer.ToString();
    }

    protected virtual void OutputHeader()
    {
        @out = (PrintWriter)output;
        colors.Clear();
        classes.Clear();
        classNames.Clear();
        buffer.Clear();
    }

    protected virtual void OutputEndOfFile()
    {
        if (Double.IsNaN(width))
        {
            if (buffer.HasNumber(WIDTH_ATTR))
                width = buffer.GetNumber(WIDTH_ATTR);
            else
                width = DEFAULT_WIDTH;
        }

        if (Double.IsNaN(height))
        {
            if (buffer.HasNumber(HEIGHT_ATTR))
                height = buffer.GetNumber(HEIGHT_ATTR);
            else
                height = DEFAULT_WIDTH;
        }

        CheckLayout();
        if (css != null)
            buffer.SetAttribute("ui.stylesheet", css);
        points = new PointsWrapper();
        @out.Printf("%%%n%% Do not forget \\usepackage{tikz} in header.%n%%%n");
        @out.Printf("\\begin{tikzpicture}");
        CheckAndOutputStyle();
        CheckXYandSize();
        buffer.Nodes().ForEach((n) =>
        {
            double x, y;
            x = GetNodeX(n);
            y = GetNodeY(n);
            if (Double.IsNaN(x) || Double.IsNaN(y))
            {
                x = Math.Random() * width;
                y = Math.Random() * height;
            }
            else
            {
                x = width * (x - xmin) / (xmax - xmin);
                y = height * (y - ymin) / (ymax - ymin);
            }

            @out.Printf(l, "\t\\node[inner sep=0pt] (%s) at (%f,%f) {};%n", FormatId(n.GetId()), x, y);
        });
        StyleGroupSet sgs = buffer.GetStyleGroups();
        foreach (HashSet<StyleGroup> groups in sgs.ZIndex())
        {
            foreach (StyleGroup group in groups)
            {
                switch (group.GetType())
                {
                    case NODE:
                        foreach (Element e in group.Elements())
                            OutputNode((Node)e);
                        break;
                    case EDGE:
                        foreach (Element e in group.Elements())
                            OutputEdge((Edge)e);
                        break;
                    default:
                        break;
                }
            }
        }

        @out.Printf("\\end{tikzpicture}%n");
    }

    private void CheckLayout()
    {
        if (!layout)
            return;
        SpringBox sbox = new SpringBox();
        GraphReplay replay = new GraphReplay("replay");
        replay.AddSink(sbox);
        sbox.AddAttributeSink(buffer);
        replay.Replay(buffer);
        do sbox.Compute();
        while (sbox.GetStabilization() < 0.9);
        buffer.RemoveSink(sbox);
        sbox.RemoveAttributeSink(buffer);
    }

    private void CheckXYandSize()
    {
        xmin = ymin = Double.MAX_VALUE;
        xmax = ymax = Double.MIN_VALUE;
        buffer.Nodes().ForEach((n) =>
        {
            double x, y;
            x = GetNodeX(n);
            y = GetNodeY(n);
            if (!Double.IsNaN(x) && !Double.IsNaN(y))
            {
                xmin = Math.Min(xmin, x);
                xmax = Math.Max(xmax, x);
                ymin = Math.Min(ymin, y);
                ymax = Math.Max(ymax, y);
            }
            else
            {
                LOGGER.Warning(String.Format("%% [warning] missing node (x,y).%n"));
            }

            if (n.HasNumber("ui.size"))
            {
                minSize = Math.Min(minSize, n.GetNumber("ui.size"));
                maxSize = Math.Max(maxSize, n.GetNumber("ui.size"));
            }
        });
        if (minSize == maxSize)
            maxSize += 1;
        buffer.Edges().ForEach((e) =>
        {
            points.SetElement(e);
            if (points.Check())
            {
                for (int i = 0; i < points.GetPointsCount(); i++)
                {
                    double x = points.GetX(i);
                    double y = points.GetY(i);
                    xmin = Math.Min(xmin, x);
                    xmax = Math.Max(xmax, x);
                    ymin = Math.Min(ymin, y);
                    ymax = Math.Max(ymax, y);
                }
            }
        });
    }

    private void CheckAndOutputStyle()
    {
        string nodeStyle = "circle,draw=black,fill=black";
        string edgeStyle = "draw=black";
        StyleGroupSet sgs = buffer.GetStyleGroups();
        foreach (StyleGroup sg in sgs.Groups())
        {
            string key = String.Format("class%02d", classIndex++);
            classNames.Put(sg.GetId(), key);
            classes.Put(key, GetTikzStyle(sg));
        }

        @out.Printf("[%n");
        foreach (string key in classes.KeySet())
            @out.Printf(l, "\t%s/.style={%s},%n", key, classes[key]);
        @out.Printf(l, "\ttikzgsnode/.style={%s},%n", nodeStyle);
        @out.Printf(l, "\ttikzgsedge/.style={%s}%n", edgeStyle);
        @out.Printf("]%n");
        foreach (string rgb in colors.KeySet())
            @out.Printf(l, "\t\\definecolor{%s}{rgb}{%s}%n", colors[rgb], rgb);
    }

    private void OutputNode(Node n)
    {
        string label;
        string style = GetNodeStyle(n);
        label = n.HasAttribute("label") ? (string)n.GetLabel("label") : (n.HasAttribute("ui.label") ? (string)n.GetLabel("ui.label") : "");
        @out.Printf(l, "\t\\node[%s] at (%s) {%s};%n", style, FormatId(n.GetId()), label);
    }

    private void OutputEdge(Edge e)
    {
        string style = GetEdgeStyle(e);
        string uiPoints = "";
        points.SetElement(e);
        if (points.Check())
        {
            for (int i = 0; i < points.GetPointsCount(); i++)
            {
                double x, y;
                x = points.GetX(i);
                y = points.GetY(i);
                x = width * (x - xmin) / (xmax - xmin);
                y = height * (y - ymin) / (ymax - ymin);
                uiPoints = String.Format(l, "%s-- (%.3f,%.3f) ", uiPoints, x, y);
            }
        }

        @out.Printf(l, "\t\\draw[%s] (%s) %s%s (%s);%n", style, FormatId(e.GetSourceNode().GetId()), uiPoints, e.IsDirected() ? "->" : "--", FormatId(e.GetTargetNode().GetId()));
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        buffer.GraphAttributeAdded(sourceId, timeId, attribute, value);
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        buffer.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        buffer.GraphAttributeRemoved(sourceId, timeId, attribute);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        buffer.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        buffer.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        buffer.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        buffer.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        buffer.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        buffer.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        buffer.NodeAdded(sourceId, timeId, nodeId);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        buffer.NodeRemoved(sourceId, timeId, nodeId);
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        buffer.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        buffer.EdgeRemoved(sourceId, timeId, edgeId);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        buffer.GraphCleared(sourceId, timeId);
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        buffer.StepBegins(sourceId, timeId, step);
    }

    protected class PointsWrapper
    {
        object[] points;
        PointsWrapper()
        {
        }

        public virtual void SetElement(Element e)
        {
            if (e.HasArray("ui.points"))
                points = e.GetArray("ui.points");
            else
                points = null;
        }

        public virtual bool Check()
        {
            if (points == null)
                return false;
            for (int i = 0; i < points.Length; i++)
            {
                if (!(points[i] is Point3) && !points[i].GetType().IsArray())
                    return false;
            }

            return true;
        }

        public virtual int GetPointsCount()
        {
            return points == null ? 0 : points.Length;
        }

        public virtual double GetX(int i)
        {
            if (points == null || i >= points.Length)
                return Double.NaN;
            object p = points[i];
            if (p is Point3)
                return ((Point3)p).x;
            else
            {
                object x = Array.Get(p, 0);
                if (x is Number)
                    return ((Number)x).DoubleValue();
                else
                    return Array.GetDouble(p, 0);
            }
        }

        public virtual double GetY(int i)
        {
            if (i >= points.Length)
                return Double.NaN;
            object p = points[i];
            if (p is Point3)
                return ((Point3)p).y;
            else
            {
                object y = Array.Get(p, 0);
                if (y is Number)
                    return ((Number)y).DoubleValue();
                else
                    return Array.GetDouble(p, 1);
            }
        }
    }
}
