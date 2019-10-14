using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    private List<ShakePower> powers;

    private Rigidbody rigidPlane;
    [SerializeField] private float timeScale;

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
            Debug.Log(power.ns + " , " + power.ew + " , " + power.ud);
            Vector3 force = Vector3.forward * power.ns + Vector3.right * power.ew + Vector3.up * power.ud;
            rigidPlane.AddForce(force);
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log("finish");
    }
}
