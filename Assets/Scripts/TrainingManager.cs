using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainingPhase
{
    Idle = 0,
    SpatialAwareness,
    SpatialAwarenessCompleted,
    Prepare,
    PrepareCompleted,
    InTraining,
    FinishedTraining
}

[System.Serializable]
public class FurnitureInfoRoot
{
    public List<FurnitureInfomation> infos;
    public FurnitureInfoRoot()
    {
        infos = new List<FurnitureInfomation>();
    }

    public void AddFurniture(GameObject furniture)
    {
        FurnitureInfomation info = new FurnitureInfomation(furniture.name, furniture.transform.position, furniture.transform.rotation);
        infos.Add(info);
    }
}

[System.Serializable]
public class FurnitureInfomation
{
    // 家具の名前
    public string name;

    // 家具の位置
    public FurniturePosition pos;

    // 家具の角度
    public FurnitureRotation rot;

    public FurnitureInfomation(string name, Vector3 pos, Quaternion rot)
    {
        this.name = name;
        this.pos = new FurniturePosition(pos);
        this.rot = new FurnitureRotation(rot);
    }
}

[System.Serializable]
public class FurniturePosition
{
    public double x;
    public double y;
    public double z;
    public FurniturePosition(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }
}

[System.Serializable]
public class FurnitureRotation
{
    public double x;
    public double y;
    public double z;
    public FurnitureRotation(Quaternion rot)
    {
        Vector3 angle = rot.eulerAngles;
        this.x = angle.x;
        this.y = angle.y;
        this.z = angle.z;
    }
}



public class TrainingManager : MonoBehaviour
{
    // 訓練オブジェクトのリスト
    [SerializeField] private List<TrainingObjectBase> trainingObjects;

    // 揺れているかどうか
    [HideInInspector] public bool isQuaking;

    // 訓練が終了しているかどうか
    [HideInInspector] public bool isFinishedTraining;

    // 訓練中かどうか
    private bool isInTraining;

    // 家具の宝箱が出現しているかどうか
    [HideInInspector] public bool isTreasureBoxSpawned;

    // 訓練システムのフェーズ
    private TrainingPhase trainingPhase;

    // ログ出力制御変数
    private bool outLog;

    // プレイヤーのUIのクラス
    private PlayerUI playerUI;

    // 家具の位置情報と角度情報のデータ
    private FurnitureInfoRoot infoRoot;

    // 地震発生プログラム
    private Shaker shaker;

    // PrepareManager
    private PrepareManager prepareManager;

    private void Awake()
    {
        Application.logMessageReceived += LogCallBackHandler;
    }

    private void LogCallBackHandler(string conditon, string stackTrace, LogType logType)
    {
        //WebLogger.SendLog("[Conditon]" + System.Environment.NewLine
        //    + conditon + System.Environment.NewLine
        //    + "[StackTrace]" + System.Environment.NewLine
        //    + stackTrace + System.Environment.NewLine
        //    + "[Log Type]" + System.Environment.NewLine
        //    + logType.ToString() + System.Environment.NewLine); 
        WebLogger.SendLog(conditon);
    }

    void Start()
    {
        isQuaking = false;
        isFinishedTraining = false;
        //isInTraining = false;
        isTreasureBoxSpawned = false;
        trainingPhase = TrainingPhase.Idle;
        outLog = false;
        prepareManager = GameObject.Find("PrepareManager").GetComponent<PrepareManager>();
        shaker = GameObject.Find("Shaker").GetComponent<Shaker>();
        StartCoroutine(TrainingCor());
        playerUI = Camera.main.transform.GetChild(0).GetComponent<PlayerUI>();
        infoRoot = new FurnitureInfoRoot();
    }


    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Return))
            GameObject.Find("SpeechRecognitionController").GetComponent<SpeechRecognitionController>().SpawnTreasureBox();
        if (Input.GetKeyDown(KeyCode.C))
            CompletePrepare();
