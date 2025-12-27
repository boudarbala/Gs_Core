using Java.Util;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.GraphicGraph.GraphicElement;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
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

public class StyleGroup : Style, Iterable<Element>
{
    protected string id;
    protected List<Rule> rules = new List<Rule>();
    protected HashMap<string, Element> elements = new HashMap<string, Element>();
    protected StyleGroupSet.EventSet eventSet;
    protected HashMap<Element, ElementEvents> eventsFor;
    protected HashSet<Element> dynamicOnes;
    protected string[] curEvents;
    protected BulkElements bulkElements = new BulkElements();
    public HashMap<string, SwingElementRenderer> renderers;
    public StyleGroup(string identifier, Collection<Rule> rules, Element firstElement, StyleGroupSet.EventSet eventSet)
    {
        this.id = identifier;
        this.rules.AddAll(rules);
        this.elements.Put(firstElement.GetId(), firstElement);
        this.values = null;
        this.eventSet = eventSet;
        foreach (Rule rule in rules)
            rule.AddGroup(identifier);
    }

    public virtual string GetId()
    {
        return id;
    }

    public virtual Selector.Type GetType()
    {
        return rules[0].selector.type;
    }

    public virtual bool HasDynamicElements()
    {
        return (dynamicOnes != null && dynamicOnes.Count > 0);
    }

    public virtual bool HasEventElements()
    {
        return (eventsFor != null && eventsFor.Count > 0);
    }

    public virtual bool ElementHasEvents(Element element)
    {
        return (eventsFor != null && eventsFor.ContainsKey(element));
    }

    public virtual bool ElementIsDynamic(Element element)
    {
        return (dynamicOnes != null && dynamicOnes.Contains(element));
    }

    public override object GetValue(string property, params string[] events)
    {
        int n = rules.Count;
        if (events == null || events.Length == 0)
        {
            if (curEvents != null && curEvents.Length > 0)
            {
                events = curEvents;
            }
            else if (eventSet.events != null && eventSet.events.Length > 0)
            {
                events = eventSet.events;
            }
        }

        for (int i = 1; i < n; i++)
        {
            Style style = rules[i].GetStyle();
            if (style.HasValue(property, events))
                return style.GetValue(property, events);
        }

        return rules[0].GetStyle().GetValue(property, events);
    }

    public virtual bool IsEmpty()
    {
        return elements.IsEmpty();
    }

    public virtual bool Contains(string elementId)
    {
        return elements.ContainsKey(elementId);
    }

    public virtual bool Contains(Element element)
    {
        return elements.ContainsKey(element.GetId());
    }

    public virtual Element GetElement(string id)
    {
        return elements[id];
    }

    public virtual int GetElementCount()
    {
        return elements.Count;
    }

    public virtual IEnumerator<TWildcardTodoElement> GetElementIterator()
    {
        return elements.Values().Iterator();
    }

    public virtual Iterable<TWildcardTodoElement> Elements()
    {
        return elements.Values();
    }

    public virtual Iterable<TWildcardTodoElement> BulkElements()
    {
        return bulkElements;
    }

    public virtual Iterable<ElementEvents> ElementsEvents()
    {
        return eventsFor.Values();
    }

    public virtual Iterable<Element> DynamicElements()
    {
        return dynamicOnes;
    }

    public virtual IEnumerator<Element> Iterator()
    {
        return elements.Values().Iterator();
    }

    public virtual SwingElementRenderer GetRenderer(string id)
    {
        if (renderers != null)
            return renderers[id];
        return null;
    }

    public virtual ElementEvents GetEventsFor(Element element)
    {
        if (eventsFor != null)
            return eventsFor[element];
        return null;
    }

    public virtual bool IsElementDynamic(Element element)
    {
        if (dynamicOnes != null)
            return dynamicOnes.Contains(element);
        return false;
    }

    public virtual void AddElement(Element element)
    {
        elements.Put(element.GetId(), element);
    }

    public virtual Element RemoveElement(Element element)
    {
        if (eventsFor != null && eventsFor.ContainsKey(element))
            eventsFor.Remove(element);
        if (dynamicOnes != null && dynamicOnes.Contains(element))
            dynamicOnes.Remove(element);
        return elements.Remove(element.GetId());
    }

    protected virtual void PushEventFor(Element element, string @event)
    {
        if (elements.ContainsKey(element.GetId()))
        {
            if (eventsFor == null)
                eventsFor = new HashMap<Element, ElementEvents>();
            ElementEvents evs = eventsFor[element];
            if (evs == null)
            {
                evs = new ElementEvents(element, this, @event);
                eventsFor.Put(element, evs);
            }
            else
            {
                evs.PushEvent(@event);
            }
        }
    }

    protected virtual void PopEventFor(Element element, string @event)
    {
        if (elements.ContainsKey(element.GetId()))
        {
            if (eventsFor != null)
            {
                ElementEvents evs = eventsFor[element];
                if (evs != null)
                {
                    evs.PopEvent(@event);
                    if (evs.EventCount() == 0)
                        eventsFor.Remove(element);
                }

                if (eventsFor.IsEmpty())
                    eventsFor = null;
            }
        }
    }

