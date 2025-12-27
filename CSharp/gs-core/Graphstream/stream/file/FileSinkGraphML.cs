using Java.Io;
using Java.Util;
using Java.Util.Concurrent.Atomic;
using Java.Util.Function;
using Gs_Core.Graphstream.Graph;
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

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkGraphML : FileSinkBase
{
    protected virtual void OutputEndOfFile()
    {
        Print("</graphml>\n");
    }

    protected virtual void OutputHeader()
    {
        Print("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
        Print("<graphml xmlns=\"http://graphml.graphdrawing.org/xmlns\"\n");
        Print("\t xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"\n");
        Print("\t xsi:schemaLocation=\"http://graphml.graphdrawing.org/xmlns\n");
        Print("\t   http://graphml.graphdrawing.org/xmlns/1.0/graphml.xsd\">\n");
    }

    private void Print(string format, params object[] args)
    {
        output.Write(String.Format(format, args));
    }

    protected override void ExportGraph(Graph g)
    {
        Consumer<Exception> onException = Exception.PrintStackTrace();
        AtomicInteger attribute = new AtomicInteger(0);
        HashMap<string, string> nodeAttributes = new HashMap();
        HashMap<string, string> edgeAttributes = new HashMap();
        g.Nodes().ForEach((n) =>
        {
            n.AttributeKeys().ForEach((k) =>
            {
                if (!nodeAttributes.ContainsKey(k))
                {
                    object value = n.GetAttribute(k);
                    string type;
                    if (value == null)
                        return;
                    string id = String.Format("attr%04X", attribute.GetAndIncrement());
                    if (value is bool)
                        type = "boolean";
                    else if (value is long)
                        type = "long";
                    else if (value is int)
                        type = "int";
                    else if (value is Double)
                        type = "double";
                    else if (value is float)
                        type = "float";
                    else
                        type = "string";
                    nodeAttributes.Put(k, id);
                    try
                    {
                        Print("\t<key id=\"%s\" for=\"node\" attr.name=\"%s\" attr.type=\"%s\"/>\n", id, EscapeXmlString(k), type);
                    }
                    catch (Exception ex)
                    {
                        onException.Accept(ex);
                    }
                }
            });
        });
        g.Edges().ForEach((n) =>
        {
            n.AttributeKeys().ForEach((k) =>
            {
                if (!edgeAttributes.ContainsKey(k))
                {
                    object value = n.GetAttribute(k);
                    string type;
                    if (value == null)
                        return;
                    string id = String.Format("attr%04X", attribute.GetAndIncrement());
                    if (value is bool)
                        type = "boolean";
                    else if (value is long)
                        type = "long";
                    else if (value is int)
                        type = "int";
                    else if (value is Double)
                        type = "double";
                    else if (value is float)
                        type = "float";
                    else
                        type = "string";
                    edgeAttributes.Put(k, id);
                    try
                    {
                        Print("\t<key id=\"%s\" for=\"edge\" attr.name=\"%s\" attr.type=\"%s\"/>\n", id, EscapeXmlString(k), type);
                    }
                    catch (Exception ex)
                    {
                        onException.Accept(ex);
                    }
                }
            });
        });
        try
        {
            Print("\t<graph id=\"%s\" edgedefault=\"undirected\">\n", EscapeXmlString(g.GetId()));
        }
        catch (Exception e)
        {
            onException.Accept(e);
        }

        g.Nodes().ForEach((n) =>
        {
            try
            {
                Print("\t\t<node id=\"%s\">\n", n.GetId());
                n.AttributeKeys().ForEach((k) =>
                {
                    try
                    {
                        Print("\t\t\t<data key=\"%s\">%s</data>\n", nodeAttributes[k], EscapeXmlString(n.GetAttribute(k).ToString()));
                    }
                    catch (IOException e)
                    {
                        onException.Accept(e);
                    }
                });
                Print("\t\t</node>\n");
            }
            catch (Exception ex)
            {
                onException.Accept(ex);
            }
        });
        g.Edges().ForEach((e) =>
        {
            try
            {
                Print("\t\t<edge id=\"%s\" source=\"%s\" target=\"%s\" directed=\"%s\">\n", e.GetId(), e.GetSourceNode().GetId(), e.GetTargetNode().GetId(), e.IsDirected());
                e.AttributeKeys().ForEach((k) =>
                {
                    try
                    {
                        Print("\t\t\t<data key=\"%s\">%s</data>\n", edgeAttributes[k], EscapeXmlString(e.GetAttribute(k).ToString()));
                    }
                    catch (IOException e1)
                    {
                        onException.Accept(e1);
                    }
                });
                Print("\t\t</edge>\n");
            }
            catch (Exception ex)
            {
                onException.Accept(ex);
            }
        });
        try
        {
            Print("\t</graph>\n");
        }
        catch (Exception e)
        {
            onException.Accept(e);
        }
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        throw new NotSupportedException();
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        throw new NotSupportedException();
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        throw new NotSupportedException();
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        throw new NotSupportedException();
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        throw new NotSupportedException();
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        throw new NotSupportedException();
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        throw new NotSupportedException();
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        throw new NotSupportedException();
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        throw new NotSupportedException();
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        throw new NotSupportedException();
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        throw new NotSupportedException();
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        throw new NotSupportedException();
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        throw new NotSupportedException();
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        throw new NotSupportedException();
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        throw new NotSupportedException();
    }

    private static string EscapeXmlString(string @string)
    {
        return @string.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
    }
}
