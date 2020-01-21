using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    //訓練オブジェクトのリスト
    [SerializeField] private List<TrainingObjectBase> trainingObjects;
    //揺れているかどうか
    [HideInInspector] public bool isQuaking;
    //訓練が終了しているかどうか
    [HideInInspector] public bool isFinishedTraining;
    
    void Start()
    {

    }

    
    void Update()
    {
        
    }
    
    //訓練オブジェクトをリストへ追加
    public void AddTrainingObject(TrainingObjectBase trainingObject)
    {
		trainingObjects.Add(trainingObject);
	}
    
    //訓練
    public void Train()
    {
    	StartCoroutine( TrainCor() );
    }
    
    //訓練のコルーチン
    private IEnumerator TrainCor()
    {
    	Debug.Log( "Start Training" );
        int clearCount = 0;
        while (true)
        {
    	    // Start Training
    	    for(int i = 0; i < trainingObjects.Count; i++)
    	    {
                if (trainingObjects[i].isClear)
                    clearCount++;
                yield return null;
    	    }
            if (clearCount == trainingObjects.Count)
            {
                isFinishedTraining = true;
        	    Debug.Log("Finish Training");
                yield break;
            }
        }
    }

	//訓練終了
    public void ClearTraining()
    {
        Debug.Log("訓練終了");
    } 
}
