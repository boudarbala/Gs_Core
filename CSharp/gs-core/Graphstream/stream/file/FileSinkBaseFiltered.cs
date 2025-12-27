using Java.Util;
using Gs_Core.Graphstream.Stream.File;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;

namespace Gs_Core.Graphstream.Stream.File;

public abstract class FileSinkBaseFiltered : FileSinkBase
{
    protected bool noFilterGraphAttributeAdded;
    protected bool noFilterGraphAttributeChanged;
    protected bool noFilterGraphAttributeRemoved;
    protected bool noFilterNodeAttributeAdded;
    protected bool noFilterNodeAttributeChanged;
    protected bool noFilterNodeAttributeRemoved;
    protected bool noFilterNodeAdded;
    protected bool noFilterNodeRemoved;
    protected bool noFilterEdgeAttributeAdded;
    protected bool noFilterEdgeAttributeChanged;
    protected bool noFilterEdgeAttributeRemoved;
    protected bool noFilterEdgeAdded;
    protected bool noFilterEdgeRemoved;
    protected bool noFilterGraphCleared;
    protected bool noFilterStepBegins;
    protected List<string> graphAttributesFiltered;
    protected List<string> nodeAttributesFiltered;
    protected List<string> edgeAttributesFiltered;
    public FileSinkBaseFiltered()
    {
        noFilterGraphAttributeAdded = true;
        noFilterGraphAttributeChanged = true;
        noFilterGraphAttributeRemoved = true;
        noFilterNodeAttributeAdded = true;
        noFilterNodeAttributeChanged = true;
        noFilterNodeAttributeRemoved = true;
        noFilterNodeAdded = true;
        noFilterNodeRemoved = true;
        noFilterEdgeAttributeAdded = true;
        noFilterEdgeAttributeChanged = true;
        noFilterEdgeAttributeRemoved = true;
        noFilterEdgeAdded = true;
        noFilterEdgeRemoved = true;
        noFilterGraphCleared = true;
        noFilterStepBegins = true;
        graphAttributesFiltered = new List<string>();
        nodeAttributesFiltered = new List<string>();
        edgeAttributesFiltered = new List<string>();
    }

    public virtual List<string> GetGraphAttributesFiltered()
    {
        return graphAttributesFiltered;
    }

    public virtual void SetGraphAttributesFiltered(List<string> graphAttributesFiltered)
    {
        this.graphAttributesFiltered = graphAttributesFiltered;
    }

    public virtual bool AddGraphAttributeFiltered(string attr)
    {
        return graphAttributesFiltered.Add(attr);
    }

    public virtual bool RemoveGraphAttributeFilter(string attr)
    {
        return graphAttributesFiltered.Remove(attr);
    }

    public virtual List<string> GetNodeAttributesFiltered()
    {
        return graphAttributesFiltered;
    }

    public virtual void SetNodeAttributesFiltered(List<string> nodeAttributesFiltered)
    {
        this.nodeAttributesFiltered = nodeAttributesFiltered;
    }

    public virtual bool AddNodeAttributeFiltered(string attr)
    {
        return nodeAttributesFiltered.Add(attr);
    }

    public virtual bool RemoveNodeAttributeFilter(string attr)
    {
        return nodeAttributesFiltered.Remove(attr);
    }

    public virtual List<string> GetEdgeAttributesFiltered()
    {
        return edgeAttributesFiltered;
    }

    public virtual void SetEdgeAttributesFiltered(List<string> edgeAttributesFiltered)
    {
        this.edgeAttributesFiltered = edgeAttributesFiltered;
    }

    public virtual bool AddEdgeAttributeFiltered(string attr)
    {
        return edgeAttributesFiltered.Add(attr);
    }

    public virtual bool RemoveEdgeAttributeFilter(string attr)
    {
        return edgeAttributesFiltered.Remove(attr);
    }

    public virtual bool IsNoFilterGraphAttributeAdded()
    {
        return noFilterGraphAttributeAdded;
    }

    public virtual void SetNoFilterGraphAttributeAdded(bool noFilterGraphAttributeAdded)
    {
        this.noFilterGraphAttributeAdded = noFilterGraphAttributeAdded;
    }

    public virtual bool IsNoFilterGraphAttributeChanged()
    {
        return noFilterGraphAttributeChanged;
    }

    public virtual void SetNoFilterGraphAttributeChanged(bool noFilterGraphAttributeChanged)
    {
        this.noFilterGraphAttributeChanged = noFilterGraphAttributeChanged;
    }

    public virtual bool IsNoFilterGraphAttributeRemoved()
    {
        return noFilterGraphAttributeRemoved;
    }

    public virtual void SetNoFilterGraphAttributeRemoved(bool noFilterGraphAttributeRemoved)
    {
        this.noFilterGraphAttributeRemoved = noFilterGraphAttributeRemoved;
    }

    public virtual bool IsNoFilterNodeAttributeAdded()
    {
        return noFilterNodeAttributeAdded;
    }

    public virtual void SetNoFilterNodeAttributeAdded(bool noFilterNodeAttributeAdded)
    {
        this.noFilterNodeAttributeAdded = noFilterNodeAttributeAdded;
    }

    public virtual bool IsNoFilterNodeAttributeChanged()
    {
        return noFilterNodeAttributeChanged;
    }

    public virtual void SetNoFilterNodeAttributeChanged(bool noFilterNodeAttributeChanged)
    {
        this.noFilterNodeAttributeChanged = noFilterNodeAttributeChanged;
    }

    public virtual bool IsNoFilterNodeAttributeRemoved()
    {
        return noFilterNodeAttributeRemoved;
    }

    public virtual void SetNoFilterNodeAttributeRemoved(bool noFilterNodeAttributeRemoved)
    {
        this.noFilterNodeAttributeRemoved = noFilterNodeAttributeRemoved;
    }

    public virtual bool IsNoFilterNodeAdded()
    {
        return noFilterNodeAdded;
    }

    public virtual void SetNoFilterNodeAdded(bool noFilterNodeAdded)
    {
        this.noFilterNodeAdded = noFilterNodeAdded;
    }

    public virtual bool IsNoFilterNodeRemoved()
    {
        return noFilterNodeRemoved;
    }

    public virtual void SetNoFilterNodeRemoved(bool noFilterNodeRemoved)
    {
        this.noFilterNodeRemoved = noFilterNodeRemoved;
    }

    public virtual bool IsNoFilterEdgeAttributeAdded()
    {
        return noFilterEdgeAttributeAdded;
    }

    public virtual void SetNoFilterEdgeAttributeAdded(bool noFilterEdgeAttributeAdded)
    {
        this.noFilterEdgeAttributeAdded = noFilterEdgeAttributeAdded;
    }

    public virtual bool IsNoFilterEdgeAttributeChanged()
    {
        return noFilterEdgeAttributeChanged;
    }

    public virtual void SetNoFilterEdgeAttributeChanged(bool noFilterEdgeAttributeChanged)
    {
        this.noFilterEdgeAttributeChanged = noFilterEdgeAttributeChanged;
    }

    public virtual bool IsNoFilterEdgeAttributeRemoved()
    {
        return noFilterEdgeAttributeRemoved;
    }

    public virtual void SetNoFilterEdgeAttributeRemoved(bool noFilterEdgeAttributeRemoved)
    {
        this.noFilterEdgeAttributeRemoved = noFilterEdgeAttributeRemoved;
    }

    public virtual bool IsNoFilterEdgeAdded()
    {
        return noFilterEdgeAdded;
    }

    public virtual void SetNoFilterEdgeAdded(bool noFilterEdgeAdded)
    {
        this.noFilterEdgeAdded = noFilterEdgeAdded;
    }

    public virtual bool IsNoFilterEdgeRemoved()
    {
        return noFilterEdgeRemoved;
    }

    public virtual void SetNoFilterEdgeRemoved(bool noFilterEdgeRemoved)
    {
        this.noFilterEdgeRemoved = noFilterEdgeRemoved;
    }

    public virtual bool IsNoFilterGraphCleared()
    {
        return noFilterGraphCleared;
    }

    public virtual void SetNoFilterGraphCleared(bool noFilterGraphCleared)
    {
        this.noFilterGraphCleared = noFilterGraphCleared;
    }

    public virtual bool IsNoFilterStepBegins()
    {
        return noFilterStepBegins;
    }

    public virtual void SetNoFilterStepBegins(bool noFilterStepBegins)
    {
        this.noFilterStepBegins = noFilterStepBegins;
    }
}
