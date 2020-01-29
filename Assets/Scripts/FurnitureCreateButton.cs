using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureCreateButton : MonoBehaviour
{
    [HideInInspector]// 家具にインデックス
    public int furnitureIndex;

    [HideInInspector]// 設置対象の家具のオブジェクト
    public PrepareObject prepareObject;

    [HideInInspector]// PrepareManager
    public PrepareManager prepareManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void SpawnFurniture(){
        GameObject user = Camera.main.gameObject;
        Vector3 spawnPos = user.transform.position + user.transform.forward / 2 + transform.up / 10;
        GameObject obje = Instantiate(prepareObject.gameObject, spawnPos, Quaternion.identity);
        if (obje.transform.GetChild(0).GetComponent<TrainingObjectBase>() != null)
        {
            prepareManager.AddFurniture(obje.transform.GetComponent<PrepareObject>());
        }
    }



}
