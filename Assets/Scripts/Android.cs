using UnityEngine;
using System.Runtime.InteropServices;


public class Android : MonoBehaviour
{
    public static Android instance = null;
#if UNITY_ANDROID
    //public static AndroidJavaObject jo;
    public static AndroidJavaClass jc;
#endif
    void Awake()
    {
#if UNITY_ANDROID
        //获取安卓类，用于掉用安卓的接口
        jc = new AndroidJavaClass("com.codestalkers.plugin.Main");
        //jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
    }


#if UNITY_ANDROID
    public static string GetWifiMacAddress()
    {
        return jc.CallStatic<string>("getConnectedWifiMacAddress");
        //jo.Call("openPdf");
    }
#elif UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern string getWifiMacAddress();
    public static string GetWifiMacAddress()
    {
        return getWifiMacAddress();
    }
#else
    public static void GetWifiMacAddress(string path)
    {
        return "";
    }
#endif
}

