using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class TV_Controller : MonoBehaviour{

    //private Interactable[] controllerButtons;
    private Interactable tvControllerButton;
    private TV tv;

    private void Start() {
        tvControllerButton = GameObject.Find("TVControllerButton").GetComponent<Interactable>();
        tvControllerButton.OnClick.AddListener(TurnOnTV);
        tv = GameObject.Find("TV").GetComponent<TV>();
        //controllerButtons = new Interactable[12];
        //for(int i = 0; i < 12; i++)
        //{
        //    controllerButtons[i] = GameObject.Find("TV_ControllerButton" + (i + 1)).GetComponent<Interactable>();
        //}
    }

    private void Update(){

    }

    //ƒeƒŒƒr‚ð‚Â‚¯‚é
    private void TurnOnTV()
    {
        tv.TurnOn();
    }
}