using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    private List<ShakePower> powers;

    private Rigidbody rigidPlane;
    [Header("タイムスケール"),SerializeField] private float timeScale = 1;
    [Header("地震のパワーバイアス"),SerializeField] private float powerBias = 1;


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
    }

    private IEnumerator Shake(List<ShakePower> powers){
        Debug.Log("start");
        foreach(ShakePower power in powers){
            Vector3 force = Vector3.forward * power.ns * powerBias + Vector3.right * power.ew * powerBias + Vector3.up * power.ud * powerBias;
            rigidPlane.AddForce(force);
            yield return new WaitForSeconds(1 / 100);
        }
        Debug.Log("finish");
    }
}
