using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    //テレビ画面
    [SerializeField]
    private GameObject tvScreen;


    void Start()
    {
        tvScreen.SetActive(false);
    }


    void Update()
    {
        
    }

    //テレビをつける
    public void TurnOn()
    {
        tvScreen.SetActive(true);
    }
}
