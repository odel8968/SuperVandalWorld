using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    // Choose the enemy and player ridgid body
    private Rigidbody2D rb;
    public GameObject player;

    Renderer rnr;                       // TODO: Fix to move enemy when player is near 
    Vector3 move = Vector3.zero;        // Variable to change x-axis value

    // Choose enemy speed 
    [Range(1,10)]
    public float enemySpeed = 2.5f;

    // Set position of enemy at launch
    public Vector2 initPos;
    void Start() {
        initPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rnr = GetComponent<Renderer>();
    }

    // Track and follow player location
    void Update() {
        // Check if the enemy is visibly on the scene
        if (rnr.isVisible) {
            // Debug.Log("The enemy is visible!!!");
            enemyMovement();
        } else {
            // Debug.Log("The enemy is not visible.");
            move.x = 0;
            transform.position += move * Time.deltaTime;
        }
    }

    void enemyMovement() {

        // Find the difference in location of the player and the enemy
        float diff = player.transform.position.x - transform.position.x;

        // If the difference is greater than zero move right
        if (diff > 0) {
            move.x = enemySpeed * Mathf.Min(diff, 1.0f);
        }

        // If the difference is less than zero move left
        if (diff < 0) {
            move.x = -(enemySpeed * Mathf.Min(-diff, 1.0f));
        }

        // Update position of enemy
        transform.position += move * Time.deltaTime;
    }
}