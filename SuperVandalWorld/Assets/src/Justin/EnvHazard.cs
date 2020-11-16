using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class EnvHazard : EnvObject
{
    //Variable to hold reference to Pause Menu
    PauseMenu pauseObj;

    //Variable to hold if BC mode is on or off
    bool easyMode;

    void Start()
    {
        //Find Pause Menu script
        pauseObj = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        //Check if BC mode is on or off
        easyMode = pauseObj.DrBCMode();
    }

    //send notifications to EnvListener
    public static event Action<string> objectCollisionNotification = delegate { };

    //Dynamically bound collision function - Overriding static from parent
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            //if BC mode is on, send notification to listener
            if(easyMode == true)
            {
                objectCollisionNotification("BCMODE");
            }
            else
            {
                //otherwise BC mode is off, send notification to listener
                if(playerAlive == true)
                {
                    objectCollisionNotification("EnvHazard");
                }
            }
        }
    }

    //Function for trigger collisions
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //if BC mode is on, send notification to listener
            if(easyMode == true)
            {
                objectCollisionNotification("BCMODE");
            }
            else
            {
                //otherwise BC mode is off, send notification to listener
                if(playerAlive == true)
                {
                    objectCollisionNotification("WaterHazard");
                }
            }
        }
    }

}
