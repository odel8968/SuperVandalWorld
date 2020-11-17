using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUp : pickupsManager
{
    public int healthChange;
    PauseMenu pause;
    bool bcMode;

    void Start()
    {
        //find pause menu to be able to check for BC mode
        pause = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        bcMode = pause.DrBCMode();
    }

    //override OnTriggerEnter2D from parent class pickUpsManager
    public override void OnTriggerEnter2D(Collider2D col)
    {
        //check for bc mode and kill "powerUp"
        if(bcMode && this.name.Contains("badApple"))
        {
            //change object name to BCMODE to negate kill effects
            this.name = "BCMODE";
            Debug.Log(this.name);
            //send to listener
            objectCollisionNotification(this);
        }
        else
        {
            //send to listener
            objectCollisionNotification(this);
        }

        //call static method from pickUpsManager to destroy object
        removeAsset(this.gameObject);
    } 

    private int changeHealthValue(Collider2D other)
    {
        var obj = other.GetComponent<PowerUp>();
        return obj.healthChange;
    }

    //send notifications to listener
    public static event Action<PowerUp> objectCollisionNotification = delegate { };
}
