using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : TrainingObjectBase
{
	void Start()
	{
		StartSetting();
	}
	
	protected override void GetInformationOfEarthquake()
	{
		base.GetInformationOfEarthquake();
	}

    //ƒeƒŒƒr‚ð‚Â‚¯‚é
    public void TurnOn()
    {
        Debug.Log("Turn On");
    }

}