using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daEnemy : MonoBehaviour 
{

    // Retrieve player movement
    public float restartWait = 1f;
    Player_Movement playerMvmnt;
    protected bool playerAlive = false;

    // Choose the enemy and player ridgid body
    private Rigidbody2D rb;
    private GameObject player;

    Renderer rnr;                       // TODO: Fix to move enemy when player is near 
    Vector3 move = Vector3.zero;        // Variable to change x-axis value

    // Choose enemy speed 
    [Range(1,10)]
    public float enemySpeed = 4.0f;

    // Set position of enemy at launch
    [HideInInspector]
    public Vector2 initPos;

    float someScale;
//hs
    void Start() 
    {
        playerAlive = true;
        playerMvmnt = FindObjectOfType<Player_Movement>();

        someScale = transform.localScale.x; // assuming this is facing right
        initPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        rnr = GetComponent<Renderer>();
    }

    // Track and follow player location
    void Update() 
    {
        // Check if the enemy is visibly on the scene
        if (rnr.isVisible) 
        {
            // Debug.Log("The enemy is visible!!!");
            enemyMovement();
        } 
        else 
        {
            // Debug.Log("The enemy is not visible.");
            move.x = 0;
            transform.position += move * Time.deltaTime;
        }
    }

    void enemyMovement() 
    {

        // Find the difference in location of the player and the enemy
        float diff = player.transform.position.x - transform.position.x;

        // If the difference is greater than zero move right
        if (diff > 0) 
        {
            move.x = enemySpeed * Mathf.Min(diff, 1.0f);
            transform.localScale = new Vector2(someScale, transform.localScale.y);
        }

        // If the difference is less than zero move left
        if (diff < 0) 
        {
            move.x = -(enemySpeed * Mathf.Min(-diff, 1.0f));
            transform.localScale = new Vector2(-someScale, transform.localScale.y);
        }

        // Update position of enemy
        transform.position += move * Time.deltaTime;
    }

    // public void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (playerAlive == true)
    //     {
    //         if (collision.collider.tag == "Player")
    //         {
    //             playerMvmnt.enabled = false;
    //             enemySpeed = 0;
    //         }

    //         Debug.Log("You died by an enemy.");
            
    //         playerAlive = false;

    //         Invoke("ResetLevel", restartWait);
    //         Invoke("ReEnablePlayerMovement", restartWait);
    //     }
    // }

    // void ResetLevel()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }

    // void ReEnablePlayerMovement()
    // {
    //     playerMvmnt.enabled = true;
    // }
}