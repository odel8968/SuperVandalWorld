using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    public static void collisionPowerUp(Collider2D other){
        //on trigger with pickup

        removeAsset(other);
        updateScore(other);
        triggerSound(other);
        LogInfo(other);

    }

    public void enableAbility(){

    }
}
