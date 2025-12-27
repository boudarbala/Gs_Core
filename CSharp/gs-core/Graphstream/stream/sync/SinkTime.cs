using Java.Security;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.Sync.ElementType;
using static Org.Graphstream.Stream.Sync.EventType;
using static Org.Graphstream.Stream.Sync.AttributeChangeEvent;
using static Org.Graphstream.Stream.Sync.Mode;
using static Org.Graphstream.Stream.Sync.What;
using static Org.Graphstream.Stream.Sync.TimeFormat;
using static Org.Graphstream.Stream.Sync.OutputType;
using static Org.Graphstream.Stream.Sync.OutputPolicy;
using static Org.Graphstream.Stream.Sync.LayoutPolicy;
using static Org.Graphstream.Stream.Sync.Quality;
using static Org.Graphstream.Stream.Sync.Option;
using static Org.Graphstream.Stream.Sync.AttributeType;
using static Org.Graphstream.Stream.Sync.Balise;
using static Org.Graphstream.Stream.Sync.GEXFAttribute;
using static Org.Graphstream.Stream.Sync.METAAttribute;
using static Org.Graphstream.Stream.Sync.GRAPHAttribute;
using static Org.Graphstream.Stream.Sync.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.Sync.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.Sync.NODESAttribute;
using static Org.Graphstream.Stream.Sync.NODEAttribute;
using static Org.Graphstream.Stream.Sync.ATTVALUEAttribute;
using static Org.Graphstream.Stream.Sync.PARENTAttribute;
using static Org.Graphstream.Stream.Sync.EDGESAttribute;
using static Org.Graphstream.Stream.Sync.SPELLAttribute;
using static Org.Graphstream.Stream.Sync.COLORAttribute;
using static Org.Graphstream.Stream.Sync.POSITIONAttribute;
using static Org.Graphstream.Stream.Sync.SIZEAttribute;
using static Org.Graphstream.Stream.Sync.NODESHAPEAttribute;
using static Org.Graphstream.Stream.Sync.EDGEAttribute;
using static Org.Graphstream.Stream.Sync.THICKNESSAttribute;
using static Org.Graphstream.Stream.Sync.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.Sync.IDType;
using static Org.Graphstream.Stream.Sync.ModeType;
using static Org.Graphstream.Stream.Sync.WeightType;
using static Org.Graphstream.Stream.Sync.EdgeType;
using static Org.Graphstream.Stream.Sync.NodeShapeType;
using static Org.Graphstream.Stream.Sync.EdgeShapeType;
using static Org.Graphstream.Stream.Sync.ClassType;
using static Org.Graphstream.Stream.Sync.TimeFormatType;
using static Org.Graphstream.Stream.Sync.GPXAttribute;
using static Org.Graphstream.Stream.Sync.WPTAttribute;
using static Org.Graphstream.Stream.Sync.LINKAttribute;
using static Org.Graphstream.Stream.Sync.EMAILAttribute;
using static Org.Graphstream.Stream.Sync.PTAttribute;
using static Org.Graphstream.Stream.Sync.BOUNDSAttribute;
using static Org.Graphstream.Stream.Sync.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.Sync.FixType;
using static Org.Graphstream.Stream.Sync.GraphAttribute;
using static Org.Graphstream.Stream.Sync.LocatorAttribute;
using static Org.Graphstream.Stream.Sync.NodeAttribute;
using static Org.Graphstream.Stream.Sync.EdgeAttribute;
using static Org.Graphstream.Stream.Sync.DataAttribute;
using static Org.Graphstream.Stream.Sync.PortAttribute;
using static Org.Graphstream.Stream.Sync.EndPointAttribute;
using static Org.Graphstream.Stream.Sync.EndPointType;
using static Org.Graphstream.Stream.Sync.HyperEdgeAttribute;
using static Org.Graphstream.Stream.Sync.KeyAttribute;
using static Org.Graphstream.Stream.Sync.KeyDomain;
using static Org.Graphstream.Stream.Sync.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.Sync;

public class SinkTime
{
    public static readonly string SYNC_DISABLE_KEY = "org.graphstream.stream.sync.disable";
    protected static readonly bool disableSync;
    static SinkTime()
    {
        bool off;
        try
        {
            off = System.GetProperty(SYNC_DISABLE_KEY) != null;
        }
        catch (AccessControlException ex)
        {
            off = false;
        }

        disableSync = off;
    }

    protected HashMap<string, long> times = new HashMap<string, long>();
    protected virtual bool SetTimeFor(string sourceId, long timeId)
    {
        long knownTimeId = times[sourceId];
        if (knownTimeId == null)
        {
            times.Put(sourceId, timeId);
            return true;
        }
        else if (timeId > knownTimeId)
        {
            times.Put(sourceId, timeId);
            return true;
        }

        return false;
    }

    public virtual bool IsNewEvent(string sourceId, long timeId)
    {
        return disableSync || SetTimeFor(sourceId, timeId);
    }
}
