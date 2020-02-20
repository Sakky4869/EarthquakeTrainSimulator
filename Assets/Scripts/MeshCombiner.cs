using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;

public class MeshCombiner : MonoBehaviour
{
    [SerializeField, Header("環境認識オブジェクトのマテリアル")]
    private Material targetMaterial;

    void Start()
    {

    }



    void Update()
    {
        
    }

    public void Combine()
    {
        StartCoroutine(CombineCor());
    }


    public IEnumerator CombineCor()
    {
        
        GameObject meshObserver = GameObject.Find("WindowsMixedRealitySpatialMeshObserver");
        Debug.Log(meshObserver);
        Component[] meshFilters = meshObserver.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while(i < meshFilters.Length)
        {
            combine[i].mesh = ((MeshFilter)meshFilters[i]).sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
            i++;
        }

        Debug.Log(combine.Length);

        meshObserver.AddComponent<MeshFilter>();
        meshObserver.AddComponent<MeshRenderer>();

        yield return null;

        meshObserver.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        meshObserver.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        meshObserver.transform.gameObject.SetActive(true);

        // マテリアルを再設定
        meshObserver.transform.GetComponent<MeshRenderer>().material = targetMaterial;
    }
}
