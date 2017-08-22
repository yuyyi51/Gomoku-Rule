using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess
{
    private int _x;
    private int _y;
    private int _color;

    public int x
    {
        get
        {
            return _x;
        }

        set
        {
            _x = value;
        }
    }
    public int y
    {
        get
        {
            return _y;
        }

        set
        {
            _y = value;
        }
    }
    public int color
    {
        get
        {
            return _color;
        }

        set
        {
            _color = value;
        }
    }

    Chess()
    {

    }
    Chess(int x, int y, int color)
    {
        _x = x;
        _y = y;
        _color = color;
    }

}
