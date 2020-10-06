using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : pickupsManager
{
    
    public static void collisionItem(Collider2D other){
        //on trigger with pickup

        removeAsset(other);
        updateScore(other);
        triggerSound(other);
        LogInfo(other);
    }
}
