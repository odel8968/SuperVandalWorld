using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : pickupsManager
{
    public int scoreValue;

    public static void collisionItem(Collider2D col)
    {
        //on trigger with pickup
        Item gem = col.gameObject.GetComponent<Item>();

        //find sound manager and scoreboard
        SoundManager sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        //call sound manager function to play item sound
        sounds.PlaySound("PowerUp");

        //call score function to udpate score
        score.AddScore(updateScore(gem));

        //delete item
        removeAsset(col);
    }

    //get score value of item and return int
    private static int updateScore(Item other)
    {
        var obj = other.GetComponent<Item>();
        return obj.scoreValue;
    }
}
