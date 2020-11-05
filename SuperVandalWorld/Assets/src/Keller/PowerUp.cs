using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : pickupsManager
{
    public int scoreValue;
    public int healthChange;
    public static void collisionPowerUp(Collider2D col)
    {
        //on trigger with pickup
        PowerUp gObject = col.gameObject.GetComponent<PowerUp>();
        Character_Movement player = GameObject.Find("Player").GetComponent<Character_Movement>();
        SoundManager sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();
        

        //Character_Movement.currentAbility = changeAbility(Character_Movement.currentAbility, other);
        if(player.hasAbility)
        {
            if(!checkAbilityNames(gObject, player))
            {
                //changeAbility(gObject, player);
                if(gObject.name.Contains("Axe"))
                {
                    GameObject.Find("Player").GetComponent<powerAxe>().enabled = true;
                    player.abilityName = (gObject.name);
                }
                else if(gObject.name.Contains("Jump"))
                {
                    GameObject.Find("Player").GetComponent<multiJump>().enabled = true;
                    player.abilityName = (gObject.name);
                }
            }
            Debug.Log(player.abilityName);
        }
        else if(!player.hasAbility)
        {
            if(gObject.name.Contains("Axe"))
            {
                GameObject.Find("Player").GetComponent<powerAxe>().enabled = true;
            }
            else if(gObject.name.Contains("Jump"))
            {
                GameObject.Find("Player").GetComponent<multiJump>().enabled = true;
            }
        }
        score.AddScore(updateScore(gObject));
        sounds.PlaySound("PowerUp");
        removeAsset(col);
    }

    private static bool checkAbilityNames(PowerUp pwr, Character_Movement player)
    {
        if(player.abilityName.Contains(pwr.name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private int changeHealthValue(Collider2D other)
    {
        var obj = other.GetComponent<PowerUp>();
        return obj.healthChange;
    }

    private static int updateScore(PowerUp other)
    {
        var obj = other.GetComponent<PowerUp>();
        return obj.scoreValue;
    }
}
