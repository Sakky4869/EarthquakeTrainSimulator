using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bath : MonoBehaviour
{
    [HideInInspector] public bool isPlayerInBath;

    void Start()
    {
        
    }



    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
            return;
        isPlayerInBath = true;
    }
}
