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

    [SerializeField]// 家具配置ボタンの親オブジェクト
    private GameObject furnitureButtonParent;

    [SerializeField]// 家具一覧表示をするパネル
    private GameObject furniturePanel;

    // 準備箱が開いているかどうか
    private bool isOpened;

    // ユーザ
    [SerializeField]
    private GameObject user;

    void Start()
    {
        isOpened = false;
    }

    void Update()
    {
        
    }

    // 家具の生成
    public void SpawnFurniture()
    {
        user = Camera.main.gameObject;
        Vector3 spawnPos = user.transform.position + user.transform.forward + transform.up;
        Instantiate(prepareObjects[0], spawnPos, Quaternion.identity);
    }

    // 準備箱の操作
    public void OperateBox()
    {
        // 開いているときは閉じる
        if (isOpened)
        {
            furniturePanel.SetActive(false);
        }
        // 閉じているときは開く
        else
        {
            furniturePanel.SetActive(true);
        }
    }

    // 一覧画面に，家具の生成ボタンを配置する
    // 配置する際は，横に４つずつとする
    private void SetButtonsToSpawnFurnitures()
    {
        // 縦の個数を計算
        int verticalCount = prepareObjects.Length / 4;
        
        int index = 0;
        
        // 縦の間隔
        float distanceVertical = 1f;

        // 横の間隔
        float distanceHorizontal = 1f;

        
        // 配置
        for(int i = 0; i < verticalCount; i++)
        {
            for(int j = 0; j < 4; j++){
                // 生成
                GameObject button = Instantiate(furnitureButton.gameObject, transform.position, Quaternion.identity);
                
                // 親オブジェクトの設定
                button.transform.SetParent(furnitureButtonParent.transform);

                // スケールを整える
                button.transform.localScale = Vector3.one;

                // ボタンのテキストを変える（テキストはTextMeshPro）
                button.transform.GetChild(3).GetComponent<TextMeshPro>().text = prepareObjects[index++].furnitureName;

                // ボタンの位置を調整する
                Vector3 pos = button.transform.localPosition;
                pos.x += distanceHorizontal * j;
                pos.y -= distanceVertical * i;

            }
        }
    }
}
