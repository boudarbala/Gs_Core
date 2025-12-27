using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Cumulative.ElementType;
using static Org.Graphstream.Util.Cumulative.EventType;
using static Org.Graphstream.Util.Cumulative.AttributeChangeEvent;
using static Org.Graphstream.Util.Cumulative.Mode;
using static Org.Graphstream.Util.Cumulative.What;
using static Org.Graphstream.Util.Cumulative.TimeFormat;
using static Org.Graphstream.Util.Cumulative.OutputType;
using static Org.Graphstream.Util.Cumulative.OutputPolicy;
using static Org.Graphstream.Util.Cumulative.LayoutPolicy;
using static Org.Graphstream.Util.Cumulative.Quality;
using static Org.Graphstream.Util.Cumulative.Option;
using static Org.Graphstream.Util.Cumulative.AttributeType;
using static Org.Graphstream.Util.Cumulative.Balise;
using static Org.Graphstream.Util.Cumulative.GEXFAttribute;
using static Org.Graphstream.Util.Cumulative.METAAttribute;
using static Org.Graphstream.Util.Cumulative.GRAPHAttribute;
using static Org.Graphstream.Util.Cumulative.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Cumulative.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Cumulative.NODESAttribute;
using static Org.Graphstream.Util.Cumulative.NODEAttribute;
using static Org.Graphstream.Util.Cumulative.ATTVALUEAttribute;
using static Org.Graphstream.Util.Cumulative.PARENTAttribute;
using static Org.Graphstream.Util.Cumulative.EDGESAttribute;
using static Org.Graphstream.Util.Cumulative.SPELLAttribute;
using static Org.Graphstream.Util.Cumulative.COLORAttribute;
using static Org.Graphstream.Util.Cumulative.POSITIONAttribute;
using static Org.Graphstream.Util.Cumulative.SIZEAttribute;
using static Org.Graphstream.Util.Cumulative.NODESHAPEAttribute;
using static Org.Graphstream.Util.Cumulative.EDGEAttribute;
using static Org.Graphstream.Util.Cumulative.THICKNESSAttribute;
using static Org.Graphstream.Util.Cumulative.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Cumulative.IDType;
using static Org.Graphstream.Util.Cumulative.ModeType;
using static Org.Graphstream.Util.Cumulative.WeightType;
using static Org.Graphstream.Util.Cumulative.EdgeType;
using static Org.Graphstream.Util.Cumulative.NodeShapeType;
using static Org.Graphstream.Util.Cumulative.EdgeShapeType;
using static Org.Graphstream.Util.Cumulative.ClassType;
using static Org.Graphstream.Util.Cumulative.TimeFormatType;
using static Org.Graphstream.Util.Cumulative.GPXAttribute;
using static Org.Graphstream.Util.Cumulative.WPTAttribute;
using static Org.Graphstream.Util.Cumulative.LINKAttribute;
using static Org.Graphstream.Util.Cumulative.EMAILAttribute;
using static Org.Graphstream.Util.Cumulative.PTAttribute;
using static Org.Graphstream.Util.Cumulative.BOUNDSAttribute;
using static Org.Graphstream.Util.Cumulative.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Cumulative.FixType;
using static Org.Graphstream.Util.Cumulative.GraphAttribute;
using static Org.Graphstream.Util.Cumulative.LocatorAttribute;
using static Org.Graphstream.Util.Cumulative.NodeAttribute;
using static Org.Graphstream.Util.Cumulative.EdgeAttribute;
using static Org.Graphstream.Util.Cumulative.DataAttribute;
using static Org.Graphstream.Util.Cumulative.PortAttribute;
using static Org.Graphstream.Util.Cumulative.EndPointAttribute;
using static Org.Graphstream.Util.Cumulative.EndPointType;
using static Org.Graphstream.Util.Cumulative.HyperEdgeAttribute;
using static Org.Graphstream.Util.Cumulative.KeyAttribute;
using static Org.Graphstream.Util.Cumulative.KeyDomain;
using static Org.Graphstream.Util.Cumulative.KeyAttrType;
using static Org.Graphstream.Util.Cumulative.GraphEvents;
using static Org.Graphstream.Util.Cumulative.ThreadingModel;
using static Org.Graphstream.Util.Cumulative.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Cumulative;

