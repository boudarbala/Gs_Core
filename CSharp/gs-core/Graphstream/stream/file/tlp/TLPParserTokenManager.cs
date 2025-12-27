using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Tlp.ElementType;
using static Org.Graphstream.Stream.File.Tlp.EventType;
using static Org.Graphstream.Stream.File.Tlp.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Tlp.Mode;
using static Org.Graphstream.Stream.File.Tlp.What;
using static Org.Graphstream.Stream.File.Tlp.TimeFormat;
using static Org.Graphstream.Stream.File.Tlp.OutputType;
using static Org.Graphstream.Stream.File.Tlp.OutputPolicy;
using static Org.Graphstream.Stream.File.Tlp.LayoutPolicy;
using static Org.Graphstream.Stream.File.Tlp.Quality;
using static Org.Graphstream.Stream.File.Tlp.Option;
using static Org.Graphstream.Stream.File.Tlp.AttributeType;
using static Org.Graphstream.Stream.File.Tlp.Balise;
using static Org.Graphstream.Stream.File.Tlp.GEXFAttribute;
using static Org.Graphstream.Stream.File.Tlp.METAAttribute;
using static Org.Graphstream.Stream.File.Tlp.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODEAttribute;
using static Org.Graphstream.Stream.File.Tlp.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Tlp.PARENTAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESAttribute;
using static Org.Graphstream.Stream.File.Tlp.SPELLAttribute;
using static Org.Graphstream.Stream.File.Tlp.COLORAttribute;
using static Org.Graphstream.Stream.File.Tlp.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Tlp.SIZEAttribute;
using static Org.Graphstream.Stream.File.Tlp.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGEAttribute;
using static Org.Graphstream.Stream.File.Tlp.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Tlp.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Tlp.IDType;
using static Org.Graphstream.Stream.File.Tlp.ModeType;
using static Org.Graphstream.Stream.File.Tlp.WeightType;
using static Org.Graphstream.Stream.File.Tlp.EdgeType;
using static Org.Graphstream.Stream.File.Tlp.NodeShapeType;
using static Org.Graphstream.Stream.File.Tlp.EdgeShapeType;
using static Org.Graphstream.Stream.File.Tlp.ClassType;
using static Org.Graphstream.Stream.File.Tlp.TimeFormatType;
using static Org.Graphstream.Stream.File.Tlp.GPXAttribute;
using static Org.Graphstream.Stream.File.Tlp.WPTAttribute;
using static Org.Graphstream.Stream.File.Tlp.LINKAttribute;
using static Org.Graphstream.Stream.File.Tlp.EMAILAttribute;
using static Org.Graphstream.Stream.File.Tlp.PTAttribute;
using static Org.Graphstream.Stream.File.Tlp.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Tlp.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Tlp.FixType;
using static Org.Graphstream.Stream.File.Tlp.GraphAttribute;
using static Org.Graphstream.Stream.File.Tlp.LocatorAttribute;
using static Org.Graphstream.Stream.File.Tlp.NodeAttribute;
using static Org.Graphstream.Stream.File.Tlp.EdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.DataAttribute;
using static Org.Graphstream.Stream.File.Tlp.PortAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointAttribute;
using static Org.Graphstream.Stream.File.Tlp.EndPointType;
using static Org.Graphstream.Stream.File.Tlp.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyAttribute;
using static Org.Graphstream.Stream.File.Tlp.KeyDomain;
using static Org.Graphstream.Stream.File.Tlp.KeyAttrType;
using static Org.Graphstream.Stream.File.Tlp.GraphEvents;
using static Org.Graphstream.Stream.File.Tlp.ThreadingModel;
using static Org.Graphstream.Stream.File.Tlp.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Tlp.Measures;
using static Org.Graphstream.Stream.File.Tlp.Token;
using static Org.Graphstream.Stream.File.Tlp.Extension;
using static Org.Graphstream.Stream.File.Tlp.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Tlp.AttrType;
using static Org.Graphstream.Stream.File.Tlp.Resolutions;
using static Org.Graphstream.Stream.File.Tlp.PropertyType;

namespace Gs_Core.Graphstream.Stream.File.Tlp;

