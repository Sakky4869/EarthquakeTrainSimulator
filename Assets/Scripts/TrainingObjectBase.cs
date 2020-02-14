using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingObjectBase : MonoBehaviour
{

    [HideInInspector] 
    public bool isClear { get; private set; }

    [HideInInspector] 
    public int categoryId;
    
    [HideInInspector]
    public TaskItem taskItem;

    [HideInInspector]
    public TrainingManager trainingManager;

    public int id;
    
    public string taskName;

    private Player player;
    


    void Start()
    {

    }

    void Update()
    {

    }

    //開始時のセッティング
    protected void StartSetting()
    {
        isClear = false;
        player = Camera.main.transform.GetComponent<Player>();
    }

    //タスクのクリア時に実行
    protected void ClearTask()
    {
        isClear = true;
        taskItem.ClearTask();
        trainingManager.ClearTask();
        player.PlaySound("TaskClear");
    }

    //ユーザから何かしらの干渉があった場合に実行
    public virtual void Interact()
    {
        ClearTask();
    }

}