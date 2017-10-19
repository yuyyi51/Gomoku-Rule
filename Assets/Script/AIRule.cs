using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRule : Rule
{
    //new protected const string ruleName = "AI";
    Evaluator evaluator;
    int num = 0;
    public int scolor = 1;
    protected int MoveByDelta(int x, int y, int dx, int dy, int co)
    {
        int re = 0;
        int tx = x + dx;
        int ty = y + dy;
        while (board.GetPieceByCoordinate(tx, ty) == co)
        {
            ++re;
            tx += dx;
            ty += dy;
        }
        return re;
    }

    public override void CheckByCoordinate(int x, int y)
    {
        Debug.Log(ruleName + "按坐标运行");
        int color = board.GetPieceByCoordinate(x, y);
        int max = 0;
        int t = MoveByDelta(x, y, 1, 0, color) + MoveByDelta(x, y, -1, 0, color) + 1;
        max = t > max ? t : max;
        t = MoveByDelta(x, y, 0, 1, color) + MoveByDelta(x, y, 0, -1, color) + 1;
        max = t > max ? t : max;
        t = MoveByDelta(x, y, 1, 1, color) + MoveByDelta(x, y, -1, -1, color) + 1;
        max = t > max ? t : max;
        t = MoveByDelta(x, y, 1, -1, color) + MoveByDelta(x, y, -1, 1, color) + 1;
        max = t > max ? t : max;
        if (max >= 5)
        {
            if (color == (int)Board.PicecColor.Black)
            {
                SendMessageUpwards("AddBlackScore", 1, SendMessageOptions.RequireReceiver);
                num = 1;
            }

            if (color == (int)Board.PicecColor.White)
            {
                SendMessageUpwards("AddWhiteScore", 1, SendMessageOptions.RequireReceiver);
                num = 2;
            }

        }
        if(board.turn % 2 == scolor)
            AIPlacePiece();
    }

    public override void CheckAllBoard()
    {
        Debug.Log(ruleName + "全局运行");
    }

    private void AIPlacePiece()
    {
        object[] obj = new object[2];
        if (board.empty)
        {
            obj = new object[2];
            obj[0] = 9;
            obj[1] = 9;
            board.ApplyPlace(obj);
            return;
        }
        if (board.turn % 2 == 1)
        {
            evaluator = new Evaluator(board.data, 2, 1);
        }
        else
        {
            evaluator = new Evaluator(board.data, 1, 2);
        }
        List<KeyValuePair<int, int>> li = evaluator.Evaluate();
        if (li == null)
            return;
        System.Random rand = new System.Random();
        KeyValuePair<int, int> pair = li[rand.Next(li.Count)];
        obj[0] = pair.Key;
        obj[1] = pair.Value;
        Debug.Log(pair.Key + " " + pair.Value);
        board.ApplyPlace(obj);
    }

    public override int CheckVictoryCondition(int a, int b)
    {
        int re = num;
        num = 0;
        return re;
    }
    void Start()
    {
        ruleName = "AI";
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            board.Reset();
            scolor = 1 - scolor;
            if (scolor == 0)
                AIPlacePiece();
        }
    }
    public override void Reset()
    {
        base.Reset();
        scolor = 1 - scolor;
        if (scolor == 0)
            AIPlacePiece();
    }
}
