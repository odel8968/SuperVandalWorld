using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class LevelLoader : MonoBehaviour
{
    //Reference to EnvObject class
    public EnvObject envObject;

    public Player_Movement playerMvmt;

    //Varialbe to see if the next level has been loaded
    public bool nxtLevel = false;

    public GameObject lvlComplete;

    void Start()
    {
        playerMvmt = FindObjectOfType<Player_Movement>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //store the information of the game object that collidied with the EndPoint of the level
        GameObject collisionGameObject = collision.gameObject;

        //If the game object name is Player
        if(collisionGameObject.name == "Player")
        {
            //If player finishes last level, load back to title
            if(SceneManager.GetActiveScene().name == "Level 3")
            {
                SceneManager.LoadScene("GameFinish");
            }

            else
            {
                lvlComplete.SetActive(true);
                playerMvmt.enabled = false;
                //Set variable for next level loaded to be true
                nxtLevel = true;
                Invoke("NextLevel", 2f); 
                playerMvmt.enabled = true;
                
            }
            
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

    void NextLevel()
    {
        //Load next level - called from EnvObject script
        envObject.LoadNextLevel();
        Debug.Log("Next Level Loaded");
    }

}
