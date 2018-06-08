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
    private Sprite lockedSprite;
    [SerializeField]
    private Sprite unlockedSprite;
    public static Dictionary<string, AchievementItem> achivementList = new Dictionary<string, AchievementItem>(); 
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
   public void Unlock()
    {

        foreach (KeyValuePair<string, AchievementItem> i in achivementList)
        {
            if (i.Value.IsUnlocked)
            {
                i.Value.AchivementRef.transform.Find("Image").GetComponent<Image>().sprite = unlockedSprite;
            }
        }  
    }
    // Use this for initialization
    void Start() {

       if(achivementList.Count == 0)
        {
            CreateAchievement("Press W", "Get 1000 point", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("A", "A Score", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("B", "B Score", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("C", "D Score", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("X", "X Score", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("Y", "Y Score", 10, false, 0, lockedSprite, this.gameObject);
            CreateAchievement("Z", "Z Score", 10, false, 0, lockedSprite, this.gameObject);

        }
       foreach(KeyValuePair<string,AchievementItem> i in achivementList)
        {
            Destroy(i.Value.AchivementRef);
        }



    }
    
  
    //public IEnumerator HideAchievement(GameObject achivement)
    //{
    //    yield return new WaitForSeconds(3f);
    //    Destroy(achivement);
    //}
    public void CreateAchievement(string name, string description, int point, bool isUnlocked, int time,Sprite sprite,GameObject gameObject)
    {
        gameObject  = (GameObject)Instantiate(achivementPrefab, new Vector3(100,100,100), Quaternion.identity, achivementContent);
       
        AchievementItem achievement = new AchievementItem(name, description, point, isUnlocked, time, sprite, gameObject);
        achivementList.Add(name, achievement);
        SetAchievement(gameObject, name);
    }
    public void InstanceAchievemnt()
    {
        foreach (KeyValuePair<string, AchievementItem> i in achivementList)
        {
          GameObject  gameObject = (GameObject)Instantiate(achivementPrefab, new Vector3(100, 100, 100), Quaternion.identity, achivementContent);
            i.Value.AchivementRef = gameObject;
            SetAchievement(gameObject, i.Value.Name);
        }
    }
    void SetAchievement(GameObject achievement,string title) 
    {
        achievement.transform.Find("Text").GetComponent<Text>().text = achivementList[title].Description ;
        achievement.transform.Find("Image").GetComponent<Image>().sprite = achivementList[title].Sprite;
    }
   
}
