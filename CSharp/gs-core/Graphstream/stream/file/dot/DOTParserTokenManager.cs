using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Dot.ElementType;
using static Org.Graphstream.Stream.File.Dot.EventType;
using static Org.Graphstream.Stream.File.Dot.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Dot.Mode;
using static Org.Graphstream.Stream.File.Dot.What;
using static Org.Graphstream.Stream.File.Dot.TimeFormat;
using static Org.Graphstream.Stream.File.Dot.OutputType;
using static Org.Graphstream.Stream.File.Dot.OutputPolicy;
using static Org.Graphstream.Stream.File.Dot.LayoutPolicy;
using static Org.Graphstream.Stream.File.Dot.Quality;
using static Org.Graphstream.Stream.File.Dot.Option;
using static Org.Graphstream.Stream.File.Dot.AttributeType;
using static Org.Graphstream.Stream.File.Dot.Balise;
using static Org.Graphstream.Stream.File.Dot.GEXFAttribute;
using static Org.Graphstream.Stream.File.Dot.METAAttribute;
using static Org.Graphstream.Stream.File.Dot.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESAttribute;
using static Org.Graphstream.Stream.File.Dot.NODEAttribute;
using static Org.Graphstream.Stream.File.Dot.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Dot.PARENTAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESAttribute;
using static Org.Graphstream.Stream.File.Dot.SPELLAttribute;
using static Org.Graphstream.Stream.File.Dot.COLORAttribute;
using static Org.Graphstream.Stream.File.Dot.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Dot.SIZEAttribute;
using static Org.Graphstream.Stream.File.Dot.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGEAttribute;
using static Org.Graphstream.Stream.File.Dot.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Dot.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Dot.IDType;
using static Org.Graphstream.Stream.File.Dot.ModeType;
using static Org.Graphstream.Stream.File.Dot.WeightType;
using static Org.Graphstream.Stream.File.Dot.EdgeType;
using static Org.Graphstream.Stream.File.Dot.NodeShapeType;
using static Org.Graphstream.Stream.File.Dot.EdgeShapeType;
using static Org.Graphstream.Stream.File.Dot.ClassType;
using static Org.Graphstream.Stream.File.Dot.TimeFormatType;
using static Org.Graphstream.Stream.File.Dot.GPXAttribute;
using static Org.Graphstream.Stream.File.Dot.WPTAttribute;
using static Org.Graphstream.Stream.File.Dot.LINKAttribute;
using static Org.Graphstream.Stream.File.Dot.EMAILAttribute;
using static Org.Graphstream.Stream.File.Dot.PTAttribute;
using static Org.Graphstream.Stream.File.Dot.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Dot.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Dot.FixType;
using static Org.Graphstream.Stream.File.Dot.GraphAttribute;
using static Org.Graphstream.Stream.File.Dot.LocatorAttribute;
using static Org.Graphstream.Stream.File.Dot.NodeAttribute;
using static Org.Graphstream.Stream.File.Dot.EdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.DataAttribute;
using static Org.Graphstream.Stream.File.Dot.PortAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointAttribute;
using static Org.Graphstream.Stream.File.Dot.EndPointType;
using static Org.Graphstream.Stream.File.Dot.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyAttribute;
using static Org.Graphstream.Stream.File.Dot.KeyDomain;
using static Org.Graphstream.Stream.File.Dot.KeyAttrType;
using static Org.Graphstream.Stream.File.Dot.GraphEvents;
using static Org.Graphstream.Stream.File.Dot.ThreadingModel;
using static Org.Graphstream.Stream.File.Dot.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Dot.Measures;
using static Org.Graphstream.Stream.File.Dot.Token;

namespace Gs_Core.Graphstream.Stream.File.Dot;

