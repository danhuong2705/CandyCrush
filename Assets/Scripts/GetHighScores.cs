using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GetHighScores : MonoBehaviour {
    private List<HighScore> highScoreList;
    public static GetHighScores current;
    void Start()
    {
    }
    void Awake()
    {
        current = this;
    }
    public  List<HighScore> GetHighScoreList()
    {
        LoadHighScoreList();
        SortByScore();
        return highScoreList;
    }
    public void LoadHighScoreList()
    {

        highScoreList = new List<HighScore>();
        StreamReader reader = new StreamReader(@"G:\unity\Project\Candy Crush\input.txt");
        string line;
      
        while ((line = reader.ReadLine()) != null)
        {
            string[] s = line.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
            
            string name = s[0];
            int score = Convert.ToInt32(s[1]);
            highScoreList.Add(new HighScore(name, score));
        }
        reader.Close();
        SortByScore();
    }
    public void SaveHighScoreList()
    {
            StreamWriter writer = new StreamWriter(@"G:\unity\Project\Candy Crush\input.txt");
            foreach (HighScore s in highScoreList)
                    {
                writer.WriteLine(s.UserName +"|" + s.Score);
              
                    }
            writer.Close();

        highScoreList.Clear();

    }
    public void AddHighScore(string userName,int score)
    {
        LoadHighScoreList();
        highScoreList.Add(new HighScore(userName, score));
        SaveHighScoreList();
    }
    public void SortByScore()
    {
        highScoreList.Sort((x, y) => y.Score.CompareTo(x.Score));
        if (highScoreList.Count > 5)
        {
            highScoreList.RemoveAt(highScoreList.Count - 1);
        }
    }
}
