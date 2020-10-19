using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Item")){
            Item.collisionItem(other);
        }
        if(other.gameObject.CompareTag("PowerUp")){
            PowerUp.collisionPowerUp(other);
        }
    }

    public static void LogInfo(Collider2D other){
        Debug.Log("Collision with " + other.gameObject.name);
        
    }
    
    public static int updateScore(Collider2D other){

        int addPoints = 0;

        string pickUpType = other.gameObject.tag;
        switch(other.gameObject.tag){
            case "Item":
            {
                var obj = other.GetComponent<Item>();
                addPoints = obj.scoreValue;

                Debug.Log("Adding " + addPoints + " to score");    
                return addPoints;
            }
            
            case "PowerUp":
            {
                var obj = other.GetComponent<PowerUp>();
                addPoints = obj.scoreValue;

                Debug.Log("Adding " + addPoints + " to score");    
                return addPoints;
            }
            default:
            {
                return addPoints;
            }
        }   
    }

    public static void removeAsset(Collider2D other){
        //remove asset from level via destroy function
        Destroy(other.gameObject);
    }

    public static void triggerSound(Collider2D other){
        //call Sound managers function to play sound

    }
}