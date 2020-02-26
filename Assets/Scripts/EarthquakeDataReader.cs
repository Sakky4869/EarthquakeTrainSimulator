using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ShakePower{
    public float ns;
    public float ew;
    public float ud;
    public bool flag;
}

public class EarthquakeDataReader : MonoBehaviour
{

    private Shaker shaker;

    void Start()
    {
        shaker = GameObject.Find("Shaker").GetComponent<Shaker>();
        StartCoroutine(ReadCSVData("2011_03_11_14_46_miyagi"));
    }


    void Update()
    {
        
    }

    public IEnumerator ReadCSVData(string fileName){
        List<ShakePower> read_data = new List<ShakePower>();

#if UNITY_EDITOR
        using (System.IO.StreamReader streamReader = new System.IO.StreamReader(Application.dataPath + "/Data/" + fileName + ".txt"))
        {
            int count = 0;
            while (streamReader.Peek() > 0)
            {
                count++;
                //8行目からが揺れのデータなので、それのみを読み取る
                if (count >= 8)
                {
                    //読み取ったデータを小数に変換
                    try
                    {
                        ShakePower power = new ShakePower();
                        string rawData = streamReader.ReadLine();
                        power.flag = (rawData.Contains("flag"));
                        if (power.flag == false)
                        {
                            string[] data = rawData.Split('\t');
                            power.ns = float.Parse(data[0]);
                            power.ew = float.Parse(data[1]);
                            power.ud = float.Parse(data[2]);
                        }
                        read_data.Add(power);
                    }
                    catch
                    {
                    }
                }
            }
        }
        yield return null;
#else
        DirectoryInfo directory = new DirectoryInfo(Application.persistentDataPath + "/Data");
        if(directory.Exists == false)
            directory.Create();
        if(new FileInfo(Application.persistentDataPath + "/Data/" + fileName + ".txt").Exists == false)
        {
            WWW www = new WWW("http://grapefruit.sys.wakayama-u.ac.jp/~sakai/UnityLog/EarthquakeTrainSimulator/" + fileName + ".txt");
            yield return www;

            if(string.IsNullOrEmpty(www.error))
            {
                File.WriteAllBytes(Application.persistentDataPath + "/Data/" + Path.GetFileName(www.url), www.bytes);
            }
        }


        using (System.IO.StreamReader streamReader = new System.IO.StreamReader(Application.persistentDataPath + "/Data/" + fileName + ".txt")){
            int count = 0;
            while(streamReader.Peek() > 0){
                count++;
                //8行目からが揺れのデータなので、それのみを読み取る
                if(count >= 8){
                    //読み取ったデータを小数に変換
                    try{
                        ShakePower power = new ShakePower();
                        string rawData = streamReader.ReadLine();
                        power.flag = (rawData.Contains("flag"));
                        if (power.flag == false)
                        {
                            string[] data = rawData.Split('\t');
                            power.ns = float.Parse(data[0]);
                            power.ew = float.Parse(data[1]);
                            power.ud = float.Parse(data[2]);
                        }
                        read_data.Add(power);
                    }catch{
                    }
                }
            }
        }
#endif
        shaker.powers = read_data;

        //return (read_data.Count == 0)? null: read_data;
        //shaker.StartShake();
    }

}
