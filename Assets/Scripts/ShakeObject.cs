using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;
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
        rigidbody.AddForceAtPosition(direct * power, shakePointObject.transform.position, ForceMode.Impulse);
    }

    public void Shake(Vector3 direct)
    {
        rigidbody.AddForceAtPosition(direct, shakePointObject.transform.position, ForceMode.Impulse);
    }
}
