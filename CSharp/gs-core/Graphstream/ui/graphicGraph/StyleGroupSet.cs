using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.EventType;
using static Org.Graphstream.Ui.GraphicGraph.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Mode;
using static Org.Graphstream.Ui.GraphicGraph.What;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Option;
using static Org.Graphstream.Ui.GraphicGraph.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Balise;
using static Org.Graphstream.Ui.GraphicGraph.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.IDType;
using static Org.Graphstream.Ui.GraphicGraph.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.FixType;
using static Org.Graphstream.Ui.GraphicGraph.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.GraphEvents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph;

public class StyleGroupSet : StyleSheetListener
{
    protected StyleSheet stylesheet;
    protected readonly Dictionary<string, StyleGroup> groups = new TreeMap<string, StyleGroup>();
    protected readonly Dictionary<string, string> byNodeIdGroups = new TreeMap<string, string>();
    protected readonly Dictionary<string, string> byEdgeIdGroups = new TreeMap<string, string>();
    protected readonly Dictionary<string, string> bySpriteIdGroups = new TreeMap<string, string>();
    protected readonly Dictionary<string, string> byGraphIdGroups = new TreeMap<string, string>();
    protected NodeSet nodeSet = new NodeSet();
    protected EdgeSet edgeSet = new EdgeSet();
    protected SpriteSet spriteSet = new SpriteSet();
    protected GraphSet graphSet = new GraphSet();
    protected EventSet eventSet = new EventSet();
    protected ZIndex zIndex = new ZIndex();
    protected ShadowSet shadow = new ShadowSet();
    protected bool removeEmptyGroups = true;
    protected List<StyleGroupListener> listeners = new List();
    public StyleGroupSet(StyleSheet stylesheet)
    {
        this.stylesheet = stylesheet;
        stylesheet.AddListener(this);
    }

    public virtual int GetGroupCount()
    {
        return groups.Count;
    }

    public virtual StyleGroup GetGroup(string groupId)
    {
        return groups[groupId];
    }

    public virtual IEnumerator<TWildcardTodoStyleGroup> GetGroupIterator()
    {
        return groups.Values().Iterator();
    }

    public virtual Iterable<TWildcardTodoStyleGroup> Groups()
    {
        return groups.Values();
    }

    public virtual IEnumerator<HashSet<StyleGroup>> GetZIterator()
    {
        return zIndex.GetIterator();
    }

    public virtual Iterable<HashSet<StyleGroup>> ZIndex()
    {
        return zIndex;
    }

    public virtual IEnumerator<StyleGroup> GetShadowIterator()
    {
        return shadow.GetIterator();
    }

    public virtual Iterable<StyleGroup> Shadows()
    {
        return shadow;
    }

    public virtual bool ContainsNode(string id)
    {
        return byNodeIdGroups.ContainsKey(id);
    }

    public virtual bool ContainsEdge(string id)
    {
        return byEdgeIdGroups.ContainsKey(id);
    }

    public virtual bool ContainsSprite(string id)
    {
        return bySpriteIdGroups.ContainsKey(id);
    }

    public virtual bool ContainsGraph(string id)
    {
        return byGraphIdGroups.ContainsKey(id);
    }

    protected virtual Element GetElement(string id, Dictionary<string, string> elt2grp)
    {
        string gid = elt2grp[id];
        if (gid != null)
        {
            StyleGroup group = groups[gid];
            return group.GetElement(id);
        }

        return null;
    }

    public virtual Node GetNode(string id)
    {
        return (Node)GetElement(id, byNodeIdGroups);
    }

    public virtual Edge GetEdge(string id)
    {
        return (Edge)GetElement(id, byEdgeIdGroups);
    }

    public virtual GraphicSprite GetSprite(string id)
    {
        return (GraphicSprite)GetElement(id, bySpriteIdGroups);
    }

    public virtual Graph GetGraph(string id)
    {
        return (Graph)GetElement(id, byGraphIdGroups);
    }

    public virtual int GetNodeCount()
    {
        return byNodeIdGroups.Count;
    }

    public virtual int GetEdgeCount()
    {
        return byEdgeIdGroups.Count;
    }

    public virtual int GetSpriteCount()
    {
        return bySpriteIdGroups.Count;
    }

    public virtual IEnumerator<TWildcardTodoNode> GetNodeIterator()
    {
        return new ElementIterator<Node>(byNodeIdGroups);
    }

    public virtual IEnumerator<TWildcardTodoGraph> GetGraphIterator()
    {
        return new ElementIterator<Graph>(byGraphIdGroups);
    }

