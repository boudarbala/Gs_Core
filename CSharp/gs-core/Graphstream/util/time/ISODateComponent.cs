using Java.Text;
using Java.Util;
using Java.Util.Regex;
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

public abstract class ISODateComponent
{
    protected readonly string directive;
    protected readonly string replace;
    public ISODateComponent(string directive, string replace)
    {
        this.directive = directive;
        this.replace = replace;
    }

    public virtual string GetDirective()
    {
        return directive;
    }

    public virtual bool IsAlias()
    {
        return false;
    }

    public virtual string GetReplacement()
    {
        return replace;
    }

    public abstract void Set(string value, Calendar calendar);
    public abstract string Get(Calendar calendar);
    public class AliasComponent : ISODateComponent
    {
        public AliasComponent(string shortcut, string replace) : base(shortcut, replace)
        {
        }

        public virtual bool IsAlias()
        {
            return true;
        }

        public virtual void Set(string value, Calendar calendar)
        {
        }

        public virtual string Get(Calendar calendar)
        {
            return "";
        }
    }

    public class TextComponent : ISODateComponent
    {
        string unquoted;
        public TextComponent(string value) : base(null, Pattern.Quote(value))
        {
            unquoted = value;
        }

        public virtual void Set(string value, Calendar calendar)
        {
        }

        public virtual string Get(Calendar calendar)
        {
            return unquoted;
        }
    }

    public class FieldComponent : ISODateComponent
    {
        protected readonly int field;
        protected readonly int offset;
        protected readonly string format;
        public FieldComponent(string shortcut, string replace, int field, string format) : this(shortcut, replace, field, 0, format)
        {
        }

        public FieldComponent(string shortcut, string replace, int field, int offset, string format) : base(shortcut, replace)
        {
            this.field = field;
            this.offset = offset;
            this.format = format;
        }

        public virtual void Set(string value, Calendar calendar)
        {
            while (value.CharAt(0) == '0' && value.Length > 1)
                value = value.Substring(1);
            int val = Integer.ParseInt(value);
            calendar[field] = val + offset;
        }

        public virtual string Get(Calendar calendar)
        {
            return String.Format(format, calendar[field]);
        }
    }

    protected abstract class LocaleDependentComponent : ISODateComponent
    {
        protected Locale locale;
        protected DateFormatSymbols symbols;
        public LocaleDependentComponent(string shortcut, string replace) : this(shortcut, replace, Locale.GetDefault())
        {
        }

        public LocaleDependentComponent(string shortcut, string replace, Locale locale) : base(shortcut, replace)
        {
            this.locale = locale;
            this.symbols = DateFormatSymbols.GetInstance(locale);
        }
    }

    public class AMPMComponent : LocaleDependentComponent
    {
        public AMPMComponent() : base("%p", "AM|PM|am|pm")
        {
        }

        public virtual void Set(string value, Calendar calendar)
        {
            if (value.EqualsIgnoreCase(symbols.GetAmPmStrings()[Calendar.AM]))
                calendar[Calendar.AM_PM] = Calendar.AM;
            else if (value.EqualsIgnoreCase(symbols.GetAmPmStrings()[Calendar.PM]))
                calendar[Calendar.AM_PM] = Calendar.PM;
        }

        public virtual string Get(Calendar calendar)
        {
            return symbols.GetAmPmStrings()[calendar[Calendar.AM_PM]];
        }
    }

    public class UTCOffsetComponent : ISODateComponent
    {
        public UTCOffsetComponent() : base("%z", "(?:[-+]\\d{4}|Z)")
        {
        }

        public virtual void Set(string value, Calendar calendar)
        {
            if (value.Equals("Z"))
            {
                calendar.GetTimeZone().SetRawOffset(0);
            }
            else
            {
                string hs = value.Substring(1, 3);
                string ms = value.Substring(3, 5);
                if (hs.CharAt(0) == '0')
                    hs = hs.Substring(1);
                if (ms.CharAt(0) == '0')
                    ms = ms.Substring(1);
                int i = value.CharAt(0) == '+' ? 1 : -1;
                int h = Integer.ParseInt(hs);
                int m = Integer.ParseInt(ms);
                calendar.GetTimeZone().SetRawOffset(i * (h * 60 + m) * 60000);
            }
        }

        public virtual string Get(Calendar calendar)
        {
            int offset = calendar.GetTimeZone().GetRawOffset();
            string sign = "+";
            if (offset < 0)
            {
                sign = "-";
                offset = -offset;
            }

            offset /= 60000;
            int h = offset / 60;
            int m = offset % 60;
            return String.Format("%s%02d%02d", sign, h, m);
        }
    }

    public class EpochComponent : ISODateComponent
    {
        public EpochComponent() : base("%K", "\\d+")
        {
        }

        public virtual void Set(string value, Calendar calendar)
        {
            long e = Long.ParseLong(value);
            calendar.SetTimeInMillis(e);
        }

        public virtual string Get(Calendar calendar)
        {
            return String.Format("%d", calendar.GetTimeInMillis());
        }
    }

    public class NotImplementedComponent : ISODateComponent
    {
        public NotImplementedComponent(string shortcut, string replace) : base(shortcut, replace)
        {
        }

        public virtual void Set(string value, Calendar cal)
        {
            throw new Exception("not implemented component");
        }

        public virtual string Get(Calendar calendar)
        {
            throw new Exception("not implemented component");
        }
    }

