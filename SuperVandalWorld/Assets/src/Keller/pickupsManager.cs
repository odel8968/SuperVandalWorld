using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{
    public  void onTriggerEnter2D(Collision2D trig){
        //on trigger with pickup
        Debug.Log("Collision with " + trig.gameObject.tag);

        removeAsset(trig);
        updateScore(trig);
        triggerSound(trig);
    }

    public  void updateScore(Collision2D trig){
        //call GUI manager's function for score to be updated

    }

    public  void removeAsset(Collision2D trig){
        //remove asset from level via destroy function
        Destroy(trig.gameObject);
    }

    public  void triggerSound(Collision2D trig){
        //call Sound managers function to play sound
    }
}