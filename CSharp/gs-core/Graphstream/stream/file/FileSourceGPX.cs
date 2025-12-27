using Java.Io;
using Java.Util;
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
using static Org.Graphstream.Stream.File.GPXAttribute;
using static Org.Graphstream.Stream.File.WPTAttribute;
using static Org.Graphstream.Stream.File.LINKAttribute;
using static Org.Graphstream.Stream.File.EMAILAttribute;
using static Org.Graphstream.Stream.File.PTAttribute;
using static Org.Graphstream.Stream.File.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.FixType;

namespace Gs_Core.Graphstream.Stream.File;

public class FileSourceGPX : FileSourceXML
{
    protected GPXParser parser;
    public FileSourceGPX()
    {
    }

    protected virtual void AfterStartDocument()
    {
        parser = new GPXParser();
        parser.__gpx();
    }

    protected virtual void BeforeEndDocument()
    {
        parser = null;
    }

    public virtual bool NextEvents()
    {
        return false;
    }

    protected class WayPoint
    {
        string name;
        double lat, lon, ele;
        HashMap<string, object> attributes;
        WayPoint()
        {
            attributes = new HashMap<string, object>();
            name = null;
            lat = lon = ele = 0;
        }

        virtual void Deploy()
        {
            SendNodeAdded(sourceId, name);
            SendNodeAttributeAdded(sourceId, name, "xyz", new double[] { lon, lat, ele });
            foreach (string key in attributes.KeySet())
                SendNodeAttributeAdded(sourceId, name, key, attributes[key]);
        }
    }

    protected class GPXParser : Parser, GPXConstants
    {
        int automaticPointId;
        int automaticRouteId;
        int automaticEdgeId;
        GPXParser()
        {
            automaticRouteId = 0;
            automaticPointId = 0;
            automaticEdgeId = 0;
        }

