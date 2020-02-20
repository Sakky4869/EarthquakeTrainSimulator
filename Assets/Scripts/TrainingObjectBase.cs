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

    [SerializeField]
    private PlayerUI playerUI;


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
        playerUI = Camera.main.transform.GetChild(0).GetComponent<PlayerUI>();
    }

    //タスクのクリア時に実行
    protected void ClearTask()
    {
        isClear = true;
        taskItem.ClearTask();
        trainingManager.ClearTask();
        if(playerUI)
            playerUI.PlaySound("TaskClear");
    }

    //ユーザから何かしらの干渉があった場合に実行
    public virtual void Interact()
    {
        ClearTask();
    }

}