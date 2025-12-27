using Java.Io;
using Java.Util;
using Gs_Core.Graphstream.Ui.Geom;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
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

public class FileSinkUnstyledSVG : FileSinkBase
{
    protected PrintWriter out;
    protected enum What
    {
        NODE,
        EDGE,
        OTHER
    }

    protected HashMap<string, Point3> nodePos = new HashMap<string, Point3>();
    public FileSinkUnstyledSVG()
    {
    }

    public override void End()
    {
        if (@out != null)
        {
            @out.Flush();
            @out.Dispose();
            @out = null;
        }
    }

    protected override void OutputHeader()
    {
        @out = (PrintWriter)output;
        @out.Printf("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>%n");
        @out.Printf("<svg" + " xmlns:svg=\"http://www.w3.org/2000/svg\"" + " width=\"100%%\"" + " height=\"100%%\"" + ">%n");
    }

    protected override void OutputEndOfFile()
    {
        OutputNodes();
        @out.Printf("</svg>%n");
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        SetNodePos(nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        SetNodePos(nodeId, attribute, newValue);
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        Point3 p0 = nodePos[fromNodeId];
        Point3 p1 = nodePos[toNodeId];
        if (p0 != null && p1 != null)
        {
            @out.Printf("  <g id=\"%s\">%n", edgeId);
            @out.Printf("    <line x1=\"%f\" y1=\"%f\" x2=\"%f\" y2=\"%f\"/>%n", p0.x, p0.y, p1.x, p1.y);
            @out.Printf("  </g>%n");
        }
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        nodePos.Put(nodeId, new Point3());
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        nodePos.Remove(nodeId);
    }

    public virtual void StepBegins(string graphId, long timeId, double time)
    {
    }

    protected virtual void SetNodePos(string nodeId, string attribute, object value)
    {
        Point3 p = nodePos[nodeId];
        double x, y, z;
        if (p == null)
        {
            x = Math.Random();
            y = Math.Random();
            z = 0;
        }
        else
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }

        if (attribute.Equals("x"))
        {
            if (value is Number)
                x = ((Number)value).FloatValue();
        }
        else if (attribute.Equals("y"))
        {
            if (value is Number)
                y = ((Number)value).FloatValue();
        }
        else if (attribute.Equals("z"))
        {
            if (value is Number)
                z = ((Number)value).FloatValue();
        }
        else if (attribute.Equals("xy"))
        {
            if (value is Object[])
            {
                object[] xy = ((Object[])value);
                if (xy.Length > 1)
                {
                    x = ((Number)xy[0]).FloatValue();
                    y = ((Number)xy[1]).FloatValue();
                }
            }
        }
        else if (attribute.Equals("xyz"))
        {
            if (value is Object[])
            {
                object[] xyz = ((Object[])value);
                if (xyz.Length > 1)
                {
                    x = ((Number)xyz[0]).FloatValue();
                    y = ((Number)xyz[1]).FloatValue();
                }

                if (xyz.Length > 2)
                {
                    z = ((Number)xyz[2]).FloatValue();
                }
            }
        }

        nodePos.Put(nodeId, new Point3(x, y, z));
    }

    protected virtual void OutputStyle(string styleSheet)
    {
        string style = null;
        if (styleSheet != null)
        {
            StyleSheet ssheet = new StyleSheet();
            try
            {
                if (styleSheet.StartsWith("url("))
                {
                    styleSheet = styleSheet.Substring(5);
                    int pos = styleSheet.LastIndexOf(')');
                    styleSheet = styleSheet.Substring(0, pos);
                    ssheet.ParseFromFile(styleSheet);
                }
                else
                {
                    ssheet.ParseFromString(styleSheet);
                }

                style = StyleSheetToSVG(ssheet);
            }
            catch (IOException e)
            {
                e.PrintStackTrace();
                ssheet = null;
            }
        }

        if (style == null)
            style = "circle { fill: grey; stroke: none; } line { stroke-width: 1; stroke: black; }";
        @out.Printf("<defs><style type=\"text/css\"><![CDATA[%n");
        @out.Printf("    %s%n", style);
        @out.Printf("]]></style></defs>%n");
    }

    protected virtual void OutputNodes()
    {
        IEnumerator<TWildcardTodostring> keys = nodePos.KeySet().Iterator();
        while (keys.HasNext())
        {
            string key = keys.Next();
            Point3 pos = nodePos[key];
            @out.Printf("  <g id=\"%s\">%n", key);
            @out.Printf("    <circle cx=\"%f\" cy=\"%f\" r=\"4\"/>%n", pos.x, pos.y);
            @out.Printf("  </g>%n");
        }
    }

    protected virtual string StyleSheetToSVG(StyleSheet sheet)
    {
        StringBuilder out = new StringBuilder();
        AddRule(@out, sheet.GetDefaultGraphRule());
        return @out.ToString();
    }

    protected virtual void AddRule(StringBuilder @out, Rule rule)
    {
    }
}
