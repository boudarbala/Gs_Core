using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Util.Parser.ElementType;
using static Org.Graphstream.Util.Parser.EventType;
using static Org.Graphstream.Util.Parser.AttributeChangeEvent;
using static Org.Graphstream.Util.Parser.Mode;
using static Org.Graphstream.Util.Parser.What;
using static Org.Graphstream.Util.Parser.TimeFormat;
using static Org.Graphstream.Util.Parser.OutputType;
using static Org.Graphstream.Util.Parser.OutputPolicy;
using static Org.Graphstream.Util.Parser.LayoutPolicy;
using static Org.Graphstream.Util.Parser.Quality;
using static Org.Graphstream.Util.Parser.Option;
using static Org.Graphstream.Util.Parser.AttributeType;
using static Org.Graphstream.Util.Parser.Balise;
using static Org.Graphstream.Util.Parser.GEXFAttribute;
using static Org.Graphstream.Util.Parser.METAAttribute;
using static Org.Graphstream.Util.Parser.GRAPHAttribute;
using static Org.Graphstream.Util.Parser.ATTRIBUTESAttribute;
using static Org.Graphstream.Util.Parser.ATTRIBUTEAttribute;
using static Org.Graphstream.Util.Parser.NODESAttribute;
using static Org.Graphstream.Util.Parser.NODEAttribute;
using static Org.Graphstream.Util.Parser.ATTVALUEAttribute;
using static Org.Graphstream.Util.Parser.PARENTAttribute;
using static Org.Graphstream.Util.Parser.EDGESAttribute;
using static Org.Graphstream.Util.Parser.SPELLAttribute;
using static Org.Graphstream.Util.Parser.COLORAttribute;
using static Org.Graphstream.Util.Parser.POSITIONAttribute;
using static Org.Graphstream.Util.Parser.SIZEAttribute;
using static Org.Graphstream.Util.Parser.NODESHAPEAttribute;
using static Org.Graphstream.Util.Parser.EDGEAttribute;
using static Org.Graphstream.Util.Parser.THICKNESSAttribute;
using static Org.Graphstream.Util.Parser.EDGESHAPEAttribute;
using static Org.Graphstream.Util.Parser.IDType;
using static Org.Graphstream.Util.Parser.ModeType;
using static Org.Graphstream.Util.Parser.WeightType;
using static Org.Graphstream.Util.Parser.EdgeType;
using static Org.Graphstream.Util.Parser.NodeShapeType;
using static Org.Graphstream.Util.Parser.EdgeShapeType;
using static Org.Graphstream.Util.Parser.ClassType;
using static Org.Graphstream.Util.Parser.TimeFormatType;
using static Org.Graphstream.Util.Parser.GPXAttribute;
using static Org.Graphstream.Util.Parser.WPTAttribute;
using static Org.Graphstream.Util.Parser.LINKAttribute;
using static Org.Graphstream.Util.Parser.EMAILAttribute;
using static Org.Graphstream.Util.Parser.PTAttribute;
using static Org.Graphstream.Util.Parser.BOUNDSAttribute;
using static Org.Graphstream.Util.Parser.COPYRIGHTAttribute;
using static Org.Graphstream.Util.Parser.FixType;
using static Org.Graphstream.Util.Parser.GraphAttribute;
using static Org.Graphstream.Util.Parser.LocatorAttribute;
using static Org.Graphstream.Util.Parser.NodeAttribute;
using static Org.Graphstream.Util.Parser.EdgeAttribute;
using static Org.Graphstream.Util.Parser.DataAttribute;
using static Org.Graphstream.Util.Parser.PortAttribute;
using static Org.Graphstream.Util.Parser.EndPointAttribute;
using static Org.Graphstream.Util.Parser.EndPointType;
using static Org.Graphstream.Util.Parser.HyperEdgeAttribute;
using static Org.Graphstream.Util.Parser.KeyAttribute;
using static Org.Graphstream.Util.Parser.KeyDomain;
using static Org.Graphstream.Util.Parser.KeyAttrType;
using static Org.Graphstream.Util.Parser.GraphEvents;
using static Org.Graphstream.Util.Parser.ThreadingModel;
using static Org.Graphstream.Util.Parser.CloseFramePolicy;

