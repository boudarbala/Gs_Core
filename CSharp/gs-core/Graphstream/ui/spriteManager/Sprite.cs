using Java;
using Java.Util.Stream;
using Gs_Core.Graphstream.Graph;
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

public class Sprite : Element
{
    protected string id;
    protected string completeId;
    protected SpriteManager manager;
    protected Values position;
    protected Element attachment;
    protected Sprite()
    {
    }

    protected Sprite(string id, SpriteManager manager) : this(id, manager, null)
    {
    }

    protected Sprite(string id, SpriteManager manager, Values position)
    {
        Init(id, manager, position);
    }

    protected virtual void Init(string id, SpriteManager manager, Values position)
    {
        this.id = id;
        this.completeId = String.Format("ui.sprite.%s", id);
        this.manager = manager;
        if (!manager.graph.HasAttribute(completeId))
        {
            if (position != null)
            {
                manager.graph.SetAttribute(completeId, position);
                this.position = position;
            }
            else
            {
                this.position = new Values(Style.Units.GU, 0F, 0F, 0F);
                manager.graph.SetAttribute(completeId, this.position);
            }
        }
        else
        {
            if (position != null)
            {
                manager.graph.SetAttribute(completeId, position);
                this.position = position;
            }
            else
            {
                this.position = SpriteManager.GetPositionValue(manager.graph.GetAttribute(completeId));
            }
        }
    }

    protected virtual void Removed()
    {
        manager.graph.RemoveAttribute(completeId);
        string start = String.Format("%s.", completeId);
        if (Attached())
            Detach();
        List<string> keys = new List<string>();
        manager.graph.AttributeKeys().ForEach((key) =>
        {
            if (key.StartsWith(start))
                keys.Add(key);
        });
        foreach (string key in keys)
            manager.graph.RemoveAttribute(key);
    }

    public virtual Element GetAttachment()
    {
        return attachment;
    }

    public virtual bool Attached()
    {
        return (attachment != null);
    }

    public virtual double GetX()
    {
        if (position.values.Count > 0)
            return position.values[0];
        return 0;
    }

    public virtual double GetY()
    {
        if (position.values.Count > 1)
            return position.values[1];
        return 0;
    }

    public virtual double GetZ()
    {
        if (position.values.Count > 2)
            return position.values[2];
        return 0;
    }

    public virtual Style.Units GetUnits()
    {
        return position.units;
    }

    public virtual void AttachToNode(string id)
    {
        if (attachment != null)
            Detach();
        attachment = manager.graph.GetNode(id);
        if (attachment != null)
            attachment.SetAttribute(completeId);
    }

    public virtual void AttachToEdge(string id)
    {
        if (attachment != null)
            Detach();
        attachment = manager.graph.GetEdge(id);
        if (attachment != null)
            attachment.SetAttribute(completeId);
    }

    public virtual void Detach()
    {
        if (attachment != null)
        {
            attachment.RemoveAttribute(completeId);
            attachment = null;
        }
    }

    public virtual void SetPosition(double percent)
    {
        SetPosition(position.units, percent, 0, 0);
    }

    public virtual void SetPosition(double x, double y, double z)
    {
        SetPosition(position.units, x, y, z);
    }

    public virtual void SetPosition(Style.Units units, double x, double y, double z)
    {
        bool changed = false;
        if (position[0] != x)
        {
            changed = true;
            position.SetValue(0, x);
        }

        if (position[1] != y)
        {
            changed = true;
            position.SetValue(1, y);
        }

        if (position[2] != z)
        {
            changed = true;
            position.SetValue(2, z);
        }

        if (position.units != units)
        {
            changed = true;
            position.SetUnits(units);
        }

        if (changed)
            manager.graph.SetAttribute(completeId, new Values(position));
    }

    protected virtual void SetPosition(Values values)
    {
        if (values != null)
        {
            int n = values.values.Count;
            if (n > 2)
            {
                SetPosition(values.units, values[0], values[1], values[2]);
            }
            else if (n > 0)
            {
                SetPosition(values[0]);
            }
        }
    }

