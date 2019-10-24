using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : TrainingObject
{
	void Start()
	{
		StartSetting();
	}
	
	override void GetInformationOfEarthquake()
	{
		base.Clear();
	}



}