    public virtual Stream<Node> Nodes()
    {
        return byNodeIdGroups.EntrySet().Stream().Map((entry) =>
        {
            return (Node)groups[entry.GetValue()].GetElement(entry.GetKey());
        });
    }

    public virtual Stream<Edge> Edges()
    {
        return byEdgeIdGroups.EntrySet().Stream().Map((entry) =>
        {
            return (Edge)groups[entry.GetValue()].GetElement(entry.GetKey());
        });
    }

    public virtual Stream<GraphicSprite> Sprites()
    {
        return bySpriteIdGroups.EntrySet().Stream().Map((entry) =>
        {
            return (GraphicSprite)groups[entry.GetValue()].GetElement(entry.GetKey());
        });
    }

    public virtual Iterable<TWildcardTodoGraph> Graphs()
    {
        return graphSet;
    }

    public virtual IEnumerator<TWildcardTodoEdge> GetEdgeIterator()
    {
        return new ElementIterator<Edge>(byEdgeIdGroups);
    }

    public virtual IEnumerator<TWildcardTodoGraphicSprite> GetSpriteIterator()
    {
        return new ElementIterator<GraphicSprite>(bySpriteIdGroups);
    }

    public virtual string GetElementGroup(Element element)
    {
        if (element is Node)
        {
            return byNodeIdGroups[element.GetId()];
        }
        else if (element is Edge)
        {
            return byEdgeIdGroups[element.GetId()];
        }
        else if (element is GraphicSprite)
        {
            return bySpriteIdGroups[element.GetId()];
        }
        else if (element is Graph)
        {
            return byGraphIdGroups[element.GetId()];
        }
        else
        {
            throw new Exception("What ?");
        }
    }

    public virtual StyleGroup GetStyleForElement(Element element)
    {
        string gid = GetElementGroup(element);
        return groups[gid];
    }

    public virtual StyleGroup GetStyleFor(Node node)
    {
        string gid = byNodeIdGroups[node.GetId()];
        return groups[gid];
    }

    public virtual StyleGroup GetStyleFor(Edge edge)
    {
        string gid = byEdgeIdGroups[edge.GetId()];
        return groups[gid];
    }

    public virtual StyleGroup GetStyleFor(GraphicSprite sprite)
    {
        string gid = bySpriteIdGroups[sprite.GetId()];
        return groups[gid];
    }

    public virtual StyleGroup GetStyleFor(Graph graph)
    {
        string gid = byGraphIdGroups[graph.GetId()];
        return groups[gid];
    }

    public virtual bool AreEmptyGroupRemoved()
    {
        return removeEmptyGroups;
    }

    public virtual ZIndex GetZIndex()
    {
        return zIndex;
    }

    public virtual ShadowSet GetShadowSet()
    {
        return shadow;
    }

    public virtual void Release()
    {
        stylesheet.RemoveListener(this);
    }

    public virtual void Clear()
    {
        byEdgeIdGroups.Clear();
        byNodeIdGroups.Clear();
        bySpriteIdGroups.Clear();
        byGraphIdGroups.Clear();
        groups.Clear();
        zIndex.Clear();
        shadow.Clear();
    }

    public virtual void SetRemoveEmptyGroups(bool on)
    {
        if (removeEmptyGroups == false && on == true)
        {
            IEnumerator<TWildcardTodoStyleGroup> i = groups.Values().Iterator();
            while (i.HasNext())
            {
                StyleGroup g = i.Next();
                if (g.IsEmpty())
                    i.Remove();
            }
        }

        removeEmptyGroups = on;
    }

    protected virtual StyleGroup AddGroup(string id, List<Rule> rules, Element firstElement)
    {
        StyleGroup group = new StyleGroup(id, rules, firstElement, eventSet);
        groups.Put(id, group);
        zIndex.GroupAdded(group);
        shadow.GroupAdded(group);
        return group;
    }

    protected virtual void RemoveGroup(StyleGroup group)
    {
        zIndex.GroupRemoved(group);
        shadow.GroupRemoved(group);
        groups.Remove(group.GetId());
        group.Release();
    }

    public virtual StyleGroup AddElement(Element element)
    {
        StyleGroup group = AddElement_(element);
        foreach (StyleGroupListener listener in listeners)
            listener.ElementStyleChanged(element, null, group);
        return group;
    }

    protected virtual StyleGroup AddElement_(Element element)
    {
        List<Rule> rules = stylesheet.GetRulesFor(element);
        string gid = stylesheet.GetStyleGroupIdFor(element, rules);
        StyleGroup group = groups[gid];
        if (group == null)
            group = AddGroup(gid, rules, element);
        else
            group.AddElement(element);
        AddElementToReverseSearch(element, gid);
        return group;
    }

