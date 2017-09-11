using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : MonoBehaviour
{
    protected Board board ;
    protected const string ruleName = "";
    public virtual void Place(int x, int y, int color)
    {
        if(board.GetPieceByCoordinate(x,y) == 0)
        {
            board.PlacePiece(x, y, color);
            board.turn++;
        }
    }
	public abstract void CheckByCoordinate(int x, int y);
    public abstract void CheckAllBoard();
    public abstract int CheckVictoryCondition(int a, int b);
    public virtual void Reset()
    {
        board.Reset();
    }
    public virtual void Init(Board b)
    {
        board = b;
    }
}
