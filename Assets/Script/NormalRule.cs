using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRule : Rule
{
    new protected const string ruleName = "普通模式";

    protected override int MoveByDelta(int x, int y, int dx, int dy, int co)
    {
        int re = 0;
        int tx = x + dx;
        int ty = y + dy;
        while(board.GetPieceByCoordinate(tx, ty) == co)
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
        t = MoveByDelta(x, y, 1, -11, color) + MoveByDelta(x, y, -1, 1, color) + 1;
        max = t > max ? t : max;
        if( max >= 5 )
        {
            if (color == (int)Board.PicecColor.Black)
                SendMessageUpwards("AddBlackScore", 1, SendMessageOptions.RequireReceiver);
            if (color == (int)Board.PicecColor.White)
                SendMessageUpwards("AddWhiteScore", 1, SendMessageOptions.RequireReceiver);
        }
    }

    public override void CheckAllBoard()
    {
        Debug.Log(ruleName + "全局运行");
    }

    public override int CheckVictoryCondition(int a, int b)
    {
        Debug.Log(ruleName + "检查胜利条件");
        if(a > b && a >= 1)
        {
            return 1;
        }
        else if( a < b && b >= 1)
        {
            return 2;
        }
        return 0;
    }
}
