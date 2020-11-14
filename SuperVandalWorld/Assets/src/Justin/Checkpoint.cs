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
        cm = GameObject.FindGameObjectWithTag("CM").GetComponent<CheckpointManager>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    //call when player collides with checkpoint
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cm.lastCheckPointPos = transform.position;
            anim.enabled = true;
            Invoke("DisableAnimation", 1.5f * Time.deltaTime);
            coll.enabled = false;
        }
    }

    void DisableAnimation()
    {
        anim.enabled = false;
    }
}
