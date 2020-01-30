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
    private RectTransform taskItemBaseTransform;
    [SerializeField]// タスクアイテムの親オブジェクト
    private Transform taskItemParent;


    void Start()
    {
        // messagePanel = GameObject.Find("MessagePanel");
        // messageText = GameObject.Find("MessageText").GetComponent<Text>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
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
    private void SortItems(){
        // リスト内を入れ替えながら，UIの位置も入れ替える
        taskItems.Sort();

        // 一番上の位置情報
        RectTransform targetRect = taskItemBaseTransform;

        // タスクリストの縦の感覚
        float distanceOfY = 0.1f;

        // 位置のうち，Y座標の感覚は0.1f
        for(int i = 0; i < taskItems.Count; i++){

            // 配置先の位置を決定
            targetRect = taskItemBaseTransform;
            Vector3 pos = targetRect.localPosition;
            pos.x = 0.05f;
            pos.y -= i * distanceOfY;
            targetRect.localPosition = pos;

            // 対応するタスクアイテムを移動
            foreach(RectTransform rectTransform in taskItemParent.transform){
                if(rectTransform.GetComponent<TaskItem>() == null)
                    continue;
                TaskItem item = rectTransform.GetComponent<TaskItem>();
                if(item.id == taskItems[i].id){
                    // Debug.Log("call move item");
                    rectTransform.localPosition = pos;
                    Debug.Log("local position : " + rectTransform.localPosition);
                    break;
                }
            }
        }
    }
}
