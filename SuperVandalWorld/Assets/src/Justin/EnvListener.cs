using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Observer pattern for environment collisions and BC Mode
public class EnvListener : MonoBehaviour
{
    //variable to use between restarting level
    public float restartDelay = 1f;

    //variable to hold reference to Player_Movement script
    Player_Movement playerMovement;
    EnvObject environment;

    SoundManager sounds;

    FallingPlatform fallingPlatform;

    void Start()
    {
        //Find Player_Movement script
        playerMovement = FindObjectOfType<Player_Movement>();

        //find EnvObject script
        environment = FindObjectOfType<EnvObject>();

        //Find sound manager
        sounds = GameObject.Find("SoundManager").GetComponent<SoundManager>();

        //Find FallingPlatform 
        fallingPlatform = FindObjectOfType<FallingPlatform>();
    }

    //on enable add subsribers to listener
    private void OnEnable()
    {
            EnvHazard.objectCollisionNotification += EnvHazard_objectCollisionNotification;
            MovingPlatform.objectCollisionNotification += MovingPlatform_objectCollisionNotification;
            FallingPlatform.objectCollisionNotification += FallingPlatform_objectCollisionNotification;
    }

    //switch statement for listener to determine what to do by message recieved
    private void EnvHazard_objectCollisionNotification(string name)
    {
        switch (name)
        {
                    //Player is killed by a collision
            case "EnvHazard":
                //Disable player movement
                playerMovement.enabled = false;

                //play audio
                sounds.PlaySound("Break");
               
                //print out message to console
                Debug.Log("You Died From a Collision");

                //set playerAlive to false
                environment.playerAlive = false;

                //reset the level after a time of the restart delay
                Invoke("ResetLevel", restartDelay);

                //re-enable the player movement after a time of the restart delay
                Invoke("ReEnablePlayerMovement", restartDelay);
            break;

                //Player is killed by a trigger
            case "WaterHazard":
                //Disable player movement
                playerMovement.enabled = false;

                //play audio
                sounds.PlaySound("WaterSplash");
               
                //print out message to console
                Debug.Log("You Died From Water");

                //set playerAlive to false
                environment.playerAlive = false;

                //reset the level after a time of the restart delay
                Invoke("ResetLevel", restartDelay);

                //re-enable the player movement after a time of the restart delay
                Invoke("ReEnablePlayerMovement", restartDelay);
            break;

                //If in BCMode (EasyMode) player shouldn't die when colliding with objects
            case "BCMODE":
                Debug.Log("Never Die");
            break;
        }
    }

    //based on message recieved from MovingPlatform with collision, string name, and gameobject
    private void MovingPlatform_objectCollisionNotification(Collision2D collision, string name, GameObject gm)
    {
        switch(name)
        {
            /*If message revieved is Enter set object that collided with platform's transform to be
            set as a child of the platforms*/
            case "Enter":
                collision.collider.transform.SetParent(gm.transform);
                GameObject.Find("Main Camera").transform.SetParent(gm.transform);
            break;

            /*If message revieved is Enter un-set object that collided with platform's transform to be
            set as a child of the platforms*/
            case "Exit":
                collision.collider.transform.SetParent(null);
                GameObject.Find("Main Camera").transform.SetParent(null);

            break;
        }
        
    }

    //based on message recieved from MovingPlatform with collision, string name, and gameobject
    private void FallingPlatform_objectCollisionNotification(string name)
    {
        switch(name)
        {
            //play falling sound for falling platform
            case "Falling":
               Invoke("FallingPlatformSound", fallingPlatform.fallDelay);
            break;
                
        }
    }

    //Remove listener subscribers on disable
    private void OnDisable()
    {
        EnvHazard.objectCollisionNotification -= EnvHazard_objectCollisionNotification;
        MovingPlatform.objectCollisionNotification -= MovingPlatform_objectCollisionNotification;
        FallingPlatform.objectCollisionNotification -= FallingPlatform_objectCollisionNotification;
    }

    //Function to reset the level
    void ResetLevel()
    {
        //reload active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Function to re-enable player movement
    void ReEnablePlayerMovement()
    {
        //re-enable the player's movement
        playerMovement.enabled = true;
    }

    void FallingPlatformSound()
    {
        sounds.PlaySound("FallingFP");
    }

}
