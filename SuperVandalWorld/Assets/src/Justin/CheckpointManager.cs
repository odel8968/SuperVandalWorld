using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    //Singleton - only one checkpoint instance can exist
    private static CheckpointManager instance;
    public Vector2 lastCheckPointPos;

    void Awake()
    {
        //Keep current instance between scene loads
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            //if an instance already exists, destroy it.
            Destroy(gameObject);
        }
    }
}
