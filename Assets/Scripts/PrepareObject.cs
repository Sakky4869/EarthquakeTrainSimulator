using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Utilities;

public class PrepareObject : MonoBehaviour
// , IMixedRealityInputHandler
//     , IMixedRealitySourcePoseHandler
//     , IMixedRealityPointerHandler
{
    // 訓練システムのマネージャ
    private TrainingManager trainingManager;

    // このオブジェクトが選択されたかどうか
    private bool isSelected;

    // 家具の名前
    public string furnitureName;// { get; private set; }

    // 以前の手の位置情報
    // private Vector3 beforeHandPosition;

    void Start()
    {
        //trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        // beforeHandPosition = new Vector3(100, 100, 100);
    }

    void Update()
    {
        //Debug.Log("Is Selected : " + isSelected);
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

    //---- ここからMRTKのインターフェース関係 ----

    // 指が下りた直後にCall
    //void IMixedRealityInputHandler.OnInputUp(InputEventData eventData)
    //{
    //    // 強制的に選択状態を解除する
    //    isSelected = false;
    //}

    //// 指が上がった直後にCall
    //void IMixedRealityInputHandler.OnInputDown(InputEventData eventData)
    //{
    //    // 選択されたオブジェクトが自分自身だったら，自身を選択状態にする
    //    //Debug.Log(eventData.selectedObject == gameObject);
    //    //isSelected = ( eventData.selectedObject == gameObject );
    //    //Debug.Log("Input Down");
    //}

    // トラッキングステートが取得できる
    // void IMixedRealitySourcePoseHandler.OnSourcePoseChanged(SourcePoseEventData<TrackingState> eventData)
    // {

    // }

    // // Vector2型のデータをGet
    // void IMixedRealitySourcePoseHandler.OnSourcePoseChanged(SourcePoseEventData<Vector2> eventData)
    // {
    //     throw new System.NotImplementedException();
    // }

    // // Vector3型のデータをGet
    // void IMixedRealitySourcePoseHandler.OnSourcePoseChanged(SourcePoseEventData<Vector3> eventData)
    // {

    //     //if (isSelected == false)
    //     //    return;
    //     // データ取得
    //     //Vector3 source = eventData.SourceData;
    //     //if(beforeHandPosition.x == 100 && beforeHandPosition.y == 100 && beforeHandPosition.z == 100)
    //     //{
    //     //    beforeHandPosition = source;
    //     //}
    //     //else
    //     //{
    //     //    // 動かす
    //     //    // パターン1
    //     //    gameObject.transform.position += (source - beforeHandPosition) * Time.deltaTime;
    //     //    beforeHandPosition = source;
    //     //}
    //     //Debug.Log("X : " + source.x + " , Y : " + source.y + " , Z : " + source.z);






    //     // パターン２
    //     //gameObject.transform.position += beforeHandPosition - source;


    // }

    // // Quaternion型のデータをGet
    // void IMixedRealitySourcePoseHandler.OnSourcePoseChanged(SourcePoseEventData<Quaternion> eventData)
    // {
    // }

    // void IMixedRealitySourcePoseHandler.OnSourcePoseChanged(SourcePoseEventData<MixedRealityPose> eventData)
    // {
    // }

    // // 指を検知したらCall
    // void IMixedRealitySourceStateHandler.OnSourceDetected(SourceStateEventData eventData)
    // {
    //     //Debug.Log("Source Detected");

    // }

    // // 指を見失ったらCall
    // void IMixedRealitySourceStateHandler.OnSourceLost(SourceStateEventData eventData)
    // {
    //     //Debug.Log("Source Losted : " + eventData.SourceId);
    // }

    // void IMixedRealityPointerHandler.OnPointerDown(MixedRealityPointerEventData eventData)
    // {
    //     Debug.Log("Pointer Down");
    //     if(gameObject == eventData.selectedObject)
    //     {
    //         Debug.Log("Selected");
    //         isSelected = true;
    //     }
    // }

    // void IMixedRealityPointerHandler.OnPointerDragged(MixedRealityPointerEventData eventData)
    // {
    //     Debug.Log("Pointer Dragged");

    //     Debug.Log(eventData.selectedObject.name);


    //     Vector3 source = eventData.Pointer.Position;

    //     Debug.Log("Source X : " + source.x + " , Y : " + source.y + " , Z : " + source.z);
    //     if (isSelected == false)
    //         return;
    //     if (beforeHandPosition.x == 100 && beforeHandPosition.y == 100 && beforeHandPosition.z == 100)
    //     {
    //         beforeHandPosition = source;
    //     }
    //     else
    //     {
    //         // 動かす
    //         // パターン1
    //         gameObject.transform.position += (source - beforeHandPosition) * Time.deltaTime;
    //         beforeHandPosition = source;
    //     }

    // }

    // void IMixedRealityPointerHandler.OnPointerUp(MixedRealityPointerEventData eventData)
    // {
    //     Debug.Log("Pointer Up");
    //     if (isSelected)
    //     {
    //         if(gameObject == eventData.selectedObject)
    //         {
    //             isSelected = false;
    //         }
    //     }
    // }

    // void IMixedRealityPointerHandler.OnPointerClicked(MixedRealityPointerEventData eventData)
    // {
    //     Debug.Log("Pointer Clicked");
    // }


    //---- ここまでMRTKのインターフェース関係 ----
}