    public virtual void RemoveElement(Element element)
    {
        string gid = GetElementGroup(element);
        if (null == gid)
        {
            return;
        }

        StyleGroup group = groups[gid];
        if (group != null)
        {
            group.RemoveElement(element);
            RemoveElementFromReverseSearch(element);
            if (removeEmptyGroups && group.IsEmpty())
                RemoveGroup(group);
        }
    }

    public virtual void CheckElementStyleGroup(Element element)
    {
        StyleGroup oldGroup = GetGroup(GetElementGroup(element));
        bool isDyn = false;
        StyleGroup.ElementEvents events = null;
        if (oldGroup != null)
        {
            isDyn = oldGroup.IsElementDynamic(element);
            events = oldGroup.GetEventsFor(element);
        }

        RemoveElement(element);
        AddElement_(element);
        StyleGroup newGroup = GetGroup(GetElementGroup(element));
        if (newGroup != null && events != null)
        {
            foreach (string event in events.events)
                PushEventFor(element, @event);
        }

        foreach (StyleGroupListener listener in listeners)
            listener.ElementStyleChanged(element, oldGroup, newGroup);
        if (newGroup != null && isDyn)
            newGroup.PushElementAsDynamic(element);
    }

    protected virtual void AddElementToReverseSearch(Element element, string groupId)
    {
        if (element is Node)
        {
            byNodeIdGroups.Put(element.GetId(), groupId);
        }
        else if (element is Edge)
        {
            byEdgeIdGroups.Put(element.GetId(), groupId);
        }
        else if (element is GraphicSprite)
        {
            bySpriteIdGroups.Put(element.GetId(), groupId);
        }
        else if (element is Graph)
        {
            byGraphIdGroups.Put(element.GetId(), groupId);
        }
        else
        {
            throw new Exception("What ?");
        }
    }

    protected virtual void RemoveElementFromReverseSearch(Element element)
    {
        if (element is Node)
        {
            byNodeIdGroups.Remove(element.GetId());
        }
        else if (element is Edge)
        {
            byEdgeIdGroups.Remove(element.GetId());
        }
        else if (element is GraphicSprite)
        {
            bySpriteIdGroups.Remove(element.GetId());
        }
        else if (element is Graph)
        {
            byGraphIdGroups.Remove(element.GetId());
        }
        else
        {
            throw new Exception("What ?");
        }
    }

    public virtual void PushEvent(string @event)
    {
        eventSet.PushEvent(@event);
    }

    public virtual void PushEventFor(Element element, string @event)
    {
        StyleGroup group = GetGroup(GetElementGroup(element));
        if (group != null)
            group.PushEventFor(element, @event);
    }

    public virtual void PopEvent(string @event)
    {
        eventSet.PopEvent(@event);
    }

    public virtual void PopEventFor(Element element, string @event)
    {
        StyleGroup group = GetGroup(GetElementGroup(element));
        if (group != null)
            group.PopEventFor(element, @event);
    }

    public virtual void PushElementAsDynamic(Element element)
    {
        StyleGroup group = GetGroup(GetElementGroup(element));
        if (group != null)
            group.PushElementAsDynamic(element);
    }

    public virtual void PopElementAsDynamic(Element element)
    {
        StyleGroup group = GetGroup(GetElementGroup(element));
        if (group != null)
            group.PopElementAsDynamic(element);
    }

    public virtual void AddListener(StyleGroupListener listener)
    {
        listeners.Add(listener);
    }

    public virtual void RemoveListener(StyleGroupListener listener)
    {
        int index = listeners.LastIndexOf(listener);
        if (index >= 0)
        {
            listeners.Remove(index);
        }
    }

    public virtual void StyleAdded(Rule oldRule, Rule newRule)
    {
        if (oldRule == null)
            CheckForNewStyle(newRule);
        else
            CheckZIndexAndShadow(oldRule, newRule);
    }

    public virtual void StyleSheetCleared()
    {
        List<Element> elements = new List<Element>();
        foreach (Element element in Graphs())
            elements.Add(element);
        Nodes().ForEach(elements.Add());
        Edges().ForEach(elements.Add());
        Sprites().ForEach(elements.Add());
        Clear();
        elements.ForEach(this.RemoveElement());
        elements.ForEach(this.AddElement());
    }

