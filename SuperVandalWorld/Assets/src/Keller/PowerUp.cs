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
        pause = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        bcMode = pause.DrBCMode();
    }

    public override void OnTriggerEnter2D(Collider2D col)
    {
        //pass power up object to observer
        if(bcMode && this.name.Contains("badApple"))
        {
            this.name = "BCMODE";
            Debug.Log(this.name);

            objectCollisionNotification(this);
        }
        else
        {
            objectCollisionNotification(this);
        }

        //delete powerup
        removeAsset(this.gameObject);
    } 

    private int changeHealthValue(Collider2D other)
    {
        var obj = other.GetComponent<PowerUp>();
        return obj.healthChange;
    }

    public static event Action<PowerUp> objectCollisionNotification = delegate { };
}
