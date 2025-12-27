using Java.Util;
using gs_core.Graphstream.Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using static gs_core.Graphstream.Graph.Implementations.AttributeChangeEvent;
using static gs_core.Graphstream.Graph.Implementations.ElementType;
using static gs_core.Graphstream.Graph.Implementations.EventType;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Gs_Core.Graphstream.Graph.Implementations
{
    public abstract class OneAttributeElement : Element
    {
        protected string id;
        object attribute = null;
        public OneAttributeElement(string id)
        {
            this.id = id;
        }

        public virtual string GetId()
        {
            return id;
        }

        public virtual object GetAttribute(string key)
        {
            return attribute;
        }

        public virtual object GetFirstAttributeOf(params string[] keys)
        {
            return attribute;
        }

        public virtual T GetAttribute<T>(string key, Class<T> clazz)
        {
            return (T)attribute;
        }

        public virtual T GetFirstAttributeOf<T>(Class<T> clazz, params string[] keys)
        {
            return (T)attribute;
        }

        public virtual CharSequence GetLabel(string key)
        {
            if (attribute != null && attribute is CharSequence)
                return (CharSequence)attribute;
            return null;
        }

        public virtual double GetNumber(string key)
        {
            if (attribute != null && attribute is Number)
                return ((Number)attribute).DoubleValue();
            return Double.NaN;
        }

        public virtual List<TWildcardTodoNumber> GetVector(string key)
        {
            if (attribute != null && attribute is List)
                return ((List<TWildcardTodoNumber>)attribute);
            return null;
        }

        public virtual bool HasAttribute(string key)
        {
            return true;
        }

        public virtual bool HasAttribute(string key, Class<TWildcardTodo> clazz)
        {
            if (attribute != null)
                return (clazz.IsInstance(attribute));
            return false;
        }

        public virtual bool HasLabel(string key)
        {
            if (attribute != null)
                return (attribute is CharSequence);
            return false;
        }

        public virtual bool HasNumber(string key)
        {
            if (attribute != null)
                return (attribute is Number);
            return false;
        }

        public virtual bool HasVector(string key)
        {
            if (attribute != null && attribute is List<TWildcardTodo>)
                return true;
            return false;
        }

        public virtual IEnumerator<string> GetAttributeKeyIterator()
        {
            return null;
        }

        public virtual Dictionary<string, object> GetAttributeMap()
        {
            return null;
        }

        public virtual string ToString()
        {
            return id;
        }

        public virtual void ClearAttributes()
        {
            attribute = null;
        }

        public virtual void AddAttribute(string attribute, object value)
        {
            this.attribute = value;
        }

        public virtual void ChangeAttribute(string attribute, object value)
        {
            AddAttribute(attribute, value);
        }

        public virtual void SetAttributes(Dictionary<string, object> attributes)
        {
            if (attributes.Count >= 1)
                AddAttribute("", attributes[(attributes.KeySet().ToArray()[0])]);
        }

        public virtual void RemoveAttribute(string attribute)
        {
            this.attribute = null;
        }

        public enum AttributeChangeEvent
        {
            ADD,
            CHANGE,
            REMOVE
        }

        protected abstract void AttributeChanged(string sourceId, long timeId, string attribute, AttributeChangeEvent @event, object oldValue, object newValue);
    }



}

