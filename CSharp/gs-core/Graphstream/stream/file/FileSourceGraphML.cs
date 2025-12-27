using Java.Io;
using Java.Net;
using Java.Util;
using Java.Util.Logging;
using Javax.Xml.Stream;
using Javax.Xml.Stream.Events;
using Gs_Core.Graphstream.Stream;
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
using static Org.Graphstream.Stream.File.GPXAttribute;
using static Org.Graphstream.Stream.File.WPTAttribute;
using static Org.Graphstream.Stream.File.LINKAttribute;
using static Org.Graphstream.Stream.File.EMAILAttribute;
using static Org.Graphstream.Stream.File.PTAttribute;
using static Org.Graphstream.Stream.File.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.FixType;
using static Org.Graphstream.Stream.File.GraphAttribute;
using static Org.Graphstream.Stream.File.LocatorAttribute;
using static Org.Graphstream.Stream.File.NodeAttribute;
using static Org.Graphstream.Stream.File.EdgeAttribute;
using static Org.Graphstream.Stream.File.DataAttribute;
using static Org.Graphstream.Stream.File.PortAttribute;
using static Org.Graphstream.Stream.File.EndPointAttribute;
using static Org.Graphstream.Stream.File.EndPointType;
using static Org.Graphstream.Stream.File.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.KeyAttribute;
using static Org.Graphstream.Stream.File.KeyDomain;
using static Org.Graphstream.Stream.File.KeyAttrType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceGraphML : FileSourceXML
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(FileSourceGraphML).GetName());
    public interface GraphMLConstants
    {
        enum Balise
        {
            GRAPHML,
            GRAPH,
            NODE,
            EDGE,
            HYPEREDGE,
            DESC,
            DATA,
            LOCATOR,
            PORT,
            KEY,
            DEFAULT
        }

        enum GraphAttribute
        {
            ID,
            EDGEDEFAULT
        }

        enum LocatorAttribute
        {
            XMLNS_XLINK,
            XLINK_HREF,
            XLINK_TYPE
        }

        enum NodeAttribute
        {
            ID
        }

        enum EdgeAttribute
        {
            ID,
            SOURCE,
            SOURCEPORT,
            TARGET,
            TARGETPORT,
            DIRECTED
        }

        enum DataAttribute
        {
            KEY,
            ID
        }

        enum PortAttribute
        {
            NAME
        }

        enum EndPointAttribute
        {
            ID,
            NODE,
            PORT,
            TYPE
        }

        enum EndPointType
        {
            IN,
            OUT,
            UNDIR
        }

        enum HyperEdgeAttribute
        {
            ID
        }

        enum KeyAttribute
        {
            ID,
            FOR,
            ATTR_NAME,
            ATTR_TYPE
        }

        enum KeyDomain
        {
            GRAPHML,
            GRAPH,
            NODE,
            EDGE,
            HYPEREDGE,
            PORT,
            ENDPOINT,
            ALL
        }

        enum KeyAttrType
        {
            BOOLEAN,
            INT,
            LONG,
            FLOAT,
            DOUBLE,
            STRING
        }

        class Key
        {
            KeyDomain domain;
            string name;
            KeyAttrType type;
            string def = null;
            Key()
            {
                domain = KeyDomain.ALL;
                name = null;
                type = KeyAttrType.STRING;
            }

            virtual object GetKeyValue(string value)
            {
                if (value == null)
                    return null;
                switch (type)
                {
                    case STRING:
                        return value;
                    case INT:
                        return Integer.ValueOf(value);
                    case LONG:
                        return Long.ValueOf(value);
                    case FLOAT:
                        return Float.ValueOf(value);
                    case DOUBLE:
                        return Double.ValueOf(value);
                    case BOOLEAN:
                        return Boolean.ValueOf(value);
                }

                return value;
            }

            virtual object GetDefaultValue()
            {
                return GetKeyValue(def);
            }
        }

        class Data
        {
            Key key;
            string id;
            string value;
        }

        class Locator
        {
            string href;
            string xlink;
            string type;
            Locator()
            {
                xlink = "http://www.w3.org/TR/2000/PR-xlink-20001220/";
                type = "simple";
                href = null;
            }
        }

        class Port
        {
            string name;
            string desc;
            LinkedList<Data> datas;
            LinkedList<Port> ports;
            Port()
            {
                name = null;
                desc = null;
                datas = new LinkedList<Data>();
                ports = new LinkedList<Port>();
            }
        }

        class EndPoint
        {
            string id;
            string node;
            string port;
            string desc;
            EndPointType type;
            EndPoint()
            {
                id = null;
                node = null;
                port = null;
                desc = null;
                type = EndPointType.UNDIR;
            }
        }
    }

    protected GraphMLParser parser;
    public FileSourceGraphML()
    {
    }

    protected override void AfterStartDocument()
    {
        parser = new GraphMLParser();
        parser.__graphml();
    }

    protected override void BeforeEndDocument()
    {
        parser = null;
    }

    public virtual bool NextEvents()
    {
        return false;
    }

    protected class GraphMLParser : Parser, GraphMLConstants
    {
        protected HashMap<string, Key> keys;
        protected Stack<string> graphId;
        protected int graphCounter;
        public GraphMLParser()
        {
            keys = new HashMap<string, Key>();
            graphId = new Stack<string>();
            graphCounter = 0;
        }

        private object GetValue(Data data)
        {
            return GetValue(data.key, data.value);
        }

        private object GetValue(Key key, string value)
        {
            switch (key.type)
            {
                case BOOLEAN:
                    return Boolean.ParseBoolean(value);
                case INT:
                    return Integer.ParseInt(value);
                case LONG:
                    return Long.ParseLong(value);
                case FLOAT:
                    return Float.ParseFloat(value);
                case DOUBLE:
                    return Double.ParseDouble(value);
                case STRING:
                    return value;
            }

            return value;
        }

        private object GetDefaultValue(Key key)
        {
            switch (key.type)
            {
                case BOOLEAN:
                    return Boolean.TRUE;
                case INT:
                    if (key.def != null)
                        return Integer.ValueOf(key.def);
                    return Integer.ValueOf(0);
                case LONG:
                    if (key.def != null)
                        return Long.ValueOf(key.def);
                    return Long.ValueOf(0);
                case FLOAT:
                    if (key.def != null)
                        return Float.ValueOf(key.def);
                    return Float.ValueOf(0F);
                case DOUBLE:
                    if (key.def != null)
                        return Double.ValueOf(key.def);
                    return Double.ValueOf(0);
                case STRING:
                    if (key.def != null)
                        return key.def;
                    return "";
            }

            return key.def != null ? key.def : Boolean.TRUE;
        }

        private void __graphml()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "graphml");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                __desc();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "key"))
            {
                Pushback(e);
                __key();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "data") || IsEvent(e, XMLEvent.START_ELEMENT, "graph"))
            {
                Pushback(e);
                if (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                {
                    __data();
                }
                else
                {
                    __graph();
                }

                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "graphml");
        }

        private string __desc()
        {
            XMLEvent e;
            string desc;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "desc");
            desc = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "desc");
            return desc;
        }

        private Locator __locator()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "locator");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            Locator loc = new Locator();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    LocatorAttribute attribute = LocatorAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case XMLNS_XLINK:
                            loc.xlink = a.GetValue();
                            break;
                        case XLINK_HREF:
                            loc.href = a.GetValue();
                            break;
                        case XLINK_TYPE:
                            loc.type = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid locator attribute '%s'", a.GetName().GetLocalPart());
                }
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "locator");
            if (loc.href == null)
                NewParseError(e, true, "locator requires an href");
            return loc;
        }

        private void __key()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "key");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            string id = null;
            KeyDomain domain = KeyDomain.ALL;
            KeyAttrType type = KeyAttrType.STRING;
            string name = null;
            string def = null;
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    KeyAttribute attribute = KeyAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case ID:
                            id = a.GetValue();
                            break;
                        case FOR:
                            try
                            {
                                domain = KeyDomain.ValueOf(ToConstantName(a.GetValue()));
                            }
                            catch (ArgumentException ex)
                            {
                                NewParseError(e, false, "invalid key domain '%s'", a.GetValue());
                            }

                            break;
                        case ATTR_TYPE:
                            try
                            {
                                type = KeyAttrType.ValueOf(ToConstantName(a.GetValue()));
                            }
                            catch (ArgumentException ex)
                            {
                                NewParseError(e, false, "invalid key type '%s'", a.GetValue());
                            }

                            break;
                        case ATTR_NAME:
                            name = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid key attribute '%s'", a.GetName().GetLocalPart());
                }
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "default"))
            {
                def = __characters();
                e = GetNextEvent();
                CheckValid(e, XMLEvent.END_ELEMENT, "default");
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "key");
            if (id == null)
                NewParseError(e, true, "key requires an id");
            if (name == null)
                name = id;
            Key k = new Key();
            k.name = name;
            k.domain = domain;
            k.type = type;
            k.def = def;
            keys.Put(id, k);
        }

        private Port __port()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "port");
            Port port = new Port();
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    PortAttribute attribute = PortAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case NAME:
                            port.name = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid attribute '%s' for '<port>'", a.GetName().GetLocalPart());
                }
            }

            if (port.name == null)
                NewParseError(e, true, "'<port>' element requires a 'name' attribute");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                port.desc = __desc();
            }
            else
            {
                while (IsEvent(e, XMLEvent.START_ELEMENT, "data") || IsEvent(e, XMLEvent.START_ELEMENT, "port"))
                {
                    if (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                    {
                        Data data;
                        Pushback(e);
                        data = __data();
                        port.datas.Add(data);
                    }
                    else
                    {
                        Port portChild;
                        Pushback(e);
                        portChild = __port();
                        port.ports.Add(portChild);
                    }

                    e = GetNextEvent();
                }
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "port");
            return port;
        }

        private EndPoint __endpoint()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "endpoint");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            EndPoint ep = new EndPoint();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    EndPointAttribute attribute = EndPointAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case NODE:
                            ep.node = a.GetValue();
                            break;
                        case ID:
                            ep.id = a.GetValue();
                            break;
                        case PORT:
                            ep.port = a.GetValue();
                            break;
                        case TYPE:
                            try
                            {
                                ep.type = EndPointType.ValueOf(ToConstantName(a.GetValue()));
                            }
                            catch (ArgumentException ex)
                            {
                                NewParseError(e, false, "invalid end point type '%s'", a.GetValue());
                            }

                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid attribute '%s' for '<endpoint>'", a.GetName().GetLocalPart());
                }
            }

            if (ep.node == null)
                NewParseError(e, true, "'<endpoint>' element requires a 'node' attribute");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                ep.desc = __desc();
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "endpoint");
            return ep;
        }

        private Data __data()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "data");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            string key = null, id = null, value;
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    DataAttribute attribute = DataAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case KEY:
                            key = a.GetValue();
                            break;
                        case ID:
                            id = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid attribute '%s' for '<data>'", a.GetName().GetLocalPart());
                }
            }

            if (key == null)
                NewParseError(e, true, "'<data>' element must have a 'key' attribute");
            value = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "data");
            if (!keys.ContainsKey(key))
                NewParseError(e, true, "unknown key '%s'", key);
            Data d = new Data();
            d.key = keys[key];
            d.id = id;
            d.value = value;
            return d;
        }

        private void __graph()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "graph");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            string id = null;
            string desc = null;
            bool directed = false;
            bool directedSet = false;
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    GraphAttribute attribute = GraphAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case ID:
                            id = a.GetValue();
                            break;
                        case EDGEDEFAULT:
                            if (a.GetValue().Equals("directed"))
                                directed = true;
                            else if (a.GetValue().Equals("undirected"))
                                directed = false;
                            else
                                NewParseError(e, true, "invalid 'edgedefault' value '%s'", a.GetValue());
                            directedSet = true;
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid node attribute '%s'", a.GetName().GetLocalPart());
                }
            }

            if (!directedSet)
                NewParseError(e, false, "graph requires attribute 'edgedefault'");
            string gid = "";
            if (graphId.Count > 0)
                gid = graphId.Peek() + ":";
            if (id != null)
                gid += id;
            else
                gid += Integer.ToString(graphCounter++);
            graphId.Push(gid);
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                desc = __desc();
                SendGraphAttributeAdded(sourceId, "desc", desc);
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "locator"))
            {
                Pushback(e);
                __locator();
                e = GetNextEvent();
            }
            else
            {
                while (IsEvent(e, XMLEvent.START_ELEMENT, "data") || IsEvent(e, XMLEvent.START_ELEMENT, "node") || IsEvent(e, XMLEvent.START_ELEMENT, "edge") || IsEvent(e, XMLEvent.START_ELEMENT, "hyperedge"))
                {
                    Pushback(e);
                    if (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                    {
                        Data data = __data();
                        SendGraphAttributeAdded(sourceId, data.key.name, GetValue(data));
                    }
                    else if (IsEvent(e, XMLEvent.START_ELEMENT, "node"))
                    {
                        __node();
                    }
                    else if (IsEvent(e, XMLEvent.START_ELEMENT, "edge"))
                    {
                        __edge(directed);
                    }
                    else
                    {
                        __hyperedge();
                    }

                    e = GetNextEvent();
                }
            }

            graphId.Pop();
            CheckValid(e, XMLEvent.END_ELEMENT, "graph");
        }

        private void __node()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "node");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            string id = null;
            HashSet<Key> sentAttributes = new HashSet<Key>();
            HashSet<Attribute> unexpectedAttributes = new HashSet();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    NodeAttribute attribute = NodeAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case ID:
                            id = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    if (strictMode)
                        NewParseError(e, false, "invalid node attribute '%s'", a.GetName().GetLocalPart());
                    unexpectedAttributes.Add(a);
                }
            }

            if (id == null)
                NewParseError(e, true, "node requires an id");
            SendNodeAdded(sourceId, id);
            if (!strictMode && unexpectedAttributes.Count > 0)
            {
                foreach (Attribute a in unexpectedAttributes)
                {
                    string name = a.GetName().GetLocalPart();
                    Key key = keys[name];
                    object value = key == null ? a.GetValue() : GetValue(key, a.GetValue());
                    SendNodeAttributeAdded(sourceId, id, name, value);
                    if (key != null)
                        sentAttributes.Add(key);
                }
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                string desc;
                Pushback(e);
                desc = __desc();
                SendNodeAttributeAdded(sourceId, id, "desc", desc);
            }
            else if (IsEvent(e, XMLEvent.START_ELEMENT, "locator"))
            {
                Pushback(e);
                __locator();
            }
            else
            {
                while (IsEvent(e, XMLEvent.START_ELEMENT, "data") || IsEvent(e, XMLEvent.START_ELEMENT, "port"))
                {
                    if (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                    {
                        Data data;
                        Pushback(e);
                        data = __data();
                        SendNodeAttributeAdded(sourceId, id, data.key.name, GetValue(data));
                        sentAttributes.Add(data.key);
                    }
                    else
                    {
                        Pushback(e);
                        __port();
                    }

                    e = GetNextEvent();
                }
            }

            foreach (Key k in keys.Values())
            {
                if ((k.domain == KeyDomain.NODE || k.domain == KeyDomain.ALL) && !sentAttributes.Contains(k))
                    SendNodeAttributeAdded(sourceId, id, k.name, GetDefaultValue(k));
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "graph"))
            {
                Location loc = e.GetLocation();
                System.err.Printf("[WARNING] %d:%d graph inside node is not implemented", loc.GetLineNumber(), loc.GetColumnNumber());
                Pushback(e);
                __graph();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "node");
        }

        private void __edge(bool edgedefault)
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "edge");
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            HashSet<Key> sentAttributes = new HashSet<Key>();
            HashSet<Attribute> unexpectedAttributes = new HashSet();
            string id = null;
            bool directed = edgedefault;
            string source = null;
            string target = null;
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    EdgeAttribute attribute = EdgeAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case ID:
                            id = a.GetValue();
                            break;
                        case DIRECTED:
                            directed = Boolean.ParseBoolean(a.GetValue());
                            break;
                        case SOURCE:
                            source = a.GetValue();
                            break;
                        case TARGET:
                            target = a.GetValue();
                            break;
                        case SOURCEPORT:
                        case TARGETPORT:
                            NewParseError(e, false, "sourceport and targetport not implemented");
                    }
                }
                catch (ArgumentException ex)
                {
                    if (strictMode)
                        NewParseError(e, false, "invalid graph attribute '%s'", a.GetName().GetLocalPart());
                    unexpectedAttributes.Add(a);
                }
            }

            if (source == null || target == null)
                NewParseError(e, true, "edge must have a source and a target");
            if (id == null)
            {
                id = String.Format("%s--%s", source, target);
            }

            SendEdgeAdded(sourceId, id, source, target, directed);
            if (!strictMode && unexpectedAttributes.Count > 0)
            {
                foreach (Attribute a in unexpectedAttributes)
                {
                    string name = a.GetName().GetLocalPart();
                    Key key = keys[name];
                    object value = key == null ? a.GetValue() : GetValue(key, a.GetValue());
                    SendEdgeAttributeAdded(sourceId, id, name, value);
                    if (key != null)
                        sentAttributes.Add(key);
                }
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                string desc;
                Pushback(e);
                desc = __desc();
                SendEdgeAttributeAdded(sourceId, id, "desc", desc);
            }
            else
            {
                while (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                {
                    Data data;
                    Pushback(e);
                    data = __data();
                    SendEdgeAttributeAdded(sourceId, id, data.key.name, GetValue(data));
                    sentAttributes.Add(data.key);
                    e = GetNextEvent();
                }
            }

            foreach (Key k in keys.Values())
            {
                if ((k.domain == KeyDomain.EDGE || k.domain == KeyDomain.ALL) && !sentAttributes.Contains(k))
                    SendEdgeAttributeAdded(sourceId, id, k.name, GetDefaultValue(k));
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "graph"))
            {
                NewParseError(e, false, "graph inside node is not implemented");
                Pushback(e);
                __graph();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "edge");
        }

        private void __hyperedge()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "hyperedge");
            NewParseError(e, false, "hyperedge feature is not implemented");
            string id = null;
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                try
                {
                    HyperEdgeAttribute attribute = HyperEdgeAttribute.ValueOf(ToConstantName(a));
                    switch (attribute)
                    {
                        case ID:
                            id = a.GetValue();
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    NewParseError(e, false, "invalid attribute '%s' for '<endpoint>'", a.GetName().GetLocalPart());
                }
            }

            if (id == null)
                NewParseError(e, true, "'<hyperedge>' element requires a 'node' attribute");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                __desc();
            }
            else
            {
                while (IsEvent(e, XMLEvent.START_ELEMENT, "data") || IsEvent(e, XMLEvent.START_ELEMENT, "endpoint"))
                {
                    if (IsEvent(e, XMLEvent.START_ELEMENT, "data"))
                    {
                        Pushback(e);
                        __data();
                    }
                    else
                    {
                        Pushback(e);
                        __endpoint();
                    }

                    e = GetNextEvent();
                }
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "graph"))
            {
                NewParseError(e, false, "graph inside node is not implemented");
                Pushback(e);
                __graph();
                e = GetNextEvent();
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "hyperedge");
        }
    }
}
