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
    private List<TaskItem> taskItems;
    // タスクアイテム
    [SerializeField]
    private TaskItem taskItem;
    [SerializeField]// タスクアイテムの基準のTransform
    private RectTransform taskItemBaseTransform;
    [SerializeField]// タスクアイテムの親オブジェクト
    private Transform taskItemParent;

    // private Player player;

    void Start()
    {
        messagePanel = GameObject.Find("MessagePanel");
        messageText = GameObject.Find("MessageText").GetComponent<Text>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        // player = Camera.main.transform.GetComponent<Player>();
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

    // 訓練メニューに追加する
    public void AddToTrainingMenu(TrainingObjectBase trainingObject){
        GameObject item = Instantiate(taskItem.gameObject, transform.position, Quaternion.identity);
        TaskItem tItem = item.GetComponent<TaskItem>();
        tItem.id = trainingObject.id;
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
            pos.y -= i * distanceOfY;
            targetRect.localPosition = pos;

            // 対応するタスクアイテムを移動
            foreach(RectTransform rectTransform in taskItemParent.transform){
                TaskItem item = rectTransform.GetComponent<TaskItem>();
                if(item.id == taskItems[i].id){
                    rectTransform.localPosition = pos;
                }
            }
        }
    }

    // タスク一覧を初期化する
    // private void RefreshTaskItemList(){
    //     while(taskItemParentTransform.childCount != 0){
    //         // Destroy()
    //         break;
    //     }
    // }
}
