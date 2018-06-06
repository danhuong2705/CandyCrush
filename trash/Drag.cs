using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{


    private Vector3 mouseDownPos;
    Vector3
            screenPoint,
            offset,
            scanPos,
            curPosition,
            curScreenPoint;

    void OnMouseDown()
    {


    }




    void OnMouseDrag()
    {
        scanPos = gameObject.transform.position;
        screenPoint = Camera.main.WorldToScreenPoint(scanPos);
        curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        curPosition.x = (float)(Mathf.Round(curPosition.x));
        transform.position = curPosition;
        //if (Input.GetMouseButtonDown(0))
        //{

        //    //Debug.Log("clicked");
        //    //Debug.Log(Input.mousePosition);

        //    mouseDownPos = Input.mousePosition;

        //}
        //if (Input.GetMouseButtonUp(0))
        //{

        //    if (Input.mousePosition.y > mouseDownPos.y)
        //    {
        //        Debug.Log("You dragged up!");

        //    }
        //    else if (Input.mousePosition.y < mouseDownPos.y)
        //    {
        //        Debug.Log("You dragged down!");
        //    }

        //}
    }


}

