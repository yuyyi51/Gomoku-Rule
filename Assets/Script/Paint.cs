using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : Item
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
    protected bool[,] logic;
    public override void Effect(int x, int y, int color)
    {
        List<Point> li = dfs(x, y, color, 2);
        foreach (Point p in li)
        {
            board.RemovePiece(p.x, p.y);
            board.PlacePiece(p.x, p.y, color);
        }
    }
    protected List<Point> dfs(int x, int y, int color, int radius)
    {
        List<Point> re = new List<Point>();
        logic = new bool[19, 19];
        dfsSub(x, y, color, ref re, radius);
        return re;
    }
    protected void dfsSub(int x, int y, int color, ref List<Point> list, int radius)
    {
        if (radius <= 0)
            return;
        if (!board.IsLeagl(x, y))
            return;
        if (logic[x, y] == true)
            return;
        if (board.GetPieceByCoordinate(x, y) == color || board.GetPieceByCoordinate(x, y) == 0)
            return;
        logic[x, y] = true;
        list.Add(new Point(x, y));
        dfsSub(x - 1, y, color, ref list, radius - 1);
        dfsSub(x + 1, y, color, ref list, radius - 1);
        dfsSub(x, y - 1, color, ref list, radius - 1);
        dfsSub(x, y + 1, color, ref list, radius - 1);

    }
}
