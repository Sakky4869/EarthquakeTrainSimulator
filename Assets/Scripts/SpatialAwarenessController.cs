using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.WindowsMixedReality.SpatialAwareness;

public class SpatialAwarenessController : MonoBehaviour
{
    #region MRTK関係
    private IMixedRealitySpatialAwarenessSystem spatialAwarenessSystem;

    private IMixedRealitySpatialAwarenessSystem SpatialAwarenessSystem
    {
        get
        {
            if(spatialAwarenessSystem == null)
            {
                MixedRealityServiceRegistry.TryGetService<IMixedRealitySpatialAwarenessSystem>(out spatialAwarenessSystem);
            }
            return spatialAwarenessSystem;
        }
    }

    private IMixedRealityDataProviderAccess dataProviderAccess;

    private IMixedRealityDataProvider meshObserver;
    private IMixedRealityDataProvider spatialObjectMeshObserver;
    #endregion

    private string meshObserverName;

    private bool clearObservations;
    private bool startedObserver;


    private GameObject parentObject;
    
    void Start()
    {
        startedObserver = false;
        clearObservations = false;
    }

    #region Memo
    //MixedRealityPlayspace
    //+ MainCamera
    //+ Diagnostics
    //+ DefaultCursor
    //+ SpatialAwareness
    //  + WindowsMixedRealitySpatialMeshObserver
    //    + Meshes
    #endregion

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //GameObject spatialAwareness = GameObject.Find("Spatial Awareness System");

            //if (spatialAwareness != null)
            //    parentObject = spatialAwareness.transform.GetChild(0).gameObject;
            //Debug.Log("Get System Parents");
            
            
            //if (parentObject != null)
            //    Debug.Log(parentObject.transform.childCount);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(EnvironmentSetting());
            //if(parentObject != null)
            //{
            //    foreach(Transform mesh in parentObject.transform.GetComponentsInChildren<Transform>())
            //    {
            //        mesh.gameObject.AddComponent<Rigidbody>();
            //    }
            //    Debug.Log("Add Rigidbody");
            //}
        }

        

    }



    private IEnumerator EnvironmentSetting()
    {
        GameObject spatialAwareness = GameObject.Find("Spatial Awareness System");

        if (spatialAwareness != null)
            parentObject = spatialAwareness.transform.GetChild(0).gameObject;
        if(parentObject != null)
        {
            foreach(MeshCollider mesh in parentObject.transform.GetComponentsInChildren<MeshCollider>())
            {
                mesh.convex = true;
                mesh.transform.gameObject.AddComponent<Rigidbody>();
                mesh.transform.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        yield return null;
        Debug.Log("Get System Parents");
        Rigidbody rigid = parentObject.AddComponent<Rigidbody>();
        yield return null;
        rigid.useGravity = false;
        
    }

    public void StartObserver()
    {
        if(startedObserver == false)
        {
            Debug.Log("Start Observer");
            spatialAwarenessSystem = CoreServices.SpatialAwarenessSystem;
            
            dataProviderAccess = spatialAwarenessSystem as IMixedRealityDataProviderAccess;
            meshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
            meshObserverName = "Spatial Object Mesh Observer";
            spatialObjectMeshObserver = dataProviderAccess.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>(meshObserverName);
            PauseAndResume();
            startedObserver = true;
        }
    }

    public void PauseAndResume()
    {
        //if(SpatialAwarenessSystem != null)
        //{
        //    if (clearObservations)
        //    {
        //        SpatialAwarenessSystem.SuspendObservers();
        //        clearObservations = false;
        //    }
        //    else
        //    {
        //        SpatialAwarenessSystem.ResumeObservers();
        //        clearObservations = true;
        //    }
        //}
        if(spatialAwarenessSystem != null)
        {
            if (clearObservations)
            {
                Debug.Log("Suspend Observer");
                spatialAwarenessSystem.SuspendObservers();
                clearObservations = false;
            }
            else
            {
                Debug.Log("Resume Observer");
                spatialAwarenessSystem.ResumeObservers();
                clearObservations = true;
            }
        }
    }

    public void StopObserver()
    {
        if(spatialAwarenessSystem != null)
        {
            if (startedObserver)
            {
                Debug.Log("Stop Observer");
                spatialAwarenessSystem.SuspendObservers();
                spatialAwarenessSystem.ClearObservations();
                spatialAwarenessSystem.Reset();
                startedObserver = false;
            }
        }
    }


}
