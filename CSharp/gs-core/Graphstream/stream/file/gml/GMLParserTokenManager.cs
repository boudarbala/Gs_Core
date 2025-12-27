using Gs_Core.Graphstream.Util.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static Org.Graphstream.Stream.File.Gml.ElementType;
using static Org.Graphstream.Stream.File.Gml.EventType;
using static Org.Graphstream.Stream.File.Gml.AttributeChangeEvent;
using static Org.Graphstream.Stream.File.Gml.Mode;
using static Org.Graphstream.Stream.File.Gml.What;
using static Org.Graphstream.Stream.File.Gml.TimeFormat;
using static Org.Graphstream.Stream.File.Gml.OutputType;
using static Org.Graphstream.Stream.File.Gml.OutputPolicy;
using static Org.Graphstream.Stream.File.Gml.LayoutPolicy;
using static Org.Graphstream.Stream.File.Gml.Quality;
using static Org.Graphstream.Stream.File.Gml.Option;
using static Org.Graphstream.Stream.File.Gml.AttributeType;
using static Org.Graphstream.Stream.File.Gml.Balise;
using static Org.Graphstream.Stream.File.Gml.GEXFAttribute;
using static Org.Graphstream.Stream.File.Gml.METAAttribute;
using static Org.Graphstream.Stream.File.Gml.GRAPHAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTESAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTRIBUTEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESAttribute;
using static Org.Graphstream.Stream.File.Gml.NODEAttribute;
using static Org.Graphstream.Stream.File.Gml.ATTVALUEAttribute;
using static Org.Graphstream.Stream.File.Gml.PARENTAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESAttribute;
using static Org.Graphstream.Stream.File.Gml.SPELLAttribute;
using static Org.Graphstream.Stream.File.Gml.COLORAttribute;
using static Org.Graphstream.Stream.File.Gml.POSITIONAttribute;
using static Org.Graphstream.Stream.File.Gml.SIZEAttribute;
using static Org.Graphstream.Stream.File.Gml.NODESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGEAttribute;
using static Org.Graphstream.Stream.File.Gml.THICKNESSAttribute;
using static Org.Graphstream.Stream.File.Gml.EDGESHAPEAttribute;
using static Org.Graphstream.Stream.File.Gml.IDType;
using static Org.Graphstream.Stream.File.Gml.ModeType;
using static Org.Graphstream.Stream.File.Gml.WeightType;
using static Org.Graphstream.Stream.File.Gml.EdgeType;
using static Org.Graphstream.Stream.File.Gml.NodeShapeType;
using static Org.Graphstream.Stream.File.Gml.EdgeShapeType;
using static Org.Graphstream.Stream.File.Gml.ClassType;
using static Org.Graphstream.Stream.File.Gml.TimeFormatType;
using static Org.Graphstream.Stream.File.Gml.GPXAttribute;
using static Org.Graphstream.Stream.File.Gml.WPTAttribute;
using static Org.Graphstream.Stream.File.Gml.LINKAttribute;
using static Org.Graphstream.Stream.File.Gml.EMAILAttribute;
using static Org.Graphstream.Stream.File.Gml.PTAttribute;
using static Org.Graphstream.Stream.File.Gml.BOUNDSAttribute;
using static Org.Graphstream.Stream.File.Gml.COPYRIGHTAttribute;
using static Org.Graphstream.Stream.File.Gml.FixType;
using static Org.Graphstream.Stream.File.Gml.GraphAttribute;
using static Org.Graphstream.Stream.File.Gml.LocatorAttribute;
using static Org.Graphstream.Stream.File.Gml.NodeAttribute;
using static Org.Graphstream.Stream.File.Gml.EdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.DataAttribute;
using static Org.Graphstream.Stream.File.Gml.PortAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointAttribute;
using static Org.Graphstream.Stream.File.Gml.EndPointType;
using static Org.Graphstream.Stream.File.Gml.HyperEdgeAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyAttribute;
using static Org.Graphstream.Stream.File.Gml.KeyDomain;
using static Org.Graphstream.Stream.File.Gml.KeyAttrType;
using static Org.Graphstream.Stream.File.Gml.GraphEvents;
using static Org.Graphstream.Stream.File.Gml.ThreadingModel;
using static Org.Graphstream.Stream.File.Gml.CloseFramePolicy;
using static Org.Graphstream.Stream.File.Gml.Measures;
using static Org.Graphstream.Stream.File.Gml.Token;
using static Org.Graphstream.Stream.File.Gml.Extension;
using static Org.Graphstream.Stream.File.Gml.DefaultEdgeType;
using static Org.Graphstream.Stream.File.Gml.AttrType;

namespace Gs_Core.Graphstream.Stream.File.Gml;

