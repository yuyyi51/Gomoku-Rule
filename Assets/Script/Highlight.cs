using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool _init = false ;
    private int colorTrans = 1;     //用来控制亮暗的变量
    private int x;
    private int y;
    public bool placed = false;     //该格点是否有棋子
    public void Init(int n1, int n2)
    {
        if (_init)
            return;
        _init = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        x = n1;
        y = n2;
    }
    public void Reset()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        placed = false;
    }
    // Use this for initialization
    void Start()
    {

    }

    void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    void OnMouseOver()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a >= 1.0)
            colorTrans = -1;
        if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0.0)
            colorTrans = 1;
        gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1) * Time.deltaTime * colorTrans;
    }

    void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked:" + x + " " + y);
        //TODO
        object[] obj = new object[2];
        obj[0] = x;
        obj[1] = y;
        SendMessageUpwards("ApplyPlace", obj, SendMessageOptions.RequireReceiver);      //向board的ApplyPlace函数发送消息
    }
    // Update is called once per frame
    void Update()
    {

    }
}
