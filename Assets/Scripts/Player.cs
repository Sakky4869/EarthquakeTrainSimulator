using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 音を再生するAudioSource
    private AudioSource soundPlayer;

    // タスククリアのときに再生する音
    private AudioClip taskClearSound;
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        taskClearSound = Resources.Load("Audio/SE/task_clear") as AudioClip;
    }

    void Update()
    {
        
    }

    public void PlaySound(string name){
        switch (name)
        {
            case "TaskClear":
                soundPlayer.PlayOneShot(taskClearSound);
                break;
            default:
                break;
        }
    }
}
