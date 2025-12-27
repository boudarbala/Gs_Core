using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.View.ElementType;
using static Org.Graphstream.Ui.View.EventType;
using static Org.Graphstream.Ui.View.AttributeChangeEvent;
using static Org.Graphstream.Ui.View.Mode;
using static Org.Graphstream.Ui.View.What;
using static Org.Graphstream.Ui.View.TimeFormat;
using static Org.Graphstream.Ui.View.OutputType;
using static Org.Graphstream.Ui.View.OutputPolicy;
using static Org.Graphstream.Ui.View.LayoutPolicy;
using static Org.Graphstream.Ui.View.Quality;
using static Org.Graphstream.Ui.View.Option;
using static Org.Graphstream.Ui.View.AttributeType;
using static Org.Graphstream.Ui.View.Balise;
using static Org.Graphstream.Ui.View.GEXFAttribute;
using static Org.Graphstream.Ui.View.METAAttribute;
using static Org.Graphstream.Ui.View.GRAPHAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.View.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.View.NODESAttribute;
using static Org.Graphstream.Ui.View.NODEAttribute;
using static Org.Graphstream.Ui.View.ATTVALUEAttribute;
using static Org.Graphstream.Ui.View.PARENTAttribute;
using static Org.Graphstream.Ui.View.EDGESAttribute;
using static Org.Graphstream.Ui.View.SPELLAttribute;
using static Org.Graphstream.Ui.View.COLORAttribute;
using static Org.Graphstream.Ui.View.POSITIONAttribute;
using static Org.Graphstream.Ui.View.SIZEAttribute;
using static Org.Graphstream.Ui.View.NODESHAPEAttribute;
using static Org.Graphstream.Ui.View.EDGEAttribute;
using static Org.Graphstream.Ui.View.THICKNESSAttribute;
using static Org.Graphstream.Ui.View.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.View.IDType;
using static Org.Graphstream.Ui.View.ModeType;
using static Org.Graphstream.Ui.View.WeightType;
using static Org.Graphstream.Ui.View.EdgeType;
using static Org.Graphstream.Ui.View.NodeShapeType;
using static Org.Graphstream.Ui.View.EdgeShapeType;
using static Org.Graphstream.Ui.View.ClassType;
using static Org.Graphstream.Ui.View.TimeFormatType;
using static Org.Graphstream.Ui.View.GPXAttribute;
using static Org.Graphstream.Ui.View.WPTAttribute;
using static Org.Graphstream.Ui.View.LINKAttribute;
using static Org.Graphstream.Ui.View.EMAILAttribute;
using static Org.Graphstream.Ui.View.PTAttribute;
using static Org.Graphstream.Ui.View.BOUNDSAttribute;
using static Org.Graphstream.Ui.View.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.View.FixType;
using static Org.Graphstream.Ui.View.GraphAttribute;
using static Org.Graphstream.Ui.View.LocatorAttribute;
using static Org.Graphstream.Ui.View.NodeAttribute;
using static Org.Graphstream.Ui.View.EdgeAttribute;
using static Org.Graphstream.Ui.View.DataAttribute;
using static Org.Graphstream.Ui.View.PortAttribute;
using static Org.Graphstream.Ui.View.EndPointAttribute;
using static Org.Graphstream.Ui.View.EndPointType;
using static Org.Graphstream.Ui.View.HyperEdgeAttribute;
using static Org.Graphstream.Ui.View.KeyAttribute;
using static Org.Graphstream.Ui.View.KeyDomain;
using static Org.Graphstream.Ui.View.KeyAttrType;
using static Org.Graphstream.Ui.View.GraphEvents;

namespace Gs_Core.Graphstream.Ui.View;

public class Selection
{
    public double x1, y1, x2, y2;
    public virtual bool Equals(object o)
    {
        if (this == o)
        {
            return true;
        }

        if (o == null || GetType() != o.GetType())
        {
            return false;
        }

        Selection selection = (Selection)o;
        if (Double.Compare(selection.x1, x1) != 0)
        {
            return false;
        }

        if (Double.Compare(selection.x2, x2) != 0)
        {
            return false;
        }

        if (Double.Compare(selection.y1, y1) != 0)
        {
            return false;
        }

        if (Double.Compare(selection.y2, y2) != 0)
        {
            return false;
        }

        return true;
    }

    public virtual int GetHashCode()
    {
        int result;
        long temp;
        temp = Double.DoubleToLongBits(x1);
        result = (int)(temp ^ (temp >>> 32));
        temp = Double.DoubleToLongBits(y1);
        result = 31 * result + (int)(temp ^ (temp >>> 32));
        temp = Double.DoubleToLongBits(x2);
        result = 31 * result + (int)(temp ^ (temp >>> 32));
        temp = Double.DoubleToLongBits(y2);
        result = 31 * result + (int)(temp ^ (temp >>> 32));
        return result;
    }
}
