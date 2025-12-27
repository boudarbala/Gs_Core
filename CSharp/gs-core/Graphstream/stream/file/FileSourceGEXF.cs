using Java.Awt;
using Java.Io;
using Java.Net;
using Java.Util;
using Java.Util.Regex;
using Javax.Xml.Stream;
using Javax.Xml.Stream.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.ElementType;
using static Org.Graphstream.Stream.File.EventType;
using static Org.Graphstream.Stream.File.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Mode;
using static Org.Graphstream.Stream.File.What;
using static Org.Graphstream.Stream.File.TimeFormat;
using static Org.Graphstream.Stream.File.OutputType;
using static Org.Graphstream.Stream.File.OutputPolicy;
using static Org.Graphstream.Stream.File.LayoutPolicy;
using static Org.Graphstream.Stream.File.Quality;
using static Org.Graphstream.Stream.File.Option;
using static Org.Graphstream.Stream.File.AttributeType;
using static Org.Graphstream.Stream.File.Balise;
using static Org.Graphstream.Stream.File.GEXFAttribute;
using static Org.Graphstream.Stream.File.METAAttribute;
using static Org.Graphstream.Stream.File.GRAPHAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.NODESAttribute;
using static Org.Graphstream.Stream.File.NODEAttribute;
using static Org.Graphstream.Stream.File.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.PARENTAttribute;
using static Org.Graphstream.Stream.File.EDGESAttribute;
using static Org.Graphstream.Stream.File.SPELLAttribute;
using static Org.Graphstream.Stream.File.COLORAttribute;
using static Org.Graphstream.Stream.File.POSITIONAttribute;
using static Org.Graphstream.Stream.File.SIZEAttribute;
using static Org.Graphstream.Stream.File.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.EDGEAttribute;
using static Org.Graphstream.Stream.File.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.IDType;
using static Org.Graphstream.Stream.File.ModeType;
using static Org.Graphstream.Stream.File.WeightType;
using static Org.Graphstream.Stream.File.EdgeType;
using static Org.Graphstream.Stream.File.NodeShapeType;
using static Org.Graphstream.Stream.File.EdgeShapeType;
using static Org.Graphstream.Stream.File.ClassType;
using static Org.Graphstream.Stream.File.TimeFormatType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceGEXF : FileSourceXML
{
    private static readonly Pattern IS_DOUBLE = Pattern.Compile("^-?\\d+([.]\\d+)?$");
    protected GEXFParser parser;
    protected virtual void AfterStartDocument()
    {
        parser = new GEXFParser();
        parser.__gexf();
    }

    public virtual bool NextEvents()
    {
        return false;
    }

    protected virtual void BeforeEndDocument()
    {
        parser = null;
    }

    private class Attribute : GEXFConstants
    {
        readonly string id;
        readonly string title;
        readonly AttributeType type;
        object def;
        string options;
        Attribute(string id, string title, AttributeType type)
        {
            this.id = id;
            this.title = title;
            this.type = type;
        }

        virtual object GetValue(string value)
        {
            object r;
            switch (type)
            {
                case INTEGER:
                    r = Integer.ValueOf(value);
                    break;
                case LONG:
                    r = Long.ValueOf(value);
                    break;
                case FLOAT:
                    r = Float.ValueOf(value);
                    break;
                case DOUBLE:
                    r = Double.ValueOf(value);
                    break;
                case BOOLEAN:
                    r = Boolean.ValueOf(value);
                    break;
                case LISTSTRING:
                    string[] list = value.Split("\\|");
                    bool isDouble = true;
                    for (int i = 0; i < list.Length; i++)
                        isDouble = isDouble && IS_DOUBLE.Matcher(list[i]).Matches();
                    if (isDouble)
                    {
                        double[] dlist = new double[list.Length];
                        for (int i = 0; i < list.Length; i++)
                            dlist[i] = Double.ParseDouble(list[i]);
                        r = dlist;
                    }
                    else
                        r = list;
                    break;
                case ANYURI:
                    try
                    {
                        r = new URI(value);
                    }
                    catch (URISyntaxException e)
                    {
                        throw new ArgumentException(e);
                    }

                    break;
                default:
                    r = value;
                    break;
            }

            return r;
        }

        virtual void SetDefault(string value)
        {
            this.def = GetValue(value);
        }

        virtual void SetOptions(string options)
        {
            this.options = options;
        }
    }

    private class GEXFParser : Parser, GEXFConstants
    {
        EdgeType defaultEdgeType;
        TimeFormatType timeFormat;
        HashMap<string, Attribute> nodeAttributesDefinition;
        HashMap<string, Attribute> edgeAttributesDefinition;
        GEXFParser()
        {
            defaultEdgeType = EdgeType.UNDIRECTED;
            timeFormat = TimeFormatType.INTEGER;
            nodeAttributesDefinition = new HashMap<string, Attribute>();
            edgeAttributesDefinition = new HashMap<string, Attribute>();
        }

        private long GetTime(string time)
        {
            long t = 0;
            switch (timeFormat)
            {
                case INTEGER:
                    t = Integer.ValueOf(time);
                    break;
                case DOUBLE:
                    break;
                case DATE:
                    break;
                case DATETIME:
                    break;
            }

            return t;
        }

        private void __gexf()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "gexf");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "meta"))
            {
                Pushback(e);
                __meta();
            }
            else
                Pushback(e);
            __graph();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "gexf");
        }

        private void __meta()
        {
            EnumMap<METAAttribute, string> attributes;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "meta");
            attributes = GetAttributes(typeof(METAAttribute), e.AsStartElement());
            if (attributes.ContainsKey(METAAttribute.LASTMODIFIEDDATE))
                SendGraphAttributeAdded(sourceId, "lastmodifieddate", attributes[METAAttribute.LASTMODIFIEDDATE]);
            e = GetNextEvent();
            while (!IsEvent(e, XMLEvent.END_ELEMENT, "meta"))
            {
                try
                {
                    string str;
                    Balise b = Balise.ValueOf(ToConstantName(e.AsStartElement().GetName().GetLocalPart()));
                    Pushback(e);
                    switch (b)
                    {
                        case CREATOR:
                            str = __creator();
                            SendGraphAttributeAdded(sourceId, "creator", str);
                            break;
                        case KEYWORDS:
                            str = __keywords();
                            SendGraphAttributeAdded(sourceId, "keywords", str);
                            break;
                        case DESCRIPTION:
                            str = __description();
                            SendGraphAttributeAdded(sourceId, "description", str);
                            break;
                        default:
                            NewParseError(e, false, "meta children should be one of 'creator','keywords' or 'description'");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "unknown element '%s'", e.AsStartElement().GetName().GetLocalPart());
                }

                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "meta");
        }

        private string __creator()
        {
            string creator;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "creator");
            creator = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "creator");
            return creator;
        }

        private string __keywords()
        {
            string keywords;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "keywords");
            keywords = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "keywords");
            return keywords;
        }

        private string __description()
        {
            string description;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "description");
            description = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "description");
            return description;
        }

        private void __graph()
        {
            XMLEvent e;
            EnumMap<GRAPHAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "graph");
            attributes = GetAttributes(typeof(GRAPHAttribute), e.AsStartElement());
            if (attributes.ContainsKey(GRAPHAttribute.DEFAULTEDGETYPE))
            {
                try
                {
                    defaultEdgeType = EdgeType.ValueOf(ToConstantName(attributes[GRAPHAttribute.DEFAULTEDGETYPE]));
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "'defaultedgetype' value should be one of 'directed', 'undirected' or 'mutual'");
                }
            }

            if (attributes.ContainsKey(GRAPHAttribute.TIMEFORMAT))
            {
                try
                {
                    timeFormat = TimeFormatType.ValueOf(ToConstantName(attributes[GRAPHAttribute.TIMEFORMAT]));
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "'timeformat' value should be one of 'integer', 'double', 'date' or 'datetime'");
                }
            }

            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "attributes"))
            {
                Pushback(e);
                __attributes();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "nodes") || IsEvent(e, XMLEvent.START_ELEMENT, "edges"))
            {
                if (IsEvent(e, XMLEvent.START_ELEMENT, "nodes"))
                {
                    Pushback(e);
                    __nodes();
                }
                else
                {
                    Pushback(e);
                    __edges();
                }

                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "graph");
        }

        private void __attributes()
        {
            XMLEvent e;
            EnumMap<ATTRIBUTESAttribute, string> attributes;
            Attribute a;
            ClassType type = null;
            HashMap<string, Attribute> attr;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "attributes");
            attributes = GetAttributes(typeof(ATTRIBUTESAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, ATTRIBUTESAttribute.CLASS);
            try
            {
                type = ClassType.ValueOf(ToConstantName(attributes[ATTRIBUTESAttribute.CLASS]));
            }
            catch (ArgumentException ex)
            {
                NewParseError(e, true, "'class' value shoudl be one of 'node' or 'edge'");
            }

            if (type == ClassType.NODE)
                attr = nodeAttributesDefinition;
            else
                attr = edgeAttributesDefinition;
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "attribute"))
            {
                Pushback(e);
                a = __attribute();
                attr.Put(a.id, a);
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "attributes");
        }

        private Attribute __attribute()
        {
            XMLEvent e;
            EnumMap<ATTRIBUTEAttribute, string> attributes;
            string id, title;
            AttributeType type = null;
            Attribute theAttribute;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "attribute");
            attributes = GetAttributes(typeof(ATTRIBUTEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, ATTRIBUTEAttribute.ID, ATTRIBUTEAttribute.TITLE, ATTRIBUTEAttribute.TYPE);
            id = attributes[ATTRIBUTEAttribute.ID];
            title = attributes[ATTRIBUTEAttribute.TITLE];
            try
            {
                type = AttributeType.ValueOf(ToConstantName(attributes[ATTRIBUTEAttribute.TYPE]));
            }
            catch (ArgumentException ex)
            {
                NewParseError(e, true, "'type' of attribute should be one of 'integer', 'long', 'float, 'double', 'string', 'liststring', 'anyURI' or 'boolean'");
            }

            theAttribute = new Attribute(id, title, type);
            e = GetNextEvent();
            while (!IsEvent(e, XMLEvent.END_ELEMENT, "attribute"))
            {
                try
                {
                    Balise b = Balise.ValueOf(ToConstantName(e.AsStartElement().GetName().GetLocalPart()));
                    Pushback(e);
                    switch (b)
                    {
                        case DEFAULT:
                            try
                            {
                                theAttribute.SetDefault(__default());
                            }
                            catch (Exception invalid)
                            {
                                NewParseError(e, false, "invalid 'default' value");
                            }

                            break;
                        case OPTIONS:
                            theAttribute.SetOptions(__options());
                            break;
                        default:
                            NewParseError(e, true, "attribute children should be one of 'default' or 'options'");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "unknown element '%s'", e.AsStartElement().GetName().GetLocalPart());
                }

                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "attribute");
            return theAttribute;
        }

        private string __default()
        {
            string def;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "default");
            def = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "default");
            return def;
        }

        private string __options()
        {
            string options;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "options");
            options = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "options");
            return options;
        }

        private void __nodes()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "nodes");
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "node"))
            {
                Pushback(e);
                __node();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "nodes");
        }

        private void __node()
        {
            XMLEvent e;
            EnumMap<NODEAttribute, string> attributes;
            string id;
            HashSet<string> defined = new HashSet<string>();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "node");
            attributes = GetAttributes(typeof(NODEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, NODEAttribute.ID);
            id = attributes[NODEAttribute.ID];
            SendNodeAdded(sourceId, id);
            if (attributes.ContainsKey(NODEAttribute.LABEL))
                SendNodeAttributeAdded(sourceId, id, "label", attributes[NODEAttribute.LABEL]);
            e = GetNextEvent();
            while (!IsEvent(e, XMLEvent.END_ELEMENT, "node"))
            {
                try
                {
                    Balise b = Balise.ValueOf(ToConstantName(e.AsStartElement().GetName().GetLocalPart()));
                    Pushback(e);
                    switch (b)
                    {
                        case ATTVALUES:
                            defined.AddAll(__attvalues(ClassType.NODE, id));
                            break;
                        case COLOR:
                            __color(ClassType.NODE, id);
                            break;
                        case POSITION:
                            __position(id);
                            break;
                        case SIZE:
                            __size(id);
                            break;
                        case SHAPE:
                            __node_shape(id);
                            break;
                        case SPELLS:
                            __spells();
                            break;
                        case NODES:
                            __nodes();
                            break;
                        case EDGES:
                            __edges();
                            break;
                        case PARENTS:
                            __parents(id);
                            break;
                        default:
                            NewParseError(e, true, "attribute children should be one of 'attvalues', 'color', 'position', 'size', shape', 'spells', 'nodes, 'edges' or 'parents'");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "unknown element '%s'", e.AsStartElement().GetName().GetLocalPart());
                }

                e = GetNextEvent();
            }

            foreach (Attribute theAttribute in nodeAttributesDefinition.Values())
            {
                if (!defined.Contains(theAttribute.id))
                {
                    SendNodeAttributeAdded(sourceId, id, theAttribute.title, theAttribute.def);
                }
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "node");
        }

        private HashSet<string> __attvalues(ClassType type, string elementId)
        {
            XMLEvent e;
            HashSet<string> defined = new HashSet<string>();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "attvalues");
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "attvalue"))
            {
                Pushback(e);
                defined.Add(__attvalue(type, elementId));
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "attvalues");
            return defined;
        }

        private string __attvalue(ClassType type, string elementId)
        {
            XMLEvent e;
            EnumMap<ATTVALUEAttribute, string> attributes;
            Attribute theAttribute;
            object value = null;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "attvalue");
            attributes = GetAttributes(typeof(ATTVALUEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, ATTVALUEAttribute.FOR, ATTVALUEAttribute.VALUE);
            if (type == ClassType.NODE)
                theAttribute = nodeAttributesDefinition[attributes[ATTVALUEAttribute.FOR]];
            else
                theAttribute = edgeAttributesDefinition[attributes[ATTVALUEAttribute.FOR]];
            if (theAttribute == null)
                NewParseError(e, false, "undefined attribute \"%s\"", attributes[ATTVALUEAttribute.FOR]);
            else
            {
                try
                {
                    value = theAttribute.GetValue(attributes[ATTVALUEAttribute.VALUE]);
                }
                catch (Exception ex)
                {
                    NewParseError(e, true, "invalid 'value' value");
                }

                switch (type)
                {
                    case NODE:
                        SendNodeAttributeAdded(sourceId, elementId, theAttribute.title, value);
                        break;
                    case EDGE:
                        SendEdgeAttributeAdded(sourceId, elementId, theAttribute.title, value);
                        break;
                }
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "attvalue");
            return theAttribute == null ? null : theAttribute.id;
        }

        private void __spells()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "spells");
            do
            {
                __spell();
                e = GetNextEvent();
            }
            while (IsEvent(e, XMLEvent.START_ELEMENT, "spell"));
            CheckValid(e, XMLEvent.END_ELEMENT, "spells");
        }

        private void __spell()
        {
            XMLEvent e;
            EnumMap<SPELLAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "spell");
            attributes = GetAttributes(typeof(SPELLAttribute), e.AsStartElement());
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "spell");
        }

        private void __parents(string nodeId)
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "parents");
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "parent"))
            {
                __parent(nodeId);
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "parents");
        }

        private void __parent(string nodeId)
        {
            XMLEvent e;
            EnumMap<PARENTAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "parent");
            attributes = GetAttributes(typeof(PARENTAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, PARENTAttribute.FOR);
            SendNodeAttributeAdded(sourceId, attributes[PARENTAttribute.FOR], "parent", nodeId);
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "parent");
        }

        private void __color(ClassType type, string id)
        {
            XMLEvent e;
            EnumMap<COLORAttribute, string> attributes;
            Color color;
            int r, g, b, a = 255;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "color");
            attributes = GetAttributes(typeof(COLORAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, COLORAttribute.R, COLORAttribute.G, COLORAttribute.B);
            r = Integer.ValueOf(attributes[COLORAttribute.R]);
            g = Integer.ValueOf(attributes[COLORAttribute.G]);
            b = Integer.ValueOf(attributes[COLORAttribute.B]);
            if (attributes.ContainsKey(COLORAttribute.A))
                a = Integer.ValueOf(attributes[COLORAttribute.A]);
            color = new Color(r, g, b, a);
            switch (type)
            {
                case NODE:
                    SendNodeAttributeAdded(sourceId, id, "ui.color", color);
                    break;
                case EDGE:
                    SendEdgeAttributeAdded(sourceId, id, "ui.color", color);
                    break;
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "color");
        }

        private void __position(string nodeId)
        {
            XMLEvent e;
            EnumMap<POSITIONAttribute, string> attributes;
            double[] xyz = new[]
            {
                0,
                0,
                0
            };
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "position");
            attributes = GetAttributes(typeof(POSITIONAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, POSITIONAttribute.X, POSITIONAttribute.Y, POSITIONAttribute.Z);
            xyz[0] = Double.ValueOf(attributes[POSITIONAttribute.X]);
            xyz[1] = Double.ValueOf(attributes[POSITIONAttribute.Y]);
            xyz[2] = Double.ValueOf(attributes[POSITIONAttribute.Z]);
            SendNodeAttributeAdded(sourceId, nodeId, "xyz", xyz);
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "position");
        }

        private void __size(string nodeId)
        {
            XMLEvent e;
            EnumMap<SIZEAttribute, string> attributes;
            double value;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "size");
            attributes = GetAttributes(typeof(SIZEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, SIZEAttribute.VALUE);
            value = Double.ValueOf(attributes[SIZEAttribute.VALUE]);
            SendNodeAttributeAdded(sourceId, nodeId, "ui.size", value);
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "size");
        }

        private void __node_shape(string nodeId)
        {
            XMLEvent e;
            EnumMap<NODESHAPEAttribute, string> attributes;
            NodeShapeType type = null;
            string uri;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "shape");
            attributes = GetAttributes(typeof(NODESHAPEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, NODESHAPEAttribute.VALUE);
            try
            {
                type = NodeShapeType.ValueOf(ToConstantName(attributes[NODESHAPEAttribute.VALUE]));
            }
            catch (ArgumentException ex)
            {
                NewParseError(e, true, "'value' should be one of 'disc', 'diamond', 'triangle', 'square' or 'image'");
            }

            switch (type)
            {
                case IMAGE:
                    if (!attributes.ContainsKey(NODESHAPEAttribute.URI))
                        NewParseError(e, true, "'image' shape type needs 'uri' attribute");
                    uri = attributes[NODESHAPEAttribute.URI];
                    SendNodeAttributeAdded(sourceId, nodeId, "ui.style", String.Format("fill-mode: image-scaled; fill-image: url('%s');", uri));
                    break;
                default:
                    SendNodeAttributeAdded(sourceId, nodeId, "ui.style", String.Format("shape: %s;", type.Name().ToLowerCase()));
                    break;
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "shape");
        }

        private void __edges()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "edges");
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "edge"))
            {
                Pushback(e);
                __edge();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "edges");
        }

        private void __edge()
        {
            XMLEvent e;
            EnumMap<EDGEAttribute, string> attributes;
            string id, source, target;
            EdgeType type = defaultEdgeType;
            HashSet<string> defined = new HashSet<string>();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "edge");
            attributes = GetAttributes(typeof(EDGEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, EDGEAttribute.ID, EDGEAttribute.SOURCE, EDGEAttribute.TARGET);
            id = attributes[EDGEAttribute.ID];
            source = attributes[EDGEAttribute.SOURCE];
            target = attributes[EDGEAttribute.TARGET];
            if (attributes.ContainsKey(EDGEAttribute.TYPE))
            {
                try
                {
                    type = EdgeType.ValueOf(ToConstantName(attributes[EDGEAttribute.TYPE]));
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "edge type should be one of 'undirected', 'undirected' or 'mutual'");
                }
            }

            switch (type)
            {
                case DIRECTED:
                    SendEdgeAdded(sourceId, id, source, target, true);
                    break;
                case MUTUAL:
                case UNDIRECTED:
                    SendEdgeAdded(sourceId, id, source, target, false);
                    break;
            }

            if (attributes.ContainsKey(EDGEAttribute.LABEL))
                SendEdgeAttributeAdded(sourceId, id, "ui.label", attributes[EDGEAttribute.LABEL]);
            if (attributes.ContainsKey(EDGEAttribute.WEIGHT))
            {
                try
                {
                    double d = Double.ValueOf(attributes[EDGEAttribute.WEIGHT]);
                    SendEdgeAttributeAdded(sourceId, id, "weight", d);
                }
                catch (NumberFormatException ex)
                {
                    NewParseError(e, true, "'weight' attribute of edge should be a real");
                }
            }

            e = GetNextEvent();
            while (!IsEvent(e, XMLEvent.END_ELEMENT, "edge"))
            {
                try
                {
                    Balise b = Balise.ValueOf(ToConstantName(e.AsStartElement().GetName().GetLocalPart()));
                    Pushback(e);
                    switch (b)
                    {
                        case ATTVALUES:
                            defined.AddAll(__attvalues(ClassType.EDGE, id));
                            break;
                        case SPELLS:
                            __spells();
                            break;
                        case COLOR:
                            __color(ClassType.EDGE, id);
                            break;
                        case THICKNESS:
                            __thickness(id);
                            break;
                        case SHAPE:
                            __edge_shape(id);
                            break;
                        default:
                            NewParseError(e, true, "edge children should be one of 'attvalues', 'color', 'thicknes', 'shape' or 'spells'");
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, true, "unknown tag '%s'", e.AsStartElement().GetName().GetLocalPart());
                }

                e = GetNextEvent();
            }

            foreach (string key in edgeAttributesDefinition.KeySet())
            {
                if (!defined.Contains(key))
                    SendEdgeAttributeAdded(sourceId, id, key, edgeAttributesDefinition[key].def);
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "edge");
        }

        private void __edge_shape(string edgeId)
        {
            XMLEvent e;
            EnumMap<EDGESHAPEAttribute, string> attributes;
            EdgeShapeType type;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "shape");
            attributes = GetAttributes(typeof(EDGESHAPEAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, EDGESHAPEAttribute.VALUE);
            try
            {
                type = EdgeShapeType.ValueOf(ToConstantName(attributes[EDGESHAPEAttribute.VALUE]));
            }
            catch (ArgumentException ex)
            {
                NewParseError(e, true, "'value' of shape should be one of 'solid', 'dotted', 'dashed' or 'double'");
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "shape");
        }

        private void __thickness(string edgeId)
        {
            XMLEvent e;
            EnumMap<THICKNESSAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "thickness");
            attributes = GetAttributes(typeof(THICKNESSAttribute), e.AsStartElement());
            CheckRequiredAttributes(e, attributes, THICKNESSAttribute.VALUE);
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "spells"))
            {
                Pushback(e);
                __spells();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "thickness");
        }
    }

    public interface GEXFConstants
    {
        public enum Balise
        {
            GEXF,
            GRAPH,
            META,
            CREATOR,
            KEYWORDS,
            DESCRIPTION,
            NODES,
            NODE,
            EDGES,
            EDGE,
            COLOR,
            POSITION,
            SIZE,
            SHAPE,
            THICKNESS,
            DEFAULT,
            OPTIONS,
            ATTVALUES,
            PARENTS,
            SPELLS
        }

        public enum GEXFAttribute
        {
            XMLNS,
            VERSION
        }

        public enum METAAttribute
        {
            LASTMODIFIEDDATE
        }

        public enum GRAPHAttribute
        {
            TIMEFORMAT,
            START,
            STARTOPEN,
            END,
            ENDOPEN,
            DEFAULTEDGETYPE,
            IDTYPE,
            MODE
        }

        public enum ATTRIBUTESAttribute
        {
            CLASS,
            MODE,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum ATTRIBUTEAttribute
        {
            ID,
            TITLE,
            TYPE
        }

        public enum NODESAttribute
        {
            COUNT
        }

        public enum NODEAttribute
        {
            START,
            STARTOPEN,
            END,
            ENDOPEN,
            PID,
            ID,
            LABEL
        }

        public enum ATTVALUEAttribute
        {
            FOR,
            VALUE,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum PARENTAttribute
        {
            FOR
        }

        public enum EDGESAttribute
        {
            COUNT
        }

        public enum SPELLAttribute
        {
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum COLORAttribute
        {
            R,
            G,
            B,
            A,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum POSITIONAttribute
        {
            X,
            Y,
            Z,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum SIZEAttribute
        {
            VALUE,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum NODESHAPEAttribute
        {
            VALUE,
            URI,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum EDGEAttribute
        {
            START,
            STARTOPEN,
            END,
            ENDOPEN,
            ID,
            TYPE,
            LABEL,
            SOURCE,
            TARGET,
            WEIGHT
        }

        public enum THICKNESSAttribute
        {
            VALUE,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum EDGESHAPEAttribute
        {
            VALUE,
            START,
            STARTOPEN,
            END,
            ENDOPEN
        }

        public enum IDType
        {
            INTEGER,
            STRING
        }

        public enum ModeType
        {
            STATIC,
            DYNAMIC
        }

        public enum WeightType
        {
            FLOAT
        }

        public enum EdgeType
        {
            DIRECTED,
            UNDIRECTED,
            MUTUAL
        }

        public enum NodeShapeType
        {
            DISC,
            SQUARE,
            TRIANGLE,
            DIAMOND,
            IMAGE
        }

        public enum EdgeShapeType
        {
            SOLID,
            DOTTED,
            DASHED,
            DOUBLE
        }

        public enum AttributeType
        {
            INTEGER,
            LONG,
            FLOAT,
            DOUBLE,
            BOOLEAN,
            ANYURI,
            LISTSTRING,
            STRING
        }

        public enum ClassType
        {
            NODE,
            EDGE
        }

        public enum TimeFormatType
        {
            INTEGER,
            DOUBLE,
            DATE,
            DATETIME
        }
    }
}
