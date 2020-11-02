using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : pickupsManager
{
    public int scoreValue;

    void Start()
    {
        
    }
    public static void collisionItem(Collider2D other)
    {
        //on trigger with pickup
        var ui = GameObject.Find("Score").GetComponent<UIManager>();
        var soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        soundManager.PlaySound("Gem");
        removeAsset(other);
        ui.AddScore(updateScore(other));
        //LogInfo(other); 
    }
}
