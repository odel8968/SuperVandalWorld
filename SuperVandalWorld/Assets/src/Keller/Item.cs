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
        SoundManager sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        sounds.PlaySound("PowerUp");
        score.AddScore(updateScore(gem));
        removeAsset(col);
    }
    private static int updateScore(Item other)
    {
        var obj = other.GetComponent<Item>();
        return obj.scoreValue;
    }
}
