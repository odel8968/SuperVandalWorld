using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    private static pickupsManager player;
    public static void collisionPowerUp(Collider2D other){
        //on trigger with pickup

        //enableAbility(Character_Movement.hasAbility); 
        //need to call player manager script to see if player already has ability enabled


        removeAsset(other);
        updateScore(other);
        triggerSound(other);
        LogInfo(other);
        

    }

    public static void enableAbility(bool value){
        if(value == false){
            value = true;
            Debug.Log("Ablitiy has been enabled");
        }
        else{
            Debug.Log("Ability already enabled");
        }
    }
}
