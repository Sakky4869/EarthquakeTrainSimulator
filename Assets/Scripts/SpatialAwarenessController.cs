using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;

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


    void Start()
    {
        startedObserver = false;
        clearObservations = false;
    }

    public void StartObserver()
    {
        if(startedObserver == false)
        {
            
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
                spatialAwarenessSystem.SuspendObservers();
                clearObservations = false;
            }
            else
            {
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
                spatialAwarenessSystem.SuspendObservers();
                spatialAwarenessSystem.ClearObservations();
                spatialAwarenessSystem.Reset();
                startedObserver = false;
            }
        }
    }


}
