using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Item
{
    public override void Effect(int x, int y, int color)
    {
        for(int i = -1; i <= 1; ++i)
        {
            for(int j = -1; j <= 1; ++j)
            {
                board.RemovePiece(x + i, y + j);
            }
        }
    }
}
