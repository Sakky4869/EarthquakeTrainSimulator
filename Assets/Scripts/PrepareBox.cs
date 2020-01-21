using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

// 配置する家具の一覧を表示する
public class PrepareBox : MonoBehaviour
{
    [SerializeField]// 配置対象の家具のプレハブ
    private PrepareObject[] prepareObjects;

    [SerializeField]// 家具配置ボタンのプレハブ
    private Interactable furnitureButton;

    [SerializeField]// 家具配置ボタンの親オブジェクト
    private GameObject furnitureButtonParent;

    void Start()
    {

    }

    void Update()
    {
        
    }

    // 一覧画面に，家具の生成ボタンを配置する
    // 配置する際は，横に４つずつとする
    private void SetButtonsToSpawnFurnitures()
    {
        // 縦の個数を計算
        int verticalCount = prepareObjects.Length / 4;
        int buttonIndex = 0;
        
        // 縦の感覚

        // 横の感覚


        
        // 配置
        for(int i = 0; i < verticalCount; i++)
        {
            
        }
    }
}
