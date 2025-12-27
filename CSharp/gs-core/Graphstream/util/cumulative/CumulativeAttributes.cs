using Java.Util;
using Gs_Core.Graphstream.Util.Cumulative.CumulativeSpells;
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

public class CumulativeAttributes
{
    bool nullAttributesAreErrors;
    HashMap<string, CumulativeSpells> data;
    double date;
    public CumulativeAttributes(double date)
    {
        data = new HashMap<string, CumulativeSpells>();
    }

    public virtual object Get(string key)
    {
        CumulativeSpells o = data[key];
        if (o != null)
        {
            Spell s = o.GetCurrentSpell();
            return s == null ? null : s.GetAttachedData();
        }

        return null;
    }

    public virtual object GetAny(string key)
    {
        CumulativeSpells o = data[key];
        if (o != null)
        {
            Spell s = o.GetSpell(0);
            return s == null ? null : s.GetAttachedData();
        }

        return null;
    }

    public virtual Iterable<string> GetAttributes()
    {
        return data.KeySet();
    }

    public virtual Iterable<Spell> GetAttributeSpells(string key)
    {
        CumulativeSpells o = data[key];
        if (o != null)
            return Collections.UnmodifiableList(o.spells);
        return Collections.EMPTY_LIST;
    }

    public virtual int GetAttributesCount()
    {
        return data.Count;
    }

    public virtual void Set(string key, object value)
    {
        CumulativeSpells spells = data[key];
        if (spells == null)
        {
            spells = new CumulativeSpells();
            data.Put(key, spells);
        }

        Spell s = spells.CloseSpell();
        if (s != null)
            s.SetEndOpen(true);
        s = spells.StartSpell(date);
        s.SetAttachedData(value);
    }

    public virtual void Remove(string key)
    {
        CumulativeSpells spells = data[key];
        if (spells == null)
            return;
        spells.CloseSpell();
    }

    public virtual void Remove()
    {
        foreach (CumulativeSpells spells in data.Values())
            spells.CloseSpell();
    }

    public virtual void UpdateDate(double date)
    {
        this.date = date;
        foreach (CumulativeSpells spells in data.Values())
            spells.UpdateCurrentSpell(date);
    }

    public virtual string ToString()
    {
        StringBuilder buffer = new StringBuilder();
        buffer.Append("(");
        foreach (string key in data.KeySet())
        {
            buffer.Append(key).Append(":").Append(data[key]);
        }

        buffer.Append(")");
        return buffer.ToString();
    }
}
