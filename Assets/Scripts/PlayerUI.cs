using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

public class PlayerUI : MonoBehaviour
{
    //メッセージパネル
    [SerializeField]
    private GameObject messagePanel;

    //メッセージテキストボックス
    [SerializeField]
    private TextMesh messageText;
    
    //TrainingManager
    private TrainingManager trainingManager;

    // タスクリストに表示するアイテムのリスト
    [SerializeField]
    private List<TaskItem> taskItems;
    
    // タスクアイテム
    [SerializeField]
    private TaskItem taskItem;
    
    [SerializeField]// タスクアイテムの基準のTransform
    private Transform taskItemBaseTransform;
    
    [SerializeField]// タスクアイテムの親オブジェクト
    private Transform taskItemParent;

    // メッセージを表示中かどうか
    private bool isMessageActive;

    private IEnumerator coroutineMethod;

    // SEを再生するAudioSource
    [SerializeField]
    private AudioSource sePlayer;

    // タスククリアの音
    [SerializeField]
    private AudioClip taskClearSound;

    // 警告音
    [SerializeField]
    private AudioClip siren;

    [SerializeField]// BGMを再生するAudioSource
    private AudioSource bgmPlayer;

    [SerializeField]
    private AudioClip dinari;

    private Queue<string> helpMessages;


    void Start()
    {
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        isMessageActive = false;
        helpMessages = new Queue<string>();
    }

    void Update()
    {
        
    }

    // タスクアイテムをクリアする
    public void ClearTask(int id){
        foreach(TaskItem i in taskItems){
            if(i.id == id){
                i.ClearTask();
            }
        }
    }

    // 警告文を表示する
    public void ShowMessage(string msg)
    {
        helpMessages.Enqueue(msg);
        if (isMessageActive)
            return;
        msg = helpMessages.Dequeue();
        string message = null;
        if(msg.Length > 18)
        {
            string first = msg.Substring(0, 18);
            string second = msg.Substring(18, msg.Length - 18);
            message = first + System.Environment.NewLine + second;
        }

        if(message == null)
        {
            coroutineMethod = ShowMessageCor(msg);
            //StartCoroutine(ShowMessageCor(msg));
        }
        else
        {
            coroutineMethod = ShowMessageCor(message);
            WebLogger.SendLog("メッセージ：" + System.Environment.NewLine + message);
            //StartCoroutine(ShowMessageCor(message));
        }




        StartCoroutine(coroutineMethod);
    }

    // 警告文を表示する
    public void ShowMessage(string msg, float time)
    {
        helpMessages.Enqueue(msg + ":" + time);
        if (isMessageActive)
            return;
        string[] data = helpMessages.Dequeue().Split(':');
        msg = data[0];
        float t = float.Parse(data[1]);
        string message = null;
        if (data.Length > 18)
        {
            string first = msg.Substring(0, 18);
            string second = msg.Substring(18, msg.Length - 18);
            message = first + System.Environment.NewLine + second;
        }

        if (message == null)
        {
            coroutineMethod = ShowMessageCor(msg, t);
            //StartCoroutine(ShowMessageCor(msg, time));
        }
        else
        {
            coroutineMethod = ShowMessageCor(message, t);
            WebLogger.SendLog("メッセージ：" + System.Environment.NewLine + message);
            //StartCoroutine(ShowMessageCor(message, time));
        }

        StartCoroutine(coroutineMethod);
    }

    public IEnumerator ShowMessageCall(string msg)
    {
        yield return ShowMessageCor(msg);
    }

    // 警告文を表示する
    private IEnumerator ShowMessageCor(string msg, float time)
    {
        isMessageActive = true;
        messagePanel.SetActive(true);
        messageText.text = msg;
        yield return new WaitForSeconds(time);
        HideMessage();
    }

    // 警告文を表示する
    // 表示時間を指定しない場合は1秒間
    private IEnumerator ShowMessageCor(string msg)
    {
        isMessageActive = true;
        messagePanel.SetActive(true);
        messageText.text = msg;
        yield return new WaitForSeconds(3);
        HideMessage();
    }

