using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementItem  {
    private string name;
    private string description;
    private int point;
    private bool isUnlocked;
    private int time;
    private Sprite sprite;
    private GameObject achivementRef;
    
    public string Name
    {
        get { return name; }
        set
        {
            name = value;
        }
    }
    public string Description
    {
        get { return description; }
        set
        {
            description = value;
        }
    }
    public int Point
    {
        get { return point; }
        set
        {
            point = value;
        }
    }
    public bool IsUnlocked
    {
        get { return isUnlocked; }
        set
        {
            isUnlocked = value;
        }
    }
    public int Time
    {
        get { return time; }
        set
        {
            time = value;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }

        set
        {
            sprite = value;
        }
    }

    public GameObject AchivementRef
    {
        get
        {
            return achivementRef;
        }

        set
        {
            achivementRef = value;
        }
    }

    public AchievementItem()
    {

    }

    public AchievementItem(string _name,string _description,int _point,bool _isUnlocked,int _time,Sprite _sprite,GameObject AchiementRef)
    {
        name = _name;
        description = _description;
        point = _point;
        isUnlocked = false;
        time = _time;
        sprite = _sprite;
        achivementRef = AchiementRef;
    }

}
