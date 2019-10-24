using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    
    [SerializeField] private List<TrainingObjectBase> trainingObjects;
    
    
    void Start()
    {

    }

    
    void Update()
    {
        
    }
    
    public void Train()
    {
    	StartCoroutine( TrainCor() );
    }
    
    private IEnumerator TrainCor()
    {
    	Debug.Log( "Start Training" );
    	// Start Training
    	for(int i = 0; i < trainingObjects.Count; i++)
    	{
   		if( trainingObjects[ i ].isClear == false )
   			yield return null;
    	}
    	Debug.Log("Finish Training");
    }
    
}
