using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakePower{
    public float ns;
    public float ew;
    public float ud;
}

public class EarthquakeDataReader : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public List<ShakePower> ReadCSVData(string fileName){
        List<ShakePower> read_data = new List<ShakePower>();
        using(System.IO.StreamReader streamReader = new System.IO.StreamReader(Application.dataPath + "/Data/" + fileName + ".txt")){
            int count = 0;
            while(streamReader.Peek() > 0){
                count++;
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
                    }
                }
            }
        }
        return (read_data.Count == 0)? null: read_data;
    }

}