    public virtual string GetId()
    {
        return id;
    }

    public virtual CharSequence GetLabel(string key)
    {
        return manager.graph.GetLabel(String.Format("%s.%s", completeId, key));
    }

    public virtual object GetAttribute(string key)
    {
        return manager.graph.GetAttribute(String.Format("%s.%s", completeId, key));
    }

    public virtual T GetAttribute<T>(string key, Class<T> clazz)
    {
        return manager.graph.GetAttribute(String.Format("%s.%s", completeId, key), clazz);
    }

    public virtual int GetAttributeCount()
    {
        string start = String.Format("%s.", completeId);
        return (int)manager.graph.AttributeKeys().Filter((key) => key.StartsWith(start)).Count();
    }

    public virtual Stream<string> AttributeKeys()
    {
        throw new Exception("not implemented");
    }

    public virtual Dictionary<string, object> GetAttributeMap()
    {
        throw new Exception("not implemented");
    }

    public virtual object GetFirstAttributeOf(params string[] keys)
    {
        string[] completeKeys = new string[keys.Length];
        int i = 0;
        foreach (string key in keys)
        {
            completeKeys[i] = String.Format("%s.%s", completeId, key);
            i++;
        }

        return manager.graph.GetFirstAttributeOf(completeKeys);
    }

    public virtual T GetFirstAttributeOf<T>(Class<T> clazz, params string[] keys)
    {
        string[] completeKeys = new string[keys.Length];
        int i = 0;
        foreach (string key in keys)
        {
            completeKeys[i] = String.Format("%s.%s", completeId, key);
            i++;
        }

        return manager.graph.GetFirstAttributeOf(clazz, completeKeys);
    }

    public virtual Object[] GetArray(string key)
    {
        return manager.graph.GetArray(String.Format("%s.%s", completeId, key));
    }

    public virtual Map<?, ?> GetMap(string key)
    {
        return manager.graph.GetMap(String.Format("%s.%s", completeId, key));
    }

    public virtual double GetNumber(string key)
    {
        return manager.graph.GetNumber(String.Format("%s.%s", completeId, key));
    }

    public virtual IList<TWildcardTodoNumber> GetVector(string key)
    {
        return manager.graph.GetVector(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasAttribute(string key)
    {
        return manager.graph.HasAttribute(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasArray(string key)
    {
        return manager.graph.HasArray(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasAttribute(string key, Class<TWildcardTodo> clazz)
    {
        return manager.graph.HasAttribute(String.Format("%s.%s", completeId, key), clazz);
    }

    public virtual bool HasMap(string key)
    {
        return manager.graph.HasMap(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasLabel(string key)
    {
        return manager.graph.HasLabel(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasNumber(string key)
    {
        return manager.graph.HasNumber(String.Format("%s.%s", completeId, key));
    }

    public virtual bool HasVector(string key)
    {
        return manager.graph.HasVector(String.Format("%s.%s", completeId, key));
    }

    public virtual void SetAttribute(string attribute, params object[] values)
    {
        manager.graph.SetAttribute(String.Format("%s.%s", completeId, attribute), values);
    }

    public virtual void SetAttributes(Dictionary<string, object> attributes)
    {
        foreach (string key in attributes.KeySet())
            manager.graph.SetAttribute(String.Format("%s.%s", completeId, key), attributes[key]);
    }

    public virtual void ClearAttributes()
    {
        string start = String.Format("%s.", completeId);
        manager.graph.AttributeKeys().Filter((key) => key.StartsWith(start)).Collect(Collectors.ToList()).ForEach((key) => manager.graph.RemoveAttribute(key));
    }

    public virtual void RemoveAttribute(string attribute)
    {
        manager.graph.RemoveAttribute(String.Format("%s.%s", completeId, attribute));
    }

    public virtual int GetIndex()
    {
        return 0;
    }
}
