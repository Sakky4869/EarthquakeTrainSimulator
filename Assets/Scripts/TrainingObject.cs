using System;
using System.Collections;
using System.Collections.Generic;

public class TrainingObjectBase : MonoBehaviour
{
	private bool isClear;
	[HideInInspector] public bool isClear_prop {get {return isClear;} private set;}
	void Start()
	{
		isClear = false;
	}
	
	void Update()
	{
	
	}
	
	protected void Clear()
	{
		isClear = true;
	}
	
	

}