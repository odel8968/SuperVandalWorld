using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daEnemy : MonoBehaviour 
{

    // Retrieve player movement
    public float restartWait = 0.5f;
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
    public float fallMultiplier = 2.5f;

    PauseMenu easyButton;
    bool easyMode;

    void Start() 
    {
        playerAlive = true;
        playerMvmnt = FindObjectOfType<Player_Movement>();

        someScale = transform.localScale.x; // assuming this is facing right
        initPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        rnr = GetComponent<Renderer>();

        easyButton = FindObjectOfType<PauseMenu>();
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

        // Control falling speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        }

        easyMode = easyButton.DrBCMode();

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

    public void OnTriggerEnter2D(Collider2D collider)
    {
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        if (collider.tag == "Projectile")
        {
            Debug.Log("You killed an enemy with a projectile!");
            score.AddScore(10);

            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        if (collision.collider.tag == "Projectile")
        {
            Debug.Log("You killed an enemy with a projectile!");
            score.AddScore(10);

            Destroy(gameObject);
        }

        if (collision.collider.tag == "EnvHazard")
        {
            Debug.Log("Oops, enemy died from natural causes.");
            enemySpeed = 0;
            Destroy(gameObject);
        }

        if (playerAlive == true)
        {
            if (collision.collider.tag == "Player")
            {
                if (easyMode)
                {
                    Physics.IgnoreLayerCollision(0,10);
                }
                else
                {    // see if the obect is futher left/right or top/bottom
                    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    {

                        if(direction.x > 0)
                        {
                            playerMvmnt.enabled = false;
                            enemySpeed = 0;

                            Debug.Log("You died by an enemy on your right.");
                            
                            playerAlive = false;

                            deathSceneManager.lastActiveScene = SceneManager.GetActiveScene().name;
                            goToKillPlayerScene();
                            Invoke("ReEnablePlayerMovement", restartWait);
                        }
                        else
                        {
                            playerMvmnt.enabled = false;
                            enemySpeed = 0;

                            Debug.Log("You died by an enemy on your left.");
                            
                            playerAlive = false;

                            deathSceneManager.lastActiveScene = SceneManager.GetActiveScene().name;
                            goToKillPlayerScene();
                            Invoke("ReEnablePlayerMovement", restartWait);
                        }
                    
                    }
                    else
                    {

                        if(direction.y > 0)
                        {
                            playerMvmnt.enabled = false;
                            enemySpeed = 0;

                            Debug.Log("You died by an enemy falling on you.");
                            
                            playerAlive = false;

                            deathSceneManager.lastActiveScene = SceneManager.GetActiveScene().name;
                            goToKillPlayerScene();
                            Invoke("ReEnablePlayerMovement", restartWait);
                        }
                        else
                        {
                            Debug.Log("You killed an enemy.");
                            
                            enemySpeed = 0;
                            playerMvmnt.enabled = true;
                            score.AddScore(100);

                            Destroy(gameObject);
                        }
                    }
                }
            }
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void ReEnablePlayerMovement()
    {
        playerMvmnt.enabled = true;
    }

    void goToKillPlayerScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}