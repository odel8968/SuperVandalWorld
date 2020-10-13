using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : EnvObject 
{
    Rigidbody2D platformRB;

    Collider2D platColl;

    Vector2 startPos;

    public float fallDelay = 1f;
    public float respawnDelay = 3f;

    public bool respawns = true;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        //Get RB of platform
        platformRB = GetComponent<Rigidbody2D>();
        //Get Collider of platform
        platColl = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        //Drop platform if player collides with platform
        //Respawn platform after a delay
        if(collision.gameObject.name.Equals("Player"))
        {
            Invoke("DropPlatform", fallDelay);
            Invoke("RespawnPlatform", respawnDelay);
        }
    }

    void DropPlatform()
    {
        //Drop platform by disabling kinematic rb
        platformRB.isKinematic = false;
        //Set Collider to trigger so it falls through environment
        platColl.isTrigger = true;
    }

    void RespawnPlatform()
    {
        //respawn platform at it's positon, re-enable rb and collider
        platformRB.isKinematic = true;
        platColl.isTrigger = false;
        platformRB.velocity = new Vector3(0,0,0);
        platformRB.position = startPos;

    }
}
