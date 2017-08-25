using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRule : Rule
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
    protected int[,] logicBoard = new int[19, 19];
    new protected const string ruleName = "第二模式";
    public override void CheckAllBoard()
    {
        throw new NotImplementedException();
    }
    public override void CheckByCoordinate(int x, int y)
    {
        Debug.Log(ruleName + "按坐标运行");
        int score = 0;
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
            int c = t1.Count + t2.Count + 1;
            score += c / mininum;
        }
        t1 = MoveByDelta(x, y, 0, 1, color);
        t2 = MoveByDelta(x, y, 0, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
            int c = t1.Count + t2.Count + 1;
            score += c / mininum;
        }
        t1 = MoveByDelta(x, y, 1, 1, color);
        t2 = MoveByDelta(x, y, -1, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
            int c = t1.Count + t2.Count + 1;
            score += c / mininum;
        }
        t1 = MoveByDelta(x, y, -1, 1, color);
        t2 = MoveByDelta(x, y, 1, -1, color);
        if (t1.Count + t2.Count + 1 >= mininum)
        {
            t.AddRange(t1);
            t.AddRange(t2);
            int c = t1.Count + t2.Count + 1;
            score += c / mininum;
        }
        if (t.Count > 0)
        {
            t.Add(new Point(x, y));
            if (color == (int)Board.PicecColor.Black)
                SendMessageUpwards("AddBlackScore", score, SendMessageOptions.RequireReceiver);
            if (color == (int)Board.PicecColor.White)
                SendMessageUpwards("AddWhiteScore", score, SendMessageOptions.RequireReceiver);
            DealPieces(t);
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
        while (board.GetPieceByCoordinate(tx, ty) == co && logicBoard[tx,ty] != 1)
        {
            re.Add(new Point(tx, ty));
            tx += dx;
            ty += dy;
        }
        return re;
    }
    protected void DealPieces(List<Point> li)
    {
        foreach(Point p in li)
        {
            int x = p.x;
            int y = p.y;
            logicBoard[x, y] = 1;
            GameObject obj = board.GetPieceObjectByCoordinate(x, y);
            obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
        }
    }
}
