using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

public class PlayerUI : MonoBehaviour
{
    //メッセージパネル
    private GameObject messagePanel;

    //メッセージテキストボックス
    private Text messageText;
    
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


    void Start()
    {
        // messagePanel = GameObject.Find("MessagePanel");
        // messageText = GameObject.Find("MessageText").GetComponent<Text>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        isMessageActive = false;
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
        if (isMessageActive)
            return;
        StartCoroutine(ShowMessageCor(msg));
    }

    // 警告文を表示する
    public void ShowMessage(string msg, float time)
    {
        if (isMessageActive)
            return;
        StartCoroutine(ShowMessageCor(msg, time));
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
        yield return new WaitForSeconds(1);
        HideMessage();
    }

    //警告文を非表示にする
    public void HideMessage()
    {
        messagePanel.SetActive(false);
        isMessageActive = false;
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
        item.GetComponent<RectTransform>().localScale = Vector3.one;
        
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

        // 念のために並べ替えが正常に行われているかどうかを確認
        //string outPutData = "";
        //for(int i = 0; i < taskItems.Count; i++){
        //    if(i == 0){
        //        outPutData += taskItems[i].name;
        //    }else{
        //        outPutData += ", " + taskItems[i].name;
        //    }
        //}

        //Debug.Log(outPutData);

        // 一番上の位置情報
        Transform targetTransform;
        Vector3 pos = new Vector3(-0.4f, 0.25f, 0);
        Vector3 size = new Vector3(0.1f,0.1f,1);


        // タスクリストの縦の感覚
        float distanceOfY = 0.1f;

        // 位置のうち，Y座標の感覚は0.1f
        for(int i = 0; i < taskItems.Count; i++){

            // 配置先の位置を決定
            
            targetTransform = taskItemBaseTransform;
            Vector3 position = targetTransform.localPosition;
            position.x = -0.4f;
            position.y -= i * distanceOfY;
            targetTransform.localPosition = position;

            // 対応するアイテムを移動
            foreach(Transform transform in taskItemParent.transform)
            {
                if (transform.GetComponent<TaskItem>() == null)
                    continue;
                TaskItem item = transform.GetComponent<TaskItem>();
                if(item.id == taskItems[i].id)
                {
                    transform.localPosition = targetTransform.localPosition;
                    break;
                }
            }

            // 対応するタスクアイテムを移動
            //foreach(RectTransform rectTransform in taskItemParent.transform){
            //    if(rectTransform.GetComponent<TaskItem>() == null)
            //        continue;
            //    TaskItem item = rectTransform.GetComponent<TaskItem>();
            //    if(item.id == taskItems[i].id){
            //        // Debug.Log("call move item");
            //        rectTransform.localPosition = pos;
            //        Debug.Log("Task Item Base Position " + " : " + taskItemBaseTransform.localPosition);
            //        break;
            //    }
            //}
        }
    }
}
