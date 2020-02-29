using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency : MonoBehaviour
{
    void Start()
    {
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameObject.Find("PrepareManager").GetComponent<PrepareManager>().Prepare();
            ShakeObject[] shakeObjects = (ShakeObject[])FindObjectsOfType(typeof(ShakeObject));
            Shaker shaker = GameObject.Find("Shaker").GetComponent<Shaker>();
            foreach (ShakeObject shakeObject in shakeObjects)
            {
                shaker.AddShakeObject(shakeObject);
            }

            shaker.StartShake();

        }

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    GameObject.Find("MeshCombiner").GetComponent<MeshCombiner>().Combine();
        //}
    }

}
