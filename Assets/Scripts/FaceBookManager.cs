using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using Facebook.MiniJSON;
using Facebook.Unity;


public class FaceBookManager : MonoBehaviour
{
    [SerializeField]
    private Text text;
    public string get_data;
    public string fbname;
    // public Text UserIDText;
    void Awake()
    {
       
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }


    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            text.text = "Failed to Initialize the Facebook SDK";
        }

        if (FB.IsLoggedIn)
        {
            FB.API("/me?fields=name", HttpMethod.GET, DispName);
         //   FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, GetPicture);
        //    btnLi.SetActive(false); btnLo.SetActive(true);
        }
        else
        {
            text.text = "Please login to continue.";
          //  btnLi.SetActive(true); btnLo.SetActive(false);
        }
    }

    public void LoginWithFB()
    {
        var perms = new List<string>() { "public_profile" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (result.Error != null)
        {
            text.text = result.Error;
        }
    }
    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0; //pause
        }
        else
        {
            Time.timeScale = 1; //resume
        }
    }
    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback: OnLogIn);

    }
    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
        //    UserIDText.text = token.UserId;
        }
        else
        {
            Debug.Log("Canceled LogIn");
        }
    }
    public void Share()
    {
        FB.ShareLink(contentTitle: "Candy Crush message",
            contentURL: new System.Uri("https://fb.com"),
            contentDescription: "Here is a link to my website",
            callback: OnShare);
       
    }
    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("ShareLink error: " + result);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share succeed");
        }
    
    }


    void DispName(IResult result)
    {
        if (result.Error != null)
        {
            text.text = result.Error;
        }
        else
        {
            text.text = "Hi there: " + result.ResultDictionary["name"];
        }
    }
}
