using Java.Io;
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

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceFactory
{
    public static FileSource SourceFor(string fileName)
    {
        File file = new File(fileName);
        if (!file.IsFile())
            throw new IOException("not a regular file '" + fileName + "'");
        if (!file.CanRead())
            throw new IOException("not a readable file '" + fileName + "'");
        RandomAccessFile in = new RandomAccessFile(fileName, "r");
        byte[] b = new byte[10];
        int n = @in.Read(b, 0, 10);
        @in.Dispose();
        if (n >= 3 && b[0] == 'D' && b[1] == 'G' && b[2] == 'S')
        {
            if (n >= 6 && b[3] == '0' && b[4] == '0')
            {
                if (b[5] == '1' || b[5] == '2')
                {
                    return new FileSourceDGS1And2();
                }
                else if (b[5] == '3' || b[5] == '4')
                {
                    return new FileSourceDGS();
                }
            }
        }

        if (n >= 7 && b[0] == 'g' && b[1] == 'r' && b[2] == 'a' && b[3] == 'p' && b[4] == 'h' && b[5] == ' ' && b[6] == '[')
        {
            return new FileSourceGML();
        }

        if (n >= 4 && b[0] == '(' && b[1] == 't' && b[2] == 'l' && b[3] == 'p')
            return new FileSourceTLP();
        string flc = fileName.ToLowerCase();
        if (flc.EndsWith(".dgs"))
        {
            return new FileSourceDGS();
        }

        if (flc.EndsWith(".gml") || flc.EndsWith(".dgml"))
        {
            return new FileSourceGML();
        }

        if (flc.EndsWith(".net"))
        {
            return new FileSourcePajek();
        }

        if (flc.EndsWith(".chaco") || flc.EndsWith(".graph"))
        {
        }

        if (flc.EndsWith(".dot"))
        {
            return new FileSourceDOT();
        }

        if (flc.EndsWith(".edge"))
        {
            return new FileSourceEdge();
        }

        if (flc.EndsWith(".lgl"))
        {
            return new FileSourceLGL();
        }

        if (flc.EndsWith(".ncol"))
        {
            return new FileSourceNCol();
        }

        if (flc.EndsWith(".tlp"))
        {
            return new FileSourceTLP();
        }

        if (flc.EndsWith(".xml"))
        {
            string root = GetXMLRootElement(fileName);
            if (root.EqualsIgnoreCase("gexf"))
                return new FileSourceGEXF();
            return new FileSourceGraphML();
        }

        if (flc.EndsWith(".gexf"))
        {
            return new FileSourceGEXF();
        }

        return null;
    }

    public static string GetXMLRootElement(string fileName)
    {
        FileReader stream = new FileReader(fileName);
        XMLEventReader reader;
        XMLEvent e;
        string root;
        try
        {
            reader = XMLInputFactory.NewInstance().CreateXMLEventReader(stream);
            do
            {
                e = reader.NextEvent();
            }
            while (!e.IsStartElement() && !e.IsEndDocument());
            if (e.IsEndDocument())
                throw new IOException("document ended before catching root element");
            root = e.AsStartElement().GetName().GetLocalPart();
            reader.Dispose();
            stream.Dispose();
            return root;
        }
        catch (XMLStreamException ex)
        {
            throw new IOException(ex);
        }
        catch (FactoryConfigurationError ex)
        {
            throw new IOException(ex);
        }
    }
}
