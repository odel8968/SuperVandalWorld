using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : pickupsManager
{

    //override OnTriggerEnter2D from parent class pickUpsManager
    public override void OnTriggerEnter2D(Collider2D col)
    {
        //send to listener
        objectCollisionNotification(this);

        //delete item
        removeAsset(this.gameObject);
    }

    //send notifications to listener
    public static event Action<Item> objectCollisionNotification = delegate { };
}
