using Java.Util;
using Java.Util.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Gs_Core.Graphstream.Graph;

public interface Element
{
    string GetId();
    int GetIndex();
    object GetAttribute(string key);
    object GetFirstAttributeOf(string keys);
    T GetAttribute<T>(string key, Class<T> clazz);
    T GetFirstAttributeOf<T>(Class<T> clazz, string keys);
    CharSequence GetLabel(string key)
    {
        return GetAttribute(key, typeof(CharSequence));
    }

    double GetNumber(string key)
    {
        object o = GetAttribute(key);
        if (o != null)
        {
            if (o is Number)
                return ((Number)o).DoubleValue();
            if (o is CharSequence)
            {
                try
                {
                    return Double.ParseDouble(o.ToString());
                }
                catch (NumberFormatException ignored)
                {
                }
            }
        }

        return Double.NaN;
    }

    IList<TWildcardTodoNumber> GetVector(string key)
    {
        object o = GetAttribute(key);
        if (o != null && o is IList)
        {
            IList<TWildcardTodo> l = (IList<TWildcardTodo>)o;
            if (l.Count > 0 && l[0] is Number)
                return (IList<TWildcardTodoNumber>)l;
        }

        return null;
    }

    Object[] GetArray(string key)
    {
        return GetAttribute(key, typeof(Object[]));
    }

    Map<?, ?> GetMap(string key)
    {
        object o = GetAttribute(key);
        if (o != null)
        {
            if (o is Map<?, ?>)
                return ((Map<?, ?>)o);
        }

        return null;
    }

    bool HasAttribute(string key);
    bool HasAttribute(string key, Class<TWildcardTodo> clazz);
    bool HasLabel(string key)
    {
        return GetAttribute(key, typeof(CharSequence)) != null;
    }

    bool HasNumber(string key)
    {
        if (GetAttribute(key, typeof(Number)) != null)
            return true;
        CharSequence o = GetAttribute(key, typeof(CharSequence));
        if (o != null)
        {
            try
            {
                Double.ParseDouble(o.ToString());
                return true;
            }
            catch (NumberFormatException ignored)
            {
            }
        }

        return false;
    }

    bool HasVector(string key)
    {
        IList<TWildcardTodo> o = GetAttribute(key, typeof(IList));
        if (o != null && o.Count > 0)
        {
            return o[0] is Number;
        }

        return false;
    }

    bool HasArray(string key)
    {
        return GetAttribute(key, typeof(Object[])) != null;
    }

    bool HasMap(string key)
    {
        object o = GetAttribute(key);
        return o != null && (o is Map<?, ?>);
    }

    Stream<string> AttributeKeys();
    void ClearAttributes();
    void SetAttribute(string attribute, object values);
    void SetAttributes(Dictionary<string, object> attributes)
    {
        attributes.ForEach(this.SetAttribute());
    }

    void RemoveAttribute(string attribute);
    int GetAttributeCount();
}
