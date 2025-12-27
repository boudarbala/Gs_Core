using Java.Io;
using Java.Lang.Reflect;
using Java.Util;
using Gs_Core.Graphstream.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.ElementType;
using static Org.Graphstream.Util.EventType;

namespace Gs_Core.Graphstream.Util;

public class VerboseSink : Sink
{
    public static readonly string DEFAULT_AN_FORMAT = "%prefix%[%sourceId%:%timeId%] add node \"%nodeId%\"%suffix%";
    public static readonly string DEFAULT_CNA_FORMAT = "%prefix%[%sourceId%:%timeId%] set node \"%nodeId%\" +\"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CNC_FORMAT = "%prefix%[%sourceId%:%timeId%] set node \"%nodeId%\" \"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CNR_FORMAT = "%prefix%[%sourceId%:%timeId%] set node \"%nodeId%\" -\"%attributeId%\"%suffix%";
    public static readonly string DEFAULT_DN_FORMAT = "%prefix%[%sourceId%:%timeId%] remove node \"%nodeId%\"%suffix%";
    public static readonly string DEFAULT_AE_FORMAT = "%prefix%[%sourceId%:%timeId%] add edge \"%edgeId%\" : \"%source%\" -- \"%target%\"%suffix%";
    public static readonly string DEFAULT_CEA_FORMAT = "%prefix%[%sourceId%:%timeId%] set edge \"%edgeId%\" +\"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CEC_FORMAT = "%prefix%[%sourceId%:%timeId%] set edge \"%edgeId%\" \"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CER_FORMAT = "%prefix%[%sourceId%:%timeId%] set edge \"%edgeId%\" -\"%attributeId%\"%suffix%";
    public static readonly string DEFAULT_DE_FORMAT = "%prefix%[%sourceId%:%timeId%] remove edge \"%edgeId%\"%suffix%";
    public static readonly string DEFAULT_CGA_FORMAT = "%prefix%[%sourceId%:%timeId%] set +\"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CGC_FORMAT = "%prefix%[%sourceId%:%timeId%] set \"%attributeId%\"=%value%%suffix%";
    public static readonly string DEFAULT_CGR_FORMAT = "%prefix%[%sourceId%:%timeId%] set -\"%attributeId%\"%suffix%";
    public static readonly string DEFAULT_CL_FORMAT = "%prefix%[%sourceId%:%timeId%] clear%suffix%";
    public static readonly string DEFAULT_ST_FORMAT = "%prefix%[%sourceId%:%timeId%] step %step% begins%suffix%";
    private class Args : HashMap<string, object>
    {
        private static readonly long serialVersionUID = 3064164898156692557;
    }

    public enum EventType
    {
        ADD_NODE,
        ADD_NODE_ATTRIBUTE,
        SET_NODE_ATTRIBUTE,
        DEL_NODE_ATTRIBUTE,
        DEL_NODE,
        ADD_EDGE,
        ADD_EDGE_ATTRIBUTE,
        SET_EDGE_ATTRIBUTE,
        DEL_EDGE_ATTRIBUTE,
        DEL_EDGE,
        ADD_GRAPH_ATTRIBUTE,
        SET_GRAPH_ATTRIBUTE,
        DEL_GRAPH_ATTRIBUTE,
        CLEAR,
        STEP_BEGINS
    }

    protected bool autoflush;
    protected readonly PrintStream out;
    protected readonly EnumMap<EventType, string> formats;
    protected readonly EnumMap<EventType, bool> enable;
    private readonly Stack<Args> argsStack;
    protected string prefix;
    protected string suffix;
    public VerboseSink() : this(System.@out)
    {
    }

    public VerboseSink(PrintStream @out)
    {
        this.@out = @out;
        argsStack = new Stack<Args>();
        enable = new EnumMap<EventType, bool>(typeof(EventType));
        formats = new EnumMap<EventType, string>(typeof(EventType));
        formats.Put(EventType.ADD_NODE, DEFAULT_AN_FORMAT);
        formats.Put(EventType.ADD_NODE_ATTRIBUTE, DEFAULT_CNA_FORMAT);
        formats.Put(EventType.SET_NODE_ATTRIBUTE, DEFAULT_CNC_FORMAT);
        formats.Put(EventType.DEL_NODE_ATTRIBUTE, DEFAULT_CNR_FORMAT);
        formats.Put(EventType.DEL_NODE, DEFAULT_DN_FORMAT);
        formats.Put(EventType.ADD_EDGE, DEFAULT_AE_FORMAT);
        formats.Put(EventType.ADD_EDGE_ATTRIBUTE, DEFAULT_CEA_FORMAT);
        formats.Put(EventType.SET_EDGE_ATTRIBUTE, DEFAULT_CEC_FORMAT);
        formats.Put(EventType.DEL_EDGE_ATTRIBUTE, DEFAULT_CER_FORMAT);
        formats.Put(EventType.DEL_EDGE, DEFAULT_DE_FORMAT);
        formats.Put(EventType.ADD_GRAPH_ATTRIBUTE, DEFAULT_CGA_FORMAT);
        formats.Put(EventType.SET_GRAPH_ATTRIBUTE, DEFAULT_CGC_FORMAT);
        formats.Put(EventType.DEL_GRAPH_ATTRIBUTE, DEFAULT_CGR_FORMAT);
        formats.Put(EventType.CLEAR, DEFAULT_CL_FORMAT);
        formats.Put(EventType.STEP_BEGINS, DEFAULT_ST_FORMAT);
        foreach (EventType t in EventType.Values())
            enable.Put(t, Boolean.TRUE);
        suffix = "";
        prefix = "";
    }

