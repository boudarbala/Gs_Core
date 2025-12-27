using Gs_Core.Graphstream.Graph;
using Gs_Core.Graphstream.Util.Cumulative;
using Gs_Core.Graphstream.Util.Cumulative.CumulativeSpells;
using Java.Io;
using Java.Net;
using Java.Text;
using Java.Util;
using Java.Util.Function;
using Java.Util.Stream;
using Javax.Xml.Stream;
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

namespace Gs_Core.Graphstream.Stream.File;

public class FileSinkGEXF : FileSinkBase
{
    public enum TimeFormat
    {
        INTEGER,
        DOUBLE,
        DATE,
        DATETIME
    }

    XMLStreamWriter stream;
    bool smart;
    int depth;
    int currentAttributeIndex = 0;
    GraphSpells graphSpells;
    TimeFormat timeFormat;
    public FileSinkGEXF()
    {
        smart = true;
        depth = 0;
        graphSpells = null;
        timeFormat = TimeFormat.DOUBLE;
    }

    public virtual void SetTimeFormat(TimeFormat format)
    {
        this.timeFormat = format;
    }

    protected virtual void PutSpellAttributes(Spell s)
    {
        if (s.IsStarted())
        {
            string start = s.IsStartOpen() ? "startopen" : "start";
            string date = timeFormat.format.Format(s.GetStartDate());
            stream.WriteAttribute(start, date);
        }

        if (s.IsEnded())
        {
            string end = s.IsEndOpen() ? "endopen" : "end";
            string date = timeFormat.format.Format(s.GetEndDate());
            stream.WriteAttribute(end, date);
        }
    }

