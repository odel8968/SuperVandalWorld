using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    
    public int scoreValue;
    public static void collisionPowerUp(Collider2D other)
    {
        //on trigger with pickup

        Character_Movement.hasAbility = enableAbility(Character_Movement.hasAbility);
        //Character_Movement.currentAbility = changeAbility(Character_Movement.currentAbility, other);

        removeAsset(other);
        updateScore(other);
        triggerSound(other);
        LogInfo(other);
        
    }

    //enable use of ability for character
    public static bool enableAbility(bool value)
    {
        if(value == false)
        {
            value = true;
            Debug.Log("Ablitiy has been enabled");
            Debug.Log(value);
        }
        else
        {
            Debug.Log("Ability already enabled");
        }

        return value;
    }

    public static void changeAbility(string str, Collider2D other)
    {
        if(other.gameObject.name.Contains(str))
        {
            removeAsset(other);
            updateScore(other);
            LogInfo(other);
        }
        else
        {
            removeAsset(other);
            updateScore(other);
            triggerSound(other);
            LogInfo(other);
        }
    }
}