    public virtual void SetAutoFlush(bool on)
    {
        this.autoflush = on;
    }

    public virtual void SetEventFormat(EventType type, string format)
    {
        formats.Put(type, format);
    }

    public virtual void SetEventEnabled(EventType type, bool on)
    {
        enable.Put(type, on);
    }

    public virtual void SetElementEventEnabled(bool on)
    {
        enable.Put(EventType.ADD_EDGE_ATTRIBUTE, on);
        enable.Put(EventType.SET_EDGE_ATTRIBUTE, on);
        enable.Put(EventType.DEL_EDGE_ATTRIBUTE, on);
        enable.Put(EventType.ADD_NODE_ATTRIBUTE, on);
        enable.Put(EventType.SET_NODE_ATTRIBUTE, on);
        enable.Put(EventType.DEL_NODE_ATTRIBUTE, on);
        enable.Put(EventType.ADD_GRAPH_ATTRIBUTE, on);
        enable.Put(EventType.SET_GRAPH_ATTRIBUTE, on);
        enable.Put(EventType.DEL_GRAPH_ATTRIBUTE, on);
    }

    public virtual void SetAttributeEventEnabled(bool on)
    {
        enable.Put(EventType.ADD_EDGE, on);
        enable.Put(EventType.DEL_EDGE, on);
        enable.Put(EventType.ADD_NODE, on);
        enable.Put(EventType.DEL_NODE, on);
        enable.Put(EventType.CLEAR, on);
    }

    public virtual void SetPrefix(string prefix)
    {
        this.prefix = prefix;
    }

    public virtual void SetSuffix(string suffix)
    {
        this.suffix = suffix;
    }

    private void Print(EventType type, Args args)
    {
        if (!enable[type])
            return;
        string out = formats[type];
        foreach (string k in args.KeySet())
        {
            object o = args[k];
            @out = @out.Replace(String.Format("%%%s%%", k), o == null ? "null" : o.ToString());
        }

        this.@out.Print(@out);
        this.@out.Printf("\n");
        if (autoflush)
            this.@out.Flush();
        ArgsPnP(args);
    }

    private Args ArgsPnP(Args args)
    {
        if (args == null)
        {
            if (argsStack.Count > 0)
                args = argsStack.Pop();
            else
                args = new Args();
            args.Put("prefix", prefix);
            args.Put("suffix", suffix);
            return args;
        }
        else
        {
            args.Clear();
            argsStack.Push(args);
            return null;
        }
    }

    private string ToStringValue(object o)
    {
        if (o == null)
            return "<null>";
        if (o is string)
            return "\"" + ((string)o).Replace("\"", "\\\"") + "\"";
        else if (o.GetType().IsArray())
        {
            StringBuilder buffer = new StringBuilder();
            buffer.Append("{");
            for (int i = 0; i < Array.GetLength(o); i++)
            {
                if (i > 0)
                    buffer.Append(", ");
                buffer.Append(ToStringValue(Array.Get(o, i)));
            }

            buffer.Append("}");
            return buffer.ToString();
        }

        return o.ToString();
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("edgeId", edgeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(value));
        Print(EventType.ADD_EDGE_ATTRIBUTE, args);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("edgeId", edgeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(newValue));
        Print(EventType.SET_EDGE_ATTRIBUTE, args);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("edgeId", edgeId);
        args.Put("attributeId", attribute);
        Print(EventType.DEL_EDGE_ATTRIBUTE, args);
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(value));
        Print(EventType.ADD_GRAPH_ATTRIBUTE, args);
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(newValue));
        Print(EventType.SET_GRAPH_ATTRIBUTE, args);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("attributeId", attribute);
        Print(EventType.DEL_GRAPH_ATTRIBUTE, args);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("nodeId", nodeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(value));
        Print(EventType.ADD_NODE_ATTRIBUTE, args);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("nodeId", nodeId);
        args.Put("attributeId", attribute);
        args.Put("value", ToStringValue(newValue));
        Print(EventType.SET_NODE_ATTRIBUTE, args);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("nodeId", nodeId);
        args.Put("attributeId", attribute);
        Print(EventType.DEL_NODE_ATTRIBUTE, args);
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("edgeId", edgeId);
        args.Put("source", fromNodeId);
        args.Put("target", toNodeId);
        Print(EventType.ADD_EDGE, args);
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("edgeId", edgeId);
        Print(EventType.DEL_EDGE, args);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        Print(EventType.CLEAR, args);
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("nodeId", nodeId);
        Print(EventType.ADD_NODE, args);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("nodeId", nodeId);
        Print(EventType.DEL_NODE, args);
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        Args args = ArgsPnP(null);
        args.Put("sourceId", sourceId);
        args.Put("timeId", timeId);
        args.Put("step", step);
        Print(EventType.STEP_BEGINS, args);
    }
}
