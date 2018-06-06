using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour {
    [SerializeField]
    private GameObject achivementPrefab;
    [SerializeField]
    private Transform achivementContent;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private GameObject visualAchievement;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private Sprite lockedSprite;
     [SerializeField]
    private Sprite unlockedSprite;
    private int fadeTime = 2;
    Dictionary<string, AchievementItem> achivementList = new Dictionary<string, AchievementItem>();
    public static AchivementManager instance;
    void Awake()
    {
        instance = this;
    }
    public static AchivementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchivementManager>();

            }
            return AchivementManager.instance;
        }

    }
   
    // Use this for initialization
    void Start() {

        CreateAchievement("Press W", "Get 1000 point",10,false,0, lockedSprite,this.gameObject);
       CreateAchievement("A", "A Score", 10, false, 0, lockedSprite, this.gameObject);
       CreateAchievement("B", "B Score", 10, false, 0, lockedSprite, this.gameObject);
        CreateAchievement("C", "D Score", 10, false, 0, lockedSprite, this.gameObject);
        CreateAchievement("X", "X Score", 10, false, 0, lockedSprite, this.gameObject);
       CreateAchievement("Y", "Y Score", 10, false, 0, lockedSprite, this.gameObject);
       CreateAchievement("Z", "Z Score", 10, false, 0, lockedSprite, this.gameObject);
    }
    
    public void Update()
    {
       
    }
    public void EarnAchievement(string title)
    {
        if (achivementList[title].IsUnlocked==false)
        {
         
            achivementList[title].IsUnlocked = true;
            achivementList[title].AchivementRef.transform.Find("Image").GetComponent<Image>().sprite = unlockedSprite;
            GameObject achivement = (GameObject)Instantiate(visualAchievement, parent);
            StartCoroutine(FadeAchievement(achivement));
        }
       
    }
    public IEnumerator HideAchievement(GameObject achivement)
    {
        yield return new WaitForSeconds(3f);
        Destroy(achivement);
    }
    public void CreateAchievement(string name, string description, int point, bool isUnlocked, int time,Sprite sprite,GameObject gameObject)
    {
        gameObject  = (GameObject)Instantiate(achivementPrefab, new Vector3(100,100,100), Quaternion.identity, achivementContent);
       
        AchievementItem achievement = new AchievementItem(name, description, point, isUnlocked, time, sprite, gameObject);
        achivementList.Add(name, achievement);
        SetAchievement(gameObject, name);
    }
    void SetAchievement(GameObject achievement,string title) 
    {
        achievement.transform.Find("Text").GetComponent<Text>().text = achivementList[title].Description ;
        achievement.transform.Find("Image").GetComponent<Image>().sprite = achivementList[title].Sprite;
    }
    private IEnumerator FadeAchievement(GameObject achievemt)
    {
        CanvasGroup canvasGroup = achievemt.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

       
        for(int i = 0; i < 2; i++)
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
