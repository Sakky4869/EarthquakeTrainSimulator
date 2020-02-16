using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class WebLogger
{
    public static void SendLog(string log)
    {
        // �ʐM���URL
        string url = "http://grapefruit.sys.wakayama-u.ac.jp/~sakai/UnityLog/LogReceiver.php?log=" + log;

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        webRequest.SendWebRequest();
    }
}