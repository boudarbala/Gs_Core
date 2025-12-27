using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Stream;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Java.Util;
using Java.Util.Logging;
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

public class SpriteManager : Iterable<Sprite>, AttributeSink
{
    private static readonly Logger logger = Logger.GetLogger(typeof(SpriteManager).GetName());
    protected Graph graph;
    protected HashMap<string, Sprite> sprites = new HashMap<string, Sprite>();
    protected SpriteFactory factory = new SpriteFactory();
    bool attributeLock = false;
    public SpriteManager(Graph graph)
    {
        this.graph = graph;
        LookForExistingSprites();
        graph.AddAttributeSink(this);
    }

    protected virtual void LookForExistingSprites()
    {
        if (graph.GetAttributeCount() > 0)
        {
            graph.AttributeKeys().Filter((key) => key.StartsWith("ui.sprite.")).ForEach((key) =>
            {
                string id = key.Substring(10);
                if (id.IndexOf('.') < 0)
                {
                    AddSprite(id);
                }
                else
                {
                    string sattr = id.Substring(id.IndexOf('.') + 1);
                    id = id.Substring(0, id.IndexOf('.'));
                    Sprite s = GetSprite(id);
                    if (s == null)
                        s = AddSprite(id);
                    s.SetAttribute(sattr, graph.GetAttribute(key));
                }
            });
        }
    }

    public virtual int GetSpriteCount()
    {
        return sprites.Count;
    }

    public virtual bool HasSprite(string identifier)
    {
        return (sprites[identifier] != null);
    }

    public virtual Sprite GetSprite(string identifier)
    {
        return sprites[identifier];
    }

    public virtual Iterable<TWildcardTodoSprite> Sprites()
    {
        return sprites.Values();
    }

    public virtual IEnumerator<TWildcardTodoSprite> SpriteIterator()
    {
        return sprites.Values().Iterator();
    }

    public virtual IEnumerator<Sprite> Iterator()
    {
        return sprites.Values().Iterator();
    }

    public virtual SpriteFactory GetSpriteFactory()
    {
        return factory;
    }

    public virtual void Detach()
    {
        graph.RemoveAttributeSink(this);
        sprites.Clear();
        graph = null;
    }

    public virtual void SetSpriteFactory(SpriteFactory factory)
    {
        this.factory = factory;
    }

    public virtual void ResetSpriteFactory()
    {
        factory = new SpriteFactory();
    }

    public virtual Sprite AddSprite(string identifier)
    {
        return AddSprite(identifier, (Values)null);
    }

    protected virtual Sprite AddSprite(string identifier, Values position)
    {
        if (identifier.IndexOf('.') >= 0)
            throw new InvalidSpriteIDException("Sprite identifiers cannot contain dots.");
        Sprite sprite = sprites[identifier];
        if (sprite == null)
        {
            attributeLock = true;
            sprite = factory.NewSprite(identifier, this, position);
            sprites.Put(identifier, sprite);
            attributeLock = false;
        }
        else
        {
            if (position != null)
                sprite.SetPosition(position);
        }

        return sprite;
    }

    public virtual T AddSprite<T extends Sprite>(string identifier, Class<T> spriteClass)
    {
        return AddSprite(identifier, spriteClass, null);
    }

    public virtual T AddSprite<T extends Sprite>(string identifier, Class<T> spriteClass, Values position)
    {
        try
        {
            T sprite = spriteClass.NewInstance();
            sprite.Init(identifier, this, position);
            return sprite;
        }
        catch (Exception e)
        {
            logger.Log(Level.WARNING, String.Format("Error while trying to instantiate class %s.", spriteClass.GetName()), e);
        }

        return null;
    }

    public virtual void RemoveSprite(string identifier)
    {
        Sprite sprite = sprites[identifier];
        if (sprite != null)
        {
            attributeLock = true;
            sprites.Remove(identifier);
            sprite.Removed();
            attributeLock = false;
        }
    }

