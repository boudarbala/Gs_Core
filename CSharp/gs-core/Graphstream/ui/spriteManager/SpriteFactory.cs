using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.SpriteManager.ElementType;
using static Org.Graphstream.Ui.SpriteManager.EventType;
using static Org.Graphstream.Ui.SpriteManager.AttributeChangeEvent;
using static Org.Graphstream.Ui.SpriteManager.Mode;
using static Org.Graphstream.Ui.SpriteManager.What;
using static Org.Graphstream.Ui.SpriteManager.TimeFormat;
using static Org.Graphstream.Ui.SpriteManager.OutputType;
using static Org.Graphstream.Ui.SpriteManager.OutputPolicy;
using static Org.Graphstream.Ui.SpriteManager.LayoutPolicy;
using static Org.Graphstream.Ui.SpriteManager.Quality;
using static Org.Graphstream.Ui.SpriteManager.Option;
using static Org.Graphstream.Ui.SpriteManager.AttributeType;
using static Org.Graphstream.Ui.SpriteManager.Balise;
using static Org.Graphstream.Ui.SpriteManager.GEXFAttribute;
using static Org.Graphstream.Ui.SpriteManager.METAAttribute;
using static Org.Graphstream.Ui.SpriteManager.GRAPHAttribute;
using static Org.Graphstream.Ui.SpriteManager.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.SpriteManager.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.SpriteManager.NODESAttribute;
using static Org.Graphstream.Ui.SpriteManager.NODEAttribute;
using static Org.Graphstream.Ui.SpriteManager.ATTVALUEAttribute;
using static Org.Graphstream.Ui.SpriteManager.PARENTAttribute;
using static Org.Graphstream.Ui.SpriteManager.EDGESAttribute;
using static Org.Graphstream.Ui.SpriteManager.SPELLAttribute;
using static Org.Graphstream.Ui.SpriteManager.COLORAttribute;
using static Org.Graphstream.Ui.SpriteManager.POSITIONAttribute;
using static Org.Graphstream.Ui.SpriteManager.SIZEAttribute;
using static Org.Graphstream.Ui.SpriteManager.NODESHAPEAttribute;
using static Org.Graphstream.Ui.SpriteManager.EDGEAttribute;
using static Org.Graphstream.Ui.SpriteManager.THICKNESSAttribute;
using static Org.Graphstream.Ui.SpriteManager.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.SpriteManager.IDType;
using static Org.Graphstream.Ui.SpriteManager.ModeType;
using static Org.Graphstream.Ui.SpriteManager.WeightType;
using static Org.Graphstream.Ui.SpriteManager.EdgeType;
using static Org.Graphstream.Ui.SpriteManager.NodeShapeType;
using static Org.Graphstream.Ui.SpriteManager.EdgeShapeType;
using static Org.Graphstream.Ui.SpriteManager.ClassType;
using static Org.Graphstream.Ui.SpriteManager.TimeFormatType;
using static Org.Graphstream.Ui.SpriteManager.GPXAttribute;
using static Org.Graphstream.Ui.SpriteManager.WPTAttribute;
using static Org.Graphstream.Ui.SpriteManager.LINKAttribute;
using static Org.Graphstream.Ui.SpriteManager.EMAILAttribute;
using static Org.Graphstream.Ui.SpriteManager.PTAttribute;
using static Org.Graphstream.Ui.SpriteManager.BOUNDSAttribute;
using static Org.Graphstream.Ui.SpriteManager.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.SpriteManager.FixType;
using static Org.Graphstream.Ui.SpriteManager.GraphAttribute;
using static Org.Graphstream.Ui.SpriteManager.LocatorAttribute;
using static Org.Graphstream.Ui.SpriteManager.NodeAttribute;
using static Org.Graphstream.Ui.SpriteManager.EdgeAttribute;
using static Org.Graphstream.Ui.SpriteManager.DataAttribute;
using static Org.Graphstream.Ui.SpriteManager.PortAttribute;
using static Org.Graphstream.Ui.SpriteManager.EndPointAttribute;
using static Org.Graphstream.Ui.SpriteManager.EndPointType;
using static Org.Graphstream.Ui.SpriteManager.HyperEdgeAttribute;
using static Org.Graphstream.Ui.SpriteManager.KeyAttribute;
using static Org.Graphstream.Ui.SpriteManager.KeyDomain;
using static Org.Graphstream.Ui.SpriteManager.KeyAttrType;
using static Org.Graphstream.Ui.SpriteManager.GraphEvents;

namespace Gs_Core.Graphstream.Ui.SpriteManager;

public class SpriteFactory
{
    public virtual Sprite NewSprite(string identifier, SpriteManager manager, Values position)
    {
        if (position != null)
            return new Sprite(identifier, manager, position);
        return new Sprite(identifier, manager);
    }
}