public class GMLParserTokenManager : GMLParserConstants
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
                if ((active0 & 0x3000) != 0)
                {
                    jjmatchedKind = 14;
                    return 11;
                }

                return -1;
            case 1:
                if ((active0 & 0x3000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 1;
                    return 11;
                }

                return -1;
            case 2:
                if ((active0 & 0x3000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 2;
                    return 11;
                }

                return -1;
            case 3:
                if ((active0 & 0x3000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 3;
                    return 11;
                }

                return -1;
            case 4:
                if ((active0 & 0x1000) != 0)
                    return 11;
                if ((active0 & 0x2000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 4;
                    return 11;
                }

                return -1;
            case 5:
                if ((active0 & 0x2000) != 0)
                {
                    jjmatchedKind = 14;
                    jjmatchedPos = 5;
                    return 11;
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
            case 91:
                return JjStopAtPos(0, 8);
            case 93:
                return JjStopAtPos(0, 9);
            case 68:
            case 100:
                return JjMoveStringLiteralDfa1_0(0x2000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa1_0(0x1000);
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
            case 73:
            case 105:
                return JjMoveStringLiteralDfa2_0(active0, 0x2000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa2_0(active0, 0x1000);
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
                return JjMoveStringLiteralDfa3_0(active0, 0x1000);
            case 71:
            case 103:
                return JjMoveStringLiteralDfa3_0(active0, 0x2000);
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
            case 80:
            case 112:
                return JjMoveStringLiteralDfa4_0(active0, 0x1000);
            case 82:
            case 114:
                return JjMoveStringLiteralDfa4_0(active0, 0x2000);
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
                return JjMoveStringLiteralDfa5_0(active0, 0x2000);
            case 72:
            case 104:
                if ((active0 & 0x1000) != 0)
                    return JjStartNfaWithStates_0(4, 12, 11);
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
            case 80:
            case 112:
                return JjMoveStringLiteralDfa6_0(active0, 0x2000);
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
                if ((active0 & 0x2000) != 0)
                    return JjStartNfaWithStates_0(6, 13, 11);
                break;
            default:
                break;
        }

        return JjStartNfa_0(5, active0);
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
        jjnewStateCnt = 15;
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
                                if (kind > 10)
                                    kind = 10;
                                JjCheckNAddTwoStates(1, 2);
                            }
                            else if ((0x280000000000 & l) != 0)
                            {
                                if (kind > 14)
                                    kind = 14;
                                JjCheckNAdd(11);
                            }
                            else if (curChar == 35)
                                JjCheckNAddTwoStates(13, 14);
                            else if (curChar == 39)
                                JjCheckNAddTwoStates(8, 9);
                            else if (curChar == 34)
                                JjCheckNAddTwoStates(5, 6);
                            if ((0x280000000000 & l) != 0)
                                JjCheckNAdd(1);
                            break;
                        case 1:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 10)
                                kind = 10;
                            JjCheckNAddTwoStates(1, 2);
                            break;
                        case 2:
                            if (curChar == 46)
                                JjCheckNAdd(3);
                            break;
                        case 3:
                            if ((0x3ff000000000000 & l) == 0)
                                break;
                            if (kind > 10)
                                kind = 10;
                            JjCheckNAdd(3);
                            break;
                        case 4:
                            if (curChar == 34)
                                JjCheckNAddTwoStates(5, 6);
                            break;
                        case 5:
                            if ((0xfffffffbffffffff & l) != 0)
                                JjCheckNAddTwoStates(5, 6);
                            break;
                        case 6:
                            if (curChar == 34 && kind > 11)
                                kind = 11;
                            break;
                        case 7:
                            if (curChar == 39)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 8:
                            if ((0xffffff7fffffffff & l) != 0)
                                JjCheckNAddTwoStates(8, 9);
                            break;
                        case 9:
                            if (curChar == 39 && kind > 11)
                                kind = 11;
                            break;
                        case 10:
                            if ((0x280000000000 & l) == 0)
                                break;
                            if (kind > 14)
                                kind = 14;
                            JjCheckNAdd(11);
                            break;
                        case 11:
                            if ((0x3ff600000000000 & l) == 0)
                                break;
                            if (kind > 14)
                                kind = 14;
                            JjCheckNAdd(11);
                            break;
                        case 12:
                            if (curChar == 35)
                                JjCheckNAddTwoStates(13, 14);
                            break;
                        case 13:
                            if ((0xffffffffffffdbff & l) != 0)
                                JjCheckNAddTwoStates(13, 14);
                            break;
                        case 14:
                            if ((0x2400 & l) != 0 && kind > 15)
                                kind = 15;
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
                            if ((0x7fffffe07fffffe & l) == 0)
                                break;
                            if (kind > 14)
                                kind = 14;
                            JjCheckNAdd(11);
                            break;
                        case 5:
                            JjAddStates(0, 1);
                            break;
                        case 8:
                            JjAddStates(2, 3);
                            break;
                        case 11:
                            if ((0x7fffffe87fffffe & l) == 0)
                                break;
                            if (kind > 14)
                                kind = 14;
                            JjCheckNAdd(11);
                            break;
                        case 13:
                            JjAddStates(4, 5);
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
                        case 5:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(0, 1);
                            break;
                        case 8:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(2, 3);
                            break;
                        case 13:
                            if ((jjbitVec0[i2] & l2) != 0)
                                JjAddStates(4, 5);
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
            if ((i = jjnewStateCnt) == (startsAt = 15 - (jjnewStateCnt = startsAt)))
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
        5,
        6,
        8,
        9,
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
        "[",
        "]",
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
        0xff01
    };
    static readonly long[] jjtoSkip = new[]
    {
        0x1e
    };
    protected SimpleCharStream input_stream;
    private readonly int[] jjrounds = new int[15];
    private readonly int[] jjstateSet = new int[30];
    protected char curChar;
    public GMLParserTokenManager(SimpleCharStream stream)
    {
        if (SimpleCharStream.staticFlag)
            throw new Exception("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
        input_stream = stream;
    }

    public GMLParserTokenManager(SimpleCharStream stream, int lexState) : this(stream)
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
        for (i = 15; i-- > 0;)
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
}
