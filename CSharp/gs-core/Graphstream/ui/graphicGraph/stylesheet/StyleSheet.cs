using Java.Io;
using Java.Net;
using Java.Util;
using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.StyleConstants;
using Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.Parser;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EventType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Mode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.What;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Option;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Balise;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IDType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FixType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.GraphEvents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ThreadingModel;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.CloseFramePolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Measures;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Token;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Extension;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.DefaultEdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.AttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Resolutions;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.PropertyType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Type;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Units;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.FillMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.StrokeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShadowMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.VisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextVisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextStyle;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.IconMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SizeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextAlignment;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.TextBackgroundMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ShapeKind;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Shape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.SpriteOrientation;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.ArrowShape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.JComponents;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet;

public class StyleSheet
{
    public Rule defaultRule;
    public NameSpace graphRules = new NameSpace(Selector.Type.GRAPH);
    public NameSpace nodeRules = new NameSpace(Selector.Type.NODE);
    public NameSpace edgeRules = new NameSpace(Selector.Type.EDGE);
    public NameSpace spriteRules = new NameSpace(Selector.Type.SPRITE);
    public List<StyleSheetListener> listeners = new List<StyleSheetListener>();
    public StyleSheet()
    {
        InitRules();
    }

    public virtual Rule GetDefaultGraphRule()
    {
        return graphRules.defaultRule;
    }

    public virtual Rule GetDefaultNodeRule()
    {
        return nodeRules.defaultRule;
    }

    public virtual Rule GetDefaultEdgeRule()
    {
        return edgeRules.defaultRule;
    }

    public virtual Rule GetDefaultSpriteRule()
    {
        return spriteRules.defaultRule;
    }

    public virtual Style GetDefaultGraphStyle()
    {
        return GetDefaultGraphRule().GetStyle();
    }

    public virtual Style GetDefaultNodeStyle()
    {
        return GetDefaultNodeRule().GetStyle();
    }

    public virtual Style GetDefaultEdgeStyle()
    {
        return GetDefaultEdgeRule().GetStyle();
    }

    public virtual Style GetDefaultSpriteStyle()
    {
        return GetDefaultSpriteRule().GetStyle();
    }

    public virtual NameSpace GetGraphStyleNameSpace()
    {
        return graphRules;
    }

    public virtual NameSpace GetNodeStyleNameSpace()
    {
        return nodeRules;
    }

    public virtual NameSpace GetEdgeStyleNameSpace()
    {
        return edgeRules;
    }

    public virtual NameSpace GetSpriteStyleNameSpace()
    {
        return spriteRules;
    }

    public virtual List<Rule> GetRulesFor(Element element)
    {
        List<Rule> rules = null;
        if (element is Graph)
        {
            rules = graphRules.GetRulesFor(element);
        }
        else if (element is Node)
        {
            rules = nodeRules.GetRulesFor(element);
        }
        else if (element is Edge)
        {
            rules = edgeRules.GetRulesFor(element);
        }
        else if (element is GraphicSprite)
        {
            rules = spriteRules.GetRulesFor(element);
        }
        else
        {
            rules = new List<Rule>();
            rules.Add(defaultRule);
        }

        return rules;
    }

    public virtual string GetStyleGroupIdFor(Element element, List<Rule> rules)
    {
        StringBuilder builder = new StringBuilder();
        if (element is Graph)
        {
            builder.Append("g");
        }
        else if (element is Node)
        {
            builder.Append("n");
        }
        else if (element is Edge)
        {
            builder.Append("e");
        }
        else if (element is GraphicSprite)
        {
            builder.Append("s");
        }
        else
        {
            throw new Exception("What ?");
        }

        if (rules[0].selector.GetId() != null)
        {
            builder.Append('_');
            builder.Append(rules[0].selector.GetId());
        }

        int n = rules.Count;
        if (n > 1)
        {
            builder.Append('(');
            builder.Append(rules[1].selector.GetClazz());
            for (int i = 2; i < n; i++)
            {
                builder.Append(',');
                builder.Append(rules[i].selector.GetClazz());
            }

            builder.Append(')');
        }

        return builder.ToString();
    }

