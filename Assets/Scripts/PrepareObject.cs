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
        Transform child = null;
        // 配置するオブジェクトのコライダーの機能を復活させる
        if (transform.GetComponentInChildren<BoxCollider>() != null)
        {
            //Debug.Log("Box Collider");
            BoxCollider boxCollider = transform.GetComponentInChildren<BoxCollider>();
            boxCollider.isTrigger = false;
            child = boxCollider.transform;
            //Debug.Log(child.gameObject.name);
            
            //transform.GetComponentInChildren<Rigidbody>().useGravity = false;
        }else if(transform.GetComponentInChildren<MeshCollider>() != null)
        {
            Debug.Log("Mesh Collider");
            MeshCollider meshCollider = transform.GetComponentInChildren<MeshCollider>();
            meshCollider.isTrigger = false;
            child = meshCollider.transform;
            //transform.GetComponentInChildren<Rigidbody>().useGravity = false;
        }

        // この時点で，一般的なオブジェクトであれば，子オブジェクトが取得できている
        // 特殊な場合は個別に処理
        if(transform.GetChild(0).gameObject.name == "Door")
        {
            child = transform.GetChild(0);
        }

        if(transform.GetComponentInChildren<Rigidbody>() != null)
            transform.GetComponentInChildren<Rigidbody>().useGravity = false;


        //親子関係を切る
        child.SetParent(null);
        yield return null;
        //yield return new WaitForSeconds(0.1f);

        //自身を削除
        Destroy(gameObject);
    }

}
