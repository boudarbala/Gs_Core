using Java.Util;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static Org.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static Org.Graphstream.Graph.Implementations.ElementType;
using static Org.Graphstream.Graph.Implementations.EventType;

namespace Gs_Core.Graphstream.Graph.Implementations
{

    public abstract class AbstractElement : Element
    {
        public enum AttributeChangeEvent
        {
            ADD,
            CHANGE,
            REMOVE
        }

        protected readonly string id;
        private int index;
        protected Dictionary<string, object> attributes = null;
        protected List<string> attributesBeingRemoved = null;
        public AbstractElement(string id)
        {
            this.id = id;
        }

        public virtual string GetId()
        {
            return id;
        }

        public virtual int GetIndex()
        {
            return index;
        }

        protected virtual void SetIndex(int index)
        {
            this.index = index;
        }

        protected abstract void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue);
        public virtual object GetAttribute(string key)
        {
            if (attributes != null)
            {
                object value = attributes[key];
                if (value != null)
                    return value;
            }

            return null;
        }

        public virtual object GetFirstAttributeOf(params string[] keys)
        {
            object o = null;
            if (attributes != null)
            {
                foreach (string key in keys)
                {
                    o = attributes[key];
                    if (o != null)
                        return o;
                }
            }

            return o;
        }

        public virtual T GetAttribute<T>(string key, Class<T> clazz)
        {
            if (attributes != null)
            {
                object o = attributes[key];
                if (o != null && clazz.IsInstance(o))
                    return clazz.Cast(o);
            }

            return null;
        }

        public virtual T GetFirstAttributeOf<T>(Class<T> clazz, params string[] keys)
        {
            object o = null;
            if (attributes == null)
                return null;
            foreach (string key in keys)
            {
                o = attributes[key];
                if (o != null && clazz.IsInstance(o))
                    return clazz.Cast(o);
            }

            return null;
        }

        public virtual bool HasAttribute(string key)
        {
            return attributes != null && attributes.ContainsKey(key);
        }

        public virtual bool HasAttribute(string key, Class<TWildcardTodo> clazz)
        {
            if (attributes != null)
            {
                object o = attributes[key];
                if (o != null)
                    return (clazz.IsInstance(o));
            }

            return false;
        }

        public virtual Stream<string> AttributeKeys()
        {
            if (attributes == null)
                return Stream.Empty();
            return attributes.KeySet().Stream();
        }

        public virtual string ToString()
        {
            return id;
        }

        public virtual int GetAttributeCount()
        {
            if (attributes != null)
                return attributes.Count;
            return 0;
        }

        public virtual void ClearAttributes()
        {
            if (attributes != null)
            {
                foreach (Map.Entry<String, Object> entry in attributes.EntrySet())
                    AttributeChanged(AttributeChangeEvent.REMOVE, entry.GetKey(), entry.GetValue(), null);
                attributes.Clear();
            }
        }

        protected virtual void ClearAttributesWithNoEvent()
        {
            if (attributes != null)
                attributes.Clear();
        }

        public virtual void SetAttribute(string attribute, params object[] values)
        {
            if (attributes == null)
                attributes = new HashMap(1);
            object oldValue;
            object value;
            if (values == null)
                value = null;
            else if (values.Length == 0)
                value = true;
            else if (values.Length == 1)
                value = values[0];
            else
                value = values;
            AttributeChangeEvent event = AttributeChangeEvent.ADD;
        if (attributes.ContainsKey(attribute))
            @event = AttributeChangeEvent.CHANGE;
        oldValue = attributes.Put(attribute, value);
        AttributeChanged(@event, attribute, oldValue, value);
    }

    public virtual void RemoveAttribute(string attribute)
        {
            if (attributes != null)
            {
                if (attributesBeingRemoved == null)
                    attributesBeingRemoved = new List();
                if (attributes.ContainsKey(attribute) && !attributesBeingRemoved.Contains(attribute))
                {
                    attributesBeingRemoved.Add(attribute);
                    AttributeChanged(AttributeChangeEvent.REMOVE, attribute, attributes[attribute], null);
                    attributesBeingRemoved.Remove(attributesBeingRemoved.Count - 1);
                    attributes.Remove(attribute);
                }
            }
        }
    }


}

