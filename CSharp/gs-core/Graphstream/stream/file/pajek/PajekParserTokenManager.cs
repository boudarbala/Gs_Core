using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Pajek.ElementType;
using static Org.Graphstream.Stream.File.Pajek.EventType;
using static Org.Graphstream.Stream.File.Pajek.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Pajek.Mode;
using static Org.Graphstream.Stream.File.Pajek.What;
using static Org.Graphstream.Stream.File.Pajek.TimeFormat;
using static Org.Graphstream.Stream.File.Pajek.OutputType;
using static Org.Graphstream.Stream.File.Pajek.OutputPolicy;
using static Org.Graphstream.Stream.File.Pajek.LayoutPolicy;
using static Org.Graphstream.Stream.File.Pajek.Quality;
using static Org.Graphstream.Stream.File.Pajek.Option;
using static Org.Graphstream.Stream.File.Pajek.AttributeType;
using static Org.Graphstream.Stream.File.Pajek.Balise;
using static Org.Graphstream.Stream.File.Pajek.GEXFAttribute;
using static Org.Graphstream.Stream.File.Pajek.METAAttribute;
using static Org.Graphstream.Stream.File.Pajek.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODEAttribute;
using static Org.Graphstream.Stream.File.Pajek.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Pajek.PARENTAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESAttribute;
using static Org.Graphstream.Stream.File.Pajek.SPELLAttribute;
using static Org.Graphstream.Stream.File.Pajek.COLORAttribute;
using static Org.Graphstream.Stream.File.Pajek.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Pajek.SIZEAttribute;
using static Org.Graphstream.Stream.File.Pajek.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGEAttribute;
using static Org.Graphstream.Stream.File.Pajek.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Pajek.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Pajek.IDType;
using static Org.Graphstream.Stream.File.Pajek.ModeType;
using static Org.Graphstream.Stream.File.Pajek.WeightType;
using static Org.Graphstream.Stream.File.Pajek.EdgeType;
using static Org.Graphstream.Stream.File.Pajek.NodeShapeType;
using static Org.Graphstream.Stream.File.Pajek.EdgeShapeType;
using static Org.Graphstream.Stream.File.Pajek.ClassType;
using static Org.Graphstream.Stream.File.Pajek.TimeFormatType;
using static Org.Graphstream.Stream.File.Pajek.GPXAttribute;
using static Org.Graphstream.Stream.File.Pajek.WPTAttribute;
using static Org.Graphstream.Stream.File.Pajek.LINKAttribute;
using static Org.Graphstream.Stream.File.Pajek.EMAILAttribute;
using static Org.Graphstream.Stream.File.Pajek.PTAttribute;
using static Org.Graphstream.Stream.File.Pajek.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Pajek.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Pajek.FixType;
using static Org.Graphstream.Stream.File.Pajek.GraphAttribute;
using static Org.Graphstream.Stream.File.Pajek.LocatorAttribute;
using static Org.Graphstream.Stream.File.Pajek.NodeAttribute;
using static Org.Graphstream.Stream.File.Pajek.EdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.DataAttribute;
using static Org.Graphstream.Stream.File.Pajek.PortAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointAttribute;
using static Org.Graphstream.Stream.File.Pajek.EndPointType;
using static Org.Graphstream.Stream.File.Pajek.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyAttribute;
using static Org.Graphstream.Stream.File.Pajek.KeyDomain;
using static Org.Graphstream.Stream.File.Pajek.KeyAttrType;
using static Org.Graphstream.Stream.File.Pajek.GraphEvents;
using static Org.Graphstream.Stream.File.Pajek.ThreadingModel;
using static Org.Graphstream.Stream.File.Pajek.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Pajek.Measures;
using static Org.Graphstream.Stream.File.Pajek.Token;
using static Org.Graphstream.Stream.File.Pajek.Extension;
using static Org.Graphstream.Stream.File.Pajek.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Pajek.AttrType;
using static Org.Graphstream.Stream.File.Pajek.Resolutions;

