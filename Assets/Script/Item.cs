using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected Board board;
    abstract public void Effect(int x, int y, int color);
    public virtual void Init(Board b)
    {
        board = b;
    }
}
