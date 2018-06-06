using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FaceBookManager : MonoBehaviour
{
   // public Text UserIDText;
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
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
}
