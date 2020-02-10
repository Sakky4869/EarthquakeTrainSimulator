using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainingPhase{
    Idle = 0,
    SpatialAwareness,
    SpatialAwarenessCompleted,
    Prepare,
    InTraining
}



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

    // 家具の宝箱が出現しているかどうか
    [HideInInspector] public bool isTreasureBoxSpawned;

    // 訓練システムのフェーズ
    private TrainingPhase trainingPhase;

    // ログ出力制御変数
    private bool outLog;
    
    void Start()
    {
        isQuaking = false;
        isFinishedTraining = false;
        isInTraining = false;
        isTreasureBoxSpawned = false;
        trainingPhase = TrainingPhase.SpatialAwareness;
        outLog = false;
        
    }

    
    void Update()
    {
        // if(isInTraining)
        // {
        //     if(isFinishedTraining){
        //         ClearTraining();
        //     }
        // }
    }

    private IEnumerator TrainingCor(){
        while(true){
            yield return null;
            switch(trainingPhase){
                case TrainingPhase.Idle:
                if(!outLog){
                    outLog = true;
                    Debug.Log("Training System : Idle");
                }
                break;
                case TrainingPhase.SpatialAwareness:
                if(!outLog){
                    outLog = true;
                    Debug.Log("Training System : SpatialAwareness");
                }
                break;
                case TrainingPhase.SpatialAwarenessCompleted:
                if(!outLog){
                    outLog = true;
                    Debug.Log("Training System : SpatialAwarenessCompleted");
                }
                break;
                case TrainingPhase.Prepare:
                if(!outLog){
                    outLog = true;
                    Debug.Log("Training System : Prepare");
                }
                break;
                case TrainingPhase.InTraining:
                if(!outLog){
                    outLog = true;
                    Debug.Log("Training System : InTraining");
                }
                if(isFinishedTraining)
                    trainingPhase = TrainingPhase.Idle;
                break;
                default:
                break;
            }
            break;
        }
    }

    // 環境認識開始
    public void StartAwareness(){
        trainingPhase++;
        // この時点で，トレーニングフェーズはSpatialAwareness
        outLog = false;
    }

    // 環境認識完了
    public void CompleteAwareness(){
        trainingPhase++;
        // この時点で，トレーニングフェーズはSpatialAwarenessCompleted
        outLog = false;
    }

    // 家具などの設置開始
    public void StartPrepare(){
        trainingPhase++;
        // この時点で，トレーニングフェーズはPrepare
        outLog = false;
    }

    // 家具などの設置完了
    public void CompletePrepare(){
        trainingPhase++;
        // この時点で，トレーニングフェーズはInTraining
        outLog = false;
    }
    
    //訓練オブジェクトをリストへ追加
    public void AddTrainingObject(TrainingObjectBase trainingObject)
    {
        if(trainingObjects == null)
            trainingObjects = new List<TrainingObjectBase>();
		trainingObjects.Add(trainingObject);
	}
    
    //訓練
    public void StartTraining()
    {
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
