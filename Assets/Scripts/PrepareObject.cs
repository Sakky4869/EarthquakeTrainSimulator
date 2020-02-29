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

        int childCount = transform.childCount;
        //Debug.Log(transform.gameObject.name);

        if (transform.gameObject.name.Contains("Bookshelf"))
        {
            Transform parent = transform.GetChild(0);
            childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                BoxCollider boxCollider;
                MeshCollider meshCollider;
                BoxCollider[] boxColliders;
                MeshCollider[] meshColliders;


                // BoxColliderについて
                if (parent.GetChild(i).GetComponent<BoxCollider>() != null)
                {
                    // BoxColliderが1つの場合
                    if ((parent.GetChild(i).GetComponents<BoxCollider>().Length == 1))
                    {
                        //Debug.Log("Box Collider　1つ");
                        boxCollider = parent.GetChild(i).GetComponent<BoxCollider>();
                        if (boxCollider.enabled == false)
                        {
                            boxCollider.enabled = true;
                        }
                    }
                    else
                    {
                        //Debug.Log("Box Collider　複数");
                        boxColliders = parent.GetChild(i).GetComponents<BoxCollider>();
                        for (int j = 0; j < boxColliders.Length; j++)
                        {
                            if (boxColliders[j].enabled == false)
                                boxColliders[j].enabled = true;
                        }
                    }

                    // 子オブジェクトを登録
                    child = parent.GetChild(i);
                }


                // MeshColliderについて
                if (parent.GetChild(i).GetComponent<MeshCollider>() != null)
                {
                    // MeshColliderが1つの場合
                    if (parent.GetChild(i).GetComponents<MeshCollider>().Length == 1)
                    {
                        //Debug.Log("Mesh Collider 1つ");
                        meshCollider = parent.GetChild(i).GetComponent<MeshCollider>();
                        if (meshCollider.enabled == false)
                        {
                            meshCollider.enabled = true;
                        }
                    }
                    else
                    {

                        meshColliders = parent.GetChild(i).GetComponents<MeshCollider>();
                        for (int j = 0; j < meshColliders.Length; j++)
                        {
                            if (meshColliders[j].enabled == false)
                            {
                                meshColliders[j].enabled = true;
                            }
                        }
                    }



                    // 子オブジェクトの登録
                    child = parent.GetChild(i);
                }

                if (parent.GetChild(i).GetComponent<Rigidbody>() != null)
                {
                    parent.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    parent.GetChild(i).GetComponent<Rigidbody>().mass = 30;
                    parent.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < childCount; i++)
            {
                BoxCollider boxCollider;
                MeshCollider meshCollider;
                BoxCollider[] boxColliders;
                MeshCollider[] meshColliders;


                // BoxColliderについて
                if(transform.GetChild(i).GetComponent<BoxCollider>() != null)
                {
                    // BoxColliderが1つの場合
                    if((transform.GetChild(i).GetComponents<BoxCollider>().Length == 1))
                    {
                        //Debug.Log("Box Collider　1つ");
                        boxCollider = transform.GetChild(i).GetComponent<BoxCollider>();
                        if(boxCollider.enabled == false)
                        {
                            boxCollider.enabled = true;
                        }
                    }
                    else
                    {
                        //Debug.Log("Box Collider　複数");
                        boxColliders = transform.GetChild(i).GetComponents<BoxCollider>();
                        for(int j = 0; j < boxColliders.Length; j++)
                        {
                            if (boxColliders[j].enabled == false)
                                boxColliders[j].enabled = true;
                        }
                    }

                    // 子オブジェクトを登録
                    child = transform.GetChild(i);
                }


                // MeshColliderについて
                if(transform.GetChild(i).GetComponent<MeshCollider>() != null)
                {
                    // MeshColliderが1つの場合
                    if(transform.GetChild(i).GetComponents<MeshCollider>().Length == 1)
                    {
                        //Debug.Log("Mesh Collider 1つ");
                        meshCollider = transform.GetChild(i).GetComponent<MeshCollider>();
                        if(meshCollider.enabled == false)
                        {
                            meshCollider.enabled = true;
                        }
                    }
                    else
                    {

                        meshColliders = transform.GetChild(i).GetComponents<MeshCollider>();
                        for(int j = 0; j < meshColliders.Length; j++)
                        {
                            if(meshColliders[j].enabled == false)
                            {
                                meshColliders[j].enabled = true;
                            }
                        }
                    }

               

                    // 子オブジェクトの登録
                    child = transform.GetChild(i);
                }

                if (transform.GetChild(i).GetComponent<Rigidbody>() != null)
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().useGravity = true;
                    transform.GetChild(i).GetComponent<Rigidbody>().mass = 30;

                }
            }

        }


        //if(childCount != 0)
        //{
            switch (transform.GetChild(0).gameObject.name)
            {
                case "Door":
                    transform.GetChild(0).GetChild(1).SetParent(null);
                    child = transform.GetChild(0);
                break;
                case "TV_Remote_Controller":
                case "Radio":
                    child = transform.GetChild(0);
                    break;
                case "Bookshelf":
                child = transform.GetChild(0);
                break;
                default:
                    break;
            }
        //}


        //親子関係を切る
        child.SetParent(null);


        //自身を削除
        Destroy(gameObject);
        yield return null;
    }

}
