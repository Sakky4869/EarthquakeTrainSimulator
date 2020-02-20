using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private GameObject shakePointObject;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shake(Vector3 direct, float power)
    {
        rigid.AddForceAtPosition(direct * power, shakePointObject.transform.position, ForceMode.Impulse);
    }

    public void Shake(Vector3 direct)
    {
        if (rigid == null)
            rigid = transform.GetChild(0).GetComponent<Rigidbody>();
        rigid.AddForceAtPosition(direct, shakePointObject.transform.position);
        //rigid.AddForce(direct);
    }
}
