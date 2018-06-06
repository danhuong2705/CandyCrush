using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController2 : MonoBehaviour {
    [SerializeField]
    private Canvas AchievementCanvas;
    [SerializeField]
    private Canvas MenuCanvas;
    [SerializeField]
    private Canvas HighScoreCanvas;
    [SerializeField]
    private Transform contentsTrans;
    List<HighScore> list;
    public void OnBtnPlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }
    public void OnBtnAchievement()
    {
        MenuCanvas.gameObject.SetActive(false);
        AchievementCanvas.gameObject.SetActive(true);

    }
    public void OnBtnAchieQuit()
    {
        MenuCanvas.gameObject.SetActive(true);
        AchievementCanvas.gameObject.SetActive(false);
    }
    public void OnbtnHighScore()
    {
        MenuCanvas.gameObject.SetActive(false);
        HighScoreCanvas.gameObject.SetActive(true);
        list = GetHighScores.current.GetHighScoreList();
        HighScoreController.current.PrintListHighScore(list);
    }
    public void OnBtnHighScoreBack()
    {
        foreach (Transform child in contentsTrans.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        MenuCanvas.gameObject.SetActive(true);
        HighScoreCanvas.gameObject.SetActive(false);
    }
}
