using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class Conro : TrainingObjectBase
{
    [SerializeField]// コンロのOnとOffを切り替えるボタン
    private Interactable conroControlButton;
    [SerializeField]// コンロの炎のParticleSystem
    private GameObject fireParticleSystem;

    private bool isTurnOn;
    void Start()
    {
        isTurnOn = true;
    }

    void Update()
    {
        
    }

    public void ControlConro(){
        fireParticleSystem.SetActive(!isTurnOn);
        conroControlButton.transform.GetChild(3).GetComponent<TextMeshPro>().text = (isTurnOn)?"Turn Off":"Turn On";
        isTurnOn = !isTurnOn;
    }

    public override void Interact(){
        base.Interact();
        ControlConro();
    }
}
