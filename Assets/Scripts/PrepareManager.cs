using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareManager : MonoBehaviour
{
	private TrainingManager trainingManager;



    void Start()
    {
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
       
    }

    void Update()
    {
        
    }
    
    // 配置が終わり，訓練を開始する直前に呼び出す
    public void Prepare()
    {
		GameObject[] trainingObjects = GameObject.FindGameObjectsWithTag("TrainingObject");
		foreach(GameObject trainingObject in trainingObjects)
		{
			PrepareObject prepareObject = trainingObject.GetComponent<PrepareObject>();
			prepareObject.Prepare();
		}
	}
}
