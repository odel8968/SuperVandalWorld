using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Reference to EnvObject class
    public EnvObject envObject;

    //Varialbe to see if the next level has been loaded
    public bool nxtLevel = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //store the information of the game object that collidied with the EndPoint of the level
        GameObject collisionGameObject = collision.gameObject;

        //If the game object name is Player
        if(collisionGameObject.name == "Player")
        {
            //Load next level - called from EnvObject script
            envObject.LoadNextLevel();
            
            //Set variable for next level loaded to be true
            nxtLevel = true;
        }

    }

    //Reset the value of nxtLevel after the next level is loaded
    public bool SetToCurrentLevel()
    {
        //reset variable of level being loaded after loading into the next level
        nxtLevel = false;
        
        //return next level variable value
        return nxtLevel;
    }
}
