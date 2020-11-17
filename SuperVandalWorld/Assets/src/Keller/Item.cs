using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : pickupsManager
{

    public override void OnTriggerEnter2D(Collider2D col)
    {
        //pass gem object to observer
        objectCollisionNotification(this);

        //delete item
        removeAsset(this.gameObject);
    }


    public static event Action<Item> objectCollisionNotification = delegate { };
}
