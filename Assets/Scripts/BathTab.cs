using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathTab : TrainingObjectBase
{

    private Bath bath;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void StartSetting()
    {
        base.StartSetting();
        bath = GameObject.Find("Bath").GetComponent<Bath>();
    }

    private void ClearTask()
    {
        base.ClearTask();
    }

    //ユーザの上に移動
    private void MoveToOverPlayer()
    {

    }

    public override void Interact()
    {
        if (bath.isPlayerInBath)
        {
            base.Interact();

        }
    }
}
