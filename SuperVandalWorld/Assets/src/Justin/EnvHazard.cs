using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvHazard : EnvObject
{

    int playerAlive;
    public float restartDelay = 2f;

    public Character_Movement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerAlive = 1;
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if(playerAlive == 1)
        {
            if(collision.collider.tag == "Player")
            {
                playerMovement.enabled = false;
            }

            Debug.Log("You Died");
            playerAlive = 0;
            Invoke("ResetLevel", restartDelay);
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerAlive = 1;
    }
}
