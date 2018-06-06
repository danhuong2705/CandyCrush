using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController1 : MonoBehaviour {
    [SerializeField]
    private Canvas main;
    [SerializeField]
    private Text score;
    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }
    void Update()
    {
        if(GameController.current.time <= 0)
        {
            GameController.current.StopCheckForPotentialMatches();
            GameController.current.DestroyAllCandy();
            main.transform.Find("GameOver").gameObject.SetActive(true);
            score.text = GameController.current.score+"";
        }
    }
    public void OnBtnQuit()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnBtnPause()
    {
        Time.timeScale = 0f;
        main.transform.Find("PausePanel").gameObject.SetActive(true);
    }
    public void OnBtnContinues()
    {
        main.transform.Find("PausePanel").gameObject.SetActive(false);
        Time.timeScale = 1f;
       
    }
    public void OnBtnGameOverQuit()
    {
        main.transform.Find("GameOver").gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
