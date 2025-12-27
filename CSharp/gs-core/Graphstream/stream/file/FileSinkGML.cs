using Java.Io;
using Java.Util;
using Java.Util.Regex;
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

public class FileSinkGML : FileSinkBase
{
    protected PrintWriter out;
    protected string nodeToFinish = null;
    protected string edgeToFinish = null;
    public FileSinkGML()
    {
    }

    protected override void OutputHeader()
    {
        @out = (PrintWriter)output;
        @out.Printf("graph [%n");
    }

    protected override void OutputEndOfFile()
    {
        EnsureToFinish();
        @out.Printf("]%n");
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        EnsureToFinish();
        string val = ValueToString(value);
        attribute = KeyToString(attribute);
        if (val != null)
        {
            @out.Printf("\t%s %s%n", attribute, val);
        }
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        EnsureToFinish();
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        EnsureToFinish();
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        if (nodeToFinish != null && nodeToFinish.Equals(nodeId))
        {
            string val = ValueToString(value);
            attribute = KeyToString(attribute);
            if (val != null)
            {
                @out.Printf("\t\t%s %s%n", attribute, val);
            }
        }
        else
        {
            EnsureToFinish();
        }
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        if (edgeToFinish != null)
            EnsureToFinish();
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        if (edgeToFinish != null)
            EnsureToFinish();
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        if (edgeToFinish != null && edgeToFinish.Equals(edgeId))
        {
            string val = ValueToString(value);
            attribute = KeyToString(attribute);
            if (val != null)
            {
                @out.Printf("\t\t%s %s%n", attribute, val);
            }
        }
        else
        {
            EnsureToFinish();
        }
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        if (nodeToFinish != null)
            EnsureToFinish();
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        if (nodeToFinish != null)
            EnsureToFinish();
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        EnsureToFinish();
        @out.Printf("\tnode [%n");
        @out.Printf("\t\tid \"%s\"%n", nodeId);
        nodeToFinish = nodeId;
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        EnsureToFinish();
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        EnsureToFinish();
        @out.Printf("\tedge [%n");
        @out.Printf("\t\tid \"%s\"%n", edgeId);
        @out.Printf("\t\tsource \"%s\"%n", fromNodeId);
        @out.Printf("\t\ttarget \"%s\"%n", toNodeId);
        edgeToFinish = edgeId;
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        EnsureToFinish();
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
    }

    Pattern forbiddenKeyChars = Pattern.Compile(".*[^a-zA-Z0-9-_.].*");
    protected virtual string KeyToString(string key)
    {
        if (forbiddenKeyChars.Matcher(key).Matches())
            return "\"" + key.Replace("\"", "\\\"") + "\"";
        return key;
    }

    protected virtual string ValueToString(object value)
    {
        if (value == null)
            return null;
        if (value is Number)
        {
            double val = ((Number)value).DoubleValue();
            if ((val - ((int)val)) == 0)
                return String.Format(Locale.US, "%d", (int)val);
            else
                return String.Format(Locale.US, "%f", val);
        }

        return String.Format("\"%s\"", value.ToString().ReplaceAll("\n|\r|\"", " "));
    }

    protected virtual void EnsureToFinish()
    {
        if (nodeToFinish != null || edgeToFinish != null)
        {
            @out.Printf("\t]%n");
            nodeToFinish = null;
            edgeToFinish = null;
        }
    }
}
