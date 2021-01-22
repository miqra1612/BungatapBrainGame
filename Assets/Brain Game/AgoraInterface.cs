using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;

public class AgoraInterface : MonoBehaviour
{
    public string appID;
    public IRtcEngine rtcEngine;
    public uint mRemotePeer;

    public static AgoraInterface instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadEngine()
    {
        Debug.Log("initialize engine");

        if(rtcEngine != null)
        {
            Debug.Log("already initialize");
            return;
        }

        rtcEngine = IRtcEngine.getEngine(appID);

        rtcEngine.SetLogFilter(LOG_FILTER.DEBUG);
    }

    public void JoiningChanel(string chanelname)
    {
        Debug.Log("Joint to channel:" + chanelname);

        if(rtcEngine == null)
        {
            return;
        }

        rtcEngine.OnJoinChannelSuccess = onJoinChannelSuccess;
        rtcEngine.OnUserJoined = onUserJoined;
        rtcEngine.OnUserOffline = onUserOffline;

        rtcEngine.EnableVideo();
        rtcEngine.EnableVideoObserver();
        rtcEngine.JoinChannel(chanelname, null, 0);
    }

    public void LeaveChannel()
    {
        Debug.Log("Leaving channel");

        if(rtcEngine == null)
        {
            Debug.Log("Engine not exist");
            return;
        }

        rtcEngine.LeaveChannel();
        rtcEngine.DisableVideoObserver();

    }

    public void UnloadEngine()
    {
        Debug.Log("Unloading agora engine");

        if (rtcEngine != null)
        {
            IRtcEngine.Destroy();
            rtcEngine = null;
        }
    }

    private void onJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        /*
        Debug.Log("JoinChannelSuccessHandler: uid = " + uid);
        GameObject textVersionGameObject = GameObject.Find("VersionText");
        textVersionGameObject.GetComponent<Text>().text = "SDK Version : " + getSdkVersion();
    */
    }

    // When a remote user joined, this delegate will be called. Typically
    // create a GameObject to render video on it
    private void onUserJoined(uint uid, int elapsed)
    {
        Debug.Log("onUserJoined: uid = " + uid + " elapsed = " + elapsed);
        // this is called in main thread

        // find a game object to render video stream from 'uid'
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);

        go.name = uid.ToString();

        VideoSurface o = go.AddComponent<VideoSurface>();
        o.SetForUser(uid);
        o.SetEnable(true);
        

    }

    private void onUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {
        // remove video stream
        Debug.Log("onUserOffline: uid = " + uid + " reason = " + reason);
        // this is called in main thread
        GameObject go = GameObject.Find(uid.ToString());
        if (!ReferenceEquals(go, null))
        {
            Object.Destroy(go);
        }
    }
}
