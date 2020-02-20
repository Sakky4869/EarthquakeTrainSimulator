using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

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


    // 家具を生成する
    public void SpawnFurniture(){
        GameObject user = Camera.main.gameObject;
        Vector3 spawnPos = user.transform.position + user.transform.forward  + transform.up / 10;
        GameObject obje = Instantiate(prepareObject.gameObject, spawnPos, Quaternion.identity);
        // 訓練オブジェクトであれば，生成数は1つ限定なので，1度生成したら二度と生成できないようにする
        if (obje.transform.GetChild(0).GetComponent<TrainingObjectBase>() != null)
        {
            // ボタンを無効か
            GetComponent<Interactable>().enabled = false;

            // 準備
            prepareManager.AddFurniture(obje.transform.GetComponent<PrepareObject>());
        }
    }
}
