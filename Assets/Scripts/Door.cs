using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : TrainingObjectBase
{
    [SerializeField]
    private float openSpeed;

    public GameObject escapeButton;


    void Start()
    {
        StartSetting();
    }

    void Update()
    {

    }

    protected new void StartSetting()
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
        while(transform.rotation.eulerAngles.y <= 90){
            transform.Rotate(0, openSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }

    public override void Interact()
    {
        //揺れているときはドアを開ける
        if (trainingManager.isQuaking)
        {
            OpenDoor();
            ClearTask();
        }
        else//揺れが止まっている状態で，タスクがすべてクリアされていたら訓練終了
        {
            if (trainingManager.isFinishedTraining)
            {
                trainingManager.ClearTraining();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag != "Player")
    //        return;
    //    if (trainingManager.GetTrainingPhase() != TrainingPhase.InTraining)
    //        return;
    //    Interact();
    //}
}