namespace Gs_Core.Graphstream.Stream.File.Pajek;

public class PajekParserTokenManager : PajekParserConstants
{
    public java.io.PrintStream debugStream = System.@out;
    public virtual void SetDebugStream(java.io.PrintStream ds)
    {
        debugStream = ds;
    }

    private int JjStopStringLiteralDfa_0(int pos, long active0)
    {
        switch (pos)
        {
            case 0:
                if ((active0 & 0x3c03023660000) != 0)
                {
                    jjmatchedKind = 53;
                    return 11;
                }

                if ((active0 & 0x20000000000) != 0)
                    return 17;
                if ((active0 & 0x1c3dcfdc180000) != 0)
                    return 11;
                return -1;
            case 1:
                if ((active0 & 0x30077e0000) != 0)
                {
                    if (jjmatchedPos != 1)
                    {
                        jjmatchedKind = 53;
                        jjmatchedPos = 1;
                    }

                    return 11;
                }

                if ((active0 & 0xfe80fe0000000) != 0)
                    return 11;
                return -1;
            case 2:
                if ((active0 & 0x1004100000) != 0)
                    return 11;
                if ((active0 & 0x28036e0000) != 0)
                {
                    jjmatchedKind = 53;
                    jjmatchedPos = 2;
                    return 11;
                }

                return -1;
            case 3:
                if ((active0 & 0x36e0000) != 0)
                {
                    jjmatchedKind = 53;
                    jjmatchedPos = 3;
                    return 11;
                }

                if ((active0 & 0x2800000000) != 0)
                    return 11;
                return -1;
            case 4:
                if ((active0 & 0x3260000) != 0)
                {
                    if (jjmatchedPos != 4)
                    {
                        jjmatchedKind = 53;
                        jjmatchedPos = 4;
                    }

                    return 11;
                }

                if ((active0 & 0x480000) != 0)
                    return 11;
                return -1;
            case 5:
                if ((active0 & 0x3000000) != 0)
                    return 11;
                if ((active0 & 0x260000) != 0)
                {
                    if (jjmatchedPos != 5)
                    {
                        jjmatchedKind = 53;
                        jjmatchedPos = 5;
                    }

                    return 11;
                }

                return -1;
            case 6:
                if ((active0 & 0x60000) != 0)
                    return 11;
                if ((active0 & 0x200000) != 0)
                {
                    jjmatchedKind = 53;
                    jjmatchedPos = 6;
                    return 11;
                }

                return -1;
            case 7:
                if ((active0 & 0x200000) != 0)
                    return 11;
                return -1;
            default:
                return -1;
                break;
        }
    }

    private int JjStartNfa_0(int pos, long active0)
    {
        return JjMoveNfa_0(JjStopStringLiteralDfa_0(pos, active0), pos + 1);
    }

    private int JjStopAtPos(int pos, int kind)
    {
        jjmatchedKind = kind;
        jjmatchedPos = pos;
        return pos + 1;
    }

