using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    // 訓練オブジェクトのリスト
    [SerializeField] private List<TrainingObjectBase> trainingObjects;

    // 揺れているかどうか
    [HideInInspector] public bool isQuaking;
    
    // 訓練が終了しているかどうか
    [HideInInspector] public bool isFinishedTraining;
    
    // 訓練中かどうか
    private bool isInTraining;
    
    void Start()
    {
        isQuaking = false;
        isFinishedTraining = false;
        isInTraining = false;
    }

    
    void Update()
    {
        if(isInTraining)
        {
            if(isFinishedTraining){
                ClearTraining();
            }
        }
    }
    
    //訓練オブジェクトをリストへ追加
    public void AddTrainingObject(TrainingObjectBase trainingObject)
    {
        if(trainingObjects == null)
            trainingObjects = new List<TrainingObjectBase>();
		trainingObjects.Add(trainingObject);
	}
    
    //訓練
    public void Train()
    {
    	// StartCoroutine( TrainCor() );
        Debug.Log("訓練開始");
        isInTraining = true;
    }

    // タスククリアの方式を，タスクオブジェクトを監視する形から，タスクオブジェクトがメッセージを送る形式に変更
    public void ClearTask(){
        bool isClear = true;
        foreach(TrainingObjectBase trainingObject in trainingObjects){
            if(trainingObject.isClear == false)
            {
                isClear = false;
            }
        }

        // クリアしていたら
        if(isClear){
            Debug.Log("Finished Training");
            isFinishedTraining = true;
        }
    }

	//訓練終了
    public void ClearTraining()
    {
        Debug.Log("訓練終了");
        isInTraining = false;
    } 
}
