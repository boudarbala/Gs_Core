using Java.Util;
using Javax.Xml.Stream;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Gexf.ElementType;
using static Org.Graphstream.Stream.File.Gexf.EventType;
using static Org.Graphstream.Stream.File.Gexf.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Gexf.Mode;
using static Org.Graphstream.Stream.File.Gexf.What;
using static Org.Graphstream.Stream.File.Gexf.TimeFormat;
using static Org.Graphstream.Stream.File.Gexf.OutputType;
using static Org.Graphstream.Stream.File.Gexf.OutputPolicy;
using static Org.Graphstream.Stream.File.Gexf.LayoutPolicy;
using static Org.Graphstream.Stream.File.Gexf.Quality;
using static Org.Graphstream.Stream.File.Gexf.Option;
using static Org.Graphstream.Stream.File.Gexf.AttributeType;
using static Org.Graphstream.Stream.File.Gexf.Balise;
using static Org.Graphstream.Stream.File.Gexf.GEXFAttribute;
using static Org.Graphstream.Stream.File.Gexf.METAAttribute;
using static Org.Graphstream.Stream.File.Gexf.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODESAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODEAttribute;
using static Org.Graphstream.Stream.File.Gexf.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Gexf.PARENTAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGESAttribute;
using static Org.Graphstream.Stream.File.Gexf.SPELLAttribute;
using static Org.Graphstream.Stream.File.Gexf.COLORAttribute;
using static Org.Graphstream.Stream.File.Gexf.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Gexf.SIZEAttribute;
using static Org.Graphstream.Stream.File.Gexf.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGEAttribute;
using static Org.Graphstream.Stream.File.Gexf.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Gexf.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gexf.IDType;
using static Org.Graphstream.Stream.File.Gexf.ModeType;
using static Org.Graphstream.Stream.File.Gexf.WeightType;
using static Org.Graphstream.Stream.File.Gexf.EdgeType;
using static Org.Graphstream.Stream.File.Gexf.NodeShapeType;
using static Org.Graphstream.Stream.File.Gexf.EdgeShapeType;
using static Org.Graphstream.Stream.File.Gexf.ClassType;
using static Org.Graphstream.Stream.File.Gexf.TimeFormatType;
using static Org.Graphstream.Stream.File.Gexf.GPXAttribute;
using static Org.Graphstream.Stream.File.Gexf.WPTAttribute;
using static Org.Graphstream.Stream.File.Gexf.LINKAttribute;
using static Org.Graphstream.Stream.File.Gexf.EMAILAttribute;
using static Org.Graphstream.Stream.File.Gexf.PTAttribute;
using static Org.Graphstream.Stream.File.Gexf.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Gexf.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Gexf.FixType;
using static Org.Graphstream.Stream.File.Gexf.GraphAttribute;
using static Org.Graphstream.Stream.File.Gexf.LocatorAttribute;
using static Org.Graphstream.Stream.File.Gexf.NodeAttribute;
using static Org.Graphstream.Stream.File.Gexf.EdgeAttribute;
using static Org.Graphstream.Stream.File.Gexf.DataAttribute;
using static Org.Graphstream.Stream.File.Gexf.PortAttribute;
using static Org.Graphstream.Stream.File.Gexf.EndPointAttribute;
using static Org.Graphstream.Stream.File.Gexf.EndPointType;
using static Org.Graphstream.Stream.File.Gexf.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Gexf.KeyAttribute;
using static Org.Graphstream.Stream.File.Gexf.KeyDomain;
using static Org.Graphstream.Stream.File.Gexf.KeyAttrType;
using static Org.Graphstream.Stream.File.Gexf.GraphEvents;
using static Org.Graphstream.Stream.File.Gexf.ThreadingModel;
using static Org.Graphstream.Stream.File.Gexf.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Gexf.Measures;
using static Org.Graphstream.Stream.File.Gexf.Token;
using static Org.Graphstream.Stream.File.Gexf.Extension;
using static Org.Graphstream.Stream.File.Gexf.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Gexf.AttrType;

namespace Gs_Core.Graphstream.Stream.File.Gexf;

public class GEXFSpells : GEXFElement
{
    GEXF root;
    LinkedList<GEXFSpell> spells;
    public GEXFSpells(GEXF root)
    {
        this.root = root;
        this.spells = new LinkedList<GEXFSpell>();
    }

    public virtual void Start()
    {
        double date = root.step;
        if (spells.Count == 0 || spells.GetLast().closed)
        {
            GEXFSpell spell = new GEXFSpell(root);
            spell.start = date;
            spells.Add(spell);
        }
    }

    public virtual void End()
    {
        double date = root.step;
        if (spells.Count > 0 && !spells.GetLast().closed)
        {
            GEXFSpell spell = spells.GetLast();
            spell.end = date;
            spell.closed = true;
        }
    }

    public virtual void Export(SmartXMLWriter stream)
    {
        if (spells.Count == 0)
            return;
        stream.StartElement("spells");
        for (int i = 0; i < spells.Count; i++)
            spells[i].Export(stream);
        stream.EndElement();
    }
}
