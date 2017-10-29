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
    public GameObject point1;       //处于棋盘三个角上的空对象，用于计算棋盘格点的坐标，由unity初始化
    public GameObject point2;
    public GameObject point3;
    private bool _init = false;
    public GameObject node;         //棋盘格点对象的预置体
    public GameObject[] piece;      //棋子对象的预置体
    public int turn;
    private GameObject[,] nodes = new GameObject[19,19];    //存放格点对象的数组
    private GameObject[,] pieces = new GameObject[19,19];   //存放棋子对象的数组
    public int[,] data;             //存储逻辑棋盘数据的数组
    public Rule gameRule;           //游戏规则
    public int ruleNum;             //当前游戏规则编号
    private bool operable ;         //棋盘是否处于可用状态
    public bool holded;             //游戏是否禁止当前玩家操作
    public enum PicecColor { Black = 1, White = 2 };
    public bool empty;              //棋盘是否为空

    //Begin from top-left
    //Left to right
    //Top to down    
    public void Reset()     //出于各种原因需要重置棋盘时调用的函数
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
    public Vector2 GetVectorByCoordinate(int x, int y)  //查找某个格点的世界坐标
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

    public void Init()  //初始化棋盘、规则、格点对象等
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
                GameObject obj = Instantiate(node, pos, gameObject.transform.rotation,transform);   //生成格点对象
                //obj.transform.parent = gameObject.transform;
                obj.GetComponent<Highlight>().Init(i,j);
                nodes[i,j] = obj;
            }
        }
        switch(ruleNum)     //决定游戏规则
        {
            case 1:
                gameObject.AddComponent<NormalRule>();  //玩家本地对战
                break;
            case 2:
                gameObject.AddComponent<AIRule>();      //人机对战
                break;
            case 3:
                gameObject.AddComponent<AIDRule>();     //人机混合对战
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

    public void PlacePiece(int x, int y, int color)     //放置棋子
    {
        Vector2 pos = GetVectorByCoordinate(x, y);
        GameObject o = Instantiate(piece[color-1], pos, gameObject.transform.rotation);
        o.transform.parent = gameObject.transform ;
        pieces[x, y] = o;
        data[x, y] = color;
        nodes[x, y].GetComponent<Highlight>().placed = true;
        nodes[x, y].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    public void ApplyPlace(object[] obj)    //用于其他对象通过消息机制调用的请求防止棋子函数
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
