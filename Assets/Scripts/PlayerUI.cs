using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //メッセージパネル
    private GameObject messagePanel;

    //メッセージテキストボックス
    private Text messageText;


    void Start()
    {
        messagePanel = GameObject.Find("MessagePanel");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
    }

    void Update()
    {
        
    }

    //警告文を表示する
    public void ShowMessage(string msg)
    {
        messagePanel.SetActive(true);
        messageText.text = msg;
    }

    //警告文を非表示にする
    public void HideMessage()
    {
        messagePanel.SetActive(false);
    }
}