#endif
    }

    // トレーニングシステムのコルーチン
    private IEnumerator TrainingCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            switch (trainingPhase)
            {
                case TrainingPhase.Idle:
                    if (!outLog)
                    {
                        outLog = true;
                    }
                    break;
                case TrainingPhase.SpatialAwareness:
                    if (!outLog)
                    {
                        outLog = true;
                    }
                    break;
                case TrainingPhase.SpatialAwarenessCompleted:
                    if (!outLog)
                    {
                        outLog = true;
                    }
                    break;
                case TrainingPhase.Prepare:
                    if (!outLog)
                    {
                        outLog = true;
                    }
                    break;
                case TrainingPhase.InTraining:
                    if (!outLog)
                    {
                        outLog = true;
                    }
                    if (isFinishedTraining)
                    {
                        trainingPhase = TrainingPhase.FinishedTraining;
                    }
                    break;
                case TrainingPhase.FinishedTraining:
                    
                    break;
                default:
                    break;
            }
            break;
        }
    }

    // 状況に応じてメッセージを表示する
    // 基本的にヘルプで使用
    public void ShowMessage()
    {
        //WebLogger.SendLog("call_help");
        string msg = "";
        float time = 0;
        switch (trainingPhase)
        {
            case TrainingPhase.Idle:
                msg = "「スキャン開始」で周囲をスキャン";
                //WebLogger.SendLog("周囲をスキャン");
                break;
            case TrainingPhase.SpatialAwareness:
                msg = "「スキャン完了」と発言し，スキャンを終える";
                //WebLogger.SendLog("「スキャン完了」と発言し，スキャンを終える");
                break;
            case TrainingPhase.SpatialAwarenessCompleted:
                msg = "「準備開始」と発言し，準備を開始する";
                //WebLogger.SendLog("「準備開始」と発言し，準備を開始する");
                break;
            case TrainingPhase.Prepare:
                msg = "「準備完了」と発言し，準備を完了する";
                //WebLogger.SendLog("「訓練開始」と発言し，訓練を開始する");
                break;
            case TrainingPhase.PrepareCompleted:
                msg = "「訓練開始」と発言し，訓練を開始する";
                //WebLogger.SendLog("「訓練開始」と発言し，訓練を開始する");
                break;
            case TrainingPhase.InTraining:
                msg = "タスクリストをこなしましょう";
                //WebLogger.SendLog("タスクリストをこなしましょう");
                break;
            case TrainingPhase.FinishedTraining:
                msg = "終了する場合は「システム終了」と発言再開する場合は「再開」と発言";
                //WebLogger.SendLog("終了する場合は「システム終了」と発言再開する場合は「再開」と発言");
                time = 8;
                break;
            default:
                break;
        }

        if(msg != "")
        {
            WebLogger.SendLog(msg);
        }

        if (time == 0)
            playerUI.ShowMessage(msg);
        else
            playerUI.ShowMessage(msg, time);
    }


    // 環境認識開始
    public void StartAwareness()
    {
        outLog = false;
        trainingPhase = TrainingPhase.SpatialAwareness;
        playerUI.ShowMessage("環境認識を開始します");
        // この時点で，トレーニングフェーズはSpatialAwareness
    }

    // 環境認識完了
    public void CompleteAwareness()
    {
        outLog = false;
        trainingPhase = TrainingPhase.SpatialAwarenessCompleted;
        playerUI.ShowMessage("環境認識を終了します");
        // この時点で，トレーニングフェーズはSpatialAwarenessCompleted
    }

    // 家具などの設置開始
    public void StartPrepare()
    {
        outLog = false;
        isTreasureBoxSpawned = true;
        trainingPhase = TrainingPhase.Prepare;
        playerUI.ShowMessage("準備を開始します");
        // この時点で，トレーニングフェーズはPrepare
    }

    // 家具などの設置完了
    public void CompletePrepare()
    {
        outLog = false;
        trainingPhase = TrainingPhase.PrepareCompleted;
        playerUI.ShowMessage("準備を終わります");
        // この時点で，トレーニングフェーズはInTraining
        prepareManager.Prepare();
    }

    //訓練オブジェクトをリストへ追加
    public void AddTrainingObject(TrainingObjectBase trainingObject)
    {
        if (trainingObjects == null)
            trainingObjects = new List<TrainingObjectBase>();
        trainingObjects.Add(trainingObject);
    }

    //訓練
    public void StartTraining()
    {
        //isInTraining = true;
        trainingPhase = TrainingPhase.InTraining;
        shaker.StartShake();

        StartCoroutine(StartTrainingCor());
    }

    private IEnumerator StartTrainingCor()
    {
        yield return playerUI.ShowMessageCall("訓練を開始します");
        //家具の位置を角度を保存
        SaveFurnitureInfo();
        playerUI.Emergency();
    }


    // タスククリアの方式を，タスクオブジェクトを監視する形から，タスクオブジェクトがメッセージを送る形式に変更
    public void ClearTask()
    {
        bool isClear = true;
        foreach (TrainingObjectBase trainingObject in trainingObjects)
        {
            if (trainingObject.isClear == false)
            {
                isClear = false;
            }
        }

        // クリアしていたら
        if (isClear)
        {
            isFinishedTraining = true;
            trainingPhase = TrainingPhase.FinishedTraining;
        }
    }

    //訓練終了
    public void ClearTraining()
    {
        //isInTraining = false;
        trainingPhase = TrainingPhase.FinishedTraining;
        playerUI.ShowMessage("終了する場合は「システム終了」と発言再開する場合は「再開」と発言", 8);
    }

    public void SaveFurnitureInfo()
    {
        StartCoroutine(SaveFurnitureInfoCor());
    }

    // 訓練開始直前に，配置したオブジェクトの位置と角度の情報を保存
    private IEnumerator SaveFurnitureInfoCor()
    {
        GameObject[] furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        int furnitureCount = furnitures.Length;
        playerUI.ShowMessage("家具の位置を記録中　個数：" + furnitureCount);
        foreach (GameObject furniture in furnitures)
        {
            infoRoot.AddFurniture(furniture);
            yield return null;
        }
        playerUI.ShowMessage("家具の位置の記録が完了");
    }

    // 訓練の再開
    public void ReloadSystem()
    {
        if(trainingPhase == TrainingPhase.FinishedTraining)
            StartCoroutine(ReloadSystemCor());
    }

    private IEnumerator ReloadSystemCor()
    {
        GameObject[] furnitures = GameObject.FindGameObjectsWithTag("Furniture");
        int furnitureCount = furnitures.Length;
        playerUI.ShowMessage("家具の再配置中　個数：" + furnitureCount);
        foreach (GameObject furniture in furnitures)
        {
            foreach(FurnitureInfomation info in infoRoot.infos)
            {
                // 名前が一致したら，その家具の位置と角度を元に戻す
                if(furniture.name == info.name)
                {
                    furniture.transform.position = new Vector3((float)info.pos.x, (float)info.pos.y, (float)info.pos.z);
                    furniture.transform.rotation = Quaternion.Euler((float)info.rot.x, (float)info.rot.y, (float)info.rot.z);
                }
            }
            yield return null;
        }

        // 再配置が完了したらトレーニングシステムを再開する
        StartTraining();
    }

    // 本システムを終了する
    public void FinishSystem()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif !UNITY_EDITOR
        Application.Quit();
#endif
    }


}
