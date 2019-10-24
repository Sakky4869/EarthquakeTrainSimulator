using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingObjectBase : MonoBehaviour
{
	[HideInInspector] public bool isClear { get; private set; }
	void Start()
	{
        StartSetting();
	}
	
	void Update()
	{
	
	}

    protected void StartSetting()
    {
        isClear = false;
    }
	
	protected void Clear()
	{
		isClear = true;
	}
	
	protected virtual void GetInformationOfEarthquake()
	{
		Clear();
	}
	

}