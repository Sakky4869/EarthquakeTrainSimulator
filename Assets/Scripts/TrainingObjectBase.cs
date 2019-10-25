using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingObjectBase : MonoBehaviour
{

    [HideInInspector] public bool isClear{get; private set;}


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void StartSetting(){
        isClear = false;
    }

    private void Clear(){
        isClear = true;
    }

    protected virtual void GetInformationOfEarthquake(){
        Clear();
    }
}
