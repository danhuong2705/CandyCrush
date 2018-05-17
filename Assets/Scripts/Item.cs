using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int X
    {
        get;
        private set;
    }
    public int Y
    {
        get;
        private set;
    }
    [HideInInspector]
    public int id;
    public void OnItemPostionChanged(int newX,int newY)
    {
        X = newX;
        Y = newY;
        gameObject.name = string.Format("Sprite[{0}][{1}]", X, Y);
    }
    void OnMouseDown()
    {
        if(OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }
    public delegate void OnMouseOverItem(Item item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
