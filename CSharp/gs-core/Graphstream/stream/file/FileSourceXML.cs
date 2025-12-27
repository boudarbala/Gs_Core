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

public abstract class FileSourceXML : SourceBase, FileSource, XMLStreamConstants
{
    private static readonly Logger LOGGER = Logger.GetLogger(typeof(FileSourceXML).GetName());
    protected XMLEventReader reader;
    private Stack<XMLEvent> events;
    protected bool strictMode = true;
    protected FileSourceXML()
    {
        events = new Stack();
    }

    public virtual bool IsStrictMode()
    {
        return strictMode;
    }

    public virtual void SetStrictMode(bool strictMode)
    {
        this.strictMode = strictMode;
    }

    public virtual void ReadAll(string fileName)
    {
        ReadAll(new FileReader(fileName));
    }

    public virtual void ReadAll(URL url)
    {
        ReadAll(url.OpenStream());
    }

    public virtual void ReadAll(InputStream stream)
    {
        ReadAll(new InputStreamReader(stream));
    }

    public virtual void ReadAll(Reader reader)
    {
        Begin(reader);
        while (NextEvents())
            ;
        End();
    }

    public virtual void Begin(string fileName)
    {
        Begin(new FileReader(fileName));
    }

    public virtual void Begin(URL url)
    {
        Begin(url.OpenStream());
    }

    public virtual void Begin(InputStream stream)
    {
        Begin(new InputStreamReader(stream));
    }

    public virtual void Begin(Reader reader)
    {
        OpenStream(reader);
    }

    protected abstract void AfterStartDocument();
    protected abstract void BeforeEndDocument();
    public abstract bool NextEvents();
    public virtual bool NextStep()
    {
        return NextEvents();
    }

    public virtual void End()
    {
        CloseStream();
    }

    protected virtual XMLEvent GetNextEvent()
    {
        SkipWhiteSpaces();
        if (events.Count > 0)
            return events.Pop();
        return reader.NextEvent();
    }

    protected virtual void Pushback(XMLEvent e)
    {
        events.Push(e);
    }

    protected virtual void NewParseError(XMLEvent e, bool critical, string msg, params object[] args)
    {
        if (!critical && !strictMode)
        {
            LOGGER.Warning(String.Format(msg, args));
        }
        else
        {
            throw new XMLStreamException(String.Format(msg, args), e.GetLocation());
        }
    }

    protected virtual bool IsEvent(XMLEvent e, int type, string name)
    {
        bool valid = e.GetEventType() == type;
        if (valid)
        {
            switch (type)
            {
                case START_ELEMENT:
                    valid = e.AsStartElement().GetName().GetLocalPart().Equals(name);
                    break;
                case END_ELEMENT:
                    valid = e.AsEndElement().GetName().GetLocalPart().Equals(name);
                    break;
                case ATTRIBUTE:
                    valid = ((Attribute)e).GetName().GetLocalPart().Equals(name);
                    break;
                case CHARACTERS:
                case NAMESPACE:
                case PROCESSING_INSTRUCTION:
                case COMMENT:
                case START_DOCUMENT:
                case END_DOCUMENT:
                case DTD:
            }
        }

        return valid;
    }

    protected virtual void CheckValid(XMLEvent e, int type, string name)
    {
        bool valid = IsEvent(e, type, name);
        if (!valid)
            NewParseError(e, true, "expecting %s, got %s", GotWhat(type, name), GotWhat(e));
    }

    private string GotWhat(XMLEvent e)
    {
        string v = null;
        switch (e.GetEventType())
        {
            case START_ELEMENT:
                v = e.AsStartElement().GetName().GetLocalPart();
                break;
            case END_ELEMENT:
                v = e.AsEndElement().GetName().GetLocalPart();
                break;
            case ATTRIBUTE:
                v = ((Attribute)e).GetName().GetLocalPart();
                break;
        }

        return GotWhat(e.GetEventType(), v);
    }

    private string GotWhat(int type, string v)
    {
        switch (type)
        {
            case START_ELEMENT:
                return String.Format("'<%s>'", v);
            case END_ELEMENT:
                return String.Format("'</%s>'", v);
            case ATTRIBUTE:
                return String.Format("attribute '%s'", v);
            case NAMESPACE:
                return "namespace";
            case PROCESSING_INSTRUCTION:
                return "processing instruction";
            case COMMENT:
                return "comment";
            case START_DOCUMENT:
                return "document start";
            case END_DOCUMENT:
                return "document end";
            case DTD:
                return "dtd";
            case CHARACTERS:
                return "characters";
            default:
                return "UNKNOWN";
                break;
        }
    }

    private void SkipWhiteSpaces()
    {
        XMLEvent e;
        do
        {
            if (events.Count > 0)
                e = events.Pop();
            else
                e = reader.NextEvent();
        }
        while (IsEvent(e, XMLEvent.CHARACTERS, null) && e.AsCharacters().GetData().Matches("^\\s*$"));
        Pushback(e);
    }

    protected virtual void OpenStream(Reader stream)
    {
        if (reader != null)
            CloseStream();
        try
        {
            XMLEvent e;
            reader = XMLInputFactory.NewInstance().CreateXMLEventReader(stream);
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_DOCUMENT, null);
            AfterStartDocument();
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

    protected virtual void CloseStream()
    {
        try
        {
            BeforeEndDocument();
            reader.Dispose();
        }
        catch (XMLStreamException e)
        {
            throw new IOException(e);
        }
        finally
        {
            reader = null;
        }
    }

    protected virtual string ToConstantName(Attribute a)
    {
        return ToConstantName(a.GetName().GetLocalPart());
    }

    protected virtual string ToConstantName(string value)
    {
        return value.ToUpperCase().ReplaceAll("\\W", "_");
    }

    protected class Parser
    {
        protected virtual string __characters()
        {
            XMLEvent e;
            StringBuilder buffer = new StringBuilder();
            e = GetNextEvent();
            while (e.GetEventType() == XMLEvent.CHARACTERS)
            {
                buffer.Append(e.AsCharacters().GetData());
                e = GetNextEvent();
            }

            Pushback(e);
            return buffer.ToString();
        }

        protected virtual EnumMap<T, string> GetAttributes<T extends Enum<T>>(Class<T> cls, StartElement e)
        {
            EnumMap<T, string> values = new EnumMap<T, string>(cls);
            IEnumerator<TWildcardTodoAttribute> attributes = e.AsStartElement().GetAttributes();
            while (attributes.HasNext())
            {
                Attribute a = attributes.Next();
                for (int i = 0; i < cls.GetEnumConstants().Length; i++)
                {
                    if (cls.GetEnumConstants()[i].Name().Equals(ToConstantName(a)))
                    {
                        values.Put(cls.GetEnumConstants()[i], a.GetValue());
                        break;
                    }
                }
            }

            return values;
        }

        protected virtual void CheckRequiredAttributes<T extends Enum<T>>(XMLEvent e, EnumMap<T, string> attributes, params T[] required)
        {
            if (required != null)
            {
                for (int i = 0; i < required.Length; i++)
                {
                    if (!attributes.ContainsKey(required[i]))
                        NewParseError(e, true, "'%s' attribute is required for <%s> element", required[i].Name().ToLowerCase(), e.AsStartElement().GetName().GetLocalPart());
                }
            }
        }
    }
}
