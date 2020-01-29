using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingObjectBase : MonoBehaviour
{

    [HideInInspector] public bool isClear { get; private set; }
    [HideInInspector] public int categoryId;
    public int id;
    public string taskName;



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
    }

    //タスクのクリア時に実行
    protected void ClearTask()
    {
        isClear = true;
        Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TaskItem>().ClearTask();
    }

    //ユーザから何かしらの干渉があった場合に実行
    public virtual void Interact()
    {
        ClearTask();
    }

}