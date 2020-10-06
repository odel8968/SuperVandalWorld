using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{
    public bool abilityEnabled;
    
    void start(){
        abilityEnabled = false;
    }
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
        //call GUI manager's function for score to be updated
        int addPoints = 0;

        switch(other.tag)
        {
            case "Item":
                if(other.gameObject.name.Contains("Sapphire")){
                    addPoints = 100;
                } 
                else if(other.gameObject.name.Contains("Emerald")){
                    addPoints = 200;
                }
                else if(other.gameObject.name.Contains("Ruby")){
                    addPoints = 400;
                }
                else if(other.gameObject.name.Contains("Diamond")){
                    addPoints = 2500;
                }
                
                Debug.Log("Points added = " + addPoints);
                break;
            case "PowerUp":
                addPoints = 1000;
                break;
        }
        
        return addPoints;
    }

    public static void removeAsset(Collider2D other){
        //remove asset from level via destroy function
        Destroy(other.gameObject);
    }

    public static void triggerSound(Collider2D other){
        //call Sound managers function to play sound

    }
}