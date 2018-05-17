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
    void Start () {
        audioBackground = GetComponent<AudioSource>();
        audioBackground.Play();
    }

    // Update is called once per frame
    void Update () {
     
	}
    public void OnBtnPlayClick()
    {
        mainCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        FillGrid.instance.Init();
    }
    public void OnBtnAchievement()
    {
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(true);
    }
    public void OnBtnPause()
    {
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(true);
    }
    public void OnBtnQuit()
    {
        FillGrid.instance.DestroyAll();
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
        achievementCanvas.gameObject.SetActive(false);

        
    }
    public void OnBtnContinues()
    {
        mainCanvas.transform.Find("PausePanel").gameObject.SetActive(false);
    }
    public void OnBtnRePlayGame()
    {
        mainCanvas.transform.Find("GameOver").gameObject.SetActive(false);
        FillGrid.instance.DestroyAll();
        FillGrid.instance.Init();
    }
    public void OnBtnExitAchievement()
    {
        achievementCanvas.gameObject.SetActive(false);
        menuCanvas.gameObject.SetActive(true);
    }
}
