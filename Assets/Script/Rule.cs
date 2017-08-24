using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule : MonoBehaviour
{
    protected Board board ;
    protected const string ruleName = "";
	public abstract void CheckByCoordinate(int x, int y);
    public abstract void CheckAllBoard();
    public abstract int CheckVictoryCondition(int a, int b);
    public void Init(Board b)
    {
        board = b;
    }
}
