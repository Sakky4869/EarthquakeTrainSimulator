using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingObjectBase : MonoBehaviour
{

    [HideInInspector] public bool isClear { get; private set; }
    [HideInInspector] public int categoryId;
    [HideInInspector] public int id;


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
    }

    //ユーザから何かしらの干渉があった場合に実行
    public virtual void Interact()
    {
        ClearTask();
    }

}