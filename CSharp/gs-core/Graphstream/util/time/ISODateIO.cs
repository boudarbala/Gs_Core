using Java.Text;
using Java.Util;
using Java.Util.Regex;
using Gs_Core.Graphstream.Util.Time.ISODateComponent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Time.ElementType;
using static Org.Graphstream.Util.Time.EventType;
using static Org.Graphstream.Util.Time.AttributeChangeEvent;
using static Org.Graphstream.Util.Time.Mode;
using static Org.Graphstream.Util.Time.What;
using static Org.Graphstream.Util.Time.TimeFormat;
using static Org.Graphstream.Util.Time.OutputType;
using static Org.Graphstream.Util.Time.OutputPolicy;
using static Org.Graphstream.Util.Time.LayoutPolicy;
using static Org.Graphstream.Util.Time.Quality;
using static Org.Graphstream.Util.Time.Option;
using static Org.Graphstream.Util.Time.AttributeType;
using static Org.Graphstream.Util.Time.Balise;
using static Org.Graphstream.Util.Time.GEXFAttribute;
using static Org.Graphstream.Util.Time.METAAttribute;
using static Org.Graphstream.Util.Time.GRAPHAttribute;
using static Org.Graphstream.Util.Time.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Time.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Time.NODESAttribute;
using static Org.Graphstream.Util.Time.NODEAttribute;
using static Org.Graphstream.Util.Time.ATTVALUEAttribute;
using static Org.Graphstream.Util.Time.PARENTAttribute;
using static Org.Graphstream.Util.Time.EDGESAttribute;
using static Org.Graphstream.Util.Time.SPELLAttribute;
using static Org.Graphstream.Util.Time.COLORAttribute;
using static Org.Graphstream.Util.Time.POSITIONAttribute;
using static Org.Graphstream.Util.Time.SIZEAttribute;
using static Org.Graphstream.Util.Time.NODESHAPEAttribute;
using static Org.Graphstream.Util.Time.EDGEAttribute;
using static Org.Graphstream.Util.Time.THICKNESSAttribute;
using static Org.Graphstream.Util.Time.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Time.IDType;
using static Org.Graphstream.Util.Time.ModeType;
using static Org.Graphstream.Util.Time.WeightType;
using static Org.Graphstream.Util.Time.EdgeType;
using static Org.Graphstream.Util.Time.NodeShapeType;
using static Org.Graphstream.Util.Time.EdgeShapeType;
using static Org.Graphstream.Util.Time.ClassType;
using static Org.Graphstream.Util.Time.TimeFormatType;
using static Org.Graphstream.Util.Time.GPXAttribute;
using static Org.Graphstream.Util.Time.WPTAttribute;
using static Org.Graphstream.Util.Time.LINKAttribute;
using static Org.Graphstream.Util.Time.EMAILAttribute;
using static Org.Graphstream.Util.Time.PTAttribute;
using static Org.Graphstream.Util.Time.BOUNDSAttribute;
using static Org.Graphstream.Util.Time.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Time.FixType;
using static Org.Graphstream.Util.Time.GraphAttribute;
using static Org.Graphstream.Util.Time.LocatorAttribute;
using static Org.Graphstream.Util.Time.NodeAttribute;
using static Org.Graphstream.Util.Time.EdgeAttribute;
using static Org.Graphstream.Util.Time.DataAttribute;
using static Org.Graphstream.Util.Time.PortAttribute;
using static Org.Graphstream.Util.Time.EndPointAttribute;
using static Org.Graphstream.Util.Time.EndPointType;
using static Org.Graphstream.Util.Time.HyperEdgeAttribute;
using static Org.Graphstream.Util.Time.KeyAttribute;
using static Org.Graphstream.Util.Time.KeyDomain;
using static Org.Graphstream.Util.Time.KeyAttrType;
using static Org.Graphstream.Util.Time.GraphEvents;
using static Org.Graphstream.Util.Time.ThreadingModel;
using static Org.Graphstream.Util.Time.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Time;

