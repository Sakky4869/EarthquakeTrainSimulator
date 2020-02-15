using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.WindowsMixedReality.Input;

public class Emergency : MonoBehaviour
{

    private IEnumerator enumerator;

    void Start()
    {
        enumerator = Coroutine();
        StartCoroutine(enumerator);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopCoroutine(enumerator);
            enumerator = null;
            enumerator = Coroutine();
            StartCoroutine(enumerator);
        }

        
    }

    private IEnumerator Coroutine()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(i + 1 + "秒");
            yield return new WaitForSeconds(1);
        }
    }
}