namespace Gs_Core.Graphstream.Util.Parser;

public class ParseException : Exception
{
    private static readonly long serialVersionUID = 1;
    public ParseException(Token currentTokenVal, int[][] expectedTokenSequencesVal, String[] tokenImageVal) : base(Initialise(currentTokenVal, expectedTokenSequencesVal, tokenImageVal))
    {
        currentToken = currentTokenVal;
        expectedTokenSequences = expectedTokenSequencesVal;
        tokenImage = tokenImageVal;
    }

    public ParseException() : base()
    {
    }

    public ParseException(string message) : base(message)
    {
    }

    public Token currentToken;
    public int[, ] expectedTokenSequences;
    public string[] tokenImage;
    private static string Initialise(Token currentToken, int[, ] expectedTokenSequences, string[] tokenImage)
    {
        string eol = System.GetProperty("line.separator", "\n");
        StringBuffer expected = new StringBuffer();
        int maxSize = 0;
        for (int i = 0; i < expectedTokenSequences.Length; i++)
        {
            if (maxSize < expectedTokenSequences[i].Length)
            {
                maxSize = expectedTokenSequences[i].Length;
            }

            for (int j = 0; j < expectedTokenSequences[i].Length; j++)
            {
                expected.Append(tokenImage[expectedTokenSequences[i][j]]).Append(' ');
            }

            if (expectedTokenSequences[i][expectedTokenSequences[i].Length - 1] != 0)
            {
                expected.Append("...");
            }

            expected.Append(eol).Append("    ");
        }

        string retval = "Encountered \"";
        Token tok = currentToken.next;
        for (int i = 0; i < maxSize; i++)
        {
            if (i != 0)
                retval += " ";
            if (tok.kind == 0)
            {
                retval += tokenImage[0];
                break;
            }

            retval += " " + tokenImage[tok.kind];
            retval += " \"";
            retval += Add_escapes(tok.image);
            retval += " \"";
            tok = tok.next;
        }

        retval += "\" at line " + currentToken.next.beginLine + ", column " + currentToken.next.beginColumn;
        retval += "." + eol;
        if (expectedTokenSequences.Length == 1)
        {
            retval += "Was expecting:" + eol + "    ";
        }
        else
        {
            retval += "Was expecting one of:" + eol + "    ";
        }

        retval += expected.ToString();
        return retval;
    }

    protected string eol = System.GetProperty("line.separator", "\n");
    static string Add_escapes(string str)
    {
        StringBuffer retval = new StringBuffer();
        char ch;
        for (int i = 0; i < str.Length; i++)
        {
            switch (str.CharAt(i))
            {
                case 0:
                    continue;
                case '\b':
                    retval.Append("\\b");
                    continue;
                case '\t':
                    retval.Append("\\t");
                    continue;
                case '\n':
                    retval.Append("\\n");
                    continue;
                case '\f':
                    retval.Append("\\f");
                    continue;
                case '\r':
                    retval.Append("\\r");
                    continue;
                case '"':
                    retval.Append("\\\"");
                    continue;
                case '\'':
                    retval.Append("\\'");
                    continue;
                case '\\':
                    retval.Append("\\\\");
                    continue;
                default:
                    if ((ch = str.CharAt(i)) < 0x20 || ch > 0x7e)
                    {
                        string s = "0000" + Integer.ToString(ch, 16);
                        retval.Append("\\u" + s.Substring(s.Length - 4, s.Length));
                    }
                    else
                    {
                        retval.Append(ch);
                    }

                    continue;
                    break;
            }
        }

        return retval.ToString();
    }
}
