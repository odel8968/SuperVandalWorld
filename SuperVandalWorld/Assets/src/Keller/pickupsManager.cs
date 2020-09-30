using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{

    public static void updateScore(Collider2D other){
        //call GUI manager's function for score to be updated

    }

    public static void removeAsset(Collider2D other){
        //remove asset from level via destroy function
        Destroy(other.gameObject);
    }

    public static void triggerSound(Collider2D other){
        //call Sound managers function to play sound
    }
}