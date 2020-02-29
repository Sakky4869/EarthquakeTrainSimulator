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

    [SerializeField]
    private Rigidbody vaseRigid;
    [SerializeField]
    private Rigidbody bookshelfRigid;
    [SerializeField]
    private Rigidbody shakeTarget;


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
        // 揺らす対象のオブジェクトをリストに格納
        ShakeObject[] objes = FindObjectsOfType<ShakeObject>();
        foreach(ShakeObject s in objes)
        {
            AddShakeObject(s);
        }

        yield return null;

        // 最初に初期情報を登録
        foreach(ShakeObject shake in shakeObjects)
        {
            if (shake.gameObject.name.Contains("Bookshelf"))
            {
                shake.SaveFirstPos(bookshelfRigid);
            }else if (shake.gameObject.name.Contains("Vase"))
            {
                shake.SaveFirstPos(vaseRigid);
            }
        }

        yield return null;

        //WebLogger.SendLog("地震開始");
        foreach (ShakePower power in powers){
            // 揺れの終了
            if (power.flag == true)
            {
                trainingManager.isQuaking = false;
                GameObject.Find("TaskPanel").GetComponent<PlayerUI>().StopBGM("Dinari");
                yield break;
            }
            else
            {
                // 力を加えるベクトルの生成
                Vector3 force = new Vector3(power.ns, power.ew, power.ud) * powerBias;

                // 揺らす対象のみ揺らす
                shakeTarget.AddForce(force);

                foreach(ShakeObject shakeObject in shakeObjects)
                {
                    // オブジェクトが本棚だった場合
                    if (shakeObject.gameObject.name.Contains("Bookshelf"))
                    {
                        shakeObject.Shake(bookshelfRigid);
                    }else if (shakeObject.gameObject.name.Contains("Vase"))
                    {
                        shakeObject.Shake(vaseRigid);
                    }
                }
            }
            yield return new WaitForSeconds(1 / 100);
        }
    }
}
