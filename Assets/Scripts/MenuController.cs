using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private AudioSource audioBackground;
    [SerializeField]
    private Canvas achievementCanvas;
    [SerializeField]
    private Canvas menuCanvas;
    [SerializeField]
    private Canvas mainCanvas;
    // Use this for initialization
    void Start() {
        audioBackground = GetComponent<AudioSource>();
        audioBackground.Play();
        mainCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update() {

    }
    public void OnBtnTimeGameClick()
    {
        
        mainCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        GameController.current.Init();
        
    }

    
    public void OnBtnAchievement()
    {

        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(true);
    }
    public void OnBtnPauseTime()
    {
     
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(true);
    }

    public void OnBtnQuitTimeGame()
    {
        menuCanvas.gameObject.SetActive(true);
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);

        achievementCanvas.gameObject.SetActive(false);
        // GameController.current.StopAllCoroutines();
        GameController.current.StopCheckForPotentialMatches();
        GameController.current.DestroyAllCandy();

    }
    public void OnBtnQuitNormalGame()
    {
    
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
    public void OnBtnContinuesTime()
    {
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
    }

    public void OnBtnRePlayGameTime()
    {
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);

    }

    public void OnBtnExitAchievement()
    {
        achievementCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
}
