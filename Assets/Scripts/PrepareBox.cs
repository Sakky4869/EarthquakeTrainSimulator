using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

// 配置する家具の一覧を表示する
public class PrepareBox : MonoBehaviour
{
    [SerializeField]// 配置対象の家具のプレハブ
    private PrepareObject[] prepareObjects;

    [SerializeField]// 家具配置ボタンのプレハブ
    private Interactable furnitureButton;
    [SerializeField]// 必須家具設置ボタンのプレハブ
    private Interactable furnitureNecessaryButton;
    [SerializeField]// 準備箱の開け閉めのボタン
    private Interactable boxOperateButton;

    [SerializeField]// 家具配置ボタンの親オブジェクト
    private GameObject furnitureButtonBase;

    [SerializeField]// 家具一覧表示をするパネル
    private GameObject furniturePanel;

    // 準備箱が開いているかどうか
    private bool isOpened;

    // ユーザ
    [SerializeField]
    private GameObject user;

    // PrepareManager
    private PrepareManager prepareManager;


    void Start()
    {
        isOpened = false;
        prepareManager = GameObject.Find("PrepareManager").GetComponent<PrepareManager>();
    }

    void Update()
    {

    }

    // 家具の生成
    public void SpawnFurniture(int index)
    {
        user = Camera.main.gameObject;
        Vector3 spawnPos = user.transform.position + (user.transform.forward / 6) + (transform.up / 6);
        GameObject obje = Instantiate(prepareObjects[index].gameObject, spawnPos, Quaternion.identity);
        if (obje.transform.GetChild(0).GetComponent<TrainingObjectBase>() != null)
        {
            prepareManager.AddFurniture(obje.transform.GetComponent<PrepareObject>());
        }
    }

    // 準備箱の操作
    public void OperateBox()
    {
        furniturePanel.SetActive(!isOpened);
        if(isOpened == false){
            SetButtonsToSpawnFurnitures();
            boxOperateButton.transform.GetChild(3).GetComponent<TextMeshPro>().text = "Close";
        }else{
            boxOperateButton.transform.GetChild(3).GetComponent<TextMeshPro>().text = "Open";
        }

        isOpened = (isOpened)?false:true;
    }

    // 一覧画面に，家具の生成ボタンを配置する
    // 配置する際は，横に２つずつとする
    // X座標について
    // 左から 0.011 , 0.0035 , -0.0035 , -0.011
    // Y座標について
    // -0.008 per item
    private void SetButtonsToSpawnFurnitures()
    {
        // 縦の個数を計算
        int verticalCount = prepareObjects.Length / 4 + 1;

        int index = 0;

        // 縦の間隔
        // float distanceVertical = 1f;

        // 横の間隔
        float distanceHorizontal = 0.008f;

        Vector3 pos = new Vector3(0.011f, 0.01f, 0);


        // 配置
        for (int i = 0; i < verticalCount; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject button = null;
                if(prepareObjects[index].trainingObject == null)
                    // 生成
                    button = Instantiate(furnitureButton.gameObject, transform.position, Quaternion.identity);
                else
                    button = Instantiate(furnitureNecessaryButton.gameObject, transform.position, Quaternion.identity);

                // 家具生成処理の登録
                FurnitureCreateButton furnitureCreate = button.GetComponent<FurnitureCreateButton>();
                furnitureCreate.furnitureIndex = index;
                furnitureCreate.prepareObject = prepareObjects[index];
                furnitureCreate.prepareManager = prepareManager;
                

                // 親オブジェクトの設定
                button.transform.SetParent(furniturePanel.transform);

                // スケールを整える
                button.transform.localScale = new Vector3(0.05f, 0.05f, 1);

                // ボタンのテキストを変える（テキストはTextMeshPro）
                button.transform.GetChild(3).GetComponent<TextMeshPro>().text = prepareObjects[index].furnitureName;

                //// 訓練に必須のオブジェクトであれば，ボタンの色を赤くする
                //Interactable interactable = button.GetComponent<Interactable>();
                //if (furnitureCreate.prepareObject.trainingObject != null)
                //{
                    
                //    //interactable.StateManager.SetStateOff(InteractableStates.InteractableStateEnum.Default);
                //    //interactable.StateManager.SetStateOn(InteractableStates.InteractableStateEnum.Custom);

                //    //.Profiles[0].Themes[0].Definitions[0].StateProperties[0].Default.Color = Color.red;
                //}
                //else
                //{
                //}

                // ボタンの位置を調整する
                switch (j){
                    case 0:
                    pos.x = 0.011f;
                    break;
                    case 1:
                    pos.x = 0.0035f;
                    break;
                    case 2:
                    pos.x = - 0.0035f;
                    break;
                    case 3:
                    pos.x = - 0.011f;
                    break;
                    default:
                    break;
                }
                button.transform.localPosition = pos;
                if(j == 3)
                    pos.y -= distanceHorizontal;

                index++;
                if(index == prepareObjects.Length)
                    return;
            }
        }
    }
}