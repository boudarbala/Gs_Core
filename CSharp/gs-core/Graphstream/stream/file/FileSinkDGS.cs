using Java.Io;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkDGS : FileSinkBase
{
    protected PrintWriter out;
    protected string graphName = "";
    protected override void OutputHeader()
    {
        @out = (PrintWriter)output;
        @out.Printf("DGS004%n");
        if (graphName.Length <= 0)
            @out.Printf("null 0 0%n");
        else
            @out.Printf("\"%s\" 0 0%n", FileSinkDGSUtility.FormatStringForQuoting(graphName));
    }

    protected override void OutputEndOfFile()
    {
    }

    public virtual void EdgeAttributeAdded(string graphId, long timeId, string edgeId, string attribute, object value)
    {
        EdgeAttributeChanged(graphId, timeId, edgeId, attribute, null, value);
    }

    public virtual void EdgeAttributeChanged(string graphId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        @out.Printf("ce \"%s\" %s%n", FileSinkDGSUtility.FormatStringForQuoting(edgeId), FileSinkDGSUtility.AttributeString(attribute, newValue, false));
    }

    public virtual void EdgeAttributeRemoved(string graphId, long timeId, string edgeId, string attribute)
    {
        @out.Printf("ce \"%s\" %s%n", FileSinkDGSUtility.FormatStringForQuoting(edgeId), FileSinkDGSUtility.AttributeString(attribute, null, true));
    }

    public virtual void GraphAttributeAdded(string graphId, long timeId, string attribute, object value)
    {
        GraphAttributeChanged(graphId, timeId, attribute, null, value);
    }

    public virtual void GraphAttributeChanged(string graphId, long timeId, string attribute, object oldValue, object newValue)
    {
        @out.Printf("cg %s%n", FileSinkDGSUtility.AttributeString(attribute, newValue, false));
    }

    public virtual void GraphAttributeRemoved(string graphId, long timeId, string attribute)
    {
        @out.Printf("cg %s%n", FileSinkDGSUtility.AttributeString(attribute, null, true));
    }

    public virtual void NodeAttributeAdded(string graphId, long timeId, string nodeId, string attribute, object value)
    {
        NodeAttributeChanged(graphId, timeId, nodeId, attribute, null, value);
    }

    public virtual void NodeAttributeChanged(string graphId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        @out.Printf("cn \"%s\" %s%n", FileSinkDGSUtility.FormatStringForQuoting(nodeId), FileSinkDGSUtility.AttributeString(attribute, newValue, false));
    }

    public virtual void NodeAttributeRemoved(string graphId, long timeId, string nodeId, string attribute)
    {
        @out.Printf("cn \"%s\" %s%n", FileSinkDGSUtility.FormatStringForQuoting(nodeId), FileSinkDGSUtility.AttributeString(attribute, null, true));
    }

    public virtual void EdgeAdded(string graphId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        edgeId = FileSinkDGSUtility.FormatStringForQuoting(edgeId);
        fromNodeId = FileSinkDGSUtility.FormatStringForQuoting(fromNodeId);
        toNodeId = FileSinkDGSUtility.FormatStringForQuoting(toNodeId);
        @out.Printf("ae \"%s\" \"%s\" %s \"%s\"%n", edgeId, fromNodeId, directed ? ">" : "", toNodeId);
    }

    public virtual void EdgeRemoved(string graphId, long timeId, string edgeId)
    {
        @out.Printf("de \"%s\"%n", FileSinkDGSUtility.FormatStringForQuoting(edgeId));
    }

    public virtual void GraphCleared(string graphId, long timeId)
    {
        @out.Printf("cl%n");
    }

    public virtual void NodeAdded(string graphId, long timeId, string nodeId)
    {
        @out.Printf("an \"%s\"%n", FileSinkDGSUtility.FormatStringForQuoting(nodeId));
    }

    public virtual void NodeRemoved(string graphId, long timeId, string nodeId)
    {
        @out.Printf("dn \"%s\"%n", FileSinkDGSUtility.FormatStringForQuoting(nodeId));
    }

    public virtual void StepBegins(string graphId, long timeId, double step)
    {
        @out.Printf(Locale.US, "st %f%n", step);
    }
}
