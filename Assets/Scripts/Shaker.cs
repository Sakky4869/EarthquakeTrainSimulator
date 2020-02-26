using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{

    private EarthquakeDataReader dataReader;
    //[SerializeField] 
    //[HideInInspector]
    public List<ShakePower> powers;
    private List<ShakeObject> shakeObjects;

    //private Rigidbody rigidPlane;
    [Header("タイムスケール"),SerializeField] private float timeScale = 1;
    [Header("地震のパワーバイアス"),SerializeField] private float powerBias = 1;

    private TrainingManager trainingManager;


    void Start()
    {
        //dataReader = GameObject.Find("EarthquakeDataReader").GetComponent<EarthquakeDataReader>();
        //powers = dataReader.ReadCSVData("2011_03_11_14_46_miyagi");
        shakeObjects = new List<ShakeObject>();
        Time.timeScale = timeScale;
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
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

        //WebLogger.SendLog("地震開始");
        foreach (ShakePower power in powers){
            // 揺れの終了
            if (power.flag == true)
            {
                trainingManager.isQuaking = false;
                //GameObject.Find("TaskPanel").GetComponent<PlayerUI>().ShowMessage("地震終わり", 10);
                //WebLogger.SendLog("地震おわり");
                yield break;
            }
            else
            {
                Vector3 force = new Vector3(power.ns, power.ew, power.ud) * powerBias; 
                foreach(ShakeObject shakeObject in shakeObjects)
                {
                    shakeObject.Shake(force);
                }
            }
            yield return new WaitForSeconds(1 / 100);
        }
    }
}
