using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Set.ElementType;
using static Org.Graphstream.Util.Set.EventType;
using static Org.Graphstream.Util.Set.AttributeChangeEvent;
using static Org.Graphstream.Util.Set.Mode;
using static Org.Graphstream.Util.Set.What;
using static Org.Graphstream.Util.Set.TimeFormat;
using static Org.Graphstream.Util.Set.OutputType;
using static Org.Graphstream.Util.Set.OutputPolicy;
using static Org.Graphstream.Util.Set.LayoutPolicy;
using static Org.Graphstream.Util.Set.Quality;
using static Org.Graphstream.Util.Set.Option;
using static Org.Graphstream.Util.Set.AttributeType;
using static Org.Graphstream.Util.Set.Balise;
using static Org.Graphstream.Util.Set.GEXFAttribute;
using static Org.Graphstream.Util.Set.METAAttribute;
using static Org.Graphstream.Util.Set.GRAPHAttribute;
using static Org.Graphstream.Util.Set.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Set.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Set.NODESAttribute;
using static Org.Graphstream.Util.Set.NODEAttribute;
using static Org.Graphstream.Util.Set.ATTVALUEAttribute;
using static Org.Graphstream.Util.Set.PARENTAttribute;
using static Org.Graphstream.Util.Set.EDGESAttribute;
using static Org.Graphstream.Util.Set.SPELLAttribute;
using static Org.Graphstream.Util.Set.COLORAttribute;
using static Org.Graphstream.Util.Set.POSITIONAttribute;
using static Org.Graphstream.Util.Set.SIZEAttribute;
using static Org.Graphstream.Util.Set.NODESHAPEAttribute;
using static Org.Graphstream.Util.Set.EDGEAttribute;
using static Org.Graphstream.Util.Set.THICKNESSAttribute;
using static Org.Graphstream.Util.Set.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Set.IDType;
using static Org.Graphstream.Util.Set.ModeType;
using static Org.Graphstream.Util.Set.WeightType;
using static Org.Graphstream.Util.Set.EdgeType;
using static Org.Graphstream.Util.Set.NodeShapeType;
using static Org.Graphstream.Util.Set.EdgeShapeType;
using static Org.Graphstream.Util.Set.ClassType;
using static Org.Graphstream.Util.Set.TimeFormatType;
using static Org.Graphstream.Util.Set.GPXAttribute;
using static Org.Graphstream.Util.Set.WPTAttribute;
using static Org.Graphstream.Util.Set.LINKAttribute;
using static Org.Graphstream.Util.Set.EMAILAttribute;
using static Org.Graphstream.Util.Set.PTAttribute;
using static Org.Graphstream.Util.Set.BOUNDSAttribute;
using static Org.Graphstream.Util.Set.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Set.FixType;
using static Org.Graphstream.Util.Set.GraphAttribute;
using static Org.Graphstream.Util.Set.LocatorAttribute;
using static Org.Graphstream.Util.Set.NodeAttribute;
using static Org.Graphstream.Util.Set.EdgeAttribute;
using static Org.Graphstream.Util.Set.DataAttribute;
using static Org.Graphstream.Util.Set.PortAttribute;
using static Org.Graphstream.Util.Set.EndPointAttribute;
using static Org.Graphstream.Util.Set.EndPointType;
using static Org.Graphstream.Util.Set.HyperEdgeAttribute;
using static Org.Graphstream.Util.Set.KeyAttribute;
using static Org.Graphstream.Util.Set.KeyDomain;
using static Org.Graphstream.Util.Set.KeyAttrType;
using static Org.Graphstream.Util.Set.GraphEvents;
using static Org.Graphstream.Util.Set.ThreadingModel;
using static Org.Graphstream.Util.Set.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Set;

public class FixedArrayList<E> : Collection<E>, RandomAccess
{
    protected List<E> elements = new List<E>();
    protected List<int> freeIndices = new List<int>();
    protected int lastIndex = -1;
    public FixedArrayList()
    {
        elements = new List<E>();
        freeIndices = new List<int>(16);
    }

    public FixedArrayList(int capacity)
    {
        elements = new List<E>(capacity);
        freeIndices = new List<int>(16);
    }

    public virtual int Size()
    {
        return elements.Count - freeIndices.Count;
    }

    public virtual int RealSize()
    {
        return elements.Count;
    }

    public virtual bool IsEmpty()
    {
        return (Size() == 0);
    }

    public virtual E Get(int i)
    {
        E e = elements[i];
        if (e == null)
            throw new NoSuchElementException("no element at index " + i);
        return e;
    }

    public virtual E UnsafeGet(int i)
    {
        return elements[i];
    }

