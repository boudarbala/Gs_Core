using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkDynamicGML : FileSinkGML
{
    public FileSinkDynamicGML()
    {
    }

    public override void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        EnsureToFinish();
        string val = ValueToString(value);
        if (val != null)
        {
            @out.Printf("\t%s %s%n", attribute, val);
        }
    }

    public override void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        EnsureToFinish();
        GraphAttributeAdded(sourceId, timeId, attribute, newValue);
    }

    public override void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        EnsureToFinish();
        @out.Printf("\t-%s%n", attribute);
    }

    public override void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        NodeAttributeChanged(sourceId, timeId, nodeId, attribute, null, value);
    }

    public override void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (nodeToFinish == null || (!nodeToFinish.Equals(nodeId)))
        {
            EnsureToFinish();
            @out.Printf("\t+node [%n");
            @out.Printf("\t\tid \"%s\"%n", nodeId);
            nodeToFinish = nodeId;
        }

        if (newValue != null)
        {
            string val = ValueToString(newValue);
            if (val != null)
            {
                @out.Printf("\t\t%s %s%n", attribute, val);
            }
        }
        else
        {
            @out.Printf("\t\t-%s%n", attribute);
        }
    }

    public override void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        NodeAttributeChanged(sourceId, timeId, nodeId, attribute, null, null);
    }

    public override void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, null, value);
    }

    public override void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (edgeToFinish == null || (!edgeToFinish.Equals(edgeId)))
        {
            EnsureToFinish();
            @out.Printf("\t+edge [%n");
            @out.Printf("\t\tid \"%s\"%n", edgeId);
            edgeToFinish = edgeId;
        }

        if (newValue != null)
        {
            string val = ValueToString(newValue);
            if (val != null)
            {
                @out.Printf("\t\t%s %s%n", attribute, val);
            }
        }
        else
        {
            @out.Printf("\t\t-%s%n", attribute);
        }
    }

    public override void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, null, null);
    }

    public override void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        EnsureToFinish();
        @out.Printf("\tnode [%n");
        @out.Printf("\t\tid \"%s\"%n", nodeId);
        nodeToFinish = nodeId;
    }

    public override void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        EnsureToFinish();
        @out.Printf("\t-node \"%s\"%n", nodeId);
    }

    public override void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        EnsureToFinish();
        @out.Printf("\tedge [%n");
        @out.Printf("\t\tid \"%s\"%n", edgeId);
        @out.Printf("\t\tsource \"%s\"%n", fromNodeId);
        @out.Printf("\t\ttarget \"%s\"%n", toNodeId);
        @out.Printf("\t\tdirected %s%n", directed ? "1" : "0");
        edgeToFinish = edgeId;
    }

    public override void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        EnsureToFinish();
        @out.Printf("\t-edge \"%s\"%n", edgeId);
    }

    public override void GraphCleared(string sourceId, long timeId)
    {
    }

    public override void StepBegins(string sourceId, long timeId, double step)
    {
        EnsureToFinish();
        if ((step - ((int)step)) == 0)
            @out.Printf("\tstep %d%n", (int)step);
        else
            @out.Printf("\tstep %f%n", step);
    }
}