public class ISODateIO
{
    private static readonly ISODateComponent[] KNOWN_COMPONENTS = new[]
    {
        ISODateComponent.ABBREVIATED_WEEKDAY_NAME,
        ISODateComponent.FULL_WEEKDAY_NAME,
        ISODateComponent.ABBREVIATED_MONTH_NAME,
        ISODateComponent.FULL_MONTH_NAME,
        ISODateComponent.LOCALE_DATE_AND_TIME,
        ISODateComponent.CENTURY,
        ISODateComponent.DAY_OF_MONTH_2_DIGITS,
        ISODateComponent.DATE,
        ISODateComponent.DAY_OF_MONTH,
        ISODateComponent.DATE_ISO8601,
        ISODateComponent.WEEK_BASED_YEAR_2_DIGITS,
        ISODateComponent.WEEK_BASED_YEAR_4_DIGITS,
        ISODateComponent.ABBREVIATED_MONTH_NAME_ALIAS,
        ISODateComponent.HOUR_OF_DAY,
        ISODateComponent.HOUR,
        ISODateComponent.DAY_OF_YEAR,
        ISODateComponent.MILLISECOND,
        ISODateComponent.EPOCH,
        ISODateComponent.MONTH,
        ISODateComponent.MINUTE,
        ISODateComponent.NEW_LINE,
        ISODateComponent.AM_PM,
        ISODateComponent.LOCALE_CLOCK_TIME_12_HOUR,
        ISODateComponent.HOUR_AND_MINUTE,
        ISODateComponent.SECOND,
        ISODateComponent.TABULATION,
        ISODateComponent.TIME_ISO8601,
        ISODateComponent.DAY_OF_WEEK_1_7,
        ISODateComponent.WEEK_OF_YEAR_FROM_SUNDAY,
        ISODateComponent.WEEK_NUMBER_ISO8601,
        ISODateComponent.DAY_OF_WEEK_0_6,
        ISODateComponent.WEEK_OF_YEAR_FROM_MONDAY,
        ISODateComponent.LOCALE_DATE_REPRESENTATION,
        ISODateComponent.LOCALE_TIME_REPRESENTATION,
        ISODateComponent.YEAR_2_DIGITS,
        ISODateComponent.YEAR_4_DIGITS,
        ISODateComponent.UTC_OFFSET,
        ISODateComponent.LOCALE_TIME_ZONE_NAME,
        ISODateComponent.PERCENT
    };
    protected LinkedList<ISODateComponent> components;
    protected Pattern pattern;
    public ISODateIO() : this("%K")
    {
    }

    public ISODateIO(string format)
    {
        SetFormat(format);
    }

    public virtual Pattern GetPattern()
    {
        return pattern;
    }

    protected virtual LinkedList<ISODateComponent> FindComponents(string format)
    {
        LinkedList<ISODateComponent> components = new LinkedList<ISODateComponent>();
        int offset = 0;
        while (offset < format.Length)
        {
            if (format.CharAt(offset) == '%')
            {
                bool found = false;
                for (int i = 0; !found && i < KNOWN_COMPONENTS.Length; i++)
                {
                    if (format.StartsWith(KNOWN_COMPONENTS[i].GetDirective(), offset))
                    {
                        found = true;
                        if (KNOWN_COMPONENTS[i].IsAlias())
                        {
                            LinkedList<ISODateComponent> sub = FindComponents(KNOWN_COMPONENTS[i].GetReplacement());
                            components.AddAll(sub);
                        }
                        else
                            components.AddLast(KNOWN_COMPONENTS[i]);
                        offset += KNOWN_COMPONENTS[i].GetDirective().Length;
                    }
                }

                if (!found)
                    throw new ParseException("unknown identifier", offset);
            }
            else
            {
                int from = offset;
                while (offset < format.Length && format.CharAt(offset) != '%')
                    offset++;
                components.AddLast(new TextComponent(format.Substring(from, offset)));
            }
        }

        return components;
    }

    protected virtual void BuildRegularExpression()
    {
        string pattern = "";
        for (int i = 0; i < components.Count; i++)
        {
            object c = components[i];
            string regexValue;
            if (c is ISODateComponent)
                regexValue = ((ISODateComponent)c).GetReplacement();
            else
                regexValue = c.ToString();
            pattern += "(" + regexValue + ")";
        }

        this.pattern = Pattern.Compile(pattern);
    }

    public virtual void SetFormat(string format)
    {
        components = FindComponents(format);
        BuildRegularExpression();
    }

    public virtual Calendar Parse(string time)
    {
        Calendar cal = Calendar.GetInstance();
        Matcher match = pattern.Matcher(time);
        if (match.Matches())
        {
            for (int i = 0; i < components.Count; i++)
                components[i][match.Group(i + 1)] = cal;
        }
        else
            return null;
        return cal;
    }

    public virtual string ToString(Calendar calendar)
    {
        StringBuffer buffer = new StringBuffer();
        for (int i = 0; i < components.Count; i++)
            buffer.Append(components[i][calendar]);
        return buffer.ToString();
    }
}
