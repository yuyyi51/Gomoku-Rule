using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _whiteScore;
    private int _blackScore;
    private Rule _gameRule;

    public int whiteScore
    {
        get
        {
            return _whiteScore;
        }
    }
    public int blackScore
    {
        get
        {
            return _blackScore;
        }
    }
    public Rule gameRule
    {
        get
        {
            return _gameRule;
        }
        set
        {
            _gameRule = value;
        }
    }

    private bool changed = false;

    public void Change()
    {
        changed = true;
    }

    private void AddWhiteScore(object obj)
    {
        if (obj is int)
            _whiteScore += (int)obj;
    }

    private void AddBlackScore(object obj)
    {
        if (obj is int)
            _blackScore += (int)obj;
    }

    public int GetScore(int n)
    {
        if (n == 1)
            return blackScore;
        if (n == 2)
            return whiteScore;
        return 0;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        int a;
        if(changed)
        {
            a = gameRule.CheckVictoryCondition(blackScore, whiteScore);
            if (a == 1)
            {
                Debug.Log("Black Win");
                gameRule.Hold();
                //gameRule.Reset();
            }
            else if (a == 2)
            {
                Debug.Log("White Win");
                gameRule.Hold();
                //gameRule.Reset();
            }

        }
        if(gameRule.Holded())
        {
            if (Input.GetKey(KeyCode.Return))
                gameRule.Reset();
        }
        changed = false;

	}
}
