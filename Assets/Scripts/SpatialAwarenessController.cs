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
    
    void Start()
    {
        startedObserver = false;
        clearObservations = false;
        shaker = GameObject.Find("Shaker").GetComponent<Shaker>();
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
            shaker.StartShake(rigidbodies);
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
            Mesh mesh = new Mesh();
            List<Vector3> vertices = new List<Vector3>();
            int count = 0;
            //頂点情報の取得
            foreach (MeshFilter meshFilter in parentObject.transform.GetComponentsInChildren<MeshFilter>())
            {
                foreach (Vector3 p in meshFilter.mesh.vertices)
                {
                    vertices.Add(p);
                    count++;
                }
                meshFilter.gameObject.SetActive(false);
            }

            mesh.SetVertices(vertices);
            int[] triangles = new int[count];
            for(int i = 0; i < count; i++)
            {
                triangles[i] = i;
            }
            mesh.SetTriangles(triangles,0);
            //作成　現状、うまくいってない
            MeshFilter filter = GameObject.Find("MeshTest").GetComponent<MeshFilter>();
            filter.mesh = mesh;

            //認識オブジェクトのメッシュコライダーの設定を変えて、揺らせるようにする
            //foreach (MeshCollider mesh in parentObject.transform.GetComponentsInChildren<MeshCollider>())
            //{
            //    mesh.convex = true;
            //    mesh.isTrigger = true;
            //    Rigidbody r = mesh.transform.gameObject.AddComponent<Rigidbody>();
            //    r.mass = 30;
            //    rigidbodies.Add(r);
            //    Destroy(mesh.transform.GetChild(0).transform.GetComponent<WorldAnchor>());
            //}
        }
        yield return null;
        Debug.Log("Set Rigidbody to Spatial Meshes");

        //メッシュコライダーのIsTriggerのチェックを外す
        //foreach (MeshCollider mesh in parentObject.transform.GetComponentsInChildren<MeshCollider>())
        //{
        //    mesh.isTrigger = false;
        //    yield return null;
        //}
        //取得したRigidbodyの速度を完全に０にする
        //foreach (Rigidbody rigidbody in rigidbodies)
        //{
        //    rigidbody.velocity = Vector3.zero;
        //    rigidbody.angularVelocity = Vector3.zero;
        //}
        ////Rigidbody rigid = parentObject.AddComponent<Rigidbody>();
        yield return null;
        //rigid.useGravity = false;
        
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
