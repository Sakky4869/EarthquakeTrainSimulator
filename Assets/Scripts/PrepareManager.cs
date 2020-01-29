using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareManager : MonoBehaviour
{
    private TrainingManager trainingManager;
    private PlayerUI playerUI;



    void Start()
    {
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        playerUI = GameObject.Find("TaskPanel").GetComponent<PlayerUI>();
    }

    void Update()
    {

    }

    // 家具
    public void AddFurniture(PrepareObject pObje)
    {
        // Debug.Log(pObje.transform.GetChild(0).GetComponent<TrainingObjectBase>());
        if(pObje.transform.GetChild(0).GetComponent<TrainingObjectBase>() != null){
            TrainingObjectBase tob = pObje.transform.GetChild(0).GetComponent<TrainingObjectBase>();
            trainingManager.AddTrainingObject(tob);
            playerUI.AddToTrainingMenu(tob);
        }
    }

    // 配置が終わり，訓練を開始する直前に呼び出す
    public void Prepare()
    {
        GameObject[] trainingObjects = GameObject.FindGameObjectsWithTag("TrainingObject");
        foreach (GameObject trainingObject in trainingObjects)
        {
            PrepareObject prepareObject = trainingObject.transform.parent.GetComponent<PrepareObject>();
            prepareObject.Prepare();
        }
    }
}
