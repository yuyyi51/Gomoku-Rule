using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameController gameController;
    private Vector2 TL;             //top-left
    private Vector2 TR;             //top-right
    private Vector2 DL;             //down-left
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    private bool _init = false;
    public GameObject node;
    public GameObject[] piece;
    public int turn;
    private GameObject[,] nodes = new GameObject[19,19];
    private GameObject[,] pieces = new GameObject[19,19];
    public int[,] data;
    public Rule gameRule;
    public int ruleNum;
    private bool operable ;
    public bool holded;
    public enum PicecColor { Black = 1, White = 2 };
    public bool empty;

    //Begin from top-left
    //Left to right
    //Top to down    
    public void Reset()
    {
        for(int i = 0; i != 19; ++i)
        {
            for(int j = 0; j != 19; ++j)
            {
                nodes[i, j].GetComponent<Highlight>().Reset();
                Destroy(pieces[i, j]);
            }
        }
        Array.Clear(data, 0, data.Length);
        operable = true;
        holded = false;
        turn = 0;
        empty = true;
    }
    public Vector2 GetVectorByCoordinate(int x, int y)
    {
        Vector2 v2 = new Vector2();
        v2.x = TL.x + (TR - TL).x / 18.0f * x;
        v2.y = TL.y + (DL - TL).y / 18.0f * y;
        return v2;
    }

    public bool IsLeagl(int x, int y)
    {
        return x >= 0 && x < 19 && y >= 0 && y < 19;
    }

    public void Init()
    {
        if (_init)
            return;
        TL = point1.transform.position;
        TR = point2.transform.position;
        DL = point3.transform.position;
        data = new int[19, 19];
        for (int i = 0; i != 19; ++i)
        {
            for(int j = 0; j != 19; ++j)
            {
                Vector2 pos = GetVectorByCoordinate(i, j);
                GameObject obj = Instantiate(node, pos, gameObject.transform.rotation,transform);
                //obj.transform.parent = gameObject.transform;
                obj.GetComponent<Highlight>().Init(i,j);
                nodes[i,j] = obj;
            }
        }
        switch(ruleNum)
        {
            case 1:
                gameObject.AddComponent<NormalRule>();
                break;
            case 2:
                gameObject.AddComponent<AIRule>();
                break;
            case 3:
                gameObject.AddComponent<AIDRule>();
                break;
            case 4:
                gameObject.AddComponent<ExplosiveRule>();
                break;
            case 5:
                gameObject.AddComponent<NewRule>();
                break;
            case 6:
                gameObject.AddComponent<ItemRule>();
                break;
            default:
                gameObject.AddComponent<NormalRule>();
                break;
        }
        
        gameRule = gameObject.GetComponent<Rule>();
        gameRule.Init(this);
        gameObject.GetComponentInParent<GameController>().gameRule = gameRule;
        _init = true;
        operable = true;
        holded = false;
        empty = true;
        turn = 0;
    }

    public void PlacePiece(int x, int y, int color)
    {
        Vector2 pos = GetVectorByCoordinate(x, y);
        GameObject o = (GameObject)Instantiate(piece[color-1], pos, gameObject.transform.rotation);
        o.transform.parent = gameObject.transform ;
        pieces[x, y] = o;
        data[x, y] = color;
        nodes[x, y].GetComponent<Highlight>().placed = true;
        nodes[x, y].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void ApplyPlace(object[] obj)
    {
        if (!operable)
            return;
        int cox = -1, coy = -1;
        if(obj[0] is int)
        {
            cox = (int)obj[0];
        }
        if(obj[1] is int)
        {
            coy = (int)obj[1];
        }
        Debug.Log("Message:" + cox + " " + coy);
        gameRule.Place(cox, coy, turn % 2 + 1);
        empty = false;
        gameRule.CheckByCoordinate(cox, coy);
        gameController.Change();
        
    }

    public int GetPieceByCoordinate(int x, int y)
    {
        if (!IsLeagl(x,y))
            return -1;
        return data[x, y];
    }

    public GameObject GetPieceObjectByCoordinate(int x, int y)
    {
        if (!IsLeagl(x, y))
            return null;
        return pieces[x, y];
    }

    public GameObject GetNodeByCoordinate(int x, int y)
    {
        if (!IsLeagl(x, y))
            return null;
        return nodes[x, y];
    }

    public void RemovePiece(int x, int y)
    {
        if (!IsLeagl(x, y))
            return;
        if (data[x, y] == 0)
            return;
        Destroy(pieces[x, y]);
        nodes[x, y].GetComponent<Highlight>().Reset();
        data[x, y] = 0;
    }
    
    // Use this for initialization
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ruleNum = 1;
            ChangeRule();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ruleNum = 2;
            ChangeRule();
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ruleNum = 3;
            ChangeRule();
        }
    }

    public void Hold()
    {
        operable = false;
        holded = true;
    }

    public void ChangeRule()
    {
        Reset();
        //Des
        //Destroy();
        DestroyImmediate(gameObject.GetComponent<Rule>());
        //gameRule = null;
        switch (ruleNum)
        {
            case 1:
                gameObject.AddComponent<NormalRule>();
                break;
            case 2:
                gameObject.AddComponent<AIRule>();
                break;
            case 3:
                gameObject.AddComponent<AIDRule>();
                break;
            case 4:
                gameObject.AddComponent<ExplosiveRule>();
                break;
            case 5:
                gameObject.AddComponent<NewRule>();
                break;
            case 6:
                gameObject.AddComponent<ItemRule>();
                break;
            default:
                gameObject.AddComponent<NormalRule>();
                break;
        }
        gameRule = gameObject.GetComponent<Rule>();
        gameRule.Init(this);
        gameObject.GetComponentInParent<GameController>().gameRule = gameRule;
        gameController.ResetScore();
    }
}
