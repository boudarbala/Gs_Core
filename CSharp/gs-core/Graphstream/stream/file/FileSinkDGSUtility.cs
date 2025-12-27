using Java.Awt;
using Java.Lang.Reflect;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkDGSUtility
{
    protected static string FormatStringForQuoting(string str)
    {
        return str.ReplaceAll("(^|[^\\\\])\"", "$1\\\\\"");
    }

    protected static string AttributeString(string key, object value, bool remove)
    {
        if (key == null || key.Length == 0)
            return null;
        if (remove)
        {
            return String.Format(" -\"%s\"", key);
        }
        else
        {
            if (value != null && value.GetType().IsArray())
                return String.Format(" \"%s\":%s", key, ArrayString(value));
            else
                return String.Format(" \"%s\":%s", key, ValueString(value));
        }
    }

    protected static string ArrayString(object value)
    {
        if (value != null && value.GetType().IsArray())
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            if (Array.GetLength(value) == 0)
                sb.Append("\"\"");
            else
                sb.Append(ArrayString(Array.Get(value, 0)));
            for (int i = 1; i < Array.GetLength(value); ++i)
                sb.Append(String.Format(",%s", ArrayString(Array.Get(value, i))));
            sb.Append("}");
            return sb.ToString();
        }
        else
        {
            return ValueString(value);
        }
    }

    protected static string ValueString(object value)
    {
        if (value == null)
            return "\"\"";
        if (value is CharSequence)
        {
            if (value is string)
                return String.Format("\"%s\"", FormatStringForQuoting((string)value));
            else
                return String.Format("\"%s\"", (CharSequence)value);
        }
        else if (value is Number)
        {
            Number nval = (Number)value;
            if (value is int || value is Short || value is Byte || value is long)
                return String.Format(Locale.US, "%d", nval.LongValue());
            else
                return String.Format(Locale.US, "%f", nval.DoubleValue());
        }
        else if (value is bool)
        {
            return String.Format(Locale.US, "%b", ((bool)value));
        }
        else if (value is Character)
        {
            return String.Format("\"%c\"", ((Character)value).CharValue());
        }
        else if (value is Object[])
        {
            object[] array = (Object[])value;
            int n = array.Length;
            StringBuffer sb = new StringBuffer();
            if (array.Length > 0)
                sb.Append(ValueString(array[0]));
            for (int i = 1; i < n; i++)
            {
                sb.Append(",");
                sb.Append(ValueString(array[i]));
            }

            return sb.ToString();
        }
        else if (value is HashMap<?, ?>)
        {
            HashMap<?, ?> hash = (HashMap<?, ?>)value;
            return HashToString(hash);
        }
        else if (value is Color)
        {
            Color c = (Color)value;
            return String.Format("#%02X%02X%02X%02X", c.GetRed(), c.GetGreen(), c.GetBlue(), c.GetAlpha());
        }
        else
        {
            return String.Format("\"%s\"", value.ToString());
        }
    }

    protected static string HashToString(HashMap<?, ?> hash)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[ ");
        foreach (object key in hash.KeySet())
        {
            sb.Append(AttributeString(key.ToString(), hash[key], false));
            sb.Append(",");
        }

        sb.Append(']');
        return sb.ToString();
    }
}
