using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : TrainingObjectBase
{
    // ライトをつけるときに，移動する場所のオブジェクト
    private Transform playerHeadTransform;

    // フラッシュライトのオブジェクト
    private Flashlight_PRO flashlight_PRO;
    void Start()
    {
        flashlight_PRO = GetComponent<Flashlight_PRO>();
    }

    void Update()
    {
        
    }

    // ユーザが触れたら，正面にライトを点灯
    public override void Interact(){
        base.Interact();
        transform.position = playerHeadTransform.position;
        flashlight_PRO.Change_Intensivity(80);
        flashlight_PRO.Switch();
    }
}
