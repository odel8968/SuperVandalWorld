using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed;    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVelocity;
    public float curTime;
    public float moveTime = 3;
    public float direction = 1;

    Player_Movement playerMovement;    //control the movement
    bool playerAlive;
    public float restartDelay = 0.5f;

    public float attackDistance = 5; // distance at which the enemy starts attacking
    public float attackForce = 100; // the force of the attack
    public float timeBetweenAttacks = 4; // the time it takes to attack again (in seconds)
    private float curAttackTime; // variable to keep track of when to attack next

    PauseMenu menuScreen;
    bool bcMode;

    // Property to get the state of the player (alive or dead)
    public bool PlayerAlive { get { return playerAlive;} set { playerAlive = value;}}

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        animator = GetComponent<Animator>();
        playerAlive = true;
        playerMovement = FindObjectOfType<Player_Movement>();
        menuScreen = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    public void Update()
    {
        // Calculate the distance between this enemy and the player
        float dist = Mathf.Abs(playerMovement.transform.position.x - transform.position.x);
        if(dist <= attackDistance) // and if it's less than the attack distance, we see if we can attack
        {
            curAttackTime += Time.deltaTime;
            // if the time from the last attack is greater than the time between attacks then we can attack
            if(curAttackTime >= timeBetweenAttacks)
            {
                //GameObject obj = GameObject.Instantiate(throwableObject, transform.position, Quaternion.identity);
                // Grab throwable object from our pool singleton class
                GameObject obj = ThrowableObjPool.Instance.GetThrowableObjectFromPool();
                if(obj != null)
                {
                    // Set position and rotation of the throwable object
                    // position is the position of this enemy, with default rotation (no rotation)
                    obj.transform.position = transform.position;
                    obj.transform.rotation = Quaternion.identity;

                    // Calculate the direction of the movement for the throwable object
                    // This is calculated based on the position of the player and the position of the enemy
                    var forceVector = (playerMovement.transform.position - transform.position).normalized * attackForce;
                    // Apply the force to the throwable object
                    obj.GetComponent<Rigidbody2D>().AddForce(forceVector);
                }

                // resetting the timer for next attack so that we're not attacking all the time
                curAttackTime = 0;
            }

            // don't move while attacking
            moveVelocity.x = 0;
        }
        else // else we're not attacking, we're just moving from one side to the other
        {
            curAttackTime = timeBetweenAttacks;
            curTime += Time.deltaTime;
            if(curTime >= moveTime)
            {
                direction *= -1;
                curTime = 0;
            }
            moveVelocity = new Vector2(direction, 0) * speed;
        }

        // Setting animation property (velocity) with the current velocity value so that the animator knows
        // which state to execute
        animator.SetFloat("velocity", moveVelocity.x);

        bcMode = menuScreen.DrBCMode();
    }

    void FixedUpdate()
    {
        // Move character with physics
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        if (collision.collider.tag == "Projectile")
        {
            Debug.Log("You killed an enemy with a projectile!");

            Destroy(gameObject);
        }

        if (playerAlive == true)
        {
            if (bcMode)
            {
                Physics.IgnoreLayerCollision(0,10);
            }
            else 
            {
                if (collision.collider.tag == "Player")
                {
                    if (direction.y < 0)
                    {
                        Debug.Log("Enemy killed.");
                        playerMovement.enabled = true;
                        score.AddScore(100);
                        Destroy(gameObject);
                    } 
                    else
                    {
                        playerMovement.enabled = false;
    
                        Debug.Log("You Died");
                        playerAlive = false;
                        deathSceneManager.lastActiveScene = SceneManager.GetActiveScene().name;
                        goToKillPlayerScene();
                        Invoke("ReEnablePlayerMovement", restartDelay);
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
        playerMovement.enabled = true;
    }

    void goToKillPlayerScene()
    {
        SceneManager.LoadScene("DeathScene");
    }

}