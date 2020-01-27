using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 音を再生するAudioSource
    private AudioSource soundPlayer;

    // タスククリアのときに再生する音
    [SerializeField]
    private AudioClip taskClearSound;
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayTaskClearSound(){
        soundPlayer.PlayOneShot(taskClearSound);
    }
}