    protected virtual void OutputEndOfFile()
    {
        try
        {
            if (graphSpells != null)
            {
                ExportGraphSpells();
                graphSpells = null;
            }

            EndElement(stream, false);
            stream.WriteEndDocument();
            stream.Flush();
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
    }

    protected virtual void OutputHeader()
    {
        Calendar cal = Calendar.GetInstance();
        Date date = cal.GetTime();
        DateFormat df = DateFormat.GetDateTimeInstance(DateFormat.SHORT, DateFormat.SHORT);
        try
        {
            stream = XMLOutputFactory.NewFactory().CreateXMLStreamWriter(output);
            stream.WriteStartDocument("UTF-8", "1.0");
            StartElement(stream, "gexf");
            stream.WriteAttribute("xmlns", "http://www.gexf.net/1.2draft");
            stream.WriteAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            stream.WriteAttribute("xsi:schemaLocation", "http://www.gexf.net/1.2draft http://www.gexf.net/1.2draft/gexf.xsd");
            stream.WriteAttribute("version", "1.2");
            StartElement(stream, "meta");
            stream.WriteAttribute("lastmodifieddate", df.Format(date));
            StartElement(stream, "creator");
            stream.WriteCharacters("GraphStream - " + GetType().GetName());
            EndElement(stream, true);
            EndElement(stream, false);
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
        catch (FactoryConfigurationError e)
        {
            throw new IOException(e);
        }
    }

    protected virtual void StartElement(XMLStreamWriter stream, string name)
    {
        if (smart)
        {
            stream.WriteCharacters("\n");
            for (int i = 0; i < depth; i++)
                stream.WriteCharacters(" ");
        }

        stream.WriteStartElement(name);
        depth++;
    }

    protected virtual void EndElement(XMLStreamWriter stream, bool leaf)
    {
        depth--;
        if (smart && !leaf)
        {
            stream.WriteCharacters("\n");
            for (int i = 0; i < depth; i++)
                stream.WriteCharacters(" ");
        }

        stream.WriteEndElement();
    }

    protected override void ExportGraph(Graph g)
    {
        GEXFAttributeMap nodeAttributes = new GEXFAttributeMap("node", g);
        GEXFAttributeMap edgeAttributes = new GEXFAttributeMap("edge", g);
        Consumer<Exception> onException = Exception.PrintStackTrace();
        try
        {
            StartElement(stream, "graph");
            stream.WriteAttribute("defaultedgetype", "undirected");
            nodeAttributes.Export(stream);
            edgeAttributes.Export(stream);
            StartElement(stream, "nodes");
            g.Nodes().ForEach((n) =>
            {
                try
                {
                    StartElement(stream, "node");
                    stream.WriteAttribute("id", n.GetId());
                    if (n.HasAttribute("label"))
                        stream.WriteAttribute("label", n.GetAttribute("label").ToString());
                    if (n.GetAttributeCount() > 0)
                    {
                        StartElement(stream, "attvalues");
                        n.AttributeKeys().ForEach((key) =>
                        {
                            try
                            {
                                nodeAttributes.Push(stream, n, key);
                            }
                            catch (XMLStreamException e)
                            {
                                onException.Accept(e);
                            }
                        });
                        EndElement(stream, false);
                    }

                    EndElement(stream, n.GetAttributeCount() == 0);
                }
                catch (Exception ex)
                {
                    onException.Accept(ex);
                }
            });
            EndElement(stream, false);
            StartElement(stream, "edges");
            g.Edges().ForEach((e) =>
            {
                try
                {
                    StartElement(stream, "edge");
                    stream.WriteAttribute("id", e.GetId());
                    stream.WriteAttribute("source", e.GetSourceNode().GetId());
                    stream.WriteAttribute("target", e.GetTargetNode().GetId());
                    if (e.GetAttributeCount() > 0)
                    {
                        StartElement(stream, "attvalues");
                        e.AttributeKeys().ForEach((key) =>
                        {
                            try
                            {
                                edgeAttributes.Push(stream, e, key);
                            }
                            catch (XMLStreamException e1)
                            {
                                onException.Accept(e1);
                            }
                        });
                        EndElement(stream, false);
                    }

                    EndElement(stream, e.GetAttributeCount() == 0);
                }
                catch (Exception ex)
                {
                    onException.Accept(ex);
                }
            });
            EndElement(stream, false);
            EndElement(stream, false);
        }
        catch (XMLStreamException e1)
        {
            onException.Accept(e1);
        }
    }

    protected virtual void ExportGraphSpells()
    {
        GEXFAttributeMap nodeAttributes = new GEXFAttributeMap("node", graphSpells);
        GEXFAttributeMap edgeAttributes = new GEXFAttributeMap("edge", graphSpells);
        try
        {
            StartElement(stream, "graph");
            stream.WriteAttribute("mode", "dynamic");
            stream.WriteAttribute("defaultedgetype", "undirected");
            stream.WriteAttribute("timeformat", timeFormat.Name().ToLowerCase());
            nodeAttributes.Export(stream);
            edgeAttributes.Export(stream);
            StartElement(stream, "nodes");
            foreach (string nodeId in graphSpells.GetNodes())
            {
                StartElement(stream, "node");
                stream.WriteAttribute("id", nodeId);
                CumulativeAttributes attr = graphSpells.GetNodeAttributes(nodeId);
                object label = attr.GetAny("label");
                if (label != null)
                    stream.WriteAttribute("label", label.ToString());
                CumulativeSpells spells = graphSpells.GetNodeSpells(nodeId);
                if (!spells.IsEternal())
                {
                    StartElement(stream, "spells");
                    for (int i = 0; i < spells.GetSpellCount(); i++)
                    {
                        Spell s = spells.GetSpell(i);
                        StartElement(stream, "spell");
                        PutSpellAttributes(s);
                        EndElement(stream, true);
                    }

                    EndElement(stream, false);
                }

                if (attr.GetAttributesCount() > 0)
                {
                    StartElement(stream, "attvalues");
                    nodeAttributes.Push(stream, nodeId, graphSpells);
                    EndElement(stream, false);
                }

                EndElement(stream, spells.IsEternal() && attr.GetAttributesCount() == 0);
            }

            EndElement(stream, false);
            StartElement(stream, "edges");
            foreach (string edgeId in graphSpells.GetEdges())
            {
                StartElement(stream, "edge");
                GraphSpells.EdgeData data = graphSpells.GetEdgeData(edgeId);
                stream.WriteAttribute("id", edgeId);
                stream.WriteAttribute("source", data.GetSource());
                stream.WriteAttribute("target", data.GetTarget());
                CumulativeAttributes attr = graphSpells.GetEdgeAttributes(edgeId);
                CumulativeSpells spells = graphSpells.GetEdgeSpells(edgeId);
                if (!spells.IsEternal())
                {
                    StartElement(stream, "spells");
                    for (int i = 0; i < spells.GetSpellCount(); i++)
                    {
                        Spell s = spells.GetSpell(i);
                        StartElement(stream, "spell");
                        PutSpellAttributes(s);
                        EndElement(stream, true);
                    }

                    EndElement(stream, false);
                }

                if (attr.GetAttributesCount() > 0)
                {
                    StartElement(stream, "attvalues");
                    edgeAttributes.Push(stream, edgeId, graphSpells);
                    EndElement(stream, false);
                }

                EndElement(stream, spells.IsEternal() && attr.GetAttributesCount() == 0);
            }

            EndElement(stream, false);
            EndElement(stream, false);
        }
        catch (XMLStreamException e1)
        {
            e1.PrintStackTrace();
        }
    }

    protected virtual void CheckGraphSpells()
    {
        if (graphSpells == null)
            graphSpells = new GraphSpells();
    }

    public virtual void EdgeAttributeAdded(string sourceId, long timeId, string edgeId, string attribute, object value)
    {
        CheckGraphSpells();
        graphSpells.EdgeAttributeAdded(sourceId, timeId, edgeId, attribute, value);
    }

    public virtual void EdgeAttributeChanged(string sourceId, long timeId, string edgeId, string attribute, object oldValue, object newValue)
    {
        CheckGraphSpells();
        graphSpells.EdgeAttributeChanged(sourceId, timeId, edgeId, attribute, oldValue, newValue);
    }

    public virtual void EdgeAttributeRemoved(string sourceId, long timeId, string edgeId, string attribute)
    {
        CheckGraphSpells();
        graphSpells.EdgeAttributeRemoved(sourceId, timeId, edgeId, attribute);
    }

    public virtual void GraphAttributeAdded(string sourceId, long timeId, string attribute, object value)
    {
        CheckGraphSpells();
        graphSpells.GraphAttributeAdded(sourceId, timeId, attribute, value);
    }

    public virtual void GraphAttributeChanged(string sourceId, long timeId, string attribute, object oldValue, object newValue)
    {
        CheckGraphSpells();
        graphSpells.GraphAttributeChanged(sourceId, timeId, attribute, oldValue, newValue);
    }

    public virtual void GraphAttributeRemoved(string sourceId, long timeId, string attribute)
    {
        CheckGraphSpells();
        graphSpells.GraphAttributeRemoved(sourceId, timeId, attribute);
    }

    public virtual void NodeAttributeAdded(string sourceId, long timeId, string nodeId, string attribute, object value)
    {
        CheckGraphSpells();
        graphSpells.NodeAttributeAdded(sourceId, timeId, nodeId, attribute, value);
    }

    public virtual void NodeAttributeChanged(string sourceId, long timeId, string nodeId, string attribute, object oldValue, object newValue)
    {
        CheckGraphSpells();
        graphSpells.NodeAttributeChanged(sourceId, timeId, nodeId, attribute, oldValue, newValue);
    }

    public virtual void NodeAttributeRemoved(string sourceId, long timeId, string nodeId, string attribute)
    {
        CheckGraphSpells();
        graphSpells.NodeAttributeRemoved(sourceId, timeId, nodeId, attribute);
    }

    public virtual void EdgeAdded(string sourceId, long timeId, string edgeId, string fromNodeId, string toNodeId, bool directed)
    {
        CheckGraphSpells();
        graphSpells.EdgeAdded(sourceId, timeId, edgeId, fromNodeId, toNodeId, directed);
    }

    public virtual void EdgeRemoved(string sourceId, long timeId, string edgeId)
    {
        CheckGraphSpells();
        graphSpells.EdgeRemoved(sourceId, timeId, edgeId);
    }

    public virtual void GraphCleared(string sourceId, long timeId)
    {
        CheckGraphSpells();
        graphSpells.GraphCleared(sourceId, timeId);
    }

    public virtual void NodeAdded(string sourceId, long timeId, string nodeId)
    {
        CheckGraphSpells();
        graphSpells.NodeAdded(sourceId, timeId, nodeId);
    }

    public virtual void NodeRemoved(string sourceId, long timeId, string nodeId)
    {
        CheckGraphSpells();
        graphSpells.NodeRemoved(sourceId, timeId, nodeId);
    }

    public virtual void StepBegins(string sourceId, long timeId, double step)
    {
        CheckGraphSpells();
        graphSpells.StepBegins(sourceId, timeId, step);
    }

    class GEXFAttribute
    {
        int index;
        string key;
        string type;
        GEXFAttribute(string key, string type)
        {
            this.index = currentAttributeIndex++;
            this.key = key;
            this.type = type;
        }
    }

    class GEXFAttributeMap : HashMap<string, GEXFAttribute>
    {
        private static readonly long serialVersionUID = 6176508111522815024;
        protected string type;
        GEXFAttributeMap(string type, Graph g)
        {
            this.type = type;
            Stream<TWildcardTodoElement> stream;
            if (type.Equals("node"))
                stream = g.Nodes();
            else
                stream = g.Edges();
            stream.ForEach((e) =>
            {
                e.AttributeKeys().ForEach((key) =>
                {
                    object value = e.GetAttribute(key);
                    Check(key, value);
                });
            });
        }

        GEXFAttributeMap(string type, GraphSpells spells)
        {
            this.type = type;
            if (type.Equals("node"))
            {
                foreach (string nodeId in spells.GetNodes())
                {
                    CumulativeAttributes attr = spells.GetNodeAttributes(nodeId);
                    foreach (string key in attr.GetAttributes())
                    {
                        foreach (Spell s in attr.GetAttributeSpells(key))
                        {
                            object value = s.GetAttachedData();
                            Check(key, value);
                        }
                    }
                }
            }
            else
            {
                foreach (string edgeId in spells.GetEdges())
                {
                    CumulativeAttributes attr = spells.GetEdgeAttributes(edgeId);
                    foreach (string key in attr.GetAttributes())
                    {
                        foreach (Spell s in attr.GetAttributeSpells(key))
                        {
                            object value = s.GetAttachedData();
                            Check(key, value);
                        }
                    }
                }
            }
        }

        virtual void Check(string key, object value)
        {
            string id = GetID(key, value);
            string attType = "string";
            if (ContainsKey(id))
                return;
            if (value is int || value is Short)
                attType = "integer";
            else if (value is long)
                attType = "long";
            else if (value is float)
                attType = "float";
            else if (value is Double)
                attType = "double";
            else if (value is bool)
                attType = "boolean";
            else if (value is URL || value is URI)
                attType = "anyURI";
            else if (value.GetType().IsArray() || value is Collection)
                attType = "liststring";
            Put(id, new GEXFAttribute(key, attType));
        }

        virtual string GetID(string key, object value)
        {
            return String.Format("%s@%s", key, value.GetType().GetName());
        }

        virtual void Export(XMLStreamWriter stream)
        {
            if (Size() == 0)
                return;
            StartElement(stream, "attributes");
            stream.WriteAttribute("class", type);
            foreach (GEXFAttribute a in Values())
            {
                StartElement(stream, "attribute");
                stream.WriteAttribute("id", Integer.ToString(a.index));
                stream.WriteAttribute("title", a.key);
                stream.WriteAttribute("type", a.type);
                EndElement(stream, true);
            }

            EndElement(stream, Size() == 0);
        }

        virtual void Push(XMLStreamWriter stream, Element e, string key)
        {
            string id = GetID(key, e.GetAttribute(key));
            GEXFAttribute a = Get(id);
            if (a == null)
            {
                return;
            }

            StartElement(stream, "attvalue");
            stream.WriteAttribute("for", Integer.ToString(a.index));
            stream.WriteAttribute("value", e.GetAttribute(key).ToString());
            EndElement(stream, true);
        }

        virtual void Push(XMLStreamWriter stream, string elementId, GraphSpells spells)
        {
            CumulativeAttributes attr;
            if (type.Equals("node"))
                attr = spells.GetNodeAttributes(elementId);
            else
                attr = spells.GetEdgeAttributes(elementId);
            foreach (string key in attr.GetAttributes())
            {
                foreach (Spell s in attr.GetAttributeSpells(key))
                {
                    object value = s.GetAttachedData();
                    string id = GetID(key, value);
                    GEXFAttribute a = Get(id);
                    if (a == null)
                    {
                        return;
                    }

                    StartElement(stream, "attvalue");
                    stream.WriteAttribute("for", Integer.ToString(a.index));
                    stream.WriteAttribute("value", value.ToString());
                    PutSpellAttributes(s);
                    EndElement(stream, true);
                }
            }
        }
    }
}