        private WayPoint Waypoint(string elementName)
        {
            XMLEvent e;
            WayPoint wp = new WayPoint();
            EnumMap<WPTAttribute, string> attributes;
            LinkedList<string> links = new LinkedList<string>();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, elementName);
            attributes = GetAttributes(typeof(WPTAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(WPTAttribute.LAT))
            {
                NewParseError(e, false, "attribute 'lat' is required");
            }

            if (!attributes.ContainsKey(WPTAttribute.LON))
            {
                NewParseError(e, false, "attribute 'lon' is required");
            }

            wp.lat = Double.ParseDouble(attributes[WPTAttribute.LAT]);
            wp.lon = Double.ParseDouble(attributes[WPTAttribute.LON]);
            wp.ele = 0;
            wp.attributes.Put("lat", wp.lat);
            wp.attributes.Put("lon", wp.lon);
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "ele"))
            {
                Pushback(e);
                wp.ele = __ele();
                wp.attributes.Put("ele", wp.ele);
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "time"))
            {
                Pushback(e);
                wp.attributes.Put("time", __time());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "magvar"))
            {
                Pushback(e);
                wp.attributes.Put("magvar", __magvar());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "geoidheight"))
            {
                Pushback(e);
                wp.attributes.Put("geoidheight", __geoidheight());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "name"))
            {
                Pushback(e);
                wp.name = __name();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "cmt"))
            {
                Pushback(e);
                wp.attributes.Put("cmt", __cmt());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                wp.attributes.Put("desc", __desc());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "src"))
            {
                Pushback(e);
                wp.attributes.Put("src", __src());
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "link"))
            {
                Pushback(e);
                links.Add(__link());
                e = GetNextEvent();
            }

            wp.attributes.Put("link", links.ToArray(new string[links.Count]));
            if (IsEvent(e, XMLEvent.START_ELEMENT, "sym"))
            {
                Pushback(e);
                wp.attributes.Put("sym", __sym());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "type"))
            {
                Pushback(e);
                wp.attributes.Put("type", __type());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "fix"))
            {
                Pushback(e);
                wp.attributes.Put("fix", __fix());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "sat"))
            {
                Pushback(e);
                wp.attributes.Put("sat", __sat());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "hdop"))
            {
                Pushback(e);
                wp.attributes.Put("hdop", __hdop());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "vdop"))
            {
                Pushback(e);
                wp.attributes.Put("vdop", __vdop());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "pdop"))
            {
                Pushback(e);
                wp.attributes.Put("pdop", __pdop());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "ageofdgpsdata"))
            {
                Pushback(e);
                wp.attributes.Put("ageofdgpsdata", __ageofdgpsdata());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "dgpsid"))
            {
                Pushback(e);
                wp.attributes.Put("dgpsid", __dgpsid());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, elementName);
            if (wp.name == null)
                wp.name = String.Format("wp#%08x", automaticPointId++);
            return wp;
        }

        private void __gpx()
        {
            XMLEvent e;
            EnumMap<GPXAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "gpx");
            attributes = GetAttributes(typeof(GPXAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(GPXAttribute.VERSION))
            {
                NewParseError(e, false, "attribute 'version' is required");
            }
            else
            {
                SendGraphAttributeAdded(sourceId, "gpx.version", attributes[GPXAttribute.VERSION]);
            }

            if (!attributes.ContainsKey(GPXAttribute.CREATOR))
            {
                NewParseError(e, false, "attribute 'creator' is required");
            }
            else
            {
                SendGraphAttributeAdded(sourceId, "gpx.creator", attributes[GPXAttribute.CREATOR]);
            }

            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "metadata"))
            {
                Pushback(e);
                __metadata();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "wpt"))
            {
                Pushback(e);
                __wpt();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "rte"))
            {
                Pushback(e);
                __rte();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "trk"))
            {
                Pushback(e);
                __trk();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "gpx");
        }

        private void __metadata()
        {
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "metadata");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "name"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.name", __name());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.desc", __desc());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "author"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.author", __author());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "copyright"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.copyright", __copyright());
                e = GetNextEvent();
            }

            LinkedList<string> links = new LinkedList<string>();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "link"))
            {
                Pushback(e);
                links.Add(__link());
                e = GetNextEvent();
            }

            if (links.Count > 0)
                SendGraphAttributeAdded(sourceId, "gpx.metadata.links", links.ToArray(new string[links.Count]));
            if (IsEvent(e, XMLEvent.START_ELEMENT, "time"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.time", __time());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "keywords"))
            {
                Pushback(e);
                SendGraphAttributeAdded(sourceId, "gpx.metadata.keywords", __keywords());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "bounds"))
            {
                Pushback(e);
                __bounds();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "metadata");
        }

        private void __wpt()
        {
            WayPoint wp = Waypoint("wpt");
            wp.Deploy();
        }

        private void __rte()
        {
            XMLEvent e;
            string name, cmt, desc, src, type, time;
            int number;
            LinkedList<string> links = new LinkedList<string>();
            LinkedList<WayPoint> points = new LinkedList<WayPoint>();
            name = cmt = desc = src = type = time = null;
            number = -1;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "rte");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "name"))
            {
                Pushback(e);
                name = __name();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "time"))
            {
                Pushback(e);
                time = __time();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "cmt"))
            {
                Pushback(e);
                cmt = __cmt();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                desc = __desc();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "src"))
            {
                Pushback(e);
                src = __src();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "link"))
            {
                Pushback(e);
                links.Add(__link());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "number"))
            {
                Pushback(e);
                number = __number();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "type"))
            {
                Pushback(e);
                type = __type();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "rtept"))
            {
                Pushback(e);
                points.AddLast(__rtept());
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "rte");
            if (name == null)
                name = String.Format("route#%08x", automaticRouteId++);
            SendGraphAttributeAdded(sourceId, "routes." + name, Boolean.TRUE);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".desc", desc);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".cmt", cmt);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".src", src);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".type", type);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".time", time);
            SendGraphAttributeAdded(sourceId, "routes." + name + ".number", number);
            for (int i = 0; i < points.Count; i++)
            {
                points[i].Deploy();
                if (i > 0)
                {
                    string eid = String.Format("seg#%08x", automaticEdgeId++);
                    SendEdgeAdded(sourceId, eid, points[i - 1].name, points[i].name, true);
                    SendEdgeAttributeAdded(sourceId, eid, "route", name);
                }
            }
        }

        private void __trk()
        {
            XMLEvent e;
            string name, cmt, desc, src, type, time;
            int number;
            LinkedList<string> links = new LinkedList<string>();
            name = cmt = desc = src = type = time = null;
            number = -1;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "trk");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "name"))
            {
                Pushback(e);
                name = __name();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "time"))
            {
                Pushback(e);
                time = __time();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "cmt"))
            {
                Pushback(e);
                cmt = __cmt();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "desc"))
            {
                Pushback(e);
                desc = __desc();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "src"))
            {
                Pushback(e);
                src = __src();
                e = GetNextEvent();
            }

            while (IsEvent(e, XMLEvent.START_ELEMENT, "link"))
            {
                Pushback(e);
                links.Add(__link());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "number"))
            {
                Pushback(e);
                number = __number();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "type"))
            {
                Pushback(e);
                type = __type();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            if (name == null)
                name = String.Format("route#%08x", automaticRouteId++);
            SendGraphAttributeAdded(sourceId, "tracks." + name, Boolean.TRUE);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".desc", desc);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".cmt", cmt);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".src", src);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".type", type);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".time", time);
            SendGraphAttributeAdded(sourceId, "tracks." + name + ".number", number);
            while (IsEvent(e, XMLEvent.START_ELEMENT, "trkseg"))
            {
                Pushback(e);
                IList<WayPoint> wps = __trkseg();
                for (int i = 0; i < wps.Count; i++)
                {
                    wps[i].Deploy();
                    if (i > 0)
                    {
                        string eid = String.Format("seg#%08x", automaticEdgeId++);
                        SendEdgeAdded(sourceId, eid, wps[i - 1].name, wps[i].name, true);
                        SendEdgeAttributeAdded(sourceId, eid, "route", name);
                    }
                }

                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "trk");
        }

        private void __extensions()
        {
            XMLEvent e;
            int stack = 0;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "extensions");
            e = GetNextEvent();
            while (!(IsEvent(e, XMLEvent.END_ELEMENT, "extensions") && stack == 0))
            {
                if (IsEvent(e, XMLEvent.END_ELEMENT, "extensions"))
                    stack--;
                else if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
                    stack++;
                e = GetNextEvent();
            }
        }

        private string __name()
        {
            string name;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "name");
            name = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "name");
            return name;
        }

        private string __desc()
        {
            string desc;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "desc");
            desc = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "desc");
            return desc;
        }

        private string __author()
        {
            string author = "";
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "author");
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "name"))
            {
                Pushback(e);
                author += __name();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "email"))
            {
                Pushback(e);
                author += " <" + __email() + ">";
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "link"))
            {
                Pushback(e);
                author += " (" + __link() + ")";
                e = GetNextEvent();
            }

            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "author");
            return author;
        }

        private string __copyright()
        {
            string copyright;
            XMLEvent e;
            EnumMap<COPYRIGHTAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "copyright");
            attributes = GetAttributes(typeof(COPYRIGHTAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(COPYRIGHTAttribute.AUTHOR))
            {
                NewParseError(e, false, "attribute 'author' is required");
                copyright = "unknown";
            }
            else
                copyright = attributes[COPYRIGHTAttribute.AUTHOR];
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "year"))
            {
                Pushback(e);
                copyright += " " + __year();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "license"))
            {
                Pushback(e);
                copyright += " " + __license();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "copyright");
            return copyright;
        }

        private string __link()
        {
            string link;
            XMLEvent e;
            EnumMap<LINKAttribute, string> attributes;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "link");
            attributes = GetAttributes(typeof(LINKAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(LINKAttribute.HREF))
            {
                NewParseError(e, false, "attribute 'href' is required");
                link = "unknown";
            }
            else
                link = attributes[LINKAttribute.HREF];
            e = GetNextEvent();
            if (IsEvent(e, XMLEvent.START_ELEMENT, "text"))
            {
                Pushback(e);
                __text();
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "type"))
            {
                Pushback(e);
                __type();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "link");
            return link;
        }

        private string __time()
        {
            string time;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "time");
            time = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "time");
            return time;
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

        private void __bounds()
        {
            XMLEvent e;
            EnumMap<BOUNDSAttribute, string> attributes;
            double minlat, maxlat, minlon, maxlon;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "bounds");
            attributes = GetAttributes(typeof(BOUNDSAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(BOUNDSAttribute.MINLAT))
            {
                NewParseError(e, false, "attribute 'minlat' is required");
            }

            if (!attributes.ContainsKey(BOUNDSAttribute.MAXLAT))
            {
                NewParseError(e, false, "attribute 'maxlat' is required");
            }

            if (!attributes.ContainsKey(BOUNDSAttribute.MINLON))
            {
                NewParseError(e, false, "attribute 'minlon' is required");
            }

            if (!attributes.ContainsKey(BOUNDSAttribute.MAXLON))
            {
                NewParseError(e, false, "attribute 'maxlon' is required");
            }

            minlat = Double.ParseDouble(attributes[BOUNDSAttribute.MINLAT]);
            maxlat = Double.ParseDouble(attributes[BOUNDSAttribute.MAXLAT]);
            minlon = Double.ParseDouble(attributes[BOUNDSAttribute.MINLON]);
            maxlon = Double.ParseDouble(attributes[BOUNDSAttribute.MAXLON]);
            SendGraphAttributeAdded(sourceId, "gpx.bounds", new double[] { minlat, minlon, maxlat, maxlon });
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "bounds");
        }

        private double __ele()
        {
            string ele;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "ele");
            ele = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "ele");
            return Double.ParseDouble(ele);
        }

        private double __magvar()
        {
            string magvar;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "magvar");
            magvar = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "magvar");
            return Double.ParseDouble(magvar);
        }

        private double __geoidheight()
        {
            string geoidheight;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "geoidheight");
            geoidheight = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "geoidheight");
            return Double.ParseDouble(geoidheight);
        }

        private string __cmt()
        {
            string cmt;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "cmt");
            cmt = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "cmt");
            return cmt;
        }

        private string __src()
        {
            string src;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "src");
            src = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "src");
            return src;
        }

        private string __sym()
        {
            string sym;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "sym");
            sym = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "sym");
            return sym;
        }

        private string __text()
        {
            string text;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "text");
            text = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "text");
            return text;
        }

        private string __type()
        {
            string type;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "type");
            type = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "type");
            return type;
        }

        private string __fix()
        {
            string fix;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "fix");
            fix = __characters();
            if (!fix.ToLowerCase().Matches("^(none|2d|3d|dgps|pps)$"))
                NewParseError(e, true, "invalid fix type, expecting one of 'none', '2d', '3d', 'dgps', 'pps'");
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "fix");
            return fix;
        }

        private int __sat()
        {
            string sat;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "sat");
            sat = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "sat");
            return Integer.ParseInt(sat);
        }

        private double __hdop()
        {
            string hdop;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "hdop");
            hdop = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "hdop");
            return Double.ParseDouble(hdop);
        }

        private double __vdop()
        {
            string vdop;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "vdop");
            vdop = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "vdop");
            return Double.ParseDouble(vdop);
        }

        private double __pdop()
        {
            string pdop;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "pdop");
            pdop = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "pdop");
            return Double.ParseDouble(pdop);
        }

        private double __ageofdgpsdata()
        {
            string ageofdgpsdata;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "ageofdgpsdata");
            ageofdgpsdata = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "ageofdgpsdata");
            return Double.ParseDouble(ageofdgpsdata);
        }

        private int __dgpsid()
        {
            string dgpsid;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "dgpsid");
            dgpsid = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "dgpsid");
            return Integer.ParseInt(dgpsid);
        }

        private int __number()
        {
            string number;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "number");
            number = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "number");
            return Integer.ParseInt(number);
        }

        private WayPoint __rtept()
        {
            return Waypoint("rtept");
        }

        private WayPoint __trkpt()
        {
            return Waypoint("trkpt");
        }

        private IList<WayPoint> __trkseg()
        {
            LinkedList<WayPoint> points = new LinkedList<WayPoint>();
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "trkseg");
            e = GetNextEvent();
            while (IsEvent(e, XMLEvent.START_ELEMENT, "trkpt"))
            {
                Pushback(e);
                points.AddLast(__trkpt());
                e = GetNextEvent();
            }

            if (IsEvent(e, XMLEvent.START_ELEMENT, "extensions"))
            {
                Pushback(e);
                __extensions();
                e = GetNextEvent();
            }

            CheckValid(e, XMLEvent.END_ELEMENT, "trkseg");
            return points;
        }

        private string __email()
        {
            XMLEvent e;
            EnumMap<EMAILAttribute, string> attributes;
            string email = "";
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "email");
            attributes = GetAttributes(typeof(EMAILAttribute), e.AsStartElement());
            if (!attributes.ContainsKey(EMAILAttribute.ID))
            {
                NewParseError(e, false, "attribute 'version' is required");
            }
            else
                email += attributes[EMAILAttribute.ID];
            email += "@";
            if (!attributes.ContainsKey(EMAILAttribute.DOMAIN))
            {
                NewParseError(e, false, "attribute 'version' is required");
            }
            else
                email += attributes[EMAILAttribute.DOMAIN];
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "email");
            return email;
        }

        private string __year()
        {
            string year;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "year");
            year = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "year");
            return year;
        }

        private string __license()
        {
            string license;
            XMLEvent e;
            e = GetNextEvent();
            CheckValid(e, XMLEvent.START_ELEMENT, "license");
            license = __characters();
            e = GetNextEvent();
            CheckValid(e, XMLEvent.END_ELEMENT, "license");
            return license;
        }
    }

    public interface GPXConstants
    {
        public enum Balise
        {
            GPX,
            METADATA,
            WPT,
            RTE,
            TRK,
            EXTENSIONS,
            NAME,
            DESC,
            AUTHOR,
            COPYRIGHT,
            LINK,
            TIME,
            KEYWORDS,
            BOUNDS,
            ELE,
            MAGVAR,
            GEOIDHEIGHT,
            CMT,
            SRC,
            SYM,
            TYPE,
            FIX,
            SAT,
            HDOP,
            VDOP,
            PDOP,
            AGEOFDGPSDATA,
            DGPSID,
            NUMBER,
            RTEPT,
            TRKSEG,
            TRKPT,
            YEAR,
            LICENCE,
            TEXT,
            EMAIL,
            PT
        }

        public enum GPXAttribute
        {
            CREATOR,
            VERSION
        }

        public enum WPTAttribute
        {
            LAT,
            LON
        }

        public enum LINKAttribute
        {
            HREF
        }

        public enum EMAILAttribute
        {
            ID,
            DOMAIN
        }

        public enum PTAttribute
        {
            LAT,
            LON
        }

        public enum BOUNDSAttribute
        {
            MINLAT,
            MAXLAT,
            MINLON,
            MAXLON
        }

        public enum COPYRIGHTAttribute
        {
            AUTHOR
        }

        public enum FixType
        {
            T_NONE,
            T_2D,
            T_3D,
            T_DGPS,
            T_PPS
        }
    }
}