    //警告文を非表示にする
    public void HideMessage()
    {
        messageText.text = "";
        messagePanel.SetActive(false);
        isMessageActive = false;
        if(coroutineMethod != null)
        {
            StopCoroutine(coroutineMethod);
            coroutineMethod = null;
        }

        // メッセージキューにまだ入っていた場合
        if(helpMessages.Count != 0)
        {
            // 時間付きメッセージ
            if (helpMessages.ToArray()[0].Contains(":"))
            {
                string[] data = helpMessages.Dequeue().Split(':');
                coroutineMethod = ShowMessageCor(data[0], float.Parse(data[1]));
            }
            else
            {
                string data = helpMessages.Dequeue();
                coroutineMethod = ShowMessageCor(data);
            }
            if(coroutineMethod != null)
            {
                StartCoroutine(coroutineMethod);
            }
        }


    }

    // 訓練メニューに追加する
    public void AddToTrainingMenu(TrainingObjectBase trainingObject){

        // アイテムを生成
        GameObject item = Instantiate(taskItem.gameObject, transform.position, Quaternion.identity);
        
        // タスクアイテムクラスを取得
        TaskItem tItem = item.GetComponent<TaskItem>();
        
        // 必要な情報を代入
        tItem.id = trainingObject.id;
        tItem.taskText.text = trainingObject.taskName;
        trainingObject.taskItem = tItem;
        
        // 親オブジェクトの設定
        item.transform.SetParent(taskItemParent);

        // 大きさの調整
        item.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 1);
        
        // タスクアイテムリストへの追加
        if(taskItems == null)
            taskItems = new List<TaskItem>();
        taskItems.Add(tItem);
        
        // アイテムリストの並べ替え
        SortItems();
    }

    // タスクアイテムをIDについて昇順に並べ替える
    // X : -0.4（固定値）
    // Y : 0.1間隔
    private void SortItems(){
        // リスト内を入れ替えながら，UIの位置も入れ替える
        taskItems.Sort();

        // タスクリストの縦の感覚
        float distanceOfY = 0.1f;

        // 位置のうち，Y座標の感覚は0.1f
        for(int i = 0; i < taskItems.Count; i++){

            // 配置先の位置を決定
            Vector3 position = taskItemBaseTransform.localPosition;
            position.x = -0.4f;
            position.y -= i * distanceOfY;
            position.z = -0.5f;

            // 対応するアイテムを移動
            foreach(Transform transform in taskItemParent.transform)
            {
                if (transform.GetComponent<TaskItem>() == null)
                    continue;
                TaskItem item = transform.GetComponent<TaskItem>();
                if(item.id == taskItems[i].id)
                {
                    transform.localPosition = position;
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    break;
                }
            }
        }
    }

    public void PlaySound(string name) 
    {
        switch (name)
        {
            case "TaskClear":
                sePlayer.PlayOneShot(taskClearSound);
                break;
            default:
                break;
        }
    }

    public void PlayBGM(string name)
    {
        switch (name)
        {
            case "Dinari":
                bgmPlayer.clip = dinari;
                bgmPlayer.Play();
                break;
            default:
                break;
        }
    }

    public void StopBGM(string name)
    {
        switch (name)
        {
            case "Dinari":
                bgmPlayer.Stop();
                break;
            default:
                break;
        }
    }

    public void Emergency()
    {
        ShowMessage("緊急地震速報です．" + System.Environment.NewLine + "強い揺れに警戒してください", 8);
        StartCoroutine(PlayEmergencySound(8));
    }

    private IEnumerator PlayEmergencySound(float time)
    {
        float soundTime = 0;
        while(soundTime <= time)
        {
            if(sePlayer.isPlaying == false)
            {
                sePlayer.PlayOneShot(siren);
            }
            soundTime += Time.deltaTime;
            yield return null;
        }
        PlayBGM("Dinari");
    }





}
