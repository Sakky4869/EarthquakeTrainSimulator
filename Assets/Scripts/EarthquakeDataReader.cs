using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakePower{
    //[HideInInspector] 
    public float ns;
    //[HideInInspector] 
    public float ew;
    //[HideInInspector] 
    public float ud;
}

public class EarthquakeDataReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<ShakePower> ReadCSVData(string fileName){
        // Debug.Log("start reading");
        List<ShakePower> read_data = new List<ShakePower>();
        using(System.IO.StreamReader streamReader = new System.IO.StreamReader(Application.dataPath + "/Data/" + fileName + ".txt")){
            int count = 0;
            while(streamReader.Peek() > 0){
                count++;
                // Debug.Log(streamReader)
                //8行目からが揺れのデータなので、それのみを読み取る
                if(count >= 8){
                    //読み取ったデータを小数に変換
                    try{
                        string[] data = streamReader.ReadLine().Split('\t');
                        ShakePower power = new ShakePower();
                        power.ns = float.Parse(data[0]);
                        power.ew = float.Parse(data[1]);
                        power.ud = float.Parse(data[2]);
                        read_data.Add(power);
                    }catch{
                        // Debug.Log("miss converting");
                    }
                }
            }
        }
        // Debug.Log("finish reading");
        return (read_data.Count == 0)? null: read_data;
    }

}
