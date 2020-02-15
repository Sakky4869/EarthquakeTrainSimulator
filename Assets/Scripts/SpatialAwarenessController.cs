using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;
using Microsoft.MixedReality.Toolkit;
//using Microsoft.MixedReality.Toolkit.WindowsMixedReality.SpatialAwareness;
using UnityEngine.XR.WSA;

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

    private Shaker shaker;
    private List<Rigidbody> rigidbodies;
    private TrainingManager trainingManager;
    
    void Start()
    {
        startedObserver = false;
        clearObservations = true;
        shaker = GameObject.Find("Shaker").GetComponent<Shaker>();
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
        
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
        
    }

    //環境認識オブジェクトを取得し、Rigidbodyを付与
    private IEnumerator EnvironmentSetting()
    {
        //環境認識システムのオブジェクトを取得
        GameObject spatialAwareness = GameObject.Find("Spatial Awareness System");

        //取得できたら、その最初の子オブジェクトを取得
        if (spatialAwareness != null)
            parentObject = spatialAwareness.transform.GetChild(0).gameObject;
        //環境認識オブジェクトの親を取得できたら
        if(parentObject != null)
        {
            Debug.Log(parentObject.name);
            rigidbodies = new List<Rigidbody>();


            //各認識オブジェクトを結合させたメッシュを作成する
            //List<Vector3> vertices = new List<Vector3>();
            //List<int> triangles = new List<int>();
            int count = 0;
            int childrenCount = parentObject.transform.GetComponentsInChildren<MeshFilter>().Length;
            CombineInstance[] combineInstances = new CombineInstance[childrenCount];
            //頂点情報の取得
            foreach (MeshFilter meshFilter in parentObject.transform.GetComponentsInChildren<MeshFilter>())
            {

                combineInstances[count].mesh = meshFilter.sharedMesh;
                combineInstances[count].transform = Matrix4x4.Translate(meshFilter.transform.position);
                count++;

                meshFilter.gameObject.SetActive(false);
            }

            Mesh mesh = new Mesh();

            mesh.CombineMeshes(combineInstances);

            //作成　現状、うまくいってない
            MeshFilter filter = GameObject.Find("MeshTest").GetComponent<MeshFilter>();
            filter.mesh = mesh;
        }
        yield return null;
        Debug.Log("Set Rigidbody to Spatial Meshes");

        yield return null;
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
            spatialAwarenessSystem.ResumeObservers();
            startedObserver = true;
            trainingManager.StartAwareness();
        }
    }

    public void PauseAndResume()
    {
        if(spatialAwarenessSystem != null)
        {
            if (clearObservations)
            {
                Debug.Log("Suspend Observer");
                spatialAwarenessSystem.SuspendObservers();
                clearObservations = false;
                trainingManager.CompleteAwareness();
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
