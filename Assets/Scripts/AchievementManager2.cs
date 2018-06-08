using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AchievementManager2 : MonoBehaviour {
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private GameObject visualAchievement;
    [SerializeField]
    private Sprite unlockedSprite;
    private int fadeTime = 2;
    public static AchievementManager2 current;
    // Use this for initialization

    // Update is called once per frame
    void Start()
    {

    }
    void Awake()
    {
        current = this;
    }

    public void EarnAchievement(string title)
    {

        
        if (AchivementManager.achivementList[title].IsUnlocked == false)
        {

            AchivementManager.achivementList[title].IsUnlocked = true;
          //  AchivementManager.achivementList[title].AchivementRef.transform.Find("Image").GetComponent<Image>().sprite = unlockedSprite;
            GameObject achivement = (GameObject)Instantiate(visualAchievement, parent);
            StartCoroutine(FadeAchievement(achivement));
        }

    }
    private IEnumerator FadeAchievement(GameObject achievemt)
    {
        CanvasGroup canvasGroup = achievemt.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;


        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);

                progress += rate * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }

        Destroy(achievemt);
    }
}

