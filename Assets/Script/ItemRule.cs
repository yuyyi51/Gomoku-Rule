using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRule : Rule
{
    protected int itemnum = 0;
    protected int mininum = 5;
    protected int maxdiff = 10;
    protected Item[] items = { null , new Bomb(), new Paint() };
    public override void Init(Board b)
    {
        base.Init(b);
        items[1].Init(b);
        items[2].Init(b);
    }
    public override void CheckAllBoard()
    {
        throw new NotImplementedException();
    }
    public override void CheckByCoordinate(int x, int y)
    {
        
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
    public override void Place(int x, int y, int color)
    {
        if(itemnum == 0 && board.GetPieceByCoordinate(x,y) == 0)
        {
            board.PlacePiece(x, y, color);
            board.turn++;
            return;
        }
        else if( itemnum == 1 || itemnum == 2 )
        {
            items[itemnum].Effect(x, y, color);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            itemnum = 0;
            Debug.Log("normal");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            itemnum = 1;
            Debug.Log("Bomb");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            itemnum = 2;
            Debug.Log("Paint");
        }
    }
}
