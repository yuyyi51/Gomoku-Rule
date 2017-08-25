using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected Board board;
    abstract protected void Effect(int x, int y, int color);
}