    private int JjMoveStringLiteralDfa0_0()
    {
        switch (curChar)
        {
            case 42:
                return JjMoveStringLiteralDfa1_0(0xfe00);
            case 65:
            case 97:
                jjmatchedKind = 42;
                return JjMoveStringLiteralDfa1_0(0xc080000000000);
            case 66:
            case 98:
                jjmatchedKind = 52;
                return JjMoveStringLiteralDfa1_0(0xc0100000);
            case 67:
            case 99:
                jjmatchedKind = 38;
                return JjMoveStringLiteralDfa1_0(0x80000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa1_0(0x40000);
            case 69:
            case 101:
                return JjMoveStringLiteralDfa1_0(0x420000);
            case 70:
            case 102:
                return JjMoveStringLiteralDfa1_0(0x3000000000);
            case 72:
            case 104:
                return JjMoveStringLiteralDfa1_0(0xc00000000000);
            case 73:
            case 105:
                return JjMoveStringLiteralDfa1_0(0x20000000);
            case 75:
            case 107:
                return JjMoveStringLiteralDfa1_0(0x3000000000000);
            case 76:
            case 108:
                jjmatchedKind = 44;
                return JjMoveStringLiteralDfa1_0(0x200f00000000);
            case 80:
            case 112:
                jjmatchedKind = 39;
                return JjMoveStringLiteralDfa1_0(0x4000000);
            case 81:
            case 113:
                return JjStartNfaWithStates_0(0, 28, 11);
            case 82:
            case 114:
                return JjStartNfaWithStates_0(0, 27, 11);
            case 83:
            case 115:
                return JjStartNfaWithStates_0(0, 41, 17);
            case 84:
            case 116:
                return JjMoveStringLiteralDfa1_0(0x200000);
            case 87:
            case 119:
                return JjStartNfaWithStates_0(0, 40, 11);
            case 88:
            case 120:
                return JjMoveStringLiteralDfa1_0(0x1000000);
            case 89:
            case 121:
                return JjMoveStringLiteralDfa1_0(0x2000000);
            default:
                return JjMoveNfa_0(0, 0);
                break;
        }
    }

    private int JjMoveStringLiteralDfa1_0(long active0)
    {
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(0, active0);
            return 1;
        }

        switch (curChar)
        {
            case 49:
                if ((active0 & 0x400000000000) != 0)
                    return JjStartNfaWithStates_0(1, 46, 11);
                else if ((active0 & 0x1000000000000) != 0)
                    return JjStartNfaWithStates_0(1, 48, 11);
                else if ((active0 & 0x4000000000000) != 0)
                    return JjStartNfaWithStates_0(1, 50, 11);
                break;
            case 50:
                if ((active0 & 0x800000000000) != 0)
                    return JjStartNfaWithStates_0(1, 47, 11);
                else if ((active0 & 0x2000000000000) != 0)
                    return JjStartNfaWithStates_0(1, 49, 11);
                else if ((active0 & 0x8000000000000) != 0)
                    return JjStartNfaWithStates_0(1, 51, 11);
                break;
            case 95:
                return JjMoveStringLiteralDfa2_0(active0, 0x3000000);
            case 65:
            case 97:
                if ((active0 & 0x200000000) != 0)
                    return JjStartNfaWithStates_0(1, 33, 11);
                return JjMoveStringLiteralDfa2_0(active0, 0x4800);
            case 67:
            case 99:
                if ((active0 & 0x20000000) != 0)
                    return JjStartNfaWithStates_0(1, 29, 11);
                else if ((active0 & 0x40000000) != 0)
                    return JjStartNfaWithStates_0(1, 30, 11);
                else if ((active0 & 0x100000000) != 0)
                    return JjStartNfaWithStates_0(1, 32, 11);
                break;
            case 69:
            case 101:
                return JjMoveStringLiteralDfa2_0(active0, 0x3000);
            case 72:
            case 104:
                return JjMoveStringLiteralDfa2_0(active0, 0x4000000);
            case 73:
            case 105:
                return JjMoveStringLiteralDfa2_0(active0, 0x40000);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa2_0(active0, 0x20000);
            case 77:
            case 109:
                return JjMoveStringLiteralDfa2_0(active0, 0x408000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa2_0(active0, 0x200);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa2_0(active0, 0x3000100000);
            case 80:
            case 112:
                if ((active0 & 0x80000000000) != 0)
                    return JjStartNfaWithStates_0(1, 43, 11);
                else if ((active0 & 0x200000000000) != 0)
                {
                    jjmatchedKind = 45;
                    jjmatchedPos = 1;
                }

                return JjMoveStringLiteralDfa2_0(active0, 0x800000000);
            case 82:
            case 114:
                if ((active0 & 0x400000000) != 0)
                    return JjStartNfaWithStates_0(1, 34, 11);
                return JjMoveStringLiteralDfa2_0(active0, 0x280000);
            case 86:
            case 118:
                return JjMoveStringLiteralDfa2_0(active0, 0x400);
            case 87:
            case 119:
                if ((active0 & 0x80000000) != 0)
                    return JjStartNfaWithStates_0(1, 31, 11);
                break;
            default:
                break;
        }

        return JjStartNfa_0(0, active0);
    }

    private int JjMoveStringLiteralDfa2_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(0, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(1, active0);
            return 2;
        }

        switch (curChar)
        {
            case 65:
            case 97:
                return JjMoveStringLiteralDfa3_0(active0, 0x48000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa3_0(active0, 0x3000);
            case 69:
            case 101:
                return JjMoveStringLiteralDfa3_0(active0, 0x600);
            case 70:
            case 102:
                return JjMoveStringLiteralDfa3_0(active0, 0x3000000);
            case 72:
            case 104:
                return JjMoveStringLiteralDfa3_0(active0, 0x800000000);
            case 73:
            case 105:
                if ((active0 & 0x4000000) != 0)
                    return JjStartNfaWithStates_0(2, 26, 11);
                return JjMoveStringLiteralDfa3_0(active0, 0x200000);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa3_0(active0, 0x20000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa3_0(active0, 0x2000000000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa3_0(active0, 0x80000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa3_0(active0, 0x400000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa3_0(active0, 0x4800);
            case 83:
            case 115:
                if ((active0 & 0x1000000000) != 0)
                    return JjStartNfaWithStates_0(2, 36, 11);
                break;
            case 88:
            case 120:
                if ((active0 & 0x100000) != 0)
                    return JjStartNfaWithStates_0(2, 20, 11);
                break;
            default:
                break;
        }

        return JjStartNfa_0(1, active0);
    }

    private int JjMoveStringLiteralDfa3_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(1, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(2, active0);
            return 3;
        }

        switch (curChar)
        {
            case 65:
            case 97:
                return JjMoveStringLiteralDfa4_0(active0, 0x3200000);
            case 67:
            case 99:
                return JjMoveStringLiteralDfa4_0(active0, 0x4800);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa4_0(active0, 0x3000);
            case 73:
            case 105:
                if ((active0 & 0x800000000) != 0)
                    return JjStartNfaWithStates_0(3, 35, 11);
                return JjMoveStringLiteralDfa4_0(active0, 0x20000);
            case 77:
            case 109:
                return JjMoveStringLiteralDfa4_0(active0, 0x40000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa4_0(active0, 0x400);
            case 83:
            case 115:
                return JjMoveStringLiteralDfa4_0(active0, 0x80000);
            case 84:
            case 116:
                if ((active0 & 0x2000000000) != 0)
                    return JjStartNfaWithStates_0(3, 37, 11);
                return JjMoveStringLiteralDfa4_0(active0, 0x408200);
            default:
                break;
        }

        return JjStartNfa_0(2, active0);
    }

    private int JjMoveStringLiteralDfa4_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(2, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(3, active0);
            return 4;
        }

        switch (curChar)
        {
            case 67:
            case 99:
                return JjMoveStringLiteralDfa5_0(active0, 0x3000000);
            case 69:
            case 101:
                return JjMoveStringLiteralDfa5_0(active0, 0x3000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa5_0(active0, 0x200000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa5_0(active0, 0x40000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa5_0(active0, 0x20000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa5_0(active0, 0x8000);
            case 83:
            case 115:
                if ((active0 & 0x800) != 0)
                {
                    jjmatchedKind = 11;
                    jjmatchedPos = 4;
                }
                else if ((active0 & 0x80000) != 0)
                    return JjStartNfaWithStates_0(4, 19, 11);
                return JjMoveStringLiteralDfa5_0(active0, 0x4000);
            case 84:
            case 116:
                return JjMoveStringLiteralDfa5_0(active0, 0x400);
            case 87:
            case 119:
                return JjMoveStringLiteralDfa5_0(active0, 0x200);
            case 89:
            case 121:
                if ((active0 & 0x400000) != 0)
                    return JjStartNfaWithStates_0(4, 22, 11);
                break;
            default:
                break;
        }

        return JjStartNfa_0(3, active0);
    }

    private int JjMoveStringLiteralDfa5_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(3, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(4, active0);
            return 5;
        }

        switch (curChar)
        {
            case 71:
            case 103:
                return JjMoveStringLiteralDfa6_0(active0, 0x200000);
            case 73:
            case 105:
                return JjMoveStringLiteralDfa6_0(active0, 0x8400);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa6_0(active0, 0x4000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa6_0(active0, 0x40000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa6_0(active0, 0x200);
            case 83:
            case 115:
                if ((active0 & 0x1000) != 0)
                {
                    jjmatchedKind = 12;
                    jjmatchedPos = 5;
                }

                return JjMoveStringLiteralDfa6_0(active0, 0x22000);
            case 84:
            case 116:
                if ((active0 & 0x1000000) != 0)
                    return JjStartNfaWithStates_0(5, 24, 11);
                else if ((active0 & 0x2000000) != 0)
                    return JjStartNfaWithStates_0(5, 25, 11);
                break;
            default:
                break;
        }

        return JjStartNfa_0(4, active0);
    }

    private int JjMoveStringLiteralDfa6_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(4, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(5, active0);
            return 6;
        }

        switch (curChar)
        {
            case 67:
            case 99:
                return JjMoveStringLiteralDfa7_0(active0, 0x400);
            case 68:
            case 100:
                if ((active0 & 0x40000) != 0)
                    return JjStartNfaWithStates_0(6, 18, 11);
                break;
            case 69:
            case 101:
                if ((active0 & 0x20000) != 0)
                    return JjStartNfaWithStates_0(6, 17, 11);
                break;
            case 73:
            case 105:
                return JjMoveStringLiteralDfa7_0(active0, 0x4000);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa7_0(active0, 0x202000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa7_0(active0, 0x200);
            case 88:
            case 120:
                if ((active0 & 0x8000) != 0)
                    return JjStopAtPos(6, 15);
                break;
            default:
                break;
        }

        return JjStartNfa_0(5, active0);
    }

    private int JjMoveStringLiteralDfa7_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(5, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(6, active0);
            return 7;
        }

        switch (curChar)
        {
            case 69:
            case 101:
                if ((active0 & 0x200000) != 0)
                    return JjStartNfaWithStates_0(7, 21, 11);
                return JjMoveStringLiteralDfa8_0(active0, 0x400);
            case 73:
            case 105:
                return JjMoveStringLiteralDfa8_0(active0, 0x2000);
            case 75:
            case 107:
                if ((active0 & 0x200) != 0)
                    return JjStopAtPos(7, 9);
                break;
            case 83:
            case 115:
                return JjMoveStringLiteralDfa8_0(active0, 0x4000);
            default:
                break;
        }

        return JjStartNfa_0(6, active0);
    }

    private int JjMoveStringLiteralDfa8_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(6, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(7, active0);
            return 8;
        }

        switch (curChar)
        {
            case 83:
            case 115:
                if ((active0 & 0x400) != 0)
                    return JjStopAtPos(8, 10);
                return JjMoveStringLiteralDfa9_0(active0, 0x2000);
            case 84:
            case 116:
                if ((active0 & 0x4000) != 0)
                    return JjStopAtPos(8, 14);
                break;
            default:
                break;
        }

        return JjStartNfa_0(7, active0);
    }

    private int JjMoveStringLiteralDfa9_0(long old0, long active0)
    {
        if (((active0 &= old0)) == 0)
            return JjStartNfa_0(7, old0);
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            JjStopStringLiteralDfa_0(8, active0);
            return 9;
        }

        switch (curChar)
        {
            case 84:
            case 116:
                if ((active0 & 0x2000) != 0)
                    return JjStopAtPos(9, 13);
                break;
            default:
                break;
        }

        return JjStartNfa_0(8, active0);
    }

    private int JjStartNfaWithStates_0(int pos, int kind, int state)
    {
        jjmatchedKind = kind;
        jjmatchedPos = pos;
        try
        {
            curChar = input_stream.ReadChar();
        }
        catch (java.io.IOException e)
        {
            return pos + 1;
        }

        return JjMoveNfa_0(state, pos + 1);
    }

    static readonly long[] jjbitVec0 = new[]
    {
        0x0,
        0x0,
        0xffffffffffffffff,
        0xffffffffffffffff
    };
    private int JjMoveNfa_0(int startState, int curPos)
    {
        int startsAt = 0;
        jjnewStateCnt = 26;
        int i = 1;
        jjstateSet[0] = startState;
        int kind = 0x7fffffff;
        for (;;)
        {
            if (++jjround == 0x7fffffff)
                ReInitRounds();
            if (curChar < 64)
            {
                long l = 1 << curChar;
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 0:
                            if ((0x3ff400000000000 & l) != 0)
                            {
                                if (kind > 53)
                                    kind = 53;
                                JjCheckNAdd(11);
                            }
                            else if ((0x280000000000 & l) != 0)
                                JjCheckNAddTwoStates(21, 22);
                            else if ((0x2400 & l) != 0)
                            {
                                if (kind > 3)
                                    kind = 3;
                            }
                            else if (curChar == 37)
                                JjCheckNAddTwoStates(8, 9);
                            else if (curChar == 39)
                                JjCheckNAddTwoStates(5, 6);
                            else if (curChar == 34)
                                JjCheckNAddTwoStates(2, 3);
                            if ((0x3ff000000000000 & l) != 0)
                            {
                                if (kind > 6)
                                    kind = 6;
                                JjCheckNAddStates(0, 2);
                            }

                            break;
                        case 17:
                        case 11:
                            if ((0x7ff600800000000 & l) == 0)
                                break;
                            if (kind > 53)
                                kind = 53;
                            JjCheckNAdd(11);
                            break;
                        case 1:
                            if (curChar == 34)
                                JjCheckNAddTwoStates(2, 3);
                            break;
                        case 2:
                            if ((0xfffffffbffffffff & l) != 0)
                                JjCheckNAddTwoStates(2, 3);
                            break;
                        case 3:
                            if (curChar == 34 && kind > 8)
                                kind = 8;
                            break;
                        case 4:
                            if (curChar == 39)
                                JjCheckNAddTwoStates(5, 6);
                            break;
                        case 5:
                            if ((0xffffff7fffffffff & l) != 0)
                                JjCheckNAddTwoStates(5, 6);
                            break;
                        case 6:
                            if (curChar == 39 && kind > 8)
                                kind = 8;
                            break;
                        case 7:
                            if (curChar == 37)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 8:
                            if ((0xffffffffffffdbff & l) != 0)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 9:
                            if ((0x2400 & l) == 0)
                                break;
                            if (kind > 16)
                                kind = 16;
                            JjCheckNAdd(9);
                            break;
                        case 10:
                            if ((0x3ff400000000000 & l) == 0)
                                break;
                            if (kind > 53)
                                kind = 53;
                            JjCheckNAdd(11);
                            break;
                        case 20:
                            if ((0x280000000000 & l) != 0)
                                JjCheckNAddTwoStates(21, 22);
                            break;
                        case 21:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 6)
                                kind = 6;
                            JjCheckNAdd(21);
                            break;
                        case 22:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 7)
                                kind = 7;
                            JjCheckNAddTwoStates(22, 23);
                            break;
                        case 23:
                            if (curChar == 46)
                                JjCheckNAdd(24);
                            break;
                        case 24:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 7)
                                kind = 7;
                            JjCheckNAdd(24);
                            break;
                        case 25:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 6)
                                kind = 6;
                            JjCheckNAddStates(0, 2);
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }
            else if (curChar < 128)
            {
                long l = 1 << (curChar & 63);
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 0:
                            if ((0x7fffffe07fffffe & l) != 0)
                            {
                                if (kind > 53)
                                    kind = 53;
                                JjCheckNAdd(11);
                            }

                            if ((0x8000000080000 & l) != 0)
                                JjAddStates(3, 4);
                            break;
                        case 17:
                            if ((0x7fffffe87ffffff & l) != 0)
                            {
                                if (kind > 53)
                                    kind = 53;
                                JjCheckNAdd(11);
                            }

                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 18;
                            else if (curChar == 95)
                                jjstateSet[jjnewStateCnt++] = 16;
                            break;
                        case 2:
                            JjAddStates(5, 6);
                            break;
                        case 5:
                            JjAddStates(7, 8);
                            break;
                        case 8:
                            JjAddStates(9, 10);
                            break;
                        case 10:
                            if ((0x7fffffe07fffffe & l) == 0)
                                break;
                            if (kind > 53)
                                kind = 53;
                            JjCheckNAdd(11);
                            break;
                        case 11:
                            if ((0x7fffffe87ffffff & l) == 0)
                                break;
                            if (kind > 53)
                                kind = 53;
                            JjCheckNAdd(11);
                            break;
                        case 12:
                            if ((0x8000000080000 & l) != 0)
                                JjAddStates(3, 4);
                            break;
                        case 13:
                            if ((0x2000000020 & l) != 0 && kind > 23)
                                kind = 23;
                            break;
                        case 14:
                        case 18:
                            if ((0x400000004000000 & l) != 0)
                                JjCheckNAdd(13);
                            break;
                        case 15:
                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 14;
                            break;
                        case 16:
                            if ((0x8000000080000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 15;
                            break;
                        case 19:
                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 18;
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }
            else
            {
                int i2 = (curChar & 0xff) >> 6;
                long l2 = 1 << (curChar & 63);
                do
                {
                    switch (jjstateSet[--i])
                    {
                        case 2:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(5, 6);
                            break;
                        case 5:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(7, 8);
                            break;
                        case 8:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(9, 10);
                            break;
                        default:
                            break;
                    }
                }
                while (i != startsAt);
            }

            if (kind != 0x7fffffff)
            {
                jjmatchedKind = kind;
                jjmatchedPos = curPos;
                kind = 0x7fffffff;
            }

            ++curPos;
            if ((i = jjnewStateCnt) == (startsAt = 26 - (jjnewStateCnt = startsAt)))
                return curPos;
            try
            {
                curChar = input_stream.ReadChar();
            }
            catch (java.io.IOException e)
            {
                return curPos;
            }
        }
    }

    static readonly int[] jjnextStates = new[]
    {
        21,
        22,
        23,
        17,
        19,
        2,
        3,
        5,
        6,
        8,
        9
    };
    public static readonly string[] jjstrLiteralImages = new[]
    {
        "",
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null
    };
    public static readonly string[] lexStateNames = new[]
    {
        "DEFAULT"
    };
    static readonly long[] jjtoToken = new[]
    {
        0x3fffffffffffc9
    };
    static readonly long[] jjtoSkip = new[]
    {
        0x6
    };
    protected SimpleCharStream input_stream;
    private readonly int[] jjrounds = new int[26];
    private readonly int[] jjstateSet = new int[52];
    protected char curChar;
    public PajekParserTokenManager(SimpleCharStream stream)
    {
        if (SimpleCharStream.staticFlag)
            throw new Exception("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
        input_stream = stream;
    }

    public PajekParserTokenManager(SimpleCharStream stream, int lexState) : this(stream)
    {
        SwitchTo(lexState);
    }

    public virtual void ReInit(SimpleCharStream stream)
    {
        jjmatchedPos = jjnewStateCnt = 0;
        curLexState = defaultLexState;
        input_stream = stream;
        ReInitRounds();
    }

    private void ReInitRounds()
    {
        int i;
        jjround = 0x80000001;
        for (i = 26; i-- > 0;)
            jjrounds[i] = 0x80000000;
    }

    public virtual void ReInit(SimpleCharStream stream, int lexState)
    {
        ReInit(stream);
        SwitchTo(lexState);
    }

    public virtual void SwitchTo(int lexState)
    {
        if (lexState >= 1 || lexState < 0)
            throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.INVALID_LEXICAL_STATE);
        else
            curLexState = lexState;
    }

    protected virtual Token JjFillToken()
    {
        Token t;
        string curTokenImage;
        int beginLine;
        int endLine;
        int beginColumn;
        int endColumn;
        string im = jjstrLiteralImages[jjmatchedKind];
        curTokenImage = (im == null) ? input_stream.GetImage() : im;
        beginLine = input_stream.GetBeginLine();
        beginColumn = input_stream.GetBeginColumn();
        endLine = input_stream.GetEndLine();
        endColumn = input_stream.GetEndColumn();
        t = Token.NewToken(jjmatchedKind, curTokenImage);
        t.beginLine = beginLine;
        t.endLine = endLine;
        t.beginColumn = beginColumn;
        t.endColumn = endColumn;
        return t;
    }

    int curLexState = 0;
    int defaultLexState = 0;
    int jjnewStateCnt;
    int jjround;
    int jjmatchedPos;
    int jjmatchedKind;
    public virtual Token GetNextToken()
    {
        Token matchedToken;
        int curPos = 0;
        EOFLoop:
            for (;;)
            {
                try
                {
                    curChar = input_stream.BeginToken();
                }
                catch (java.io.IOException e)
                {
                    jjmatchedKind = 0;
                    matchedToken = JjFillToken();
                    return matchedToken;
                }

                try
                {
                    input_stream.Backup(0);
                    while (curChar <= 32 && (0x100000200 & (1 << curChar)) != 0)
                        curChar = input_stream.BeginToken();
                }
                catch (java.io.IOException e1)
                {
                    continue;
                }

                jjmatchedKind = 0x7fffffff;
                jjmatchedPos = 0;
                curPos = JjMoveStringLiteralDfa0_0();
                if (jjmatchedKind != 0x7fffffff)
                {
                    if (jjmatchedPos + 1 < curPos)
                        input_stream.Backup(curPos - jjmatchedPos - 1);
                    if ((jjtoToken[jjmatchedKind >> 6] & (1 << (jjmatchedKind & 63))) != 0)
                    {
                        matchedToken = JjFillToken();
                        return matchedToken;
                    }
                    else
                    {
                        continue;
                    }
                }

                int error_line = input_stream.GetEndLine();
                int error_column = input_stream.GetEndColumn();
                string error_after = null;
                bool EOFSeen = false;
                try
                {
                    input_stream.ReadChar();
                    input_stream.Backup(1);
                }
                catch (java.io.IOException e1)
                {
                    EOFSeen = true;
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                    if (curChar == '\n' || curChar == '\r')
                    {
                        error_line++;
                        error_column = 0;
                    }
                    else
                        error_column++;
                }

                if (!EOFSeen)
                {
                    input_stream.Backup(1);
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                }

                throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LEXICAL_ERROR);
            }
    }

    private void JjCheckNAdd(int state)
    {
        if (jjrounds[state] != jjround)
        {
            jjstateSet[jjnewStateCnt++] = state;
            jjrounds[state] = jjround;
        }
    }

    private void JjAddStates(int start, int end)
    {
        do
        {
            jjstateSet[jjnewStateCnt++] = jjnextStates[start];
        }
        while (start++ != end);
    }

    private void JjCheckNAddTwoStates(int state1, int state2)
    {
        JjCheckNAdd(state1);
        JjCheckNAdd(state2);
    }

    private void JjCheckNAddStates(int start, int end)
    {
        do
        {
            JjCheckNAdd(jjnextStates[start]);
        }
        while (start++ != end);
    }
}
