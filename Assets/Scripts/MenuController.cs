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
    [SerializeField]
    private Canvas normalGame;
    // Use this for initialization
    void Start() {
        audioBackground = GetComponent<AudioSource>();
        audioBackground.Play();
        mainCanvas.gameObject.SetActive(false);
        normalGame.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        normalGame.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        normalGame.transform.Find("GameOver").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update() {

    }
    public void OnBtnTimeGameClick()
    {
        mainCanvas.gameObject.SetActive(true);
        normalGame.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        FillGrid.instance.Init();
        
    }
    public void OnBtnNormalGameClick()
    {
        normalGame.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        NormalGameController.instance.Init();
        
        
    }
    
    public void OnBtnAchievement()
    {
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(true);
    }
    public void OnBtnPauseTime()
    {
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(true);
        FillGrid.instance.SetActive(false);
    }
    public void OnBtnPauseNormal()
    {
        normalGame.transform.Find("PausePanel").gameObject.SetActive(true);
        NormalGameController.instance.setActive(false);
    }
    public void OnBtnQuitTimeGame()
    {
        menuCanvas.gameObject.SetActive(true);
        FillGrid.instance.DestroyAll();
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);

        normalGame.gameObject.SetActive(false);
        normalGame.transform.Find("PausePanel").gameObject.SetActive(false);
        normalGame.transform.Find("GameOver").gameObject.SetActive(false);

        achievementCanvas.gameObject.SetActive(false);
       
       
        

        
    }
    public void OnBtnQuitNormalGame()
    {
       
        NormalGameController.instance.DestroyAll();
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);

        normalGame.gameObject.SetActive(false);
        normalGame.transform.Find("PausePanel").gameObject.SetActive(false);
        normalGame.transform.Find("GameOver").gameObject.SetActive(false);

        achievementCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
    public void OnBtnContinuesTime()
    {
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        FillGrid.instance.SetActive(true);
    }
    public void OnBtnContinuesNormal()
    {
        normalGame.transform.Find("PausePanel").gameObject.SetActive(false);
        NormalGameController.instance.setActive(true);
    }
    public void OnBtnRePlayGameTime()
    {
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        FillGrid.instance.DestroyAll();
        FillGrid.instance.Init();
    }
    public void OnBtnReplayNormalGame()
    {
       
        NormalGameController.instance.DestroyAll();
        normalGame.transform.Find("GameOver").gameObject.SetActive(false);
        NormalGameController.instance.Init();
    }
    public void OnBtnExitAchievement()
    {
        achievementCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
}
