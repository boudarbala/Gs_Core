using Java.Io;
using Java.Util;
using Javax.Xml.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.Selector;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
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

public class FileSinkSVG : FileSink
{
    public virtual void Begin(string fileName)
    {
        throw new NotSupportedException();
    }

    public virtual void Begin(OutputStream stream)
    {
        throw new NotSupportedException();
    }

    public virtual void Begin(Writer writer)
    {
        throw new NotSupportedException();
    }

    public virtual void End()
    {
        throw new NotSupportedException();
    }

    public virtual void Flush()
    {
    }

    public virtual void WriteAll(Graph graph, string fileName)
    {
        FileWriter out = new FileWriter(fileName);
        WriteAll(graph, @out);
        @out.Dispose();
    }

    public virtual void WriteAll(Graph graph, OutputStream stream)
    {
        OutputStreamWriter out = new OutputStreamWriter(stream);
        WriteAll(graph, @out);
    }

    public virtual void WriteAll(Graph g, Writer w)
    {
        XMLWriter out = new XMLWriter();
        SVGContext ctx = new SVGContext();
        try
        {
            @out.Start(w);
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
        catch (FactoryConfigurationError e)
        {
            throw new Exception(e);
        }

        try
        {
            ctx.Init(@out, g);
            ctx.WriteElements(@out, g);
            ctx.End(@out);
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }

        try
        {
            @out.End();
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
    }

    private static string D(double d)
    {
        return String.Format(Locale.ROOT, "%f", d);
    }

    private static double GetX(Node n)
    {
        if (n.HasNumber("x"))
            return n.GetNumber("x");
        if (n.HasArray("xy"))
        {
            object[] xy = n.GetArray("xy");
            if (xy != null && xy.Length > 0 && xy[0] is Number)
                return ((Number)xy[0]).DoubleValue();
        }

        if (n.HasArray("xyz"))
        {
            object[] xyz = n.GetArray("xyz");
            if (xyz != null && xyz.Length > 0 && xyz[0] is Number)
                return ((Number)xyz[0]).DoubleValue();
        }

        System.err.Printf("[WARNING] no x attribute for node \"%s\" %s\n", n.GetId(), n.HasAttribute("xyz"));
        return Math.Random();
    }

    private static double GetY(Node n)
    {
        if (n.HasNumber("y"))
            return n.GetNumber("y");
        if (n.HasArray("xy"))
        {
            object[] xy = n.GetArray("xy");
            if (xy != null && xy.Length > 1 && xy[1] is Number)
                return ((Number)xy[1]).DoubleValue();
        }

        if (n.HasArray("xyz"))
        {
            object[] xyz = n.GetArray("xyz");
            if (xyz != null && xyz.Length > 1 && xyz[1] is Number)
                return ((Number)xyz[1]).DoubleValue();
        }

        return Math.Random();
    }

    private static string GetSize(Value v)
    {
        string u = v.units.Name().ToLowerCase();
        return String.Format(Locale.ROOT, "%f%s", v.value, u);
    }

    private static string GetSize(Values v, int index)
    {
        string u = v.units.Name().ToLowerCase();
        if (Units.PERCENTS.Equals(v.units))
            u = "%";
        return String.Format(Locale.ROOT, "%f%s", v[index], u);
    }

    class SVGContext
    {
        StyleGroupSet groups;
        StyleSheet stylesheet;
        HashMap<StyleGroup, SVGStyle> svgStyles;
        ViewBox viewBox;
        public SVGContext()
        {
            stylesheet = new StyleSheet();
            groups = new StyleGroupSet(stylesheet);
            svgStyles = new HashMap<StyleGroup, SVGStyle>();
            viewBox = new ViewBox(0, 0, 1000, 1000);
        }

        public virtual void Init(XMLWriter @out, Graph g)
        {
            if (g.HasAttribute("ui.stylesheet"))
            {
                stylesheet.Load(((string)g.GetAttribute("ui.stylesheet")));
            }

            groups.AddElement(g);
            viewBox.Compute(g, groups.GetStyleFor(g));
            @out.Open("svg");
            @out.Attribute("xmlns", "http://www.w3.org/2000/svg");
            @out.Attribute("xmlns:dc", "http://purl.org/dc/elements/1.1/");
            @out.Attribute("xmlns:cc", "http://creativecommons.org/ns#");
            @out.Attribute("xmlns:rdf", "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
            @out.Attribute("xmlns:svg", "http://www.w3.org/2000/svg");
            @out.Attribute("viewBox", String.Format(Locale.ROOT, "%f %f %f %f", viewBox.x1, viewBox.y1, viewBox.x2, viewBox.y2));
            @out.Attribute("id", g.GetId());
            @out.Attribute("version", "1.1");
            try
            {
                g.Edges().ForEach((e) =>
                {
                    groups.AddElement(e);
                    if (e.HasAttribute("ui.style"))
                    {
                        try
                        {
                            stylesheet.ParseStyleFromString(new Selector(Type.EDGE, e.GetId(), null), (string)e.GetAttribute("ui.style"));
                        }
                        catch (IOException ex)
                        {
                            throw new Exception(ex);
                        }
                    }

                    groups.CheckElementStyleGroup(e);
                });
                g.Nodes().ForEach((n) =>
                {
                    groups.AddElement(n);
                    if (n.HasAttribute("ui.style"))
                    {
                        try
                        {
                            stylesheet.ParseStyleFromString(new Selector(Type.NODE, n.GetId(), null), (string)n.GetAttribute("ui.style"));
                        }
                        catch (IOException ex)
                        {
                            throw new Exception(ex);
                        }
                    }

                    groups.CheckElementStyleGroup(n);
                });
            }
            catch (Exception e)
            {
                if (e.GetCause() is IOException)
                    throw (IOException)e.GetCause();
                if (e.GetCause() is XMLStreamException)
                    throw (IOException)e.GetCause();
            }

            foreach (StyleGroup group in groups.Groups())
                svgStyles.Put(group, new SVGStyle(group));
            @out.Open("defs");
            foreach (SVGStyle svgStyle in svgStyles.Values())
                svgStyle.WriteDef(@out);
            @out.Dispose();
        }

        public virtual void End(XMLWriter @out)
        {
            @out.Dispose();
        }

        public virtual void WriteElements(XMLWriter @out, Graph g)
        {
            @out.Open("g");
            @out.Attribute("id", "graph-misc");
            WriteElement(@out, g);
            @out.Dispose();
            IEnumerator<HashSet<StyleGroup>> it = groups.GetZIterator();
            @out.Open("g");
            @out.Attribute("id", "elements");
            while (it.HasNext())
            {
                HashSet<StyleGroup> set = it.Next();
                foreach (StyleGroup sg in set)
                    foreach (Element e in sg.Elements())
                        WriteElement(@out, e);
            }

            @out.Dispose();
        }

        public virtual void WriteElement(XMLWriter @out, Element e)
        {
            string id = "";
            SVGStyle style = null;
            string transform = null;
            if (e is Edge)
            {
                id = String.Format("egde-%s", e.GetId());
                style = svgStyles[groups.GetStyleFor((Edge)e)];
            }
            else if (e is Node)
            {
                id = String.Format("node-%s", e.GetId());
                style = svgStyles[groups.GetStyleFor((Node)e)];
                transform = String.Format(Locale.ROOT, "translate(%f,%f)", viewBox.ConvertX((Node)e), viewBox.ConvertY((Node)e));
            }
            else if (e is Graph)
            {
                id = "graph-background";
                style = svgStyles[groups.GetStyleFor((Graph)e)];
            }

            @out.Open("g");
            @out.Attribute("id", id);
            @out.Open("path");
            if (style != null)
                @out.Attribute("style", style.GetElementStyle(e));
            if (transform != null)
                @out.Attribute("transform", transform);
            @out.Attribute("d", GetPath(e, style));
            @out.Dispose();
            if (e.HasLabel("label"))
                WriteElementText(@out, (string)e.GetAttribute("label"), e, style.group);
            @out.Dispose();
        }

        public virtual void WriteElementText(XMLWriter @out, string text, Element e, StyleGroup style)
        {
            if (style == null || style.GetTextVisibilityMode() != StyleConstants.TextVisibilityMode.HIDDEN)
            {
                double x, y;
                x = 0;
                y = 0;
                if (e is Node)
                {
                    x = viewBox.ConvertX((Node)e);
                    y = viewBox.ConvertY((Node)e);
                }
                else if (e is Edge)
                {
                    Node n0, n1;
                    n0 = ((Edge)e).GetNode0();
                    n1 = ((Edge)e).GetNode0();
                    x = viewBox.ConvertX((GetX(n0) + GetX(n1)) / 2);
                    y = viewBox.ConvertY((GetY(n0) + GetY(n1)) / 2);
                }

                @out.Open("g");
                @out.Open("text");
                @out.Attribute("x", D(x));
                @out.Attribute("y", D(y));
                if (style != null)
                {
                    if (style.GetTextColorCount() > 0)
                        @out.Attribute("fill", ToHexColor(style.GetTextColor(0)));
                    switch (style.GetTextAlignment())
                    {
                        case CENTER:
                            @out.Attribute("text-anchor", "middle");
                            @out.Attribute("alignment-baseline", "central");
                            break;
                        case LEFT:
                            @out.Attribute("text-anchor", "start");
                            break;
                        case RIGHT:
                            @out.Attribute("text-anchor", "end");
                            break;
                        default:
                            break;
                    }

                    switch (style.GetTextSize().units)
                    {
                        case PX:
                        case GU:
                            @out.Attribute("font-size", D(style.GetTextSize().value));
                            break;
                        case PERCENTS:
                            @out.Attribute("font-size", D(style.GetTextSize().value) + "%");
                            break;
                    }

                    if (style.GetTextFont() != null)
                        @out.Attribute("font-family", style.GetTextFont());
                    switch (style.GetTextStyle())
                    {
                        case NORMAL:
                            break;
                        case ITALIC:
                            @out.Attribute("font-style", "italic");
                            break;
                        case BOLD:
                            @out.Attribute("font-weight", "bold");
                            break;
                        case BOLD_ITALIC:
                            @out.Attribute("font-weight", "bold");
                            @out.Attribute("font-style", "italic");
                            break;
                    }
                }

                @out.Characters(text);
                @out.Dispose();
                @out.Dispose();
            }
        }

        public virtual string GetPath(Element e, SVGStyle style)
        {
            StringBuilder buffer = new StringBuilder();
            if (e is Node)
            {
                double sx, sy;
                Values size = style.group.GetSize();
                sx = GetValue(size[0], size.units, true);
                if (size.GetValueCount() > 1)
                    sy = GetValue(size[1], size.units, false);
                else
                    sy = GetValue(size[0], size.units, false);
                switch (style.group.GetShape())
                {
                    case ROUNDED_BOX:
                        double rx, ry;
                        rx = Math.Min(5, sx / 2);
                        ry = Math.Min(5, sy / 2);
                        Concat(buffer, " m ", D(-sx / 2 + rx), " ", D(-sy / 2));
                        Concat(buffer, " h ", D(sx - 2 * rx));
                        Concat(buffer, " a ", D(rx), ",", D(ry), " 0 0 1 ", D(rx), ",", D(ry));
                        Concat(buffer, " v ", D(sy - 2 * ry));
                        Concat(buffer, " a ", D(rx), ",", D(ry), " 0 0 1 -", D(rx), ",", D(ry));
                        Concat(buffer, " h ", D(-sx + 2 * rx));
                        Concat(buffer, " a ", D(rx), ",", D(ry), " 0 0 1 -", D(rx), ",-", D(ry));
                        Concat(buffer, " v ", D(-sy + 2 * ry));
                        Concat(buffer, " a ", D(rx), ",", D(ry), " 0 0 1 ", D(rx), "-", D(ry));
                        Concat(buffer, " z");
                        break;
                    case BOX:
                        Concat(buffer, " m ", D(-sx / 2), " ", D(-sy / 2));
                        Concat(buffer, " h ", D(sx));
                        Concat(buffer, " v ", D(sy));
                        Concat(buffer, " h ", D(-sx));
                        Concat(buffer, " z");
                        break;
                    case DIAMOND:
                        Concat(buffer, " m ", D(-sx / 2), " 0");
                        Concat(buffer, " l ", D(sx / 2), " ", D(-sy / 2));
                        Concat(buffer, " l ", D(sx / 2), " ", D(sy / 2));
                        Concat(buffer, " l ", D(-sx / 2), " ", D(sy / 2));
                        Concat(buffer, " z");
                        break;
                    case TRIANGLE:
                        Concat(buffer, " m ", D(0), " ", D(-sy / 2));
                        Concat(buffer, " l ", D(sx / 2), " ", D(sy));
                        Concat(buffer, " h ", D(-sx));
                        Concat(buffer, " z");
                        break;
                    default:
                        break;
                    case CIRCLE:
                        Concat(buffer, " m ", D(-sx / 2), " 0");
                        Concat(buffer, " a ", D(sx / 2), ",", D(sy / 2), " 0 1 0 ", D(sx), ",0");
                        Concat(buffer, " ", D(sx / 2), ",", D(sy / 2), " 0 1 0 -", D(sx), ",0");
                        Concat(buffer, " z");
                        break;
                }
            }
            else if (e is Graph)
            {
                Concat(buffer, " M ", D(viewBox.x1), " ", D(viewBox.y1));
                Concat(buffer, " L ", D(viewBox.x2), " ", D(viewBox.y1));
                Concat(buffer, " L ", D(viewBox.x2), " ", D(viewBox.y2));
                Concat(buffer, " L ", D(viewBox.x1), " ", D(viewBox.y2));
                Concat(buffer, " Z");
            }
            else if (e is Edge)
            {
                double sizeEdge = GetValue(style.group.GetSize()[0], style.group.GetSize().units, true);
                double sx, sy;
                Values sizeArrow = style.group.GetArrowSize();
                sx = GetValue(sizeArrow[0], sizeArrow.units, true);
                if (sizeArrow.GetValueCount() > 1)
                    sy = GetValue(sizeArrow[1], sizeArrow.units, false);
                else
                    sy = GetValue(sizeArrow[0], sizeArrow.units, false);
                Edge edge = (Edge)e;
                Node src, trg;
                double x1, y1;
                double x2, y2;
                src = edge.GetSourceNode();
                trg = edge.GetTargetNode();
                x1 = viewBox.ConvertX(src);
                y1 = viewBox.ConvertY(src);
                x2 = viewBox.ConvertX(trg);
                y2 = viewBox.ConvertY(trg);
                double nodeSize, xCenter, yCenter, xCenterCenter, yCenterCenter;
                double[] perpen;
                switch (style.group.GetShape())
                {
                    case ANGLE:
                        double[] perpendicular = GetPerpendicular(x1, y1, x2, y2, sizeEdge);
                        double x1Prim = perpendicular[0];
                        double y1Prim = perpendicular[1];
                        double x2Prim = perpendicular[2];
                        double y2Prim = perpendicular[3];
                        Concat(buffer, " M ", D(x1), " ", D(y1));
                        Concat(buffer, " L ", D(x1Prim), " ", D(y1Prim));
                        Concat(buffer, " L ", D(x2Prim), " ", D(y2Prim));
                        Concat(buffer, " Z");
                        break;
                    case CUBIC_CURVE:
                        nodeSize = svgStyles[groups.GetStyleFor(trg)].group.GetSize()[0];
                        if (svgStyles[groups.GetStyleFor(trg)].group.GetSize().GetValueCount() > 1)
                        {
                            nodeSize = Math.Max(nodeSize, svgStyles[groups.GetStyleFor(trg)].group.GetSize()[1]);
                        }

                        xCenter = (x1 + x2) / 2;
                        yCenter = (y1 + y2) / 2;
                        perpen = GetPerpendicular(x1, y1, xCenter, yCenter, Math.Sqrt(Math.Pow(Math.Abs(x1 - xCenter), 2) + Math.Pow(Math.Abs(y1 - yCenter), 2)) * 2);
                        double x45degrees = (x1 + perpen[2]) / 2;
                        double y45degrees = (y1 + perpen[3]) / 2;
                        xCenterCenter = (x1 + xCenter) / 2;
                        yCenterCenter = (y1 + yCenter) / 2;
                        double x20degrees = (xCenterCenter + x45degrees) / 2;
                        double y20degrees = (yCenterCenter + y45degrees) / 2;
                        Concat(buffer, " M ", D(x1), " ", D(y1));
                        Concat(buffer, " C ", D(x20degrees), " ", D(y20degrees), " ", D(x20degrees), " ", D(y20degrees), " ", D(xCenter), " ", D(yCenter));
                        double x45degrees2nd = (x2 + perpen[0]) / 2;
                        double y45degrees2nd = (y2 + perpen[1]) / 2;
                        double xCenterCenter2nd = (x2 + xCenter) / 2;
                        double yCenterCenter2nd = (y2 + yCenter) / 2;
                        double x20degrees2nd = (xCenterCenter2nd + x45degrees2nd) / 2;
                        double y20degrees2nd = (yCenterCenter2nd + y45degrees2nd) / 2;
                        Concat(buffer, " S ", D(x20degrees2nd), " ", D(y20degrees2nd), " ", D(x2), " ", D(y2));
                        Concat(buffer, " C ", D(x20degrees2nd), " ", D(y20degrees2nd), " ", D(x20degrees2nd), " ", D(y20degrees2nd), " ", D(xCenter), " ", D(yCenter));
                        Concat(buffer, " S ", D(x20degrees), " ", D(y20degrees), " ", D(x1), " ", D(y1));
                        Concat(buffer, " Z");
                        break;
                    case BLOB:
                        nodeSize = svgStyles[groups.GetStyleFor(trg)].group.GetSize()[0];
                        if (svgStyles[groups.GetStyleFor(trg)].group.GetSize().GetValueCount() > 1)
                        {
                            nodeSize = Math.Max(nodeSize, svgStyles[groups.GetStyleFor(trg)].group.GetSize()[1]);
                        }

                        xCenter = (x1 + x2) / 2;
                        yCenter = (y1 + y2) / 2;
                        xCenterCenter = (x1 + xCenter) / 2;
                        yCenterCenter = (y1 + yCenter) / 2;
                        double[] perpenCenter = GetPerpendicular(x1, y1, xCenter, yCenter, sizeEdge);
                        double[] perpenX1 = GetPerpendicular(xCenter, yCenter, x1, y1, nodeSize);
                        double[] perpenXCenter1 = GetPerpendicular(x1, y1, xCenterCenter, yCenterCenter, sizeEdge);
                        Concat(buffer, " M ", D(perpenX1[0]), " ", D(perpenX1[1]));
                        Concat(buffer, " Q ", D(perpenXCenter1[0]), " ", D(perpenXCenter1[1]), " ", D(perpenCenter[0]), " ", D(perpenCenter[1]));
                        Concat(buffer, " L ", D(x2), " ", D(y2));
                        Concat(buffer, " L ", D(perpenCenter[2]), " ", D(perpenCenter[3]));
                        Concat(buffer, " Q ", D(perpenXCenter1[2]), " ", D(perpenXCenter1[3]), " ", D(perpenX1[2]), " ", D(perpenX1[3]));
                        Concat(buffer, " Z");
                        if (!edge.IsDirected())
                        {
                            double[] perpenX2 = GetPerpendicular(xCenter, yCenter, x2, y2, nodeSize);
                            xCenterCenter2nd = (x2 + xCenter) / 2;
                            yCenterCenter2nd = (y2 + yCenter) / 2;
                            double[] perpenXCenter2 = GetPerpendicular(x2, y2, xCenterCenter2nd, yCenterCenter2nd, sizeEdge);
                            Concat(buffer, " M ", D(perpenX2[0]), " ", D(perpenX2[1]));
                            Concat(buffer, " Q ", D(perpenXCenter2[0]), " ", D(perpenXCenter2[1]), " ", D(perpenCenter[0]), " ", D(perpenCenter[1]));
                            Concat(buffer, " L ", D(x1), " ", D(y1));
                            Concat(buffer, " L ", D(perpenCenter[2]), " ", D(perpenCenter[3]));
                            Concat(buffer, " Q ", D(perpenXCenter2[2]), " ", D(perpenXCenter2[3]), " ", D(perpenX2[2]), " ", D(perpenX2[3]));
                            Concat(buffer, " Z");
                        }

                        break;
                    default:
                        break;
                    case LINE:
                        Concat(buffer, " M ", D(x1), " ", D(y1));
                        Concat(buffer, " L ", D(x2), " ", D(y2));
                        break;
                }

                if (edge.IsDirected())
                {
                    nodeSize = svgStyles[groups.GetStyleFor(trg)].group.GetSize()[0];
                    double diag = -1;
                    if (svgStyles[groups.GetStyleFor(trg)].group.GetSize().GetValueCount() > 1)
                    {
                        diag = Math.Sqrt(Math.Pow(nodeSize, 2) + Math.Pow(svgStyles[groups.GetStyleFor(trg)].group.GetSize()[1], 2));
                        nodeSize = Math.Min(nodeSize, svgStyles[groups.GetStyleFor(trg)].group.GetSize()[1]);
                    }
                    else
                    {
                        diag = Math.Sqrt(Math.Pow(nodeSize, 2) + Math.Pow(nodeSize, 2));
                    }

                    if (svgStyles[groups.GetStyleFor(trg)].group.GetShape().Equals(Shape.CIRCLE))
                    {
                        nodeSize = nodeSize / 2;
                    }
                    else if (svgStyles[groups.GetStyleFor(trg)].group.GetShape().Equals(Shape.BOX) || svgStyles[groups.GetStyleFor(trg)].group.GetShape().Equals(Shape.ROUNDED_BOX) || svgStyles[groups.GetStyleFor(trg)].group.GetShape().Equals(Shape.DIAMOND) || svgStyles[groups.GetStyleFor(trg)].group.GetShape().Equals(Shape.TRIANGLE))
                    {
                        nodeSize = diag / 2;
                    }

                    double distance = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
                    double ratioPoint, ratioLine;
                    double x2Root, y2Root;
                    double x2Point, y2Point;
                    double x1Prim, y1Prim, x2Prim, y2Prim;
                    switch (style.group.GetArrowShape())
                    {
                        case CIRCLE:
                            ratioPoint = 1 - (nodeSize / distance);
                            ratioLine = 1 - (((sx / 2) + nodeSize) / distance);
                            x2Root = (((1 - ratioLine) * x1) + (ratioLine * x2));
                            y2Root = (((1 - ratioLine) * y1) + (ratioLine * y2));
                            x2Point = (((1 - ratioPoint) * x1) + (ratioPoint * x2));
                            y2Point = (((1 - ratioPoint) * y1) + (ratioPoint * y2));
                            perpen = GetPerpendicular(x2, y2, x2Root, y2Root, sy);
                            x1Prim = perpen[0];
                            y1Prim = perpen[1];
                            x2Prim = perpen[2];
                            y2Prim = perpen[3];
                            Concat(buffer, " M ", D(x1Prim), " ", D(y1Prim));
                            Concat(buffer, " A ", D(sx / 4), " ", D(sy / 4), " 0 1 0 ", D(x2Prim), " ", D(y2Prim));
                            Concat(buffer, " ", D(sx / 4), " ", D(sy / 4), " 0 1 0 ", D(x1Prim), " ", D(y1Prim));
                            Concat(buffer, " Z");
                            break;
                        case DIAMOND:
                            ratioPoint = 1 - (nodeSize / distance);
                            ratioLine = 1 - (((sx / 2) + nodeSize) / distance);
                            x2Root = (((1 - ratioLine) * x1) + (ratioLine * x2));
                            y2Root = (((1 - ratioLine) * y1) + (ratioLine * y2));
                            x2Point = (((1 - ratioPoint) * x1) + (ratioPoint * x2));
                            y2Point = (((1 - ratioPoint) * y1) + (ratioPoint * y2));
                            System.@out.Println(nodeSize + " " + sx);
                            double ratioEnd = 1 - ((sx + nodeSize) / distance);
                            double x2End = (((1 - ratioEnd) * x1) + (ratioEnd * x2));
                            double y2End = (((1 - ratioEnd) * y1) + (ratioEnd * y2));
                            perpen = GetPerpendicular(x2, y2, x2Root, y2Root, sy);
                            x1Prim = perpen[0];
                            y1Prim = perpen[1];
                            x2Prim = perpen[2];
                            y2Prim = perpen[3];
                            Concat(buffer, " M ", D(x2Point), " ", D(y2Point));
                            Concat(buffer, " L ", D(x1Prim), " ", D(y1Prim));
                            Concat(buffer, " L ", D(x2End), " ", D(y2End));
                            Concat(buffer, " L ", D(x2Prim), " ", D(y2Prim));
                            Concat(buffer, " Z");
                            break;
                        default:
                            break;
                        case ARROW:
                            ratioPoint = 1 - (nodeSize / distance);
                            ratioLine = 1 - ((sx + nodeSize) / distance);
                            x2Root = (((1 - ratioLine) * x1) + (ratioLine * x2));
                            y2Root = (((1 - ratioLine) * y1) + (ratioLine * y2));
                            x2Point = (((1 - ratioPoint) * x1) + (ratioPoint * x2));
                            y2Point = (((1 - ratioPoint) * y1) + (ratioPoint * y2));
                            perpen = GetPerpendicular(x2, y2, x2Root, y2Root, sy);
                            x1Prim = perpen[0];
                            y1Prim = perpen[1];
                            x2Prim = perpen[2];
                            y2Prim = perpen[3];
                            if (style.group.GetShape().Equals(Shape.CUBIC_CURVE))
                            {
                                double rotation = 25;
                                System.@out.Println(y2Point - y2);
                                if (y2Point - y2 <= 1)
                                    rotation = -rotation;
                                Vector2 v = RotatePoint(x2, y2, rotation, x2Point, y2Point);
                                x2Point = v.X();
                                y2Point = v.Y();
                                v = RotatePoint(x2, y2, rotation, x1Prim, y1Prim);
                                x1Prim = v.X();
                                y1Prim = v.Y();
                                v = RotatePoint(x2, y2, rotation, x2Prim, y2Prim);
                                x2Prim = v.X();
                                y2Prim = v.Y();
                            }

                            Concat(buffer, " M ", D(x1Prim), " ", D(y1Prim));
                            Concat(buffer, " L ", D(x2Prim), " ", D(y2Prim));
                            Concat(buffer, " L ", D(x2Point), " ", D(y2Point));
                            Concat(buffer, " Z");
                            System.@out.Println("Arrow = (" + x1Prim + ", " + y1Prim + ") (" + x1Prim + ", " + y2Prim + ") (" + x2Point + ", " + y2Point + ")");
                            break;
                    }
                }
            }

            return buffer.ToString();
        }

        public static Vector2 RotatePoint(double cx, double cy, double angle, double px, double py)
        {
            double absangl = Math.Abs(angle);
            double s = Math.Sin(Math.ToRadians(absangl));
            double c = Math.Cos(Math.ToRadians(absangl));
            px -= cx;
            py -= cy;
            double xnew;
            double ynew;
            if (angle > 0)
            {
                xnew = px * c - py * s;
                ynew = px * s + py * c;
            }
            else
            {
                xnew = px * c + py * s;
                ynew = -px * s + py * c;
            }

            px = xnew + cx;
            py = ynew + cy;
            return new Vector2(px, py);
        }

        public virtual double[] GetPerpendicular(double x1, double y1, double x2, double y2, double size)
        {
            double slope, slopePerpen;
            slope = (y2 - y1) / (x2 - x1);
            double x1Prim, x2Prim, y1Prim, y2Prim;
            if (Double.IsInfinite(slope))
            {
                x1Prim = x2 - (size / 2);
                y1Prim = y2;
                x2Prim = x2 + (size / 2);
                y2Prim = y2;
            }
            else if (slope == 0)
            {
                x1Prim = x2;
                y1Prim = y2 - (size / 2);
                x2Prim = x2;
                y2Prim = y2 + (size / 2);
            }
            else
            {
                slopePerpen = (-1 / slope);
                double deltaX = 1 / (Math.Sqrt((slopePerpen * slopePerpen) + 1));
                double deltaY = slopePerpen / (Math.Sqrt((slopePerpen * slopePerpen) + 1));
                x1Prim = x2 - ((size / 2) * deltaX);
                y1Prim = y2 - ((size / 2) * deltaY);
                x2Prim = x2 + ((size / 2) * deltaX);
                y2Prim = y2 + ((size / 2) * deltaY);
            }

            return new double[]
            {
                x1Prim,
                y1Prim,
                x2Prim,
                y2Prim
            };
        }

        public virtual double GetValue(Value v, bool horizontal)
        {
            return GetValue(v.value, v.units, horizontal);
        }

        public virtual double GetValue(double d, StyleConstants.Units units, bool horizontal)
        {
            switch (units)
            {
                case PX:
                    return d;
                case GU:
                    return d;
                case PERCENTS:
                    if (horizontal)
                        return (viewBox.x2 - viewBox.x1) * d / 100;
                    else
                        return (viewBox.y2 - viewBox.y1) * d / 100;
            }

            return d;
        }
    }

    class ViewBox
    {
        double x1, y1, x2, y2;
        double x3, y3, x4, y4;
        double[] padding = new[]
        {
            0,
            0
        };
        ViewBox(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        virtual void Compute(Graph g, StyleGroup style)
        {
            x3 = y3 = Double.MAX_VALUE;
            x4 = y4 = Double.MIN_VALUE;
            g.Nodes().ForEach((n) =>
            {
                x3 = Math.Min(x3, GetX(n));
                y3 = Math.Min(y3, GetY(n));
                x4 = Math.Max(x4, GetX(n));
                y4 = Math.Max(y4, GetY(n));
            });
            Values v = style.GetPadding();
            if (v.GetValueCount() > 0)
            {
                padding[0] = v[0];
                padding[1] = v.GetValueCount() > 1 ? v[1] : v[0];
            }
        }

        virtual double ConvertX(double x)
        {
            return (x2 - x1 - 2 * padding[0]) * (x - x3) / (x4 - x3) + x1 + padding[0];
        }

        virtual double ConvertX(Node n)
        {
            return ConvertX(GetX(n));
        }

        virtual double ConvertY(double y)
        {
            return (y2 - y1 - 2 * padding[1]) * (y - y3) / (y4 - y3) + y1 + padding[1];
        }

        virtual double ConvertY(Node n)
        {
            return ConvertY(GetY(n));
        }
    }

    class SVGStyle
    {
        static int gradientId = 0;
        string style;
        StyleGroup group;
        bool gradient;
        bool dynfill;
        public SVGStyle(StyleGroup group)
        {
            this.group = group;
            this.gradient = false;
            this.dynfill = false;
            switch (group.GetType())
            {
                case EDGE:
                    BuildEdgeStyle();
                    break;
                case NODE:
                    BuildNodeStyle();
                    break;
                case GRAPH:
                    BuildGraphStyle();
                    break;
                case SPRITE:
                default:
                    break;
            }
        }

        virtual void BuildNodeStyle()
        {
            StringBuilder styleSB = new StringBuilder();
            switch (group.GetFillMode())
            {
                case GRADIENT_RADIAL:
                case GRADIENT_HORIZONTAL:
                case GRADIENT_VERTICAL:
                case GRADIENT_DIAGONAL1:
                case GRADIENT_DIAGONAL2:
                    Concat(styleSB, "fill:url(#%gradient-id%);");
                    this.gradient = true;
                    break;
                case PLAIN:
                    Concat(styleSB, "fill:", ToHexColor(group.GetFillColor(0)), ";");
                    Concat(styleSB, "fill-opacity:", D(group.GetFillColor(0).GetAlpha() / 255), ";");
                    break;
                case DYN_PLAIN:
                    dynfill = true;
                    Concat(styleSB, "fill:%fill-color%;");
                    Concat(styleSB, "fill-opacity:%fill-opacity%;");
                    break;
                case IMAGE_TILED:
                case IMAGE_SCALED:
                case IMAGE_SCALED_RATIO_MAX:
                case IMAGE_SCALED_RATIO_MIN:
                case NONE:
                    break;
            }

            Concat(styleSB, "fill-rule:nonzero;");
            if (group.GetStrokeMode() != StrokeMode.NONE)
            {
                Concat(styleSB, "stroke:", ToHexColor(group.GetStrokeColor(0)), ";");
                Concat(styleSB, "stroke-width:", GetSize(group.GetStrokeWidth()), ";");
            }

            style = styleSB.ToString();
        }

        virtual void BuildGraphStyle()
        {
            BuildNodeStyle();
        }

        virtual void BuildEdgeStyle()
        {
            StringBuilder styleSB = new StringBuilder();
            switch (group.GetFillMode())
            {
                case GRADIENT_RADIAL:
                case GRADIENT_HORIZONTAL:
                case GRADIENT_VERTICAL:
                case GRADIENT_DIAGONAL1:
                case GRADIENT_DIAGONAL2:
                    Concat(styleSB, "stroke:url(#%gradient-id%);");
                    this.gradient = true;
                    break;
                case PLAIN:
                    Concat(styleSB, "fill:", ToHexColor(group.GetFillColor(0)), ";");
                    Concat(styleSB, "fill-opacity:", D(group.GetFillColor(0).GetAlpha() / 255), ";");
                    Concat(styleSB, "stroke:", ToHexColor(group.GetFillColor(0)), ";");
                    break;
                case DYN_PLAIN:
                    Concat(styleSB, "stroke:", ToHexColor(group.GetFillColor(0)), ";");
                    break;
                case IMAGE_TILED:
                case IMAGE_SCALED:
                case IMAGE_SCALED_RATIO_MAX:
                case IMAGE_SCALED_RATIO_MIN:
                case NONE:
                    break;
            }

            if (!group.GetShape().Equals(Shape.ANGLE) && !group.GetShape().Equals(Shape.BLOB))
            {
                Concat(styleSB, "stroke-width:", GetSize(group.GetSize(), 0), ";");
            }

            style = styleSB.ToString();
        }

        public virtual void WriteDef(XMLWriter @out)
        {
            if (gradient)
            {
                string gid = String.Format("gradient%x", gradientId++);
                string type = "linearGradient";
                string x1 = null, x2 = null, y1 = null, y2 = null;
                switch (group.GetFillMode())
                {
                    case GRADIENT_RADIAL:
                        type = "radialGradient";
                        break;
                    case GRADIENT_HORIZONTAL:
                        x1 = "0%";
                        y1 = "50%";
                        x2 = "100%";
                        y2 = "50%";
                        break;
                    case GRADIENT_VERTICAL:
                        x1 = "50%";
                        y1 = "0%";
                        x2 = "50%";
                        y2 = "100%";
                        break;
                    case GRADIENT_DIAGONAL1:
                        x1 = "0%";
                        y1 = "0%";
                        x2 = "100%";
                        y2 = "100%";
                        break;
                    case GRADIENT_DIAGONAL2:
                        x1 = "100%";
                        y1 = "100%";
                        x2 = "0%";
                        y2 = "0%";
                        break;
                    default:
                        break;
                }

                @out.Open(type);
                @out.Attribute("id", gid);
                @out.Attribute("gradientUnits", "objectBoundingBox");
                if (type.Equals("linearGradient"))
                {
                    @out.Attribute("x1", x1);
                    @out.Attribute("y1", y1);
                    @out.Attribute("x2", x2);
                    @out.Attribute("y2", y2);
                }

                for (int i = 0; i < group.GetFillColorCount(); i++)
                {
                    @out.Open("stop");
                    @out.Attribute("stop-color", ToHexColor(group.GetFillColor(i)));
                    @out.Attribute("stop-opacity", D(group.GetFillColor(i).GetAlpha() / 255));
                    @out.Attribute("offset", Double.ToString(i / (double)(group.GetFillColorCount() - 1)));
                    @out.Dispose();
                }

                @out.Dispose();
                style = style.Replace("%gradient-id%", gid);
            }
        }

        public virtual string GetElementStyle(Element e)
        {
            string st = style;
            if (dynfill)
            {
                if (group.GetFillColorCount() > 1)
                {
                    string color, opacity;
                    double d = e.HasNumber("ui.color") ? e.GetNumber("ui.color") : 0;
                    double a, b;
                    Colors colors = group.GetFillColors();
                    int s = Math.Min((int)(d * group.GetFillColorCount()), colors.Count - 2);
                    a = s / (double)(colors.Count - 1);
                    b = (s + 1) / (double)(colors.Count - 1);
                    d = (d - a) / (b - a);
                    Color c1 = colors[s], c2 = colors[s + 1];
                    color = String.Format("#%02x%02x%02x", (int)(c1.GetRed() + d * (c2.GetRed() - c1.GetRed())), (int)(c1.GetGreen() + d * (c2.GetGreen() - c1.GetGreen())), (int)(c1.GetBlue() + d * (c2.GetBlue() - c1.GetBlue())));
                    opacity = Double.ToString((c1.GetAlpha() + d * (c2.GetAlpha() - c1.GetAlpha())) / 255);
                    st = st.Replace("%fill-color%", color);
                    st = st.Replace("%fill-opacity%", opacity);
                }
            }

            return st;
        }
    }

    class XMLWriter
    {
        XMLStreamWriter out;
        int depth;
        bool closed;
        virtual void Start(Writer w)
        {
            if (@out != null)
                End();
            @out = XMLOutputFactory.NewInstance().CreateXMLStreamWriter(w);
            @out.WriteStartDocument();
        }

        virtual void End()
        {
            @out.WriteEndDocument();
            @out.Flush();
            @out.Dispose();
            @out = null;
        }

        virtual void Open(string name)
        {
            @out.WriteCharacters("\n");
            for (int i = 0; i < depth; i++)
                @out.WriteCharacters("  ");
            @out.WriteStartElement(name);
            depth++;
        }

        virtual void Dispose()
        {
            @out.WriteEndElement();
            depth--;
        }

        virtual void Attribute(string key, string value)
        {
            @out.WriteAttribute(key, value);
        }

        virtual void Characters(string data)
        {
            @out.WriteCharacters(data);
        }
    }

    private static void Concat(StringBuilder buffer, params object[] args)
    {
        if (args != null)
        {
            for (int i = 0; i < args.Length; i++)
                buffer.Append(args[i].ToString());
        }
    }

    private static string ToHexColor(Color c)
    {
        return String.Format("#%02x%02x%02x", c.GetRed(), c.GetGreen(), c.GetBlue());
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
    }
}
