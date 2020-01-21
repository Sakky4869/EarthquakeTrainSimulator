using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* ユーザが入ったかどうかの判定 f,i
*
*
*/

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
