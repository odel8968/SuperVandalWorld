using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    
    public int scoreValue;
    public int healthChange;
    public static void collisionPowerUp(Collider2D other)
    {
        //on trigger with pickup
        var ui = GameObject.Find("Score").GetComponent<UIManager>();
        var soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        Character_Movement.hasAbility = enableAbility(Character_Movement.hasAbility);
        //Character_Movement.currentAbility = changeAbility(Character_Movement.currentAbility, other);
        if(Character_Movement.hasAbility)
        {
            /*
            if(Character_Movement.abilityName.Contains(other.GameObject.name))
            {
                changeAbility(other);
            }
            */
            LogInfo(other);
        }
        else if(!Character_Movement.hasAbility)
        {
            
            LogInfo(other);

        }
        removeAsset(other);
        ui.AddScore(updateScore(other));
        soundManager.PlaySound("PowerUp");
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

    public static void changeAbility(Collider2D other)
    {
        //Character_Movement.abilityName = other.gameObject.name;
    }

    public int changeHealthValue(Collider2D other)
    {
        var obj = other.GetComponent<PowerUp>();
        return obj.healthChange;
    }

}
