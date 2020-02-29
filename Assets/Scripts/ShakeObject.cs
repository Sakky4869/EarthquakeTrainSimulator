using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShakeObject : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private GameObject shakePointObject;

    private Vector3 beforeQuakeObjectPos;
    private Vector3 startQuakeObjectPos;
    private Quaternion startQuakeObjectRot;
    //private Vector3 beforePos;




    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shake(Rigidbody rigidbody)
    {
        // 角度の同期
        rigid.rotation = rigidbody.rotation;

        // 位置の同期
        // 前のフレームとの差分を計算
        //Vector3 diff = rigidbody.position - beforeQuakeObjectPos;
        // 現在の位置情報に加算
        //rigid.position += diff;
    }

    public void SaveFirstPos(Rigidbody rigidbody)
    {
        beforeQuakeObjectPos = rigidbody.position;
        startQuakeObjectPos = rigid.position;
        startQuakeObjectRot = rigid.rotation;
        //beforePos = rigid.position;
    }

    //public void Shake(Vector3 direct, float power)
    //{
    //    rigid.AddForceAtPosition(direct * power, shakePointObject.transform.position, ForceMode.Impulse);
    //}

    public void Shake(Vector3 direct)
    {
        if (rigid == null)
            rigid = transform.GetChild(0).GetComponent<Rigidbody>();
        rigid.AddForce(direct);
        //rigid.AddForce(direct);
    }
}
