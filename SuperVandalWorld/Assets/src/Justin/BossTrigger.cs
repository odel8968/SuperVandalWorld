using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    //Reference to camera clamp values
    CameraFollow camClamp;

    //array of objects in the Boss Arena
    GameObject[] arena;

    void Start()
    {
        //Get CameraFollow script
        camClamp = FindObjectOfType<CameraFollow>();
    }
    

    //Function called when the player runs into the BossArena trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        //Check to make sure the Player is the object that collided with the trigger
        if(collisionGameObject.name == "Player")
        {
            //Call function from CameraFollow to enable to BossCamera(Changes clamp values)
            camClamp.BossCamera();
        }

        /*Find objects in the Boss Arena and disable the colliders trigger setting so the player
        can't leave the arena*/
        arena = GameObject.FindGameObjectsWithTag("BossArena");
        foreach(GameObject g in arena)
        {
            Collider2D wallCollider = g.GetComponent<Collider2D>();
            wallCollider.isTrigger = false;
        }

    }
}
