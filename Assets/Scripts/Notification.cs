using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : MonoBehaviour {
    private AndroidJavaObject toastExample = null;
    private AndroidJavaObject activityContext = null;
    int id = 0;
    // Use this for initialization
    void Start () {
      
        Push();
    }
    public void Push()
    {
        id++;
        if (toastExample == null)
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                activityContext = activityClass.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }


        using (AndroidJavaClass pluginClass = new AndroidJavaClass("com.example.myplugin.Test"))
        {
            if (pluginClass != null)
            {
                toastExample = pluginClass.CallStatic<AndroidJavaObject>("instance");
                toastExample.Call("setContext", activityContext);

                activityContext.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    toastExample.Call("sendNotification", "This is Candy Crush's Noti message", id, 00,46,15*1000);
                }));
            }

        }
    }

}
