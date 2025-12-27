using Java.Io;
using Gs_Core.Graphstream.Ui.GraphicGraph;
using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ElementType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EventType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttributeChangeEvent;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Mode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.What;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TimeFormat;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.OutputType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.OutputPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LayoutPolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Quality;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Option;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttributeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Balise;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GEXFAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.METAAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GRAPHAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTRIBUTESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTRIBUTEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ATTVALUEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PARENTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGESAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SPELLAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.COLORAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.POSITIONAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SIZEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NODESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.THICKNESSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EDGESHAPEAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.IDType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ModeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.WeightType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NodeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeShapeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ClassType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TimeFormatType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GPXAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.WPTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LINKAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EMAILAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.BOUNDSAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.COPYRIGHTAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.FixType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GraphAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.LocatorAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.NodeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.DataAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PortAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EndPointAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.EndPointType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.HyperEdgeAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyAttribute;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyDomain;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.KeyAttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.GraphEvents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ThreadingModel;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.CloseFramePolicy;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Measures;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Token;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Extension;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.DefaultEdgeType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.AttrType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Resolutions;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.PropertyType;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Type;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Units;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.FillMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.StrokeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ShadowMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.VisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextVisibilityMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextStyle;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.IconMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SizeMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextAlignment;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.TextBackgroundMode;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ShapeKind;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.Shape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.SpriteOrientation;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.ArrowShape;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.JComponents;
using static Org.Graphstream.Ui.GraphicGraph.Stylesheet.Parser.InteractiveElement;

namespace Gs_Core.Graphstream.Ui.GraphicGraph.Stylesheet.Parser;

public class StyleSheetParser : StyleSheetParserConstants
{
    protected StyleSheet stylesheet;
    public StyleSheetParser(StyleSheet stylesheet, InputStream stream) : this(stream)
    {
        this.stylesheet = stylesheet;
    }

    public StyleSheetParser(StyleSheet stylesheet, Reader stream) : this(stream)
    {
        this.stylesheet = stylesheet;
    }

    public class Number
    {
        public float value;
        public Style.Units units = Style.Units.PX;
        public Number(float value, Style.Units units)
        {
            this.value = value;
            this.units = units;
        }
    }

    public virtual void Dispose()
    {
        jj_input_stream.Dispose();
    }

