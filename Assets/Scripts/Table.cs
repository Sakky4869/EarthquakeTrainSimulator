using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : TrainingObjectBase
{
    private PlayerUI playerUi;
    

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
        playerUi = GameObject.Find("TaskPanel").GetComponent<PlayerUI>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    //private new void ClearTask()
    //{
    //    base.ClearTask();
    //}

    public override void Interact()
    {
        //base.Interact();

        ShowMessage("揺れがおさまるまでそのまま！");
    }

    //警告を表示する
    private void ShowMessage(string msg)
    {
        playerUi.ShowMessage(msg);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (trainingManager.GetTrainingPhase() != TrainingPhase.InTraining)
            return;
        Interact();
        if(trainingManager.isQuaking == false)
        {
            playerUi.ShowMessage("揺れが収まりました");
            ClearTask();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (trainingManager.GetTrainingPhase() != TrainingPhase.InTraining)
            return;
        if(trainingManager.isQuaking == false)
        {
            if(isClear == false)
            {
                playerUi.ShowMessage("揺れが収まりました");
                ClearTask();
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        if (trainingManager.GetTrainingPhase() != TrainingPhase.InTraining)
            return;
        if (trainingManager.isQuaking)
        {
            ShowMessage("戻って！危ない！");
        }
        else
        {
            playerUi.HideMessage();
        }
    }
}
