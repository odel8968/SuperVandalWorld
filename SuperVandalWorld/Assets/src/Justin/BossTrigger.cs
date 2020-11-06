using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    CameraFollow camClamp;

    GameObject[] arena;

    void Start()
    {
        camClamp = FindObjectOfType<CameraFollow>();
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if(collisionGameObject.name == "Player")
        {
            camClamp.BossCamera();
        }

        arena = GameObject.FindGameObjectsWithTag("BossArena");
        foreach(GameObject g in arena)
        {
            Collider2D wallCollider = g.GetComponent<Collider2D>();
            wallCollider.isTrigger = false;
        }

    }
}
