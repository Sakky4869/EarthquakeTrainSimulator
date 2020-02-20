using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmartPhone : InformationDeviceBase
{
	private GameObject screen;

	void Start()
	{
		StartSetting();
		//screen.SetActive(false);
	}

	public override void Interact()
	{
		base.Interact();
		ShowInformationOfEarthquake();
	}

	protected new void ShowInformationOfEarthquake()
	{
		screen = transform.GetChild(0).gameObject;
		screen.SetActive(true);
	}


}