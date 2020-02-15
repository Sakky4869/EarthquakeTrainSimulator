using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmartPhone : InformationDeviceBase
{
	[SerializeField]
	private GameObject screen;

	void Start()
	{
		StartSetting();
		screen.SetActive(false);
	}

	public override void Interact()
	{
		base.Interact();
	}

	protected override void ShowInformationOfEarthquake()
	{
		screen.SetActive(true);
	}


}