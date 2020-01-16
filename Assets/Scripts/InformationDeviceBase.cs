using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationDeviceBase : TrainingObjectBase
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    protected void StartSetting()
    {
        base.StartSetting();
    }

    protected void ClearTask()
    {
        base.ClearTask();
    }

    public override void Interact()
    {
        ShowInformationOfEarthquake();
        base.Interact();
    }


    //地震の情報を表示する
    protected virtual void ShowInformationOfEarthquake()
    {
        
    }
}