    protected virtual void InitRules()
    {
        defaultRule = new Rule(new Selector(Selector.Type.ANY), null);
        defaultRule.GetStyle().SetDefaults();
        graphRules.defaultRule = new Rule(new Selector(Selector.Type.GRAPH), defaultRule);
        nodeRules.defaultRule = new Rule(new Selector(Selector.Type.NODE), defaultRule);
        edgeRules.defaultRule = new Rule(new Selector(Selector.Type.EDGE), defaultRule);
        spriteRules.defaultRule = new Rule(new Selector(Selector.Type.SPRITE), defaultRule);
        graphRules.defaultRule.GetStyle().SetValue("padding", new Values(Style.Units.PX, 30));
        edgeRules.defaultRule.GetStyle().SetValue("shape", StyleConstants.Shape.LINE);
        edgeRules.defaultRule.GetStyle().SetValue("size", new Values(Style.Units.PX, 1));
        edgeRules.defaultRule.GetStyle().SetValue("z-index", new int (1));
        nodeRules.defaultRule.GetStyle().SetValue("z-index", new int (2));
        spriteRules.defaultRule.GetStyle().SetValue("z-index", new int (3));
        Colors colors = new Colors();
        colors.Add(Color.WHITE);
        graphRules.defaultRule.GetStyle().SetValue("fill-color", colors);
        graphRules.defaultRule.GetStyle().SetValue("stroke-mode", StrokeMode.NONE);
        foreach (StyleSheetListener listener in listeners)
        {
            listener.StyleAdded(defaultRule, defaultRule);
            listener.StyleAdded(graphRules.defaultRule, graphRules.defaultRule);
            listener.StyleAdded(nodeRules.defaultRule, nodeRules.defaultRule);
            listener.StyleAdded(edgeRules.defaultRule, edgeRules.defaultRule);
            listener.StyleAdded(spriteRules.defaultRule, spriteRules.defaultRule);
        }
    }

    public virtual void AddListener(StyleSheetListener listener)
    {
        listeners.Add(listener);
    }

    public virtual void RemoveListener(StyleSheetListener listener)
    {
        int index = listeners.IndexOf(listener);
        if (index >= 0)
            listeners.Remove(index);
    }

    public virtual void Clear()
    {
        graphRules.Clear();
        nodeRules.Clear();
        edgeRules.Clear();
        spriteRules.Clear();
        InitRules();
        foreach (StyleSheetListener listener in listeners)
            listener.StyleSheetCleared();
    }

    public virtual void ParseFromFile(string fileName)
    {
        Parse(new InputStreamReader(new BufferedInputStream(new FileInputStream(fileName))));
    }

    public virtual void ParseFromURL(string url)
    {
        URL u = typeof(StyleSheet).GetClassLoader().GetResource(url);
        if (u == null)
        {
            string fileUrl = url.Replace("file://", "");
            File f = new File(fileUrl);
            if (f.Exists())
                u = f.ToURI().ToURL();
            else
                u = new URL(url);
        }

        Parse(new InputStreamReader(u.OpenStream()));
    }

    public virtual void ParseFromString(string styleSheet)
    {
        Parse(new StringReader(styleSheet));
    }

    public virtual void ParseStyleFromString(Selector select, string styleString)
    {
        StyleSheetParser parser = new StyleSheetParser(this, new StringReader(styleString));
        Style style = new Style();
        try
        {
            parser.StylesStart(style);
        }
        catch (ParseException e)
        {
            throw new IOException(e.GetMessage());
        }

        Rule rule = new Rule(select);
        rule.SetStyle(style);
        AddRule(rule);
    }

    public virtual void Load(string styleSheetValue)
    {
        if (styleSheetValue.StartsWith("url"))
        {
            int beg = styleSheetValue.IndexOf('(');
            int end = styleSheetValue.LastIndexOf(')');
            if (beg >= 0 && end > beg)
                styleSheetValue = styleSheetValue.Substring(beg + 1, end);
            styleSheetValue = styleSheetValue.Trim();
            if (styleSheetValue.StartsWith("'"))
            {
                beg = 0;
                end = styleSheetValue.LastIndexOf('\'');
                if (beg >= 0 && end > beg)
                    styleSheetValue = styleSheetValue.Substring(beg + 1, end);
            }

            styleSheetValue = styleSheetValue.Trim();
            if (styleSheetValue.StartsWith("\""))
            {
                beg = 0;
                end = styleSheetValue.LastIndexOf('"');
                if (beg >= 0 && end > beg)
                    styleSheetValue = styleSheetValue.Substring(beg + 1, end);
            }

            ParseFromURL(styleSheetValue);
        }
        else
        {
            ParseFromString(styleSheetValue);
        }
    }

    protected virtual void Parse(Reader reader)
    {
        StyleSheetParser parser = new StyleSheetParser(this, reader);
        try
        {
            parser.Start();
        }
        catch (ParseException e)
        {
            throw new IOException(e.GetMessage());
        }
    }

    public virtual void AddRule(Rule newRule)
    {
        Rule oldRule = null;
        switch (newRule.selector.GetType())
        {
            case ANY:
                throw new Exception("The ANY selector should never be used, it is created automatically.");
            case GRAPH:
                oldRule = graphRules.AddRule(newRule);
                break;
            case NODE:
                oldRule = nodeRules.AddRule(newRule);
                break;
            case EDGE:
                oldRule = edgeRules.AddRule(newRule);
                break;
            case SPRITE:
                oldRule = spriteRules.AddRule(newRule);
                break;
            default:
                throw new Exception("Ho ho ho ?");
                break;
        }

        foreach (StyleSheetListener listener in listeners)
            listener.StyleAdded(oldRule, newRule);
    }