    public virtual void ActivateEventsFor(Element element)
    {
        ElementEvents evs = eventsFor[element];
        if (evs != null && curEvents == null)
            curEvents = evs.Events();
    }

    public virtual void DeactivateEvents()
    {
        curEvents = null;
    }

    protected virtual void PushElementAsDynamic(Element element)
    {
        if (dynamicOnes == null)
            dynamicOnes = new HashSet<Element>();
        dynamicOnes.Add(element);
    }

    protected virtual void PopElementAsDynamic(Element element)
    {
        dynamicOnes.Remove(element);
        if (dynamicOnes.IsEmpty())
            dynamicOnes = null;
    }

    public virtual void Release()
    {
        foreach (Rule rule in rules)
            rule.RemoveGroup(id);
        elements.Clear();
    }

    public override void SetValue(string property, object value)
    {
        throw new Exception("you cannot change the values of a style group.");
    }

    public virtual void AddRenderer(string id, SwingElementRenderer renderer)
    {
        if (renderers == null)
            renderers = new HashMap<string, SwingElementRenderer>();
        renderers.Put(id, renderer);
    }

    public virtual SwingElementRenderer RemoveRenderer(string id)
    {
        return renderers.Remove(id);
    }

    public override string ToString()
    {
        return ToString(-1);
    }

    public override string ToString(int level)
    {
        StringBuilder builder = new StringBuilder();
        string prefix = "";
        string sprefix = "    ";
        for (int i = 0; i < level; i++)
            prefix += sprefix;
        builder.Append(String.Format("%s%s%n", prefix, id));
        builder.Append(String.Format("%s%sContains : ", prefix, sprefix));
        foreach (Element element in elements.Values())
        {
            builder.Append(String.Format("%s ", element.GetId()));
        }

        builder.Append(String.Format("%n%s%sStyle : ", prefix, sprefix));
        foreach (Rule rule in rules)
        {
            builder.Append(String.Format("%s ", rule.selector.ToString()));
        }

        builder.Append(String.Format("%n"));
        return builder.ToString();
    }

    public class ElementEvents
    {
        protected string[] events;
        protected Element element;
        protected StyleGroup group;
        protected ElementEvents(Element element, StyleGroup group, string @event)
        {
            this.element = element;
            this.group = group;
            this.events = new string[1];
            events[0] = @event;
        }

        public virtual Element GetElement()
        {
            return element;
        }

        public virtual int EventCount()
        {
            if (events == null)
                return 0;
            return events.Length;
        }

        public virtual String[] Events()
        {
            return events;
        }

        public virtual void Activate()
        {
            group.ActivateEventsFor(element);
        }

        public virtual void Deactivate()
        {
            group.DeactivateEvents();
        }

        protected virtual void PushEvent(string @event)
        {
            int n = events.Length + 1;
            string[] e = new string[n];
            bool found = false;
            for (int i = 0; i < events.Length; i++)
            {
                if (!events[i].Equals(@event))
                    e[i] = events[i];
                else
                    found = true;
            }

            e[events.Length] = @event;
            if (!found)
                events = e;
        }

        protected virtual void PopEvent(string @event)
        {
            if (events.Length > 1)
            {
                string[] e = new string[events.Length - 1];
                bool found = false;
                for (int i = 0, j = 0; i < events.Length; i++)
                {
                    if (!events[i].Equals(@event))
                    {
                        if (j < e.Length)
                        {
                            e[j++] = events[i];
                        }
                    }
                    else
                    {
                        found = true;
                    }
                }

                if (found)
                    events = e;
            }
            else
            {
                if (events[0].Equals(@event))
                {
                    events = null;
                }
            }
        }

        public virtual string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("%s events {", element.GetId()));
            foreach (string event in events)
                builder.Append(String.Format(" %s", @event));
            builder.Append(" }");
            return builder.ToString();
        }
    }

    protected class BulkElements : Iterable<Element>
    {
        public virtual IEnumerator<Element> Iterator()
        {
            return new BulkIterator(elements.Values().Iterator());
        }
    }

    protected class BulkIterator : IEnumerator<Element>
    {
        protected IEnumerator<Element> iterator;
        Element next;
        public BulkIterator(IEnumerator<Element> iterator)
        {
            this.iterator = iterator;
            bool loop = true;
            while (loop && iterator.HasNext())
            {
                next = iterator.Next();
                if (!ElementHasEvents(next) && !ElementIsDynamic(next))
                    loop = false;
                else
                    next = null;
            }
        }

        public virtual bool HasNext()
        {
            return (next != null);
        }

        public virtual Element Next()
        {
            Element e = next;
            bool loop = true;
            next = null;
            while (loop && iterator.HasNext())
            {
                next = iterator.Next();
                if (!ElementIsDynamic(next) && !ElementHasEvents(next))
                    loop = false;
                else
                    next = null;
            }

            return e;
        }

        public virtual void Remove()
        {
            throw new NotSupportedException("this iterator does not allows removing elements");
        }
    }
}
