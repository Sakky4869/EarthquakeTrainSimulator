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
