using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool _init = false ;
    private int colorTrans = 1;
    private int x;
    private int y;
    public bool placed = false;
    public void Init(int n1, int n2)
    {
        if (_init)
            return;
        _init = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        x = n1;
        y = n2;
    }
    // Use this for initialization
    void Start()
    {

    }

    void OnMouseEnter()
    {
        if (placed)
            return;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    void OnMouseOver()
    {
        if (placed)
            return;
        if (gameObject.GetComponent<SpriteRenderer>().color.a >= 1.0)
            colorTrans = -1;
        if (gameObject.GetComponent<SpriteRenderer>().color.a <= 0.0)
            colorTrans = 1;
        gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 1) * Time.deltaTime * colorTrans;
    }

    void OnMouseExit()
    {
        if (placed)
            return;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }

    void OnMouseDown()
    {
        if (placed)
            return;
        Debug.Log("Clicked:" + x + " " + y);
        //TODO
        object[] obj = new object[2];
        obj[0] = x;
        obj[1] = y;
        SendMessageUpwards("ApplyPlace", obj, SendMessageOptions.RequireReceiver);
        placed = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
