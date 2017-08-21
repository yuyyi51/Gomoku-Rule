﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Vector2 TL;             //top-left
    private Vector2 TR;             //top-right
    private Vector2 DL;             //down-left
    public GameObject point1;
    public GameObject point2;
    public GameObject point3;
    private bool _init = false;
    public GameObject node;
    public GameObject[] piece;
    public int turn = 0;
    private GameObject[,] nodes = new GameObject[19,19];
    private GameObject[,] pieces = new GameObject[19,19];
    private int[,] data = new int[19, 19];

    //Begin from top-left
    //Left to right
    //Top to down
    public Vector2 GetVectorByCoordinate(int x, int y)
    {
        Vector2 v2 = new Vector2();
        v2.x = TL.x + (TR - TL).x / 18.0f * x;
        v2.y = TL.y + (DL - TL).y / 18.0f * y;
        return v2;
    }

    public void Init()
    {
        if (_init)
            return;
        TL = point1.transform.position;
        TR = point2.transform.position;
        DL = point3.transform.position;
        for(int i = 0; i != 19; ++i)
        {
            for(int j = 0; j != 19; ++j)
            {
                Vector2 pos = GetVectorByCoordinate(i, j);
                GameObject obj = Instantiate(node, pos, gameObject.transform.rotation, gameObject.transform);
                obj.GetComponent<Highlight>().Init(i,j);
                nodes[i,j] = obj;
            }
        }
        _init = true;
    }

    private void ApplyPlace(object[] obj)
    {
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
        Vector2 pos = GetVectorByCoordinate(cox, coy);
        GameObject o = Instantiate(piece[turn % 2], pos, gameObject.transform.rotation, gameObject.transform);
        pieces[cox, coy] = o;
        data[cox, coy] = turn % 2 + 1;
        //nodes[cox, coy].GetComponent<Highlight>().placed = true;

        turn++;
    }

    public int GetPieceByCoordinate(int x, int y)
    {
        if (x < 0 || x > 18 || y < 0 || y > 18)
            return -1;
        return data[x, y];
    }

    public GameObject GetPieceObjectByCoordinate(int x, int y)
    {
        if (x < 0 || x > 18 || y < 0 || y > 18)
            return null;
        return pieces[x, y];
    }

    public GameObject GetNodeByCoordinate(int x, int y)
    {
        if (x < 0 || x > 18 || y < 0 || y > 18)
            return null;
        return nodes[x, y];
    }

    public List<GameObject> CheckByCoordinate(int x, int y)
    {
        int tx = x, ty = y;

    }
    // Use this for initialization
    void Start()
    {
        Init();

    }

    // Update is called once per frame
    void Update()
    {

    }
}