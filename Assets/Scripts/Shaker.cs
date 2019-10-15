using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    private List<ShakePower> powers;

    private Rigidbody rigidPlane;
    [Header("タイムスケール"),SerializeField] private float timeScale;
    [Header("地震のパワーバイアス"),SerializeField] private float powerBias;


    void Start()
    {
        dataReader = GameObject.Find("EarthquakeDataReader").GetComponent<EarthquakeDataReader>();
        powers = dataReader.ReadCSVData("2011_03_11_14_46_miyagi");
        rigidPlane = GameObject.Find("Plane").GetComponent<Rigidbody>();
        StartCoroutine(Shake(powers));
        
    }



    void Update()
    {
        Time.timeScale = timeScale;
        // Debug.Log(Time.timeScale);
    }

    private IEnumerator Shake(List<ShakePower> powers){
        Debug.Log("start");
        foreach(ShakePower power in powers){
            // Debug.Log(power.ns + " , " + power.ew + " , " + power.ud);
            Vector3 force = Vector3.forward * power.ns * powerBias + Vector3.right * power.ew * powerBias + Vector3.up * power.ud * powerBias;
            rigidPlane.AddForce(force);
            // yield return new WaitForSeconds(0.01f);
            yield return null;
        }
        Debug.Log("finish");
    }
}
