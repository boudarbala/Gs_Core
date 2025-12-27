using Gs_Core.Graphstream.Stream.SourceBase;
using Java.Lang.Annotation;
using Java.Lang.Reflect;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Stream;

public abstract class AnnotatedSink : Sink
{
    private readonly EnumMap<ElementType, MethodMap> methods;
    protected AnnotatedSink()
    {
        methods = new EnumMap<ElementType, MethodMap>(typeof(ElementType));
        methods.Put(ElementType.GRAPH, new MethodMap());
        methods.Put(ElementType.EDGE, new MethodMap());
        methods.Put(ElementType.NODE, new MethodMap());
        Method[] ms = GetType().GetMethods();
        if (ms != null)
        {
            for (int i = 0; i < ms.Length; i++)
            {
                Method m = ms[i];
                Bind b = m.GetAnnotation(typeof(Bind));
                if (b != null)
                    methods[b.Type()].Put(b.Value(), m);
            }
        }
    }

    private void Invoke(Method m, params object[] args)
    {
        try
        {
            m.Invoke(this, args);
        }
        catch (ArgumentException e)
        {
            e.PrintStackTrace();
        }
        catch (IllegalAccessException e)
        {
            e.PrintStackTrace();
        }
        catch (InvocationTargetException e)
        {
            e.PrintStackTrace();
        }
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        Method m = methods[ElementType.EDGE][attribute];
        if (m != null)
            Invoke(m, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        Method m = methods[ElementType.EDGE][attribute];
        if (m != null)
            Invoke(m, edgeId, attribute, newValue);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        Method m = methods[ElementType.EDGE][attribute];
        if (m != null)
            Invoke(m, edgeId, attribute, null);
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        Method m = methods[ElementType.GRAPH][attribute];
        if (m != null)
            Invoke(m, attribute, value);
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        Method m = methods[ElementType.GRAPH][attribute];
        if (m != null)
            Invoke(m, attribute, newValue);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        Method m = methods[ElementType.GRAPH][attribute];
        if (m != null)
            Invoke(m, attribute, null);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        Method m = methods[ElementType.NODE][attribute];
        if (m != null)
            Invoke(m, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        Method m = methods[ElementType.NODE][attribute];
        if (m != null)
            Invoke(m, nodeId, attribute, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        Method m = methods[ElementType.NODE][attribute];
        if (m != null)
            Invoke(m, nodeId, attribute, null);
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

    private class MethodMap : HashMap<string, Method>
    {
        private static readonly long serialVersionUID = 1664854698109523697;
    }
}
