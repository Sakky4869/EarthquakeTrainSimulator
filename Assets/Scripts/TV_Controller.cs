using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_Controller : InformationDeviceBase
{
    private TV tv;


    void Start()
    {
        StartSetting();
    }

    void Update()
    {
        
    }

    public new void StartSetting()
    {
        base.StartSetting();
    }

    // テレビをつけて，情報を表示
    protected override void ShowInformationOfEarthquake()
    {
        tv = GameObject.Find("TV").GetComponent<TV>();
        tv.TurnOn();
    }

    public override void Interact()
    {
        base.Interact();
        this.ShowInformationOfEarthquake();
    }
}
