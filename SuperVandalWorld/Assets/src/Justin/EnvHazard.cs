using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvHazard : EnvObject
{
    //variable to use between restarting level
    public float restartDelay = 2f;

    //variable to hold reference to Player_Movement script
    Player_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        //Find Player_Movement script
        playerMovement = FindObjectOfType<Player_Movement>();
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
        //check if the player is alive
        if(playerAlive == true)
        {
            //check if the object that collided with the enviroment hazard is the player
            if(collision.collider.tag == "Player")
            {
                //Disable player movement
               playerMovement.enabled = false;
               
               //print out message to console
               Debug.Log("You Died");

               //set playerAlive to false
               playerAlive = false;

               //reset the level after a time of the restart delay
               Invoke("ResetLevel", restartDelay);

               //re-enable the player movement after a time of the restart delay
               Invoke("ReEnablePlayerMovement", restartDelay);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //check if the tag of the object that collided with the trigger is the player
        if(collision.tag == "Player")
        {
            //disable player movement
            playerMovement.enabled = false;

            //print out message to console
            Debug.Log("You Died");

            //set playerAlive to false
            playerAlive = false;

            //reset the level after a time of the restart delay
            Invoke("ResetLevel", restartDelay);

            //re-enable movement after a time of the restart delay
            Invoke("ReEnablePlayerMovement", restartDelay);
        }
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

}
