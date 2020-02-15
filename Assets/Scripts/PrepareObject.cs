using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

public class PrepareObject : MonoBehaviour
{
    // 訓練システムのマネージャ
    private TrainingManager trainingManager;

    // このオブジェクトが選択されたかどうか
    private bool isSelected;

    // 家具の名前
    public string furnitureName;// { get; private set; }

    // 揺らすプログラム
    public ShakeObject shakeObject;

    // 訓練オブジェクトのベースプログラム
    public TrainingObjectBase trainingObject;

    void Start()
    {

    }

    void Update()
    {
    }


    public void Prepare()
    {
        StartCoroutine(PrepareCor());
    }



    // 配置操作に必要なコンポーネントを解除し、物理挙動を復活させる
    // 訓練オブジェクトおよび家具オブジェクトは、自身の子要素
    private IEnumerator PrepareCor()
    {
        // 配置するオブジェクトのコライダーの機能を復活させる
        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = false;
        transform.GetChild(0).GetComponent<Rigidbody>().useGravity = false;

        //親子関係を切る
        transform.GetChild(0).SetParent(null);
        yield return new WaitForSeconds(0.5f);

        //自身を削除
        Destroy(gameObject);
    }

}