    protected static Values GetPositionValue(object value)
    {
        if (value is Object[])
        {
            object[] values = (Object[])value;
            if (values.Length == 4)
            {
                if (values[0] is Number && values[1] is Number && values[2] is Number && values[3] is Style.Units)
                {
                    return new Values((Style.Units)values[3], ((Number)values[0]).FloatValue(), ((Number)values[1]).FloatValue(), ((Number)values[2]).FloatValue());
                }
                else
                {
                    logger.Warning("Cannot parse values[4] for sprite position.");
                }
            }
            else if (values.Length == 3)
            {
                if (values[0] is Number && values[1] is Number && values[2] is Number)
                {
                    return new Values(Units.GU, ((Number)values[0]).FloatValue(), ((Number)values[1]).FloatValue(), ((Number)values[2]).FloatValue());
                }
                else
                {
                    logger.Warning("Cannot parse values[3] for sprite position.");
                }
            }
            else if (values.Length == 1)
            {
                if (values[0] is Number)
                {
                    return new Values(Units.GU, ((Number)values[0]).FloatValue());
                }
                else
                {
                    logger.Warning(String.Format("Sprite position percent is not a number."));
                }
            }
            else
            {
                logger.Warning(String.Format("Cannot transform value '%s' (length=%d) into a position.", Arrays.ToString(values), values.Length));
            }
        }
        else if (value is Number)
        {
            return new Values(Units.GU, ((Number)value).FloatValue());
        }
        else if (value is Value)
        {
            return new Values((Value)value);
        }
        else if (value is Values)
        {
            return new Values((Values)value);
        }
        else
        {
            System.err.Printf("GraphicGraph : cannot place sprite with posiiton '%s' (instance of %s)%n", value, value.GetType().GetName());
        }

        return null;
    }

    public virtual void GraphAttributeAdded(string graphId, long time, string attribute, object value)
    {
        if (attributeLock)
            return;
        if (attribute.StartsWith("ui.sprite."))
        {
            string spriteId = attribute.Substring(10);
            if (spriteId.IndexOf('.') < 0)
            {
                if (GetSprite(spriteId) == null)
                {
                    Values position = null;
                    if (value != null)
                        position = GetPositionValue(value);
                    try
                    {
                        AddSprite(spriteId, position);
                    }
                    catch (InvalidSpriteIDException e)
                    {
                        e.PrintStackTrace();
                        throw new Exception(e);
                    }
                }
            }
        }
    }

    public virtual void GraphAttributeChanged(string graphId, long time, string attribute, object oldValue, object newValue)
    {
        if (attributeLock)
            return;
        if (attribute.StartsWith("ui.sprite."))
        {
            string spriteId = attribute.Substring(10);
            if (spriteId.IndexOf('.') < 0)
            {
                Sprite s = GetSprite(spriteId);
                if (s != null)
                {
                    if (newValue != null)
                    {
                        Values position = GetPositionValue(newValue);
                        s.SetPosition(position);
                    }
                    else
                    {
                        logger.Warning(String.Format("%s changed but newValue == null ! (old=%s).", spriteId, oldValue));
                    }
                }
                else
                {
                    throw new InvalidOperationException("Sprite changed, but not added.");
                }
            }
        }
    }

    public virtual void GraphAttributeRemoved(string graphId, long time, string attribute)
    {
        if (attributeLock)
            return;
        if (attribute.StartsWith("ui.sprite."))
        {
            string spriteId = attribute.Substring(10);
            if (spriteId.IndexOf('.') < 0)
            {
                if (GetSprite(spriteId) != null)
                {
                    RemoveSprite(spriteId);
                }
            }
        }
    }

    public virtual void EdgeAttributeAdded(string graphId, long time, string edgeId, string attribute, object value)
    {
    }

    public virtual void EdgeAttributeChanged(string graphId, long time, string edgeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void EdgeAttributeRemoved(string graphId, long time, string edgeId, string attribute)
    {
    }

    public virtual void NodeAttributeAdded(string graphId, long time, string nodeId, string attribute, object value)
    {
    }

    public virtual void NodeAttributeChanged(string graphId, long time, string nodeId, string attribute, object oldValue, object newValue)
    {
    }

    public virtual void NodeAttributeRemoved(string graphId, long time, string nodeId, string attribute)
    {
    }
}
