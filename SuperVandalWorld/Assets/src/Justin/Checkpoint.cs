using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //reference to checkpoint manager
    private CheckpointManager cm;
    private BoxCollider2D coll;
  
    //variable to trigger animation
    private Animator anim;
    void Start()
    {
        //Get checkpoint manager component for reference
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointManager>();
        //Get animator component of checkpoints
        anim = GetComponent<Animator>();
        //Get box collider of checkpoints
        coll = GetComponent<BoxCollider2D>();
    }
    
    //What to do on collision with a checkpoint
    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the object that collided with the cp is the player
        if(other.CompareTag("Player"))
        {
            //Set last check point position in CheckpointManager to player's position 
            cm.lastCheckPointPos = transform.position;

            //Enable animation to play for checkpoint, which only plays one time(set in Unity editor)
            anim.enabled = true;

            //disable checkpoint collider so it can't be hit multiple times
            coll.enabled = false;
        }
    }

}