    public static readonly ISODateComponent ABBREVIATED_WEEKDAY_NAME = new NotImplementedComponent("%a", "\\w+[.]");
    public static readonly ISODateComponent FULL_WEEKDAY_NAME = new NotImplementedComponent("%A", "\\w+");
    public static readonly ISODateComponent ABBREVIATED_MONTH_NAME = new NotImplementedComponent("%b", "\\w+[.]");
    public static readonly ISODateComponent FULL_MONTH_NAME = new NotImplementedComponent("%B", "\\w+");
    public static readonly ISODateComponent LOCALE_DATE_AND_TIME = new NotImplementedComponent("%c", null);
    public static readonly ISODateComponent CENTURY = new NotImplementedComponent("%C", "\\d\\d");
    public static readonly ISODateComponent DAY_OF_MONTH_2_DIGITS = new FieldComponent("%d", "[012]\\d|3[01]", Calendar.DAY_OF_MONTH, "%02d");
    public static readonly ISODateComponent DATE = new AliasComponent("%D", "%m/%d/%y");
    public static readonly ISODateComponent DAY_OF_MONTH = new FieldComponent("%e", "\\d|[12]\\d|3[01]", Calendar.DAY_OF_MONTH, "%2d");
    public static readonly ISODateComponent DATE_ISO8601 = new AliasComponent("%F", "%Y-%m-%d");
    public static readonly ISODateComponent WEEK_BASED_YEAR_2_DIGITS = new FieldComponent("%g", "\\d\\d", Calendar.YEAR, "%02d");
    public static readonly ISODateComponent WEEK_BASED_YEAR_4_DIGITS = new FieldComponent("%G", "\\d{4}", Calendar.YEAR, "%04d");
    public static readonly ISODateComponent ABBREVIATED_MONTH_NAME_ALIAS = new AliasComponent("%h", "%b");
    public static readonly ISODateComponent HOUR_OF_DAY = new FieldComponent("%H", "[01]\\d|2[0123]", Calendar.HOUR_OF_DAY, "%02d");
    public static readonly ISODateComponent HOUR = new FieldComponent("%I", "0\\d|1[012]", Calendar.HOUR, "%02d");
    public static readonly ISODateComponent DAY_OF_YEAR = new FieldComponent("%j", "[012]\\d\\d|3[0-5]\\d|36[0-6]", Calendar.DAY_OF_YEAR, "%03d");
    public static readonly ISODateComponent MILLISECOND = new FieldComponent("%k", "\\d{3}", Calendar.MILLISECOND, "%03d");
    public static readonly ISODateComponent EPOCH = new EpochComponent();
    public static readonly ISODateComponent MONTH = new FieldComponent("%m", "0[1-9]|1[012]", Calendar.MONTH, -1, "%02d");
    public static readonly ISODateComponent MINUTE = new FieldComponent("%M", "[0-5]\\d", Calendar.MINUTE, "%02d");
    public static readonly ISODateComponent NEW_LINE = new AliasComponent("%n", "\n");
    public static readonly ISODateComponent AM_PM = new AMPMComponent();
    public static readonly ISODateComponent LOCALE_CLOCK_TIME_12_HOUR = new NotImplementedComponent("%r", "");
    public static readonly ISODateComponent HOUR_AND_MINUTE = new AliasComponent("%R", "%H:%M");
    public static readonly ISODateComponent SECOND = new FieldComponent("%S", "[0-5]\\d|60", Calendar.SECOND, "%02d");
    public static readonly ISODateComponent TABULATION = new AliasComponent("%t", "\t");
    public static readonly ISODateComponent TIME_ISO8601 = new AliasComponent("%T", "%H:%M:%S");
    public static readonly ISODateComponent DAY_OF_WEEK_1_7 = new FieldComponent("%u", "[1-7]", Calendar.DAY_OF_WEEK, -1, "%1d");
    public static readonly ISODateComponent WEEK_OF_YEAR_FROM_SUNDAY = new FieldComponent("%U", "[0-4]\\d|5[0123]", Calendar.WEEK_OF_YEAR, 1, "%2d");
    public static readonly ISODateComponent WEEK_NUMBER_ISO8601 = new NotImplementedComponent("%V", "0[1-9]|[2-4]\\d|5[0123]");
    public static readonly ISODateComponent DAY_OF_WEEK_0_6 = new FieldComponent("%w", "[0-6]", Calendar.DAY_OF_WEEK, "%01d");
    public static readonly ISODateComponent WEEK_OF_YEAR_FROM_MONDAY = new FieldComponent("%W", "[0-4]\\d|5[0123]", Calendar.WEEK_OF_YEAR, "%02d");
    public static readonly ISODateComponent LOCALE_DATE_REPRESENTATION = new NotImplementedComponent("%x", "");
    public static readonly ISODateComponent LOCALE_TIME_REPRESENTATION = new NotImplementedComponent("%X", "");
    public static readonly ISODateComponent YEAR_2_DIGITS = new FieldComponent("%y", "\\d\\d", Calendar.YEAR, "%02d");
    public static readonly ISODateComponent YEAR_4_DIGITS = new FieldComponent("%Y", "\\d{4}", Calendar.YEAR, "%04d");
    public static readonly ISODateComponent UTC_OFFSET = new UTCOffsetComponent();
    public static readonly ISODateComponent LOCALE_TIME_ZONE_NAME = new NotImplementedComponent("%Z", "\\w*");
    public static readonly ISODateComponent PERCENT = new AliasComponent("%%", "%");
}
