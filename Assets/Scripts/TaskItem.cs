using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class TaskItem : MonoBehaviour, System.IComparable<TaskItem>
{
    [HideInInspector]
    public int id;
    public TextMesh taskText;

    

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ClearTask(){
        taskText.color = Color.clear;
    }

    public int CompareTo(TaskItem item){
        if(item == null)
            return 1;
        return this.id - ((TaskItem)item).id;
    }
}
