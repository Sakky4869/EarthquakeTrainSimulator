using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloLensRemoteController : MonoBehaviour
{
    #region Private Serializable Fields

    #endregion

    #region Private Fields

    private string gameVersion = "1";
    [SerializeField]
    private byte maxPlayersPerRoom;

    #endregion


    private void Awake()
    {
        maxPlayersPerRoom = 2;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
