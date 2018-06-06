using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

    private string userName;
    private int score;
    public string UserName
    {
        get { return userName; }
        set
        {
            userName = value;
        }
    }
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
        }
    }
    public HighScore(string userName,int score)
    {
        this.userName = userName;
        this.score = score;
    }
}
