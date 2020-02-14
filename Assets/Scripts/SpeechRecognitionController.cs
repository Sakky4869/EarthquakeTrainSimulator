using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechRecognitionController : MonoBehaviour
{
    private TrainingManager trainingManager;
    // 家具を出現させるための宝箱
    [SerializeField] private GameObject prepareBox;
    void Start()
    {
        trainingManager = GameObject.Find("TrainingManager").GetComponent<TrainingManager>();
    }

    void Update()
    {
        
    }

    public void SpawnTreasureBox(){
        if(trainingManager.isTreasureBoxSpawned)
            return;
        Vector3 treasureBoxSpawnPos = Camera.main.gameObject.transform.position + Vector3.up / 10 + Vector3.forward / 10;
        GameObject box = Instantiate(prepareBox, treasureBoxSpawnPos, Quaternion.identity);
        box.transform.LookAt(Camera.main.transform);
        Quaternion angle = box.transform.rotation;
        Vector3 angleEu = angle.eulerAngles;
        angleEu.x = 0;
        box.transform.rotation = Quaternion.Euler(angleEu);
        trainingManager.StartPrepare();
    }

    public void StartTraining(){
        trainingManager.StartTraining();
    }

    public void CompletePrepare(){
        trainingManager.CompletePrepare();
    }

    public void Test()
    {
        Debug.Log("test");
    }
}
