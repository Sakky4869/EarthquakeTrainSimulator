using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    //[SerializeField] 
    private List<ShakePower> powers;

    private Rigidbody rigidPlane;
    [Header("タイムスケール"),SerializeField] private float timeScale = 1;
    [Header("地震のパワーバイアス"),SerializeField] private float powerBias = 1;


    void Start()
    {
        //Debug.Log("call start");
        dataReader = GameObject.Find("EarthquakeDataReader").GetComponent<EarthquakeDataReader>();
        powers = dataReader.ReadCSVData("2011_03_11_14_46_miyagi");
        //rigidPlane = GameObject.Find("Plane").GetComponent<Rigidbody>();
        //StartCoroutine(Shake(powers, "WindowsMixedRealitySpatialMeshObserver"));
    }



    void Update()
    {
        Time.timeScale = timeScale;
    }

    public void StartShake(List<Rigidbody> rigidbodies)
    {
        StartCoroutine(Shake(powers, "WindowsMixedRealitySpatialMeshObserver", rigidbodies));
    }

    private IEnumerator Shake(List<ShakePower> powers, string objectName, List<Rigidbody> rigidbodies){
        // Debug.Log("start");

        //rigidPlane = GameObject.Find(objectName).GetComponent<Rigidbody>();

        foreach(ShakePower power in powers){
            Vector3 force = new Vector3(power.ns, power.ew, power.ud) * powerBias; 
                //Vector3.forward * power.ns * powerBias + Vector3.right * power.ew * powerBias + Vector3.up * power.ud * powerBias;
            foreach(Rigidbody rigid in rigidbodies)
                rigid.AddForce(force);
            yield return new WaitForSeconds(1 / 100);
        }
        // Debug.Log("finish");
    }
}