public class TLPParserTokenManager : TLPParserConstants
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
                if ((active0 & 0x900000) != 0)
                    return 35;
                if ((active0 & 0x240000) != 0)
                    return 29;
                return -1;
            case 1:
                if ((active0 & 0x200000) != 0)
                    return 28;
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
            case 40:
                return JjStopAtPos(0, 10);
            case 41:
                return JjStopAtPos(0, 11);
            case 65:
            case 97:
                return JjMoveStringLiteralDfa1_0(0x80000);
            case 67:
            case 99:
                return JjMoveStringLiteralDfa1_0(0x240000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa1_0(0x900000);
            case 69:
            case 101:
                return JjMoveStringLiteralDfa1_0(0x30000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa1_0(0x2000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa1_0(0xc000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa1_0(0x400000);
            case 84:
            case 116:
                return JjMoveStringLiteralDfa1_0(0x1000);
            default:
                return JjMoveNfa_0(6, 0);
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
            case 65:
            case 97:
                return JjMoveStringLiteralDfa2_0(active0, 0x100000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa2_0(active0, 0x30000);
            case 69:
            case 101:
                return JjMoveStringLiteralDfa2_0(active0, 0x800000);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa2_0(active0, 0x41000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa2_0(active0, 0x20c000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa2_0(active0, 0x402000);
            case 85:
            case 117:
                return JjMoveStringLiteralDfa2_0(active0, 0x80000);
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
                return JjMoveStringLiteralDfa3_0(active0, 0x2000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa3_0(active0, 0xc000);
            case 70:
            case 102:
                return JjMoveStringLiteralDfa3_0(active0, 0x800000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa3_0(active0, 0x30000);
            case 77:
            case 109:
                return JjMoveStringLiteralDfa3_0(active0, 0x200000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa3_0(active0, 0x400000);
            case 80:
            case 112:
                if ((active0 & 0x1000) != 0)
                    return JjStopAtPos(2, 12);
                break;
            case 84:
            case 116:
                return JjMoveStringLiteralDfa3_0(active0, 0x180000);
            case 85:
            case 117:
                return JjMoveStringLiteralDfa3_0(active0, 0x40000);
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
                return JjMoveStringLiteralDfa4_0(active0, 0x800000);
            case 69:
            case 101:
                if ((active0 & 0x4000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 3;
                }
                else if ((active0 & 0x10000) != 0)
                {
                    jjmatchedKind = 16;
                    jjmatchedPos = 3;
                }
                else if ((active0 & 0x100000) != 0)
                    return JjStopAtPos(3, 20);
                return JjMoveStringLiteralDfa4_0(active0, 0x28000);
            case 72:
            case 104:
                return JjMoveStringLiteralDfa4_0(active0, 0x80000);
            case 77:
            case 109:
                return JjMoveStringLiteralDfa4_0(active0, 0x200000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa4_0(active0, 0x402000);
            case 83:
            case 115:
                return JjMoveStringLiteralDfa4_0(active0, 0x40000);
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
            case 69:
            case 101:
                return JjMoveStringLiteralDfa5_0(active0, 0x600000);
            case 72:
            case 104:
                if ((active0 & 0x2000) != 0)
                    return JjStopAtPos(4, 13);
                break;
            case 79:
            case 111:
                return JjMoveStringLiteralDfa5_0(active0, 0x80000);
            case 83:
            case 115:
                if ((active0 & 0x8000) != 0)
                    return JjStopAtPos(4, 15);
                else if ((active0 & 0x20000) != 0)
                    return JjStopAtPos(4, 17);
                break;
            case 84:
            case 116:
                return JjMoveStringLiteralDfa5_0(active0, 0x40000);
            case 85:
            case 117:
                return JjMoveStringLiteralDfa5_0(active0, 0x800000);
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
            case 69:
            case 101:
                return JjMoveStringLiteralDfa6_0(active0, 0x40000);
            case 76:
            case 108:
                return JjMoveStringLiteralDfa6_0(active0, 0x800000);
            case 78:
            case 110:
                return JjMoveStringLiteralDfa6_0(active0, 0x200000);
            case 82:
            case 114:
                if ((active0 & 0x80000) != 0)
                    return JjStopAtPos(5, 19);
                return JjMoveStringLiteralDfa6_0(active0, 0x400000);
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
            case 82:
            case 114:
                if ((active0 & 0x40000) != 0)
                    return JjStopAtPos(6, 18);
                break;
            case 84:
            case 116:
                if ((active0 & 0x800000) != 0)
                    return JjStopAtPos(6, 23);
                return JjMoveStringLiteralDfa7_0(active0, 0x600000);
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
            case 83:
            case 115:
                if ((active0 & 0x200000) != 0)
                    return JjStopAtPos(7, 21);
                break;
            case 89:
            case 121:
                if ((active0 & 0x400000) != 0)
                    return JjStopAtPos(7, 22);
                break;
            default:
                break;
        }

        return JjStartNfa_0(6, active0);
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
        jjnewStateCnt = 55;
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
                        case 6:
                            if ((0x3ff000000000000 & l) != 0)
                            {
                                if (kind > 24)
                                    kind = 24;
                                JjCheckNAddStates(0, 2);
                            }
                            else if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(11);
                            else if (curChar == 39)
                                JjCheckNAddTwoStates(20, 21);
                            else if (curChar == 34)
                                JjCheckNAddStates(3, 5);
                            else if (curChar == 59)
                                JjCheckNAddTwoStates(8, 9);
                            else if (curChar == 47)
                                jjstateSet[jjnewStateCnt++] = 0;
                            break;
                        case 0:
                            if (curChar == 42)
                                JjCheckNAddStates(6, 8);
                            break;
                        case 1:
                            if ((0xfffffbffffffffff & l) != 0)
                                JjCheckNAddStates(6, 8);
                            break;
                        case 2:
                            if (curChar == 42)
                                jjstateSet[jjnewStateCnt++] = 3;
                            break;
                        case 3:
                            if ((0xffff7fffffffffff & l) != 0)
                                JjCheckNAddStates(6, 8);
                            break;
                        case 4:
                            if (curChar == 47 && kind > 5)
                                kind = 5;
                            break;
                        case 5:
                            if (curChar == 42)
                                jjstateSet[jjnewStateCnt++] = 4;
                            break;
                        case 7:
                            if (curChar == 59)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 8:
                            if ((0xffffffffffffdbff & l) != 0)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 9:
                            if ((0x2400 & l) != 0 && kind > 6)
                                kind = 6;
                            break;
                        case 10:
                            if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(11);
                            break;
                        case 11:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 25)
                                kind = 25;
                            JjCheckNAddTwoStates(11, 12);
                            break;
                        case 12:
                            if (curChar == 46)
                                JjCheckNAdd(13);
                            break;
                        case 13:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 25)
                                kind = 25;
                            JjCheckNAdd(13);
                            break;
                        case 14:
                        case 16:
                            if (curChar == 34)
                                JjCheckNAddStates(3, 5);
                            break;
                        case 15:
                            if ((0xfffffffbffffffff & l) != 0)
                                JjCheckNAddStates(3, 5);
                            break;
                        case 18:
                            if (curChar == 34 && kind > 26)
                                kind = 26;
                            break;
                        case 19:
                            if (curChar == 39)
                                JjCheckNAddTwoStates(20, 21);
                            break;
                        case 20:
                            if ((0xffffff7fffffffff & l) != 0)
                                JjCheckNAddTwoStates(20, 21);
                            break;
                        case 21:
                            if (curChar == 39 && kind > 26)
                                kind = 26;
                            break;
                        case 53:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 24)
                                kind = 24;
                            JjCheckNAddStates(0, 2);
                            break;
                        case 54:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 24)
                                kind = 24;
                            JjCheckNAdd(54);
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
                        case 6:
                            if ((0x8000000080000 & l) != 0)
                                JjAddStates(9, 10);
                            else if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 43;
                            else if ((0x100000001000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 41;
                            else if ((0x1000000010 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 35;
                            else if ((0x800000008 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 29;
                            else if ((0x400000004 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 24;
                            break;
                        case 1:
                        case 3:
                            JjCheckNAddStates(6, 8);
                            break;
                        case 8:
                            JjAddStates(11, 12);
                            break;
                        case 15:
                            JjAddStates(3, 5);
                            break;
                        case 17:
                            if (curChar == 92)
                                jjstateSet[jjnewStateCnt++] = 16;
                            break;
                        case 20:
                            JjAddStates(13, 14);
                            break;
                        case 22:
                            if ((0x100000001000 & l) != 0 && kind > 27)
                                kind = 27;
                            break;
                        case 23:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 22;
                            break;
                        case 24:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 23;
                            break;
                        case 25:
                            if ((0x400000004 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 24;
                            break;
                        case 26:
                            if ((0x4000000040000 & l) != 0 && kind > 27)
                                kind = 27;
                            break;
                        case 27:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 26;
                            break;
                        case 28:
                            if ((0x100000001000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 27;
                            break;
                        case 29:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 28;
                            break;
                        case 30:
                            if ((0x800000008 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 29;
                            break;
                        case 31:
                            if ((0x2000000020 & l) != 0 && kind > 27)
                                kind = 27;
                            break;
                        case 32:
                            if ((0x100000001000 & l) != 0)
                                JjCheckNAdd(31);
                            break;
                        case 33:
                            if ((0x400000004 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 32;
                            break;
                        case 34:
                            if ((0x20000000200000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 33;
                            break;
                        case 35:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 34;
                            break;
                        case 36:
                            if ((0x1000000010 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 35;
                            break;
                        case 37:
                            if ((0x10000000100000 & l) != 0 && kind > 27)
                                kind = 27;
                            break;
                        case 38:
                            if ((0x20000000200000 & l) != 0)
                                JjCheckNAdd(37);
                            break;
                        case 39:
                            if ((0x800000008000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 38;
                            break;
                        case 40:
                            if ((0x200000002000000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 39;
                            break;
                        case 41:
                            if ((0x200000002 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 40;
                            break;
                        case 42:
                            if ((0x100000001000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 41;
                            break;
                        case 43:
                            if ((0x400000004000 & l) != 0)
                                JjCheckNAdd(37);
                            break;
                        case 44:
                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 43;
                            break;
                        case 45:
                            if ((0x8000000080000 & l) != 0)
                                JjAddStates(9, 10);
                            break;
                        case 46:
                            if ((0x400000004000000 & l) != 0)
                                JjCheckNAdd(31);
                            break;
                        case 47:
                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 46;
                            break;
                        case 48:
                            if ((0x8000000080 & l) != 0 && kind > 27)
                                kind = 27;
                            break;
                        case 49:
                            if ((0x400000004000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 48;
                            break;
                        case 50:
                            if ((0x20000000200 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 49;
                            break;
                        case 51:
                            if ((0x4000000040000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 50;
                            break;
                        case 52:
                            if ((0x10000000100000 & l) != 0)
                                jjstateSet[jjnewStateCnt++] = 51;
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
                        case 1:
                        case 3:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjCheckNAddStates(6, 8);
                            break;
                        case 8:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(11, 12);
                            break;
                        case 15:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(3, 5);
                            break;
                        case 20:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(13, 14);
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
            if ((i = jjnewStateCnt) == (startsAt = 55 - (jjnewStateCnt = startsAt)))
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
        54,
        11,
        12,
        15,
        17,
        18,
        1,
        2,
        5,
        47,
        52,
        8,
        9,
        20,
        21
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
        "(",
        ")",
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
        0xffffc01
    };
    static readonly long[] jjtoSkip = new[]
    {
        0x7e
    };
    protected SimpleCharStream input_stream;
    private readonly int[] jjrounds = new int[55];
    private readonly int[] jjstateSet = new int[110];
    protected char curChar;
    public TLPParserTokenManager(SimpleCharStream stream)
    {
        if (SimpleCharStream.staticFlag)
            throw new Exception("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
        input_stream = stream;
    }

    public TLPParserTokenManager(SimpleCharStream stream, int lexState) : this(stream)
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
        for (i = 55; i-- > 0;)
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
                    while (curChar <= 32 && (0x100002600 & (1 << curChar)) != 0)
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
