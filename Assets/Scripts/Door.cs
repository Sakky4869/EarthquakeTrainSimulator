using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TrainingObjectBase
{
    private TrainingManager trainingManager;


    void Start()
    {
        
    }

    void Update()
    {

    }

    private void StartSetting()
    {
        base.StartSetting();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    private void OpenDoor()
    {
        StartCoroutine(OpenDoorCor());
    }

    private IEnumerator OpenDoorCor()
    {
        for(int i = 0; i < 90; i++)
        {
            transform.Rotate(0, 1 * Time.deltaTime, 0);
            yield return null;
        }
        ClearTask();
    }

    public override void Interact()
    {
        //揺れているときはドアを開ける
        if (trainingManager.isQuaking)
        {
            OpenDoor();
        }
        else//揺れが止まっている状態で，タスクがすべてクリアされていたら訓練終了
        {
            if (trainingManager.isFinishedTraining)
            {
                trainingManager.ClearTraining();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        Interact();
    }

}
