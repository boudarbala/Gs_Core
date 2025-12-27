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

public class SourceTime
{
    protected string sourceId;
    protected long currentTimeId;
    protected SinkTime sinkTime;
    public SourceTime() : this(0)
    {
    }

    public SourceTime(long currentTimeId) : this(null, currentTimeId, null)
    {
    }

    public SourceTime(string sourceId) : this(sourceId, 0, null)
    {
    }

    public SourceTime(string sourceId, SinkTime sinkTime) : this(sourceId, 0, sinkTime)
    {
    }

    public SourceTime(string sourceId, long currentTimeId) : this(sourceId, currentTimeId, null)
    {
    }

    public SourceTime(string sourceId, long currentTimeId, SinkTime sinkTime)
    {
        this.sourceId = sourceId;
        this.currentTimeId = currentTimeId;
        this.sinkTime = sinkTime;
    }

    public virtual SinkTime GetSinkTime()
    {
        return sinkTime;
    }

    public virtual string GetSourceId()
    {
        return sourceId;
    }

    public virtual void SetSourceId(string sourceId)
    {
        this.sourceId = sourceId;
    }

    public virtual void SetSinkTime(SinkTime st)
    {
        this.sinkTime = st;
    }

    public virtual long NewEvent()
    {
        currentTimeId++;
        if (sinkTime != null)
            sinkTime.SetTimeFor(sourceId, currentTimeId);
        return currentTimeId;
    }
}
