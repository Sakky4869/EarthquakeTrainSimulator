using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.WindowsMixedReality.Input;

public class Emergency : MonoBehaviour
{

    //[SerializeField]
    //private GameObject test;

    void Start()
    {

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.GetChild(0).SetParent(null);
            Destroy(gameObject);
        }
    }

}
