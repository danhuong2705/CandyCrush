using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour {
   
   
    public int Column { get; set; }
    public int Row { get; set; }

    public string Type { get; set; }
   public  static void SwapCandy(Candy a,Candy b)
    {
        int temp = a.Row;
        a.Row = b.Row;
        b.Row = temp;

        int tmp = a.Column;
        a.Column = b.Column;
        b.Column = tmp;
    }
    public bool IsSameType(Candy other)
    {
        if ( other == null || !(other is Candy))
        {
            throw new ArgumentException("otherCandy");
        }
        return string.Compare(this.Type, other.Type) == 0 ;
    }
    public void Assign(int row,int column,string type)
    {
        if (string.IsNullOrEmpty(type))
        {
            throw new ArgumentException("type");
        }
        Row = row;
        Column = column;
        Type = type;
       
    }
}