    protected virtual void CheckZIndexAndShadow(Rule oldRule, Rule newRule)
    {
        if (oldRule != null)
        {
            if (oldRule.selector.GetId() != null || oldRule.selector.GetClazz() != null)
            {
                if (oldRule.GetGroups() != null)
                    foreach (string s in oldRule.GetGroups())
                    {
                        StyleGroup group = groups[s];
                        if (group != null)
                        {
                            zIndex.GroupChanged(group);
                            shadow.GroupChanged(group);
                        }
                    }
            }
            else
            {
                Selector.Type type = oldRule.selector.type;
                foreach (StyleGroup group in groups.Values())
                {
                    if (group.GetType() == type)
                    {
                        zIndex.GroupChanged(group);
                        shadow.GroupChanged(group);
                    }
                }
            }
        }
    }

    protected virtual void CheckForNewStyle(Rule newRule)
    {
        switch (newRule.selector.type)
        {
            case GRAPH:
                if (newRule.selector.GetId() != null)
                    CheckForNewIdStyle(newRule, byGraphIdGroups);
                else
                    CheckForNewStyle(newRule, byGraphIdGroups);
                break;
            case NODE:
                if (newRule.selector.GetId() != null)
                    CheckForNewIdStyle(newRule, byNodeIdGroups);
                else
                    CheckForNewStyle(newRule, byNodeIdGroups);
                break;
            case EDGE:
                if (newRule.selector.GetId() != null)
                    CheckForNewIdStyle(newRule, byEdgeIdGroups);
                else
                    CheckForNewStyle(newRule, byEdgeIdGroups);
                break;
            case SPRITE:
                if (newRule.selector.GetId() != null)
                    CheckForNewIdStyle(newRule, bySpriteIdGroups);
                else
                    CheckForNewStyle(newRule, bySpriteIdGroups);
                break;
            case ANY:
            default:
                throw new Exception("What ?");
                break;
        }
    }

    protected virtual void CheckForNewIdStyle(Rule newRule, Dictionary<string, string> elt2grp)
    {
        Element element = GetElement(newRule.selector.GetId(), elt2grp);
        if (element != null)
        {
            CheckElementStyleGroup(element);
        }
    }

    protected virtual void CheckForNewStyle(Rule newRule, Dictionary<string, string> elt2grp)
    {
        elt2grp.KeySet().Stream().Map((eltId) => GetElement(eltId, elt2grp)).Collect(Collectors.ToList()).ForEach(this.CheckElementStyleGroup());
    }

    public virtual string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(String.Format("Style groups (%d) :%n", groups.Count));
        foreach (StyleGroup group in groups.Values())
        {
            builder.Append(group.ToString(1));
            builder.Append(String.Format("%n"));
        }

