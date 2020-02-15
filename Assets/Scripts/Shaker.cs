using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    //[SerializeField] 
    private List<ShakePower> powers;
    private List<ShakeObject> shakeObjects;

    //private Rigidbody rigidPlane;
    [Header("タイムスケール"),SerializeField] private float timeScale = 1;
    [Header("地震のパワーバイアス"),SerializeField] private float powerBias = 1;


    void Start()
    {
        dataReader = GameObject.Find("EarthquakeDataReader").GetComponent<EarthquakeDataReader>();
        powers = dataReader.ReadCSVData("2011_03_11_14_46_miyagi");
        shakeObjects = new List<ShakeObject>();
    }



    void Update()
    {

    }

    public void AddShakeObject(ShakeObject shakeObject)
    {
        shakeObjects.Add(shakeObject);
    }

    public void StartShake()
    {
        StartCoroutine(Shake(powers));
    }

    private IEnumerator Shake(List<ShakePower> powers){

        foreach(ShakePower power in powers){
            Vector3 force = new Vector3(power.ns, power.ew, power.ud) * powerBias; 
            foreach(ShakeObject shakeObject in shakeObjects)
            {
                shakeObject.Shake(force);
            }
            yield return new WaitForSeconds(1 / 100);
        }
    }
}
