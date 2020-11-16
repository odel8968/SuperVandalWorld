using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCPM : MonoBehaviour
{
    //Destory the CheckpointManager scenes where there is no player
    void Start()
    {
        Destroy(GameObject.Find("CheckpointManager"));
    }

}
