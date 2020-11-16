using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : EnvObject 
{
    //Reference to Platform RigidBody
    Rigidbody2D platformRB;

    //Reference to Platform Collider
    Collider2D platColl;

    //Initial position of Platform
    Vector2 startPos;

    //Platform Object type
    GameObject platformType;

    //Variable for delay between collision and platform falling
    public float fallDelay = 1f;

    //Variable for delay between platform falling and platform respawning
    public float respawnDelay = 3f;

    //check for platform respawn
    public bool respawns = true;

    //Array to hold elements of a bridge
    public GameObject[] ropeBridge;

    // Start is called before the first frame update
    void Start()
    {
        //Get starting position of platform
        startPos = transform.position;

        //Get RB of platform
        platformRB = GetComponent<Rigidbody2D>();

        //Get Collider of platform
        platColl = GetComponent<Collider2D>();

        //Get the type of game object
        platformType = this.gameObject;
    }

    // Update is called once per frame
    public override void OnCollisionEnter2D(Collision2D collision)
    {

        if(platformType.tag == "Platform")
        {
            //Drop platform if player collides with platform
            //Respawn platform after a delay
            if(collision.gameObject.name.Equals("Player"))
            {
                Invoke("DropPlatform", fallDelay);
                Invoke("RespawnPlatform", respawnDelay);
            }
        }

        else if (platformType.tag == "RopeLogBridge")
        {
            //Drop bridge if the player collides with the bridge
            if(collision.gameObject.name.Equals("Player"))
            {
                Invoke("DropBridge", fallDelay);                
            }
        }
    }

    void DropPlatform()
    {
        //Drop platform by disabling kinematic rb
        platformRB.isKinematic = false;
        //Set Collider to trigger so it falls through environment
        platColl.isTrigger = true;
    }

    //Drop platform by disabling kinematic tb
    void DropBridge()
    {
        platformRB.isKinematic = false;
    }

    //respawn platform at it's positon, re-enable rb and collider
    void RespawnPlatform()
    {
        platformRB.isKinematic = true;
        platColl.isTrigger = false;
        platformRB.velocity = new Vector3(0,0,0);
        platformRB.position = startPos;

    }
}
