using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveRule : Rule
{
    protected class Point
    {
        public int x;
        public int y;
        public Point()
        {

        }
        public Point(int a, int b)
        {
            x = a;
            y = b;
        }
    }
    protected int mininum = 4;
    protected int maxdiff = 10;
    public override void CheckAllBoard()
    {
        throw new NotImplementedException();
    }
    public override void CheckByCoordinate(int x, int y)
    {
        Debug.Log(ruleName + "按坐标运行");
        int color = board.GetPieceByCoordinate(x, y);
        List<Point> t = new List<Point>();
        List<Point> t1 = null;
        List<Point> t2 = null;
        t1 = MoveByDelta(x, y, 1, 0, color);
        t2 = MoveByDelta(x, y, -1, 0, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
        }
        t1 = MoveByDelta(x, y, 0, 1, color);
        t2 = MoveByDelta(x, y, 0, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
        }
        t1 = MoveByDelta(x, y, 1, 1, color);
        t2 = MoveByDelta(x, y, -1, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
        }
        t1 = MoveByDelta(x, y, -1, 1, color);
        t2 = MoveByDelta(x, y, 1, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
        }
        if (t.Count > 0)
        {
            t.Add(new Point(x, y));
            if (color == (int)Board.PicecColor.Black)
                SendMessageUpwards("AddBlackScore", t.Count, SendMessageOptions.RequireReceiver);
            if (color == (int)Board.PicecColor.White)
                SendMessageUpwards("AddWhiteScore", t.Count, SendMessageOptions.RequireReceiver);
            RemovePieces(t);
        }
    }
    public override int CheckVictoryCondition(int a, int b)
    {
        if (a - b >= maxdiff)
        {
            return 1;
        }
        else if (b - a >= maxdiff)
        {
            return 2;
        }
        return 0;
    }
    protected List<Point> MoveByDelta(int x, int y, int dx, int dy, int co)
    {
        List<Point> re = new List<Point>();
        int tx = x + dx;
        int ty = y + dy;
        while (board.GetPieceByCoordinate(tx, ty) == co)
        {
            re.Add(new Point(tx, ty));
            tx += dx;
            ty += dy;
        }
        return re;
    }
    protected void RemovePieces(List<Point> li)
    {
        foreach (Point p in li)
        {
            board.RemovePiece(p.x, p.y);
        }
    }
}