public class DOTParserTokenManager : DOTParserConstants
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
                if ((active0 & 0x3ff07e0000) != 0)
                {
                    jjmatchedKind = 26;
                    return 16;
                }

                return -1;
            case 1:
                if ((active0 & 0xaa07e0000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 1;
                    return 16;
                }

                return -1;
            case 2:
                if ((active0 & 0x7e0000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 2;
                    return 16;
                }

                return -1;
            case 3:
                if ((active0 & 0x300000) != 0)
                    return 16;
                if ((active0 & 0x4e0000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 3;
                    return 16;
                }

                return -1;
            case 4:
                if ((active0 & 0x20000) != 0)
                    return 16;
                if ((active0 & 0x4c0000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 4;
                    return 16;
                }

                return -1;
            case 5:
                if ((active0 & 0xc0000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 5;
                    return 16;
                }

                if ((active0 & 0x400000) != 0)
                    return 16;
                return -1;
            case 6:
                if ((active0 & 0x40000) != 0)
                    return 16;
                if ((active0 & 0x80000) != 0)
                {
                    jjmatchedKind = 26;
                    jjmatchedPos = 6;
                    return 16;
                }

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
            case 44:
                return JjStopAtPos(0, 15);
            case 58:
                return JjStopAtPos(0, 14);
            case 59:
                return JjStopAtPos(0, 27);
            case 61:
                return JjStopAtPos(0, 16);
            case 91:
                return JjStopAtPos(0, 10);
            case 93:
                return JjStopAtPos(0, 11);
            case 95:
                return JjStartNfaWithStates_0(0, 26, 16);
            case 67:
            case 99:
                return JjStartNfaWithStates_0(0, 26, 16);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa1_0(0x40000);
            case 69:
            case 101:
                jjmatchedKind = 26;
                return JjMoveStringLiteralDfa1_0(0x200000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa1_0(0x20000);
            case 78:
            case 110:
                jjmatchedKind = 26;
                return JjMoveStringLiteralDfa1_0(0x820100000);
            case 83:
            case 115:
                jjmatchedKind = 26;
                return JjMoveStringLiteralDfa1_0(0x280480000);
            case 87:
            case 119:
                return JjStartNfaWithStates_0(0, 26, 16);
            case 123:
                return JjStopAtPos(0, 12);
            case 125:
                return JjStopAtPos(0, 13);
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
            case 68:
            case 100:
                return JjMoveStringLiteralDfa2_0(active0, 0x200000);
            case 69:
            case 101:
                if ((active0 & 0x20000000) != 0)
                    return JjStartNfaWithStates_0(1, 26, 16);
                else if ((active0 & 0x80000000) != 0)
                    return JjStartNfaWithStates_0(1, 26, 16);
                break;
            case 73:
            case 105:
                return JjMoveStringLiteralDfa2_0(active0, 0x40000);
            case 79:
            case 111:
                return JjMoveStringLiteralDfa2_0(active0, 0x100000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa2_0(active0, 0x20000);
            case 84:
            case 116:
                return JjMoveStringLiteralDfa2_0(active0, 0x400000);
            case 85:
            case 117:
                return JjMoveStringLiteralDfa2_0(active0, 0x80000);
            case 87:
            case 119:
                if ((active0 & 0x200000000) != 0)
                    return JjStartNfaWithStates_0(1, 26, 16);
                else if ((active0 & 0x800000000) != 0)
                    return JjStartNfaWithStates_0(1, 26, 16);
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
                return JjMoveStringLiteralDfa3_0(active0, 0x20000);
            case 66:
            case 98:
                return JjMoveStringLiteralDfa3_0(active0, 0x80000);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa3_0(active0, 0x100000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa3_0(active0, 0x240000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa3_0(active0, 0x400000);
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
            case 69:
            case 101:
                if ((active0 & 0x100000) != 0)
                    return JjStartNfaWithStates_0(3, 20, 16);
                else if ((active0 & 0x200000) != 0)
                    return JjStartNfaWithStates_0(3, 21, 16);
                break;
            case 71:
            case 103:
                return JjMoveStringLiteralDfa4_0(active0, 0x80000);
            case 73:
            case 105:
                return JjMoveStringLiteralDfa4_0(active0, 0x400000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa4_0(active0, 0x20000);
            case 82:
            case 114:
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
            case 65:
            case 97:
                return JjMoveStringLiteralDfa5_0(active0, 0x40000);
            case 67:
            case 99:
                return JjMoveStringLiteralDfa5_0(active0, 0x400000);
            case 72:
            case 104:
                if ((active0 & 0x20000) != 0)
                    return JjStartNfaWithStates_0(4, 17, 16);
                break;
            case 82:
            case 114:
                return JjMoveStringLiteralDfa5_0(active0, 0x80000);
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
            case 65:
            case 97:
                return JjMoveStringLiteralDfa6_0(active0, 0x80000);
            case 80:
            case 112:
                return JjMoveStringLiteralDfa6_0(active0, 0x40000);
            case 84:
            case 116:
                if ((active0 & 0x400000) != 0)
                    return JjStartNfaWithStates_0(5, 22, 16);
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
            case 72:
            case 104:
                if ((active0 & 0x40000) != 0)
                    return JjStartNfaWithStates_0(6, 18, 16);
                break;
            case 80:
            case 112:
                return JjMoveStringLiteralDfa7_0(active0, 0x80000);
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
            case 72:
            case 104:
                if ((active0 & 0x80000) != 0)
                    return JjStartNfaWithStates_0(7, 19, 16);
                break;
            default:
                break;
        }

        return JjStartNfa_0(6, active0);
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
        jjnewStateCnt = 28;
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
                            if ((0x3ff000000000000 & l) != 0)
                            {
                                if (kind > 24)
                                    kind = 24;
                                JjCheckNAddTwoStates(4, 5);
                            }
                            else if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(4);
                            else if (curChar == 47)
                                JjAddStates(0, 1);
                            else if (curChar == 39)
                                JjCheckNAddTwoStates(13, 14);
                            else if (curChar == 34)
                                JjCheckNAddStates(2, 4);
                            else if (curChar == 35)
                                JjCheckNAddTwoStates(1, 2);
                            if (curChar == 45)
                                JjAddStates(5, 6);
                            break;
                        case 1:
                            if ((0xffffffffffffdbff & l) != 0)
                                JjCheckNAddTwoStates(1, 2);
                            break;
                        case 2:
                            if ((0x2400 & l) != 0 && kind > 6)
                                kind = 6;
                            break;
                        case 3:
                            if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(4);
                            break;
                        case 4:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 24)
                                kind = 24;
                            JjCheckNAddTwoStates(4, 5);
                            break;
                        case 5:
                            if (curChar == 46)
                                JjCheckNAdd(6);
                            break;
                        case 6:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 24)
                                kind = 24;
                            JjCheckNAdd(6);
                            break;
                        case 7:
                        case 9:
                            if (curChar == 34)
                                JjCheckNAddStates(2, 4);
                            break;
                        case 8:
                            if ((0xfffffffbffffffff & l) != 0)
                                JjCheckNAddStates(2, 4);
                            break;
                        case 11:
                            if (curChar == 34 && kind > 25)
                                kind = 25;
                            break;
                        case 12:
                            if (curChar == 39)
                                JjCheckNAddTwoStates(13, 14);
                            break;
                        case 13:
                            if ((0xffffff7fffffffff & l) != 0)
                                JjCheckNAddTwoStates(13, 14);
                            break;
                        case 14:
                            if (curChar == 39 && kind > 25)
                                kind = 25;
                            break;
                        case 16:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 26)
                                kind = 26;
                            jjstateSet[jjnewStateCnt++] = 16;
                            break;
                        case 17:
                            if (curChar == 45)
                                JjAddStates(5, 6);
                            break;
                        case 18:
                            if (curChar == 45 && kind > 23)
                                kind = 23;
                            break;
                        case 19:
                            if (curChar == 62 && kind > 23)
                                kind = 23;
                            break;
                        case 20:
                            if (curChar == 47)
                                JjAddStates(0, 1);
                            break;
                        case 21:
                            if (curChar == 42)
                                JjCheckNAddStates(7, 9);
                            break;
                        case 22:
                            if ((0xfffffbffffffffff & l) != 0)
                                JjCheckNAddStates(7, 9);
                            break;
                        case 23:
                            if (curChar == 42)
                                jjstateSet[jjnewStateCnt++] = 24;
                            break;
                        case 24:
                            if ((0xffff7fffffffffff & l) != 0)
                                JjCheckNAddStates(7, 9);
                            break;
                        case 25:
                            if (curChar == 47 && kind > 5)
                                kind = 5;
                            break;
                        case 26:
                            if (curChar == 42)
                                jjstateSet[jjnewStateCnt++] = 25;
                            break;
                        case 27:
                            if (curChar == 47)
                                JjCheckNAddTwoStates(1, 2);
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
                        case 16:
                            if ((0x7fffffe87fffffe & l) == 0)
                                break;
                            if (kind > 26)
                                kind = 26;
                            JjCheckNAdd(16);
                            break;
                        case 1:
                            JjAddStates(10, 11);
                            break;
                        case 8:
                            JjAddStates(2, 4);
                            break;
                        case 10:
                            if (curChar == 92)
                                jjstateSet[jjnewStateCnt++] = 9;
                            break;
                        case 13:
                            JjAddStates(12, 13);
                            break;
                        case 22:
                        case 24:
                            JjCheckNAddStates(7, 9);
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
                        case 0:
                        case 16:
                            if ((jjbitVec0[i2] & l2) == 0)
                                break;
                            if (kind > 26)
                                kind = 26;
                            JjCheckNAdd(16);
                            break;
                        case 1:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(10, 11);
                            break;
                        case 8:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(2, 4);
                            break;
                        case 13:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(12, 13);
                            break;
                        case 22:
                        case 24:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjCheckNAddStates(7, 9);
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
            if ((i = jjnewStateCnt) == (startsAt = 28 - (jjnewStateCnt = startsAt)))
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
        27,
        8,
        10,
        11,
        18,
        19,
        22,
        23,
        26,
        1,
        2,
        13,
        14
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
        "[",
        "]",
        "{",
        "}",
        ":",
        ",",
        "=",
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
        ";",
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        "_"
    };
    public static readonly string[] lexStateNames = new[]
    {
        "DEFAULT"
    };
    static readonly long[] jjtoToken = new[]
    {
        0x3ffffffc01
    };
    static readonly long[] jjtoSkip = new[]
    {
        0x7e
    };
    protected SimpleCharStream input_stream;
    private readonly int[] jjrounds = new int[28];
    private readonly int[] jjstateSet = new int[56];
    protected char curChar;
    public DOTParserTokenManager(SimpleCharStream stream)
    {
        if (SimpleCharStream.staticFlag)
            throw new Exception("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
        input_stream = stream;
    }

    public DOTParserTokenManager(SimpleCharStream stream, int lexState) : this(stream)
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
        for (i = 28; i-- > 0;)
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
