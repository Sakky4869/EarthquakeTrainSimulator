using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskItem : MonoBehaviour, System.IComparable<TaskItem>
{
    [HideInInspector]
    public int id;
    public TextMeshPro taskText;
    private Player player;



    void Start()
    {
        player = Camera.main.transform.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    // タスクリストの表記を完了状態にする
    public void ClearTask(){
        taskText.fontStyle = FontStyles.Strikethrough;
        player.PlayTaskClearSound();
    }

    // タスクリストの表記をリセットする
    public void ResetTask(){
        taskText.fontStyle = FontStyles.Normal;
    }


    // タスクアイテムのリストのソートの際に，Sort()を呼ぶだけでソートされるように，
    // IComparableのメソッドを実装
    public int CompareTo(TaskItem item){
        if(item == null)
            return 1;
        return this.id - ((TaskItem)item).id;
    }
}