    public virtual string ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("StyleSheet : {\n");
        builder.Append("  default styles:\n");
        builder.Append(defaultRule.ToString(1));
        builder.Append(graphRules.ToString(1));
        builder.Append(nodeRules.ToString(1));
        builder.Append(edgeRules.ToString(1));
        builder.Append(spriteRules.ToString(1));
        return builder.ToString();
    }

    public class NameSpace
    {
        public Selector.Type type;
        public Rule defaultRule;
        public HashMap<string, Rule> byId = new HashMap<string, Rule>();
        public HashMap<string, Rule> byClass = new HashMap<string, Rule>();
        public NameSpace(Selector.Type type)
        {
            this.type = type;
        }

        public virtual Selector.Type GetGraphElementType()
        {
            return type;
        }

        public virtual int GetIdRulesCount()
        {
            return byId.Count;
        }

        public virtual int GetClassRulesCount()
        {
            return byClass.Count;
        }

        protected virtual List<Rule> GetRulesFor(Element element)
        {
            Rule rule = byId[element.GetId()];
            List<Rule> rules = new List<Rule>();
            if (rule != null)
                rules.Add(rule);
            else
                rules.Add(defaultRule);
            GetClassRules(element, rules);
            if (rules.IsEmpty())
                rules.Add(defaultRule);
            return rules;
        }

        protected virtual void GetClassRules(Element element, List<Rule> rules)
        {
            object o = element.GetAttribute("ui.class");
            if (o != null)
            {
                if (o is Object[])
                {
                    foreach (object s in (Object[])o)
                    {
                        if (s is CharSequence)
                        {
                            Rule rule = byClass[(CharSequence)s];
                            if (rule != null)
                                rules.Add(rule);
                        }
                    }
                }
                else if (o is CharSequence)
                {
                    string classList = ((CharSequence)o).ToString().Trim();
                    string[] classes = classList.Split("\\s*,\\s*");
                    foreach (string c in classes)
                    {
                        Rule rule = byClass[c];
                        if (rule != null)
                            rules.Add(rule);
                    }
                }
                else
                {
                    throw new Exception("Oups ! class attribute is of type " + o.GetType().GetName());
                }
            }
        }

        protected virtual void Clear()
        {
            defaultRule = null;
            byId.Clear();
            byClass.Clear();
        }

        protected virtual Rule AddRule(Rule newRule)
        {
            Rule oldRule = null;
            if (newRule.selector.GetPseudoClass() != null)
            {
                oldRule = AddEventRule(newRule);
            }
            else if (newRule.selector.GetId() != null)
            {
                oldRule = byId[newRule.selector.GetId()];
                if (oldRule != null)
                {
                    oldRule.GetStyle().Augment(newRule.GetStyle());
                }
                else
                {
                    byId.Put(newRule.selector.GetId(), newRule);
                    newRule.GetStyle().Reparent(defaultRule);
                }
            }
            else if (newRule.selector.GetClazz() != null)
            {
                oldRule = byClass[newRule.selector.GetClazz()];
                if (oldRule != null)
                {
                    oldRule.GetStyle().Augment(newRule.GetStyle());
                }
                else
                {
                    byClass.Put(newRule.selector.GetClazz(), newRule);
                    newRule.GetStyle().Reparent(defaultRule);
                }
            }
            else
            {
                oldRule = defaultRule;
                defaultRule.GetStyle().Augment(newRule.GetStyle());
                newRule = defaultRule;
            }

            return oldRule;
        }

        protected virtual Rule AddEventRule(Rule newRule)
        {
            Rule parentRule = null;
            if (newRule.selector.GetId() != null)
            {
                parentRule = byId[newRule.selector.GetId()];
                if (parentRule == null)
                {
                    parentRule = AddRule(new Rule(new Selector(newRule.selector.GetType(), newRule.selector.GetId(), newRule.selector.GetClazz())));
                }
            }
            else if (newRule.selector.GetClazz() != null)
            {
                parentRule = byClass[newRule.selector.GetClazz()];
                if (parentRule == null)
                {
                    parentRule = AddRule(new Rule(new Selector(newRule.selector.GetType(), newRule.selector.GetId(), newRule.selector.GetClazz())));
                }
            }
            else
            {
                parentRule = defaultRule;
            }

            newRule.GetStyle().Reparent(parentRule);
            parentRule.GetStyle().AddAlternateStyle(newRule.selector.GetPseudoClass(), newRule);
            return parentRule;
        }

        public virtual string ToString()
        {
            return ToString(-1);
        }

        public virtual string ToString(int level)
        {
            string prefix = "";
            if (level > 0)
            {
                for (int i = 0; i < level; i++)
                    prefix += "    ";
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(String.Format("%s%s default style :%n", prefix, type));
            builder.Append(defaultRule.ToString(level + 1));
            ToStringRules(level, builder, byId, String.Format("%s%s id styles", prefix, type));
            ToStringRules(level, builder, byClass, String.Format("%s%s class styles", prefix, type));
            return builder.ToString();
        }

        protected virtual void ToStringRules(int level, StringBuilder builder, HashMap<string, Rule> rules, string title)
        {
            builder.Append(title);
            builder.Append(String.Format(" :%n"));
            foreach (Rule rule in rules.Values())
                builder.Append(rule.ToString(level + 1));
        }
    }
}