public class CumulativeSpells
{
    public class Spell
    {
        private double start;
        private double end;
        private bool startOpen;
        private bool endOpen;
        private bool closed;
        private object data;
        public Spell(double start, bool startOpen, double end, bool endOpen)
        {
            this.start = start;
            this.startOpen = startOpen;
            this.end = end;
            this.endOpen = endOpen;
            this.closed = false;
        }

        public Spell(double start, double end) : this(start, false, end, false)
        {
        }

        public Spell(double start) : this(start, false, start, false)
        {
        }

        public virtual double GetStartDate()
        {
            return start;
        }

        public virtual double GetEndDate()
        {
            return end;
        }

        public virtual bool IsStartOpen()
        {
            return startOpen;
        }

        public virtual bool IsEndOpen()
        {
            return endOpen;
        }

        public virtual bool IsStarted()
        {
            return !Double.IsNaN(start);
        }

        public virtual bool IsEnded()
        {
            return closed;
        }

        public virtual void SetStartOpen(bool open)
        {
            startOpen = open;
        }

        public virtual void SetEndOpen(bool open)
        {
            endOpen = open;
        }

        public virtual object GetAttachedData()
        {
            return data;
        }

        public virtual void SetAttachedData(object data)
        {
            this.data = data;
        }

        public virtual string ToString()
        {
            string str = "";
            if (IsStarted())
            {
                str += IsStartOpen() ? "]" : "[";
                str += start + "; ";
            }
            else
                str += "[...; ";
            if (IsEnded())
            {
                str += end;
                str += IsEndOpen() ? "[" : "]";
            }
            else
                str += "...]";
            return str;
        }
    }

    LinkedList<Spell> spells;
    double currentDate;
    public CumulativeSpells()
    {
        this.spells = new LinkedList<Spell>();
        currentDate = Double.NaN;
    }

    public virtual Spell StartSpell(double date)
    {
        Spell s = new Spell(date);
        spells.Add(s);
        return s;
    }

    public virtual void UpdateCurrentSpell(double date)
    {
        if (spells.Count > 0 && !Double.IsNaN(currentDate))
        {
            Spell s = spells.GetLast();
            if (!s.closed)
                s.end = currentDate;
        }

        currentDate = date;
    }

    public virtual Spell CloseSpell()
    {
        if (spells.Count > 0)
        {
            Spell s = spells.GetLast();
            if (!s.closed)
            {
                s.closed = true;
                return s;
            }
        }

        return null;
    }

    public virtual Spell GetCurrentSpell()
    {
        Spell s = spells.GetLast();
        if (s == null)
            return null;
        return s.closed ? null : s;
    }

    public virtual Spell GetSpell(int i)
    {
        return spells[i];
    }

    public virtual int GetSpellCount()
    {
        return spells.Count;
    }

    public virtual Spell GetOrCreateSpell(double date)
    {
        Spell s = GetCurrentSpell();
        if (s == null)
            s = StartSpell(date);
        return s;
    }

    public virtual bool IsEternal()
    {
        return spells.Count == 1 && !spells[0].IsStarted() && !spells[0].IsEnded();
    }

    public virtual string ToString()
    {
        StringBuilder buffer = new StringBuilder();
        buffer.Append("{");
        for (int i = 0; i < spells.Count; i++)
        {
            if (i > 0)
                buffer.Append(", ");
            buffer.Append(spells[i].ToString());
        }

        buffer.Append("}");
        return buffer.ToString();
    }
}
