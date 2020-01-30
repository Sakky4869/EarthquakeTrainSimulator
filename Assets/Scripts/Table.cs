using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : TrainingObjectBase
{
    private PlayerUI playerUi;
    // private TrainingManager trainingManager;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected void StartSetting()
    {
        base.StartSetting();
        playerUi = GameObject.Find("MixedRealityPlayspace").GetComponent<PlayerUI>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    private void ClearTask()
    {
        base.ClearTask();
    }

    public override void Interact()
    {
        base.Interact();
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
        Interact();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Player")
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
