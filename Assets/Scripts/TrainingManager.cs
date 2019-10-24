using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{
    
    [SerializeField] private List<TrainingObjectBase> trainingObjects;
    private int objectiveCount;
    
    
    void Start()
    {
        objectiveCount = 0;
    }

    
    void Update()
    {
        
    }
    
    public void Train()
    {
    	StartCoroutime( TrainCor() );
    }
    
    private IEnumerator TrainCor()
    {
    	Debug.Log( "Start Training" );
    	// Start Training
    	for(int i = 0; i < trainingObjects.Count; i++)
    	{
   		if( trainingObjects[ i ].isClear_prop == false )
   			yield return null;
    	}
    	Debug.Log("Finish Training");
    }
    
}