    public virtual bool Contains(object o)
    {
        int n = elements.Count;
        for (int i = 0; i < n; ++i)
        {
            E e = elements[i];
            if (e != null)
            {
                if (e == o)
                    return true;
                if (elements.Equals(o))
                    return true;
            }
        }

        return false;
    }

    public virtual bool ContainsAll(Collection<TWildcardTodo> c)
    {
        foreach (object o in c)
        {
            if (!Contains(o))
                return false;
        }

        return true;
    }

    public virtual bool Equals(object o)
    {
        if (o is FixedArrayList)
        {
            FixedArrayList<TWildcardTodoE> other = (FixedArrayList<TWildcardTodoE>)o;
            int n = Size();
            if (other.Count == n)
            {
                for (int i = 0; i < n; ++i)
                {
                    E e0 = elements[i];
                    E e1 = other.elements[i];
                    if (e0 != e1)
                    {
                        if (e0 == null && e1 != null)
                            return false;
                        if (e0 != null && e1 == null)
                            return false;
                        if (!e0.Equals(e1))
                            return false;
                    }
                }

                return true;
            }
        }

        return false;
    }

    public virtual java.util.Iterator<E> Iterator()
    {
        return new FixedArrayIterator();
    }

    public virtual int GetLastIndex()
    {
        return lastIndex;
    }

    public virtual int GetNextAddIndex()
    {
        int n = freeIndices.Count;
        if (n > 0)
            return freeIndices[n - 1];
        else
            return elements.Count;
    }

    public virtual Object[] ToArray()
    {
        int n = Size();
        int m = elements.Count;
        int j = 0;
        object[] a = new object[n];
        for (int i = 0; i < m; ++i)
        {
            E e = elements[i];
            if (e != null)
                a[j++] = e;
        }

        return a;
    }

    public virtual T[] ToArray<T>(T[] a)
    {
        throw new Exception("not implemented yet");
    }

    public virtual bool Add(E element)
    {
        if (element == null)
            throw new NullReferenceException("this array cannot contain null value");
        int n = freeIndices.Count;
        if (n > 0)
        {
            int i = freeIndices.Remove(n - 1);
            elements[i] = element;
            lastIndex = i;
        }
        else
        {
            elements.Add(element);
            lastIndex = elements.Count - 1;
        }

        return true;
    }

    public virtual bool AddAll(Collection<TWildcardTodoE> c)
    {
        java.util.Iterator<?extends E> k = c.Iterator();
        while (k.HasNext())
        {
            Add(k.Next());
        }

        return true;
    }

    public virtual E Remove(int i)
    {
        int n = elements.Count;
        if (i < 0 || i >= n)
            throw new ArrayIndexOutOfBoundsException("index " + i + " does not exist");
        if (n > 0)
        {
            if (elements[i] == null)
                throw new NullReferenceException("no element stored at index " + i);
            if (i == (n - 1))
            {
                return elements.Remove(i);
            }
            else
            {
                E e = elements[i];
                elements[i] = null;
                freeIndices.Add(i);
                return e;
            }
        }

        throw new ArrayIndexOutOfBoundsException("index " + i + " does not exist");
    }

    protected virtual void RemoveIt(int i)
    {
        Remove(i);
    }

    public virtual bool Remove(object e)
    {
        int n = elements.Count;
        for (int i = 0; i < n; ++i)
        {
            if (elements[i] == e)
            {
                elements.Remove(i);
                return true;
            }
        }

        return false;
    }

    public virtual bool RemoveAll(Collection<TWildcardTodo> c)
    {
        throw new NotSupportedException("not implemented yet");
    }

    public virtual bool RetainAll(Collection<TWildcardTodo> c)
    {
        throw new NotSupportedException("not implemented yet");
    }

    public virtual void Clear()
    {
        elements.Clear();
        freeIndices.Clear();
    }

    protected class FixedArrayIterator : IEnumerator<E>
    {
        int i;
        public FixedArrayIterator()
        {
            i = -1;
        }

        public virtual bool HasNext()
        {
            int n = elements.Count;
            for (int j = i + 1; j < n; ++j)
            {
                if (elements[j] != null)
                    return true;
            }

            return false;
        }

        public virtual E Next()
        {
            int n = elements.Count;
            for (int j = i + 1; j < n; ++j)
            {
                E e = elements[j];
                if (e != null)
                {
                    i = j;
                    return e;
                }
            }

            throw new NoSuchElementException("no more elements in iterator");
        }

        public virtual void Remove()
        {
            if (i >= 0 && i < elements.Count && elements[i] != null)
            {
                RemoveIt(i);
            }
            else
            {
                throw new InvalidOperationException("no such element");
            }
        }
    }
}
