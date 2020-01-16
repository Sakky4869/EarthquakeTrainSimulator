using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Controller : InformationDeviceBase
{
    private TV tv;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartSetting()
    {
        base.StartSetting();
        tv = GameObject.Find("TV").GetComponent<TV>();
    }

    protected override void ShowInformationOfEarthquake()
    {
        tv.TurnOn();
    }

    public override void Interact()
    {
        base.Interact();
    }

}
