using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    Vector3
           screenPoint,
           offset,
           scanPos,
           curPosition,
           curScreenPoint;
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
    public int ID
    {
        get { return id; }

    }
    public void OnItemPostionChanged(int newX,int newY)
    {
        X = newX;
        Y = newY;
        gameObject.name = string.Format("Sprite[{0}][{1}] - {2}", X, Y,ID);
    }
    void OnMouseDown()
    {
        if (OnMouseOverItemEventHandler != null)
        {
            OnMouseOverItemEventHandler(this);
        }
    }
    //void OnMouseDrag()
    //{
    //    if (OnMouseOverItemEventHandler != null)
    //    {
    //        OnMouseOverItemEventHandler(this);
    //    }
    //    scanPos = gameObject.transform.position;
    //    screenPoint = Camera.main.WorldToScreenPoint(scanPos);
    //    curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    //    curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
    //    curPosition.x = (float)(Mathf.Round(curPosition.x));
    //    transform.position = curPosition;
    //   // Debug.Log(string.Format("Sprite[{0}][{1}] - {2}", X, Y, ID));
    //}

    public delegate void OnMouseOverItem(Item item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;
}
