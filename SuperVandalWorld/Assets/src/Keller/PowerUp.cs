using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    public static void collisionPowerUp(Collider2D other){
        //on trigger with pickup
        Debug.Log("Collision with " + other.gameObject.tag);

        removeAsset(other);
        updateScore(other);
        triggerSound(other);
    }

    public void enableAbility(){

    }
}
