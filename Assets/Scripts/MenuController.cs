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
        SceneManager.LoadScene("Main");
    }
    public void OnBtnAchievement()
    {
        menuCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(true);
    }
}