        return builder.ToString();
    }

    public class EventSet
    {
        public List<string> eventSet = new List<string>();
        public string[] events = new string[0];
        public virtual void PushEvent(string @event)
        {
            eventSet.Add(@event);
            events = eventSet.ToArray(events);
        }

        public virtual void PopEvent(string @event)
        {
            int index = eventSet.LastIndexOf(@event);
            if (index >= 0)
                eventSet.Remove(index);
            events = eventSet.ToArray(events);
        }

        public virtual String[] GetEvents()
        {
            return events;
        }
    }

    public class ZIndex : Iterable<HashSet<StyleGroup>>
    {
        public List<HashSet<StyleGroup>> zIndex = new List<HashSet<StyleGroup>>();
        public HashMap<string, int> reverseZIndex = new HashMap<string, int>();
        public ZIndex()
        {
            InitZIndex();
        }

        protected virtual void InitZIndex()
        {
            zIndex.EnsureCapacity(256);
            for (int i = 0; i < 256; i++)
                zIndex.Add(null);
        }

        protected virtual IEnumerator<HashSet<StyleGroup>> GetIterator()
        {
            return new ZIndexIterator();
        }

        public virtual IEnumerator<HashSet<StyleGroup>> Iterator()
        {
            return GetIterator();
        }

        protected virtual void GroupAdded(StyleGroup group)
        {
            int z = ConvertZ(group.GetZIndex());
            if (zIndex[z] == null)
                zIndex[z] = new HashSet<StyleGroup>();
            zIndex[z].Add(group);
            reverseZIndex.Put(group.GetId(), z);
        }

        protected virtual void GroupChanged(StyleGroup group)
        {
            int oldZ = reverseZIndex[group.GetId()];
            int newZ = ConvertZ(group.GetZIndex());
            if (oldZ != newZ)
            {
                HashSet<StyleGroup> map = zIndex[oldZ];
                if (map != null)
                {
                    map.Remove(group);
                    reverseZIndex.Remove(group.GetId());
                    if (map.IsEmpty())
                        zIndex[oldZ] = null;
                }

                GroupAdded(group);
            }
        }

        protected virtual void GroupRemoved(StyleGroup group)
        {
            int z = ConvertZ(group.GetZIndex());
            HashSet<StyleGroup> map = zIndex[z];
            if (map != null)
            {
                map.Remove(group);
                reverseZIndex.Remove(group.GetId());
                if (map.IsEmpty())
                    zIndex[z] = null;
            }
            else
            {
                throw new Exception("Inconsistency in Z-index");
            }
        }

        public virtual void Clear()
        {
            zIndex.Clear();
            reverseZIndex.Clear();
            InitZIndex();
        }

        protected virtual int ConvertZ(int z)
        {
            z += 127;
            if (z < 0)
                z = 0;
            else if (z > 255)
                z = 255;
            return z;
        }

        public virtual string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("Z index :%n"));
            for (int i = 0; i < 256; i++)
            {
                if (zIndex[i] != null)
                {
                    sb.Append(String.Format("    * %d -> ", i - 127));
                    HashSet<StyleGroup> map = zIndex[i];
                    foreach (StyleGroup g in map)
                        sb.Append(String.Format("%s ", g.GetId()));
                    sb.Append(String.Format("%n"));
                }
            }

            return sb.ToString();
        }

        public class ZIndexIterator : IEnumerator<HashSet<StyleGroup>>
        {
            public int index = 0;
            public ZIndexIterator()
            {
                ZapUntilACell();
            }

            protected virtual void ZapUntilACell()
            {
                while (index < 256 && zIndex[index] == null)
                    index++;
            }

            public virtual bool HasNext()
            {
                return (index < 256);
            }

            public virtual HashSet<StyleGroup> Next()
            {
                if (HasNext())
                {
                    HashSet<StyleGroup> cell = zIndex[index];
                    index++;
                    ZapUntilACell();
                    return cell;
                }

                return null;
            }

            public virtual void Remove()
            {
                throw new Exception("This iterator does not support removal.");
            }
        }
    }

    public class ShadowSet : Iterable<StyleGroup>
    {
        protected HashSet<StyleGroup> shadowSet = new HashSet<StyleGroup>();
        protected virtual IEnumerator<StyleGroup> GetIterator()
        {
            return shadowSet.Iterator();
        }

        public virtual IEnumerator<StyleGroup> Iterator()
        {
            return GetIterator();
        }

        protected virtual void GroupAdded(StyleGroup group)
        {
            if (group.GetShadowMode() != ShadowMode.NONE)
                shadowSet.Add(group);
        }

        protected virtual void GroupChanged(StyleGroup group)
        {
            if (group.GetShadowMode() == ShadowMode.NONE)
                shadowSet.Remove(group);
            else
                shadowSet.Add(group);
        }

        protected virtual void GroupRemoved(StyleGroup group)
        {
            shadowSet.Remove(group);
        }

        protected virtual void Clear()
        {
            shadowSet.Clear();
        }
    }

    protected class ElementIterator<E> : IEnumerator<E> where E : Element
    {
        protected Dictionary<string, string> elt2grp;
        protected IEnumerator<string> elts;
        public ElementIterator(Dictionary<string, string> elements2groups)
        {
            elt2grp = elements2groups;
            elts = elements2groups.KeySet().Iterator();
        }

        public virtual bool HasNext()
        {
            return elts.HasNext();
        }

        public virtual E Next()
        {
            string eid = elts.Next();
            string gid = elt2grp[eid];
            StyleGroup grp = groups[gid];
            return (E)grp.GetElement(eid);
        }

        public virtual void Remove()
        {
            throw new Exception("remove not implemented in this iterator");
        }
    }

    protected class NodeSet : Iterable<Node>
    {
        public virtual IEnumerator<Node> Iterator()
        {
            return (IEnumerator<Node>)GetNodeIterator();
        }
    }

    protected class EdgeSet : Iterable<Edge>
    {
        public virtual IEnumerator<Edge> Iterator()
        {
            return (IEnumerator<Edge>)GetEdgeIterator();
        }
    }

    protected class SpriteSet : Iterable<GraphicSprite>
    {
        public virtual IEnumerator<GraphicSprite> Iterator()
        {
            return (IEnumerator<GraphicSprite>)GetSpriteIterator();
        }
    }

    protected class GraphSet : Iterable<GraphicGraph>
    {
        public virtual IEnumerator<GraphicGraph> Iterator()
        {
            return (IEnumerator<GraphicGraph>)GetGraphIterator();
        }
    }
}
