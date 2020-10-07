using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    // Choose the enemy and player ridgid body
    public Rigidbody2D rb;
    public GameObject player;

    Renderer rnr;                       // TODO: Fix to move enemy when player is near 
    Vector3 move = Vector3.zero;        // Variable to change x-axis value

    // Choose enemy speed 
    [Range(1,10)]
    public float enemySpeed = 2.5f;

    // Set position of enemy at launch
    private Vector2 initPos;
    void Start() {
        initPos = transform.position;
        rnr = GetComponent<Renderer>();
    }

    // Track and follow player location
    void Update() {

        // Check if the enemy is visibly on the scene
        // If the enemy is visible move
        // Else do nothing
        if (rnr.isVisible) {
            //Debug.Log("The enemy is visible!!!");

            // Find the difference in location of the player and the enemy
            float diff = player.transform.position.x - transform.position.x;

            // If the difference is greater than zero move up
            if (diff > 0) {
                move.x = enemySpeed * Mathf.Min(diff, 1.0f);
            }

            // If the difference is less than zero move down
            if (diff < 0) {
                move.x = -(enemySpeed * Mathf.Min(-diff, 1.0f));
            }
            
            // Update position of enemy
            transform.position += move * Time.deltaTime;
        } else {
            transform.position = initPos;
            //Debug.Log("The enemy is not visible.");
        }
    }
}