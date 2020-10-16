using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvHazard : EnvObject
{
    public float restartDelay = 2f;

    public Character_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerAlive = true;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(playerAlive == true)
        {
            if(collision.collider.tag == "Player")
            {
                playerMovement.enabled = false;
            }

            Debug.Log("You Died");
            playerAlive = false;
            Invoke("ResetLevel", restartDelay);
        }
    }

}
