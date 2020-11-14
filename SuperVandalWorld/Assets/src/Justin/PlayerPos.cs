using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    //reference to checkpoint manager
    private CheckpointManager cm;
    void Start()
    {
        //Get the checkpointmanager object
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointManager>();

        //player's position equals the position of the last checkpoint the player triggered
        transform.position = cm.lastCheckPointPos;
    }

    void Update()
    {
        //test checkpoints - remove for final build
        if(Input.GetKeyDown("y"))
        {
           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