    public void Start()
    {
        Rule r;
        label_1:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case GRAPH:
                    case EDGE:
                    case NODE:
                    case SPRITE:
                    case COMMENT:
                        break;
                    default:
                        jj_la1[0] = jj_gen;
                        break;
                }

                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case GRAPH:
                    case EDGE:
                    case NODE:
                    case SPRITE:
                        r = Rule();
                        stylesheet.AddRule(r);
                        break;
                    case COMMENT:
                        Jj_consume_token(COMMENT);
                        break;
                    default:
                        jj_la1[1] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                        break;
                }
            }

        Jj_consume_token(0);
    }

    public Rule Rule()
    {
        Selector select;
        Style style;
        Rule rule;
        select = Select();
        rule = new Rule(select);
        style = new Style();
        rule.SetStyle(style);
        Jj_consume_token(LBRACE);
        Styles(style);
        Jj_consume_token(RBRACE);
        {
            if (true)
                return rule;
        }

        throw new Exception("Missing return statement in function");
    }

    public void Styles(Style style)
    {
        label_2:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case FILLMODE:
                    case FILLCOLOR:
                    case FILLIMAGE:
                    case STROKEMODE:
                    case STROKECOLOR:
                    case STROKEWIDTH:
                    case SHADOWMODE:
                    case SHADOWCOLOR:
                    case SHADOWWIDTH:
                    case SHADOWOFFSET:
                    case TEXTMODE:
                    case TEXTCOLOR:
                    case TEXTSTYLE:
                    case TEXTFONT:
                    case TEXTSIZE:
                    case TEXTVISIBILITYMODE:
                    case TEXTVISIBILITY:
                    case TEXTBACKGROUNDMODE:
                    case TEXTBACKGROUNDCOLOR:
                    case TEXTOFFSET:
                    case TEXTPADDING:
                    case ICONMODE:
                    case ICON:
                    case PADDING:
                    case ZINDEX:
                    case VISIBILITYMODE:
                    case VISIBILITY:
                    case SHAPE:
                    case SIZE:
                    case SIZEMODE:
                    case SHAPEPOINTS:
                    case TEXTALIGNMENT:
                    case JCOMPONENT:
                    case ARROWIMGURL:
                    case ARROWSIZE:
                    case ARROWSHAPE:
                    case SPRITEORIENT:
                    case CANVASCOLOR:
                    case COMMENT:
                        break;
                    default:
                        jj_la1[2] = jj_gen;
                        break;
                }

                Style(style);
            }
    }

    public void StylesStart(Style style)
    {
        label_3:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case FILLMODE:
                    case FILLCOLOR:
                    case FILLIMAGE:
                    case STROKEMODE:
                    case STROKECOLOR:
                    case STROKEWIDTH:
                    case SHADOWMODE:
                    case SHADOWCOLOR:
                    case SHADOWWIDTH:
                    case SHADOWOFFSET:
                    case TEXTMODE:
                    case TEXTCOLOR:
                    case TEXTSTYLE:
                    case TEXTFONT:
                    case TEXTSIZE:
                    case TEXTVISIBILITYMODE:
                    case TEXTVISIBILITY:
                    case TEXTBACKGROUNDMODE:
                    case TEXTBACKGROUNDCOLOR:
                    case TEXTOFFSET:
                    case TEXTPADDING:
                    case ICONMODE:
                    case ICON:
                    case PADDING:
                    case ZINDEX:
                    case VISIBILITYMODE:
                    case VISIBILITY:
                    case SHAPE:
                    case SIZE:
                    case SIZEMODE:
                    case SHAPEPOINTS:
                    case TEXTALIGNMENT:
                    case JCOMPONENT:
                    case ARROWIMGURL:
                    case ARROWSIZE:
                    case ARROWSHAPE:
                    case SPRITEORIENT:
                    case CANVASCOLOR:
                    case COMMENT:
                        break;
                    default:
                        jj_la1[3] = jj_gen;
                        break;
                }

                Style(style);
            }

        Jj_consume_token(0);
    }

    public Selector Select()
    {
        Token t;
        Selector select;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case GRAPH:
                Jj_consume_token(GRAPH);
                select = new Selector(Selector.Type.GRAPH);
                break;
            case NODE:
                Jj_consume_token(NODE);
                select = new Selector(Selector.Type.NODE);
                break;
            case EDGE:
                Jj_consume_token(EDGE);
                select = new Selector(Selector.Type.EDGE);
                break;
            case SPRITE:
                Jj_consume_token(SPRITE);
                select = new Selector(Selector.Type.SPRITE);
                break;
            default:
                jj_la1[4] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case DOT:
            case SHARP:
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case SHARP:
                        Jj_consume_token(SHARP);
                        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                        {
                            case IDENTIFIER:
                                t = Jj_consume_token(IDENTIFIER);
                                select.SetId(t.image);
                                break;
                            case STRING:
                                t = Jj_consume_token(STRING);
                                select.SetId(t.image.Substring(1, t.image.Length - 1));
                                break;
                            case REAL:
                                t = Jj_consume_token(REAL);
                                select.SetId(t.image.ToString());
                                break;
                            default:
                                jj_la1[5] = jj_gen;
                                Jj_consume_token(-1);
                                throw new ParseException();
                                break;
                        }

                        break;
                    case DOT:
                        Jj_consume_token(DOT);
                        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                        {
                            case IDENTIFIER:
                                t = Jj_consume_token(IDENTIFIER);
                                select.SetClass(t.image);
                                break;
                            case CLICKED:
                                t = Jj_consume_token(CLICKED);
                                select.SetClass("clicked");
                                break;
                            case SELECTED:
                                t = Jj_consume_token(SELECTED);
                                select.SetClass("selected");
                                break;
                            case STRING:
                                t = Jj_consume_token(STRING);
                                select.SetClass(t.image.Substring(1, t.image.Length - 1));
                                break;
                            default:
                                jj_la1[6] = jj_gen;
                                Jj_consume_token(-1);
                                throw new ParseException();
                                break;
                        }

                        break;
                    default:
                        jj_la1[7] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                        break;
                }

                break;
            default:
                jj_la1[8] = jj_gen;
                break;
        }

        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case COLON:
                Jj_consume_token(COLON);
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case CLICKED:
                        Jj_consume_token(CLICKED);
                        select.SetPseudoClass("clicked");
                        break;
                    case SELECTED:
                        Jj_consume_token(SELECTED);
                        select.SetPseudoClass("selected");
                        break;
                    case STRING:
                        t = Jj_consume_token(STRING);
                        select.SetPseudoClass(t.image.Substring(1, t.image.Length - 1));
                        break;
                    case IDENTIFIER:
                        t = Jj_consume_token(IDENTIFIER);
                        select.SetPseudoClass(t.image);
                        break;
                    default:
                        jj_la1[9] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                        break;
                }

                break;
            default:
                jj_la1[10] = jj_gen;
                break;
        }

        {
            if (true)
                return select;
        }

        throw new Exception("Missing return statement in function");
    }

    public void Style(Style style)
    {
        Color color;
        Colors colors;
        string url;
        Value value;
        Values values;
        Style.FillMode fillMode;
        Style.StrokeMode strokeMode;
        Style.ShadowMode shadowMode;
        Style.TextMode textMode;
        Style.TextVisibilityMode textVisMode;
        Style.TextBackgroundMode textBgMode;
        Style.TextStyle textStyle;
        Style.TextAlignment textAlignment;
        Style.IconMode iconMode;
        Style.VisibilityMode visMode;
        Style.SizeMode sizeMode;
        Style.Shape shape;
        Style.SpriteOrientation spriteOrient;
        Style.ArrowShape arrowShape;
        Style.JComponents component;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case COMMENT:
                Jj_consume_token(COMMENT);
                break;
            case ZINDEX:
                Jj_consume_token(ZINDEX);
                Jj_consume_token(COLON);
                value = Value();
                Jj_consume_token(SEMICOLON);
                style.SetValue("z-index", new int ((int)value.value));
                break;
            case FILLMODE:
                Jj_consume_token(FILLMODE);
                Jj_consume_token(COLON);
                fillMode = FillMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("fill-mode", fillMode);
                break;
            case FILLCOLOR:
                Jj_consume_token(FILLCOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("fill-color", colors);
                break;
            case FILLIMAGE:
                Jj_consume_token(FILLIMAGE);
                Jj_consume_token(COLON);
                url = Url();
                Jj_consume_token(SEMICOLON);
                style.SetValue("fill-image", url);
                break;
            case STROKEMODE:
                Jj_consume_token(STROKEMODE);
                Jj_consume_token(COLON);
                strokeMode = StrokeMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("stroke-mode", strokeMode);
                break;
            case STROKECOLOR:
                Jj_consume_token(STROKECOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("stroke-color", colors);
                break;
            case STROKEWIDTH:
                Jj_consume_token(STROKEWIDTH);
                Jj_consume_token(COLON);
                value = Value();
                Jj_consume_token(SEMICOLON);
                style.SetValue("stroke-width", value);
                break;
            case SHADOWMODE:
                Jj_consume_token(SHADOWMODE);
                Jj_consume_token(COLON);
                shadowMode = ShadowMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shadow-mode", shadowMode);
                break;
            case SHADOWCOLOR:
                Jj_consume_token(SHADOWCOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shadow-color", colors);
                break;
            case SHADOWWIDTH:
                Jj_consume_token(SHADOWWIDTH);
                Jj_consume_token(COLON);
                value = Value();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shadow-width", value);
                break;
            case SHADOWOFFSET:
                Jj_consume_token(SHADOWOFFSET);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shadow-offset", values);
                break;
            case PADDING:
                Jj_consume_token(PADDING);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("padding", values);
                break;
            case TEXTMODE:
                Jj_consume_token(TEXTMODE);
                Jj_consume_token(COLON);
                textMode = TextMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-mode", textMode);
                break;
            case TEXTVISIBILITYMODE:
                Jj_consume_token(TEXTVISIBILITYMODE);
                Jj_consume_token(COLON);
                textVisMode = TextVisMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-visibility-mode", textVisMode);
                break;
            case TEXTVISIBILITY:
                Jj_consume_token(TEXTVISIBILITY);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-visibility", values);
                break;
            case TEXTBACKGROUNDMODE:
                Jj_consume_token(TEXTBACKGROUNDMODE);
                Jj_consume_token(COLON);
                textBgMode = TextBgMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-background-mode", textBgMode);
                break;
            case TEXTCOLOR:
                Jj_consume_token(TEXTCOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-color", colors);
                break;
            case TEXTBACKGROUNDCOLOR:
                Jj_consume_token(TEXTBACKGROUNDCOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-background-color", colors);
                break;
            case TEXTSTYLE:
                Jj_consume_token(TEXTSTYLE);
                Jj_consume_token(COLON);
                textStyle = TextStyle();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-style", textStyle);
                break;
            case TEXTFONT:
                Jj_consume_token(TEXTFONT);
                Jj_consume_token(COLON);
                url = Font();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-font", url);
                break;
            case TEXTSIZE:
                Jj_consume_token(TEXTSIZE);
                Jj_consume_token(COLON);
                value = Value();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-size", value);
                break;
            case TEXTALIGNMENT:
                Jj_consume_token(TEXTALIGNMENT);
                Jj_consume_token(COLON);
                textAlignment = TextAlign();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-alignment", textAlignment);
                break;
            case TEXTOFFSET:
                Jj_consume_token(TEXTOFFSET);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-offset", values);
                break;
            case TEXTPADDING:
                Jj_consume_token(TEXTPADDING);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("text-padding", values);
                break;
            case ICONMODE:
                Jj_consume_token(ICONMODE);
                Jj_consume_token(COLON);
                iconMode = IconMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("icon-mode", iconMode);
                break;
            case ICON:
                Jj_consume_token(ICON);
                Jj_consume_token(COLON);
                url = Icon();
                Jj_consume_token(SEMICOLON);
                style.SetValue("icon", url);
                break;
            case JCOMPONENT:
                Jj_consume_token(JCOMPONENT);
                Jj_consume_token(COLON);
                component = Jcomponent();
                Jj_consume_token(SEMICOLON);
                style.SetValue("jcomponent", component);
                break;
            case VISIBILITYMODE:
                Jj_consume_token(VISIBILITYMODE);
                Jj_consume_token(COLON);
                visMode = VisMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("visibility-mode", visMode);
                break;
            case VISIBILITY:
                Jj_consume_token(VISIBILITY);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("visibility", values);
                break;
            case SIZEMODE:
                Jj_consume_token(SIZEMODE);
                Jj_consume_token(COLON);
                sizeMode = SizeMode();
                Jj_consume_token(SEMICOLON);
                style.SetValue("size-mode", sizeMode);
                break;
            case SIZE:
                Jj_consume_token(SIZE);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("size", values);
                break;
            case SHAPEPOINTS:
                Jj_consume_token(SHAPEPOINTS);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shape-points", values);
                break;
            case SHAPE:
                Jj_consume_token(SHAPE);
                Jj_consume_token(COLON);
                shape = Shape();
                Jj_consume_token(SEMICOLON);
                style.SetValue("shape", shape);
                break;
            case SPRITEORIENT:
                Jj_consume_token(SPRITEORIENT);
                Jj_consume_token(COLON);
                spriteOrient = SpriteOrient();
                Jj_consume_token(SEMICOLON);
                style.SetValue("sprite-orientation", spriteOrient);
                break;
            case ARROWSHAPE:
                Jj_consume_token(ARROWSHAPE);
                Jj_consume_token(COLON);
                arrowShape = ArrowShape();
                Jj_consume_token(SEMICOLON);
                style.SetValue("arrow-shape", arrowShape);
                break;
            case ARROWIMGURL:
                Jj_consume_token(ARROWIMGURL);
                Jj_consume_token(COLON);
                url = Url();
                Jj_consume_token(SEMICOLON);
                style.SetValue("arrow-image", url);
                break;
            case ARROWSIZE:
                Jj_consume_token(ARROWSIZE);
                Jj_consume_token(COLON);
                values = Values();
                Jj_consume_token(SEMICOLON);
                style.SetValue("arrow-size", values);
                break;
            case CANVASCOLOR:
                Jj_consume_token(CANVASCOLOR);
                Jj_consume_token(COLON);
                colors = Colors();
                Jj_consume_token(SEMICOLON);
                style.SetValue("canvas-color", colors);
                break;
            default:
                jj_la1[11] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }
    }

    public Value Value()
    {
        Token t;
        Style.Units units = Style.Units.PX;
        Value value = null;
        t = Jj_consume_token(REAL);
        string nb = t.image.ToLowerCase();
        if (nb.EndsWith("px"))
        {
            units = Style.Units.PX;
            nb = nb.Substring(0, nb.Length - 2);
        }
        else if (nb.EndsWith("gu"))
        {
            units = Style.Units.GU;
            nb = nb.Substring(0, nb.Length - 2);
        }
        else if (nb.EndsWith("%"))
        {
            units = Style.Units.PERCENTS;
            nb = nb.Substring(0, nb.Length - 1);
        }

        try
        {
            value = new Value(units, Float.ParseFloat(nb));
        }
        catch (NumberFormatException e)
        {
            GenerateParseException();
        }

        {
            if (true)
                return value;
        }

        throw new Exception("Missing return statement in function");
    }

    public Values Values()
    {
        Values values;
        Value value;
        value = Value();
        values = new Values(value.units, value.value);
        label_4:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case COMA:
                        break;
                    default:
                        jj_la1[12] = jj_gen;
                        break;
                }

                Jj_consume_token(COMA);
                value = Value();
                values.AddValues(value.value);
            }

        {
            if (true)
                return values;
        }

        throw new Exception("Missing return statement in function");
    }

    public string Url()
    {
        Token t;
        Jj_consume_token(URL);
        Jj_consume_token(LPAREN);
        t = Jj_consume_token(STRING);
        Jj_consume_token(RPAREN);
        {
            if (true)
                return t.image.Substring(1, t.image.Length - 1);
        }

        throw new Exception("Missing return statement in function");
    }

    public string Icon()
    {
        string s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case DYNICON:
                Jj_consume_token(DYNICON);
            {
                if (true)
                    return "dynamic";
            }

                break;
            case URL:
                s = Url();
            {
                if (true)
                    return s;
            }

                break;
            default:
                jj_la1[13] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        throw new Exception("Missing return statement in function");
    }

    public string Font()
    {
        Token t;
        string s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case IDENTIFIER:
                t = Jj_consume_token(IDENTIFIER);
                s = t.image;
                break;
            case STRING:
                t = Jj_consume_token(STRING);
                s = t.image.Substring(1, t.image.Length - 1);
                break;
            default:
                jj_la1[14] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return s;
        }

        throw new Exception("Missing return statement in function");
    }

    public Color Color()
    {
        Token t;
        string s;
        Token r, g, b, a;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case RGB:
                Jj_consume_token(RGB);
                Jj_consume_token(LPAREN);
                r = Jj_consume_token(REAL);
                Jj_consume_token(COMA);
                g = Jj_consume_token(REAL);
                Jj_consume_token(COMA);
                b = Jj_consume_token(REAL);
                Jj_consume_token(RPAREN);
                s = String.Format("rgb(%s,%s,%s)", r.image, g.image, b.image);
                break;
            case RGBA:
                Jj_consume_token(RGBA);
                Jj_consume_token(LPAREN);
                r = Jj_consume_token(REAL);
                Jj_consume_token(COMA);
                g = Jj_consume_token(REAL);
                Jj_consume_token(COMA);
                b = Jj_consume_token(REAL);
                Jj_consume_token(COMA);
                a = Jj_consume_token(REAL);
                Jj_consume_token(RPAREN);
                s = String.Format("rgba(%s,%s,%s,%s)", r.image, g.image, b.image, a.image);
                break;
            case HTMLCOLOR:
                t = Jj_consume_token(HTMLCOLOR);
                s = t.image;
                break;
            case IDENTIFIER:
                t = Jj_consume_token(IDENTIFIER);
                s = t.image;
                break;
            case STRING:
                t = Jj_consume_token(STRING);
                s = t.image.Substring(1, t.image.Length - 1);
                break;
            default:
                jj_la1[15] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        Color color = Style.ConvertColor(s);
        if (color == null)
            color = Color.BLACK;
        {
            if (true)
                return color;
        }

        throw new Exception("Missing return statement in function");
    }

    public Colors Colors()
    {
        Colors colors = new Colors();
        Color color;
        color = Color();
        colors.Add(color);
        label_5:
            while (true)
            {
                switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
                {
                    case COMA:
                        break;
                    default:
                        jj_la1[16] = jj_gen;
                        break;
                }

                Jj_consume_token(COMA);
                color = Color();
                colors.Add(color);
            }

        {
            if (true)
                return colors;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.FillMode FillMode()
    {
        Style.FillMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                m = Style.FillMode.NONE;
                break;
            case PLAIN:
                Jj_consume_token(PLAIN);
                m = Style.FillMode.PLAIN;
                break;
            case DYNPLAIN:
                Jj_consume_token(DYNPLAIN);
                m = Style.FillMode.DYN_PLAIN;
                break;
            case GRADIENTRADIAL:
                Jj_consume_token(GRADIENTRADIAL);
                m = Style.FillMode.GRADIENT_RADIAL;
                break;
            case GRADIENTVERTICAL:
                Jj_consume_token(GRADIENTVERTICAL);
                m = Style.FillMode.GRADIENT_VERTICAL;
                break;
            case GRADIENTHORIZONTAL:
                Jj_consume_token(GRADIENTHORIZONTAL);
                m = Style.FillMode.GRADIENT_HORIZONTAL;
                break;
            case GRADIENTDIAGONAL1:
                Jj_consume_token(GRADIENTDIAGONAL1);
                m = Style.FillMode.GRADIENT_DIAGONAL1;
                break;
            case GRADIENTDIAGONAL2:
                Jj_consume_token(GRADIENTDIAGONAL2);
                m = Style.FillMode.GRADIENT_DIAGONAL2;
                break;
            case IMAGETILED:
                Jj_consume_token(IMAGETILED);
                m = Style.FillMode.IMAGE_TILED;
                break;
            case IMAGESCALED:
                Jj_consume_token(IMAGESCALED);
                m = Style.FillMode.IMAGE_SCALED;
                break;
            case IMAGESCALEDRATIOMAX:
                Jj_consume_token(IMAGESCALEDRATIOMAX);
                m = Style.FillMode.IMAGE_SCALED_RATIO_MAX;
                break;
            case IMAGESCALEDRATIOMIN:
                Jj_consume_token(IMAGESCALEDRATIOMIN);
                m = Style.FillMode.IMAGE_SCALED_RATIO_MIN;
                break;
            default:
                jj_la1[17] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.StrokeMode StrokeMode()
    {
        Style.StrokeMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                m = Style.StrokeMode.NONE;
                break;
            case PLAIN:
                Jj_consume_token(PLAIN);
                m = Style.StrokeMode.PLAIN;
                break;
            case DASHES:
                Jj_consume_token(DASHES);
                m = Style.StrokeMode.DASHES;
                break;
            case DOTS:
                Jj_consume_token(DOTS);
                m = Style.StrokeMode.DOTS;
                break;
            case DOUBLE:
                Jj_consume_token(DOUBLE);
                m = Style.StrokeMode.DOUBLE;
                break;
            default:
                jj_la1[18] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.ShadowMode ShadowMode()
    {
        Style.ShadowMode s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                s = Style.ShadowMode.NONE;
                break;
            case PLAIN:
                Jj_consume_token(PLAIN);
                s = Style.ShadowMode.PLAIN;
                break;
            case GRADIENTRADIAL:
                Jj_consume_token(GRADIENTRADIAL);
                s = Style.ShadowMode.GRADIENT_RADIAL;
                break;
            case GRADIENTHORIZONTAL:
                Jj_consume_token(GRADIENTHORIZONTAL);
                s = Style.ShadowMode.GRADIENT_HORIZONTAL;
                break;
            case GRADIENTVERTICAL:
                Jj_consume_token(GRADIENTVERTICAL);
                s = Style.ShadowMode.GRADIENT_VERTICAL;
                break;
            case GRADIENTDIAGONAL1:
                Jj_consume_token(GRADIENTDIAGONAL1);
                s = Style.ShadowMode.GRADIENT_DIAGONAL1;
                break;
            case GRADIENTDIAGONAL2:
                Jj_consume_token(GRADIENTDIAGONAL2);
                s = Style.ShadowMode.GRADIENT_DIAGONAL2;
                break;
            default:
                jj_la1[19] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return s;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.TextMode TextMode()
    {
        Style.TextMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NORMAL:
                Jj_consume_token(NORMAL);
                m = Style.TextMode.NORMAL;
                break;
            case HIDDEN:
                Jj_consume_token(HIDDEN);
                m = Style.TextMode.HIDDEN;
                break;
            case TRUNCATED:
                Jj_consume_token(TRUNCATED);
                m = Style.TextMode.TRUNCATED;
                break;
            default:
                jj_la1[20] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.TextVisibilityMode TextVisMode()
    {
        Style.TextVisibilityMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NORMAL:
                Jj_consume_token(NORMAL);
                m = Style.TextVisibilityMode.NORMAL;
                break;
            case HIDDEN:
                Jj_consume_token(HIDDEN);
                m = Style.TextVisibilityMode.HIDDEN;
                break;
            case ATZOOM:
                Jj_consume_token(ATZOOM);
                m = Style.TextVisibilityMode.AT_ZOOM;
                break;
            case UNDERZOOM:
                Jj_consume_token(UNDERZOOM);
                m = Style.TextVisibilityMode.UNDER_ZOOM;
                break;
            case OVERZOOM:
                Jj_consume_token(OVERZOOM);
                m = Style.TextVisibilityMode.OVER_ZOOM;
                break;
            case ZOOMRANGE:
                Jj_consume_token(ZOOMRANGE);
                m = Style.TextVisibilityMode.ZOOM_RANGE;
                break;
            case ZOOMS:
                Jj_consume_token(ZOOMS);
                m = Style.TextVisibilityMode.ZOOMS;
                break;
            default:
                jj_la1[21] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.TextBackgroundMode TextBgMode()
    {
        Style.TextBackgroundMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                m = Style.TextBackgroundMode.NONE;
                break;
            case PLAIN:
                Jj_consume_token(PLAIN);
                m = Style.TextBackgroundMode.PLAIN;
                break;
            case ROUNDEDBOX:
                Jj_consume_token(ROUNDEDBOX);
                m = Style.TextBackgroundMode.ROUNDEDBOX;
                break;
            default:
                jj_la1[22] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.TextStyle TextStyle()
    {
        Style.TextStyle t;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NORMAL:
                Jj_consume_token(NORMAL);
                t = Style.TextStyle.NORMAL;
                break;
            case BOLD:
                Jj_consume_token(BOLD);
                t = Style.TextStyle.BOLD;
                break;
            case ITALIC:
                Jj_consume_token(ITALIC);
                t = Style.TextStyle.ITALIC;
                break;
            case BOLD_ITALIC:
                Jj_consume_token(BOLD_ITALIC);
                t = Style.TextStyle.BOLD_ITALIC;
                break;
            default:
                jj_la1[23] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return t;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.SizeMode SizeMode()
    {
        Style.SizeMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NORMAL:
                Jj_consume_token(NORMAL);
                m = Style.SizeMode.NORMAL;
                break;
            case FIT:
                Jj_consume_token(FIT);
                m = Style.SizeMode.FIT;
                break;
            case DYNSIZE:
                Jj_consume_token(DYNSIZE);
                m = Style.SizeMode.DYN_SIZE;
                break;
            default:
                jj_la1[24] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.TextAlignment TextAlign()
    {
        Style.TextAlignment t;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case CENTER:
                Jj_consume_token(CENTER);
                t = Style.TextAlignment.CENTER;
                break;
            case LEFT:
                Jj_consume_token(LEFT);
                t = Style.TextAlignment.LEFT;
                break;
            case RIGHT:
                Jj_consume_token(RIGHT);
                t = Style.TextAlignment.RIGHT;
                break;
            case ATLEFT:
                Jj_consume_token(ATLEFT);
                t = Style.TextAlignment.AT_LEFT;
                break;
            case ATRIGHT:
                Jj_consume_token(ATRIGHT);
                t = Style.TextAlignment.AT_RIGHT;
                break;
            case UNDER:
                Jj_consume_token(UNDER);
                t = Style.TextAlignment.UNDER;
                break;
            case ABOVE:
                Jj_consume_token(ABOVE);
                t = Style.TextAlignment.ABOVE;
                break;
            case JUSTIFY:
                Jj_consume_token(JUSTIFY);
                t = Style.TextAlignment.JUSTIFY;
                break;
            case ALONG:
                Jj_consume_token(ALONG);
                t = Style.TextAlignment.ALONG;
                break;
            default:
                jj_la1[25] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return t;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.IconMode IconMode()
    {
        Style.IconMode i;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                i = Style.IconMode.NONE;
                break;
            case ATLEFT:
                Jj_consume_token(ATLEFT);
                i = Style.IconMode.AT_LEFT;
                break;
            case ATRIGHT:
                Jj_consume_token(ATRIGHT);
                i = Style.IconMode.AT_RIGHT;
                break;
            case ABOVE:
                Jj_consume_token(ABOVE);
                i = Style.IconMode.ABOVE;
                break;
            case UNDER:
                Jj_consume_token(UNDER);
                i = Style.IconMode.UNDER;
                break;
            default:
                jj_la1[26] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return i;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.VisibilityMode VisMode()
    {
        Style.VisibilityMode m;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NORMAL:
                Jj_consume_token(NORMAL);
                m = Style.VisibilityMode.NORMAL;
                break;
            case HIDDEN:
                Jj_consume_token(HIDDEN);
                m = Style.VisibilityMode.HIDDEN;
                break;
            case ATZOOM:
                Jj_consume_token(ATZOOM);
                m = Style.VisibilityMode.AT_ZOOM;
                break;
            case UNDERZOOM:
                Jj_consume_token(UNDERZOOM);
                m = Style.VisibilityMode.UNDER_ZOOM;
                break;
            case OVERZOOM:
                Jj_consume_token(OVERZOOM);
                m = Style.VisibilityMode.OVER_ZOOM;
                break;
            case ZOOMRANGE:
                Jj_consume_token(ZOOMRANGE);
                m = Style.VisibilityMode.ZOOM_RANGE;
                break;
            case ZOOMS:
                Jj_consume_token(ZOOMS);
                m = Style.VisibilityMode.ZOOMS;
                break;
            default:
                jj_la1[27] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return m;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.Shape Shape()
    {
        Style.Shape s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case CIRCLE:
                Jj_consume_token(CIRCLE);
                s = Style.Shape.CIRCLE;
                break;
            case BOX:
                Jj_consume_token(BOX);
                s = Style.Shape.BOX;
                break;
            case ROUNDEDBOX:
                Jj_consume_token(ROUNDEDBOX);
                s = Style.Shape.ROUNDED_BOX;
                break;
            case TRIANGLE:
                Jj_consume_token(TRIANGLE);
                s = Style.Shape.TRIANGLE;
                break;
            case CROSS:
                Jj_consume_token(CROSS);
                s = Style.Shape.CROSS;
                break;
            case DIAMOND:
                Jj_consume_token(DIAMOND);
                s = Style.Shape.DIAMOND;
                break;
            case POLYGON:
                Jj_consume_token(POLYGON);
                s = Style.Shape.POLYGON;
                break;
            case FREEPLANE:
                Jj_consume_token(FREEPLANE);
                s = Style.Shape.FREEPLANE;
                break;
            case TEXTBOX:
                Jj_consume_token(TEXTBOX);
                s = Style.Shape.TEXT_BOX;
                break;
            case TEXTROUNDEDBOX:
                Jj_consume_token(TEXTROUNDEDBOX);
                s = Style.Shape.TEXT_ROUNDED_BOX;
                break;
            case TEXTCIRCLE:
                Jj_consume_token(TEXTCIRCLE);
                s = Style.Shape.TEXT_CIRCLE;
                break;
            case TEXTDIAMOND:
                Jj_consume_token(TEXTDIAMOND);
                s = Style.Shape.TEXT_DIAMOND;
                break;
            case TEXTPARAGRAPH:
                Jj_consume_token(TEXTPARAGRAPH);
                s = Style.Shape.TEXT_PARAGRAPH;
                break;
            case JCOMPONENT:
                Jj_consume_token(JCOMPONENT);
                s = Style.Shape.JCOMPONENT;
                break;
            case PIECHART:
                Jj_consume_token(PIECHART);
                s = Style.Shape.PIE_CHART;
                break;
            case FLOW:
                Jj_consume_token(FLOW);
                s = Style.Shape.FLOW;
                break;
            case ARROW:
                Jj_consume_token(ARROW);
                s = Style.Shape.ARROW;
                break;
            case LINE:
                Jj_consume_token(LINE);
                s = Style.Shape.LINE;
                break;
            case ANGLE:
                Jj_consume_token(ANGLE);
                s = Style.Shape.ANGLE;
                break;
            case CUBICCURVE:
                Jj_consume_token(CUBICCURVE);
                s = Style.Shape.CUBIC_CURVE;
                break;
            case POLYLINE:
                Jj_consume_token(POLYLINE);
                s = Style.Shape.POLYLINE;
                break;
            case POLYLINESCALED:
                Jj_consume_token(POLYLINESCALED);
                s = Style.Shape.POLYLINE_SCALED;
                break;
            case BLOB:
                Jj_consume_token(BLOB);
                s = Style.Shape.BLOB;
                break;
            case SQUARELINE:
                Jj_consume_token(SQUARELINE);
                s = Style.Shape.SQUARELINE;
                break;
            case LSQUARELINE:
                Jj_consume_token(LSQUARELINE);
                s = Style.Shape.LSQUARELINE;
                break;
            case HSQUARELINE:
                Jj_consume_token(HSQUARELINE);
                s = Style.Shape.HSQUARELINE;
                break;
            case VSQUARELINE:
                Jj_consume_token(VSQUARELINE);
                s = Style.Shape.VSQUARELINE;
                break;
            default:
                jj_la1[28] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return s;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.ArrowShape ArrowShape()
    {
        Style.ArrowShape s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                s = Style.ArrowShape.NONE;
                break;
            case ARROW:
                Jj_consume_token(ARROW);
                s = Style.ArrowShape.ARROW;
                break;
            case CIRCLE:
                Jj_consume_token(CIRCLE);
                s = Style.ArrowShape.CIRCLE;
                break;
            case DIAMOND:
                Jj_consume_token(DIAMOND);
                s = Style.ArrowShape.DIAMOND;
                break;
            case IMAGE:
                Jj_consume_token(IMAGE);
                s = Style.ArrowShape.IMAGE;
                break;
            default:
                jj_la1[29] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return s;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.JComponents Jcomponent()
    {
        Style.JComponents c;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case BUTTON:
                Jj_consume_token(BUTTON);
                c = Style.JComponents.BUTTON;
                break;
            case TEXTFIELD:
                Jj_consume_token(TEXTFIELD);
                c = Style.JComponents.TEXT_FIELD;
                break;
            case PANEL:
                Jj_consume_token(PANEL);
                c = Style.JComponents.PANEL;
                break;
            default:
                jj_la1[30] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return c;
        }

        throw new Exception("Missing return statement in function");
    }

    public Style.SpriteOrientation SpriteOrient()
    {
        Style.SpriteOrientation s;
        switch ((jj_ntk == -1) ? Jj_ntk() : jj_ntk)
        {
            case NONE:
                Jj_consume_token(NONE);
                s = Style.SpriteOrientation.NONE;
                break;
            case TO:
                Jj_consume_token(TO);
                s = Style.SpriteOrientation.TO;
                break;
            case FROM:
                Jj_consume_token(FROM);
                s = Style.SpriteOrientation.FROM;
                break;
            case NODE0:
                Jj_consume_token(NODE0);
                s = Style.SpriteOrientation.NODE0;
                break;
            case NODE1:
                Jj_consume_token(NODE1);
                s = Style.SpriteOrientation.NODE1;
                break;
            case PROJECTION:
                Jj_consume_token(PROJECTION);
                s = Style.SpriteOrientation.PROJECTION;
                break;
            default:
                jj_la1[31] = jj_gen;
                Jj_consume_token(-1);
                throw new ParseException();
                break;
        }

        {
            if (true)
                return s;
        }

        throw new Exception("Missing return statement in function");
    }

    public StyleSheetParserTokenManager token_source;
    SimpleCharStream jj_input_stream;
    public Token token;
    public Token jj_nt;
    private int jj_ntk;
    private int jj_gen;
    private readonly int[] jj_la1 = new int[32];
    private static int[] jj_la1_0;
    private static int[] jj_la1_1;
    private static int[] jj_la1_2;
    private static int[] jj_la1_3;
    private static int[] jj_la1_4;
    static StyleSheetParser()
    {
        Jj_la1_init_0();
        Jj_la1_init_1();
        Jj_la1_init_2();
        Jj_la1_init_3();
        Jj_la1_init_4();
    }

    private static void Jj_la1_init_0()
    {
        jj_la1_0 = new int[]
        {
            0xf000000,
            0xf000000,
            0xf0000000,
            0xf0000000,
            0xf000000,
            0x600000,
            0x400000,
            0x4200,
            0x4200,
            0x400000,
            0x8000,
            0xf0000000,
            0x20000,
            0x800000,
            0x400000,
            0x5c0000,
            0x20000,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0
        };
    }

    private static void Jj_la1_init_1()
    {
        jj_la1_1 = new int[]
        {
            0x0,
            0x0,
            0xffffffff,
            0xffffffff,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0xffffffff,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x10000000,
            0x0,
            0x0,
            0x0
        };
    }

    private static void Jj_la1_init_2()
    {
        jj_la1_2 = new int[]
        {
            0x0,
            0x0,
            0x3,
            0x3,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x3,
            0x0,
            0x20,
            0x0,
            0x0,
            0x0,
            0xfbe0c,
            0x801c4,
            0x83e04,
            0x304000,
            0x7d04000,
            0x80004,
            0x70100000,
            0x8100010,
            0x80000000,
            0x80000,
            0x7d04000,
            0x0,
            0x80000,
            0x0,
            0x80000
        };
    }

    private static void Jj_la1_init_3()
    {
        jj_la1_3 = new int[]
        {
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x20000,
            0x0,
            0x0,
            0xff,
            0x63,
            0x0,
            0xff1fff00,
            0x80100,
            0xe00000,
            0x0
        };
    }

    private static void Jj_la1_init_4()
    {
        jj_la1_4 = new int[]
        {
            0x8000,
            0x8000,
            0x8000,
            0x8000,
            0x0,
            0x4000,
            0x7000,
            0x0,
            0x0,
            0x7000,
            0x0,
            0x8000,
            0x0,
            0x0,
            0x4000,
            0x4000,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x0,
            0x1f,
            0x24,
            0x0,
            0xf80
        };
    }

    public StyleSheetParser(java.io.InputStream stream) : this(stream, null)
    {
    }

    public StyleSheetParser(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source = new StyleSheetParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(java.io.InputStream stream)
    {
        ReInit(stream, null);
    }

    public virtual void ReInit(java.io.InputStream stream, string encoding)
    {
        try
        {
            jj_input_stream.ReInit(stream, encoding, 1, 1);
        }
        catch (java.io.UnsupportedEncodingException e)
        {
            throw new Exception(e);
        }

        token_source.ReInit(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    public StyleSheetParser(java.io.Reader stream)
    {
        jj_input_stream = new SimpleCharStream(stream, 1, 1);
        token_source = new StyleSheetParserTokenManager(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(java.io.Reader stream)
    {
        jj_input_stream.ReInit(stream, 1, 1);
        token_source.ReInit(jj_input_stream);
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    public StyleSheetParser(StyleSheetParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    public virtual void ReInit(StyleSheetParserTokenManager tm)
    {
        token_source = tm;
        token = new Token();
        jj_ntk = -1;
        jj_gen = 0;
        for (int i = 0; i < 32; i++)
            jj_la1[i] = -1;
    }

    private Token Jj_consume_token(int kind)
    {
        Token oldToken;
        if ((oldToken = token).next != null)
            token = token.next;
        else
            token = token.next = token_source.GetNextToken();
        jj_ntk = -1;
        if (token.kind == kind)
        {
            jj_gen++;
            return token;
        }

        token = oldToken;
        jj_kind = kind;
        throw GenerateParseException();
    }

    public Token GetNextToken()
    {
        if (token.next != null)
            token = token.next;
        else
            token = token.next = token_source.GetNextToken();
        jj_ntk = -1;
        jj_gen++;
        return token;
    }

    public Token GetToken(int index)
    {
        Token t = token;
        for (int i = 0; i < index; i++)
        {
            if (t.next != null)
                t = t.next;
            else
                t = t.next = token_source.GetNextToken();
        }

        return t;
    }

    private int Jj_ntk()
    {
        if ((jj_nt = token.next) == null)
            return (jj_ntk = (token.next = token_source.GetNextToken()).kind);
        else
            return (jj_ntk = jj_nt.kind);
    }

    private java.util.List<int[]> jj_expentries = new List<int[]>();
    private int[] jj_expentry;
    private int jj_kind = -1;
    public virtual ParseException GenerateParseException()
    {
        jj_expentries.Clear();
        bool[] la1tokens = new bool[144];
        if (jj_kind >= 0)
        {
            la1tokens[jj_kind] = true;
            jj_kind = -1;
        }

        for (int i = 0; i < 32; i++)
        {
            if (jj_la1[i] == jj_gen)
            {
                for (int j = 0; j < 32; j++)
                {
                    if ((jj_la1_0[i] & (1 << j)) != 0)
                    {
                        la1tokens[j] = true;
                    }

                    if ((jj_la1_1[i] & (1 << j)) != 0)
                    {
                        la1tokens[32 + j] = true;
                    }

                    if ((jj_la1_2[i] & (1 << j)) != 0)
                    {
                        la1tokens[64 + j] = true;
                    }

                    if ((jj_la1_3[i] & (1 << j)) != 0)
                    {
                        la1tokens[96 + j] = true;
                    }

                    if ((jj_la1_4[i] & (1 << j)) != 0)
                    {
                        la1tokens[128 + j] = true;
                    }
                }
            }
        }

        for (int i = 0; i < 144; i++)
        {
            if (la1tokens[i])
            {
                jj_expentry = new int[1];
                jj_expentry[0] = i;
                jj_expentries.Add(jj_expentry);
            }
        }

        int[, ] exptokseq = new int[jj_expentries.Count];
        for (int i = 0; i < jj_expentries.Count; i++)
        {
            exptokseq[i] = jj_expentries[i];
        }

        return new ParseException(token, exptokseq, tokenImage);
    }

    public void Enable_tracing()
    {
    }

    public void Disable_tracing()
    {
    }
}
