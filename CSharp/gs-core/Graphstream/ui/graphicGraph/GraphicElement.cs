using Gs_Core.Graphstream.Graph.Implementations;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Java.Util.Logging;
using Java.Util.Regex;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.EventType;
using static Org.Graphstream.Ui.GraphicGraph.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Mode;
using static Org.Graphstream.Ui.GraphicGraph.What;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Option;
using static Org.Graphstream.Ui.GraphicGraph.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Balise;
using static Org.Graphstream.Ui.GraphicGraph.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.IDType;
using static Org.Graphstream.Ui.GraphicGraph.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.FixType;
using static Org.Graphstream.Ui.GraphicGraph.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.GraphEvents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph;

public abstract class GraphicElement : AbstractElement
{
    private static readonly Logger logger = Logger.GetLogger(typeof(GraphicElement).GetSimpleName());
    public interface SwingElementRenderer
    {
    }

    protected GraphicGraph mygraph;
    public string label;
    public StyleGroup style;
    public object component;
    public bool hidden = false;
    public GraphicElement(string id, GraphicGraph graph) : base(id)
    {
        this.mygraph = graph;
    }

    public virtual GraphicGraph MyGraph()
    {
        return mygraph;
    }

    public abstract Selector.Type GetSelectorType();
    public virtual StyleGroup GetStyle()
    {
        return style;
    }

    public virtual string GetLabel()
    {
        return label;
    }

    public abstract double GetX();
    public abstract double GetY();
    public abstract double GetZ();
    public virtual object GetComponent()
    {
        return component;
    }

    protected abstract void Removed();
    public abstract void Move(double x, double y, double z);
    public virtual void SetComponent(object component)
    {
        this.component = component;
    }

    protected override void AttributeChanged(AttributeChangeEvent @event, string attribute, object oldValue, object newValue)
    {
        if (@event == AttributeChangeEvent.ADD || @event == AttributeChangeEvent.CHANGE)
        {
            if (attribute.CharAt(0) == 'u' && attribute.CharAt(1) == 'i')
            {
                if (attribute.Equals("ui.class"))
                {
                    mygraph.styleGroups.CheckElementStyleGroup(this);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.label"))
                {
                    label = StyleConstants.ConvertLabel(newValue);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.style"))
                {
                    if (newValue is string)
                    {
                        try
                        {
                            mygraph.styleSheet.ParseStyleFromString(new Selector(GetSelectorType(), GetId(), null), (string)newValue);
                        }
                        catch (Exception e)
                        {
                            logger.Log(Level.WARNING, String.Format("Error while parsing style for %S '%s' :", GetSelectorType(), GetId()), e);
                        }

                        mygraph.graphChanged = true;
                    }
                    else
                    {
                        logger.Warning("Unknown value for style [" + newValue + "].");
                    }
                }
                else if (attribute.Equals("ui.hide"))
                {
                    hidden = true;
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.clicked"))
                {
                    style.PushEventFor(this, "clicked");
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.selected"))
                {
                    style.PushEventFor(this, "selected");
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.color"))
                {
                    style.PushElementAsDynamic(this);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.size"))
                {
                    style.PushElementAsDynamic(this);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.icon"))
                {
                    mygraph.graphChanged = true;
                }
            }
            else if (attribute.Equals("label"))
            {
                label = StyleConstants.ConvertLabel(newValue);
                mygraph.graphChanged = true;
            }
        }
        else
        {
            if (attribute.CharAt(0) == 'u' && attribute.CharAt(1) == 'i')
            {
                if (attribute.Equals("ui.class"))
                {
                    object o = attributes.Remove("ui.class");
                    mygraph.styleGroups.CheckElementStyleGroup(this);
                    attributes.Put("ui.class", o);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.label"))
                {
                    label = "";
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.hide"))
                {
                    hidden = false;
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.clicked"))
                {
                    style.PopEventFor(this, "clicked");
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.selected"))
                {
                    style.PopEventFor(this, "selected");
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.color"))
                {
                    style.PopElementAsDynamic(this);
                    mygraph.graphChanged = true;
                }
                else if (attribute.Equals("ui.size"))
                {
                    style.PopElementAsDynamic(this);
                    mygraph.graphChanged = true;
                }
            }
            else if (attribute.Equals("label"))
            {
                label = "";
                mygraph.graphChanged = true;
            }
        }
    }

    protected static Pattern acceptedAttribute;
    static GraphicElement()
    {
        acceptedAttribute = Pattern.Compile("(ui[.].*)|(layout[.].*)|x|y|z|xy|xyz|label|stylesheet");
    }

    public override void SetAttribute(string attribute, params object[] values)
    {
        Matcher matcher = acceptedAttribute.Matcher(attribute);
        if (matcher.Matches())
            base.SetAttribute(attribute, values);
    }
}
