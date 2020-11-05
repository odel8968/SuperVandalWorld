using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daBoss : MonoBehaviour
{
    // Retrieve player movement
    public float restartWait = 1f;
    Player_Movement playerMvmnt;
    protected bool playerAlive = false;

    // Choose the enemy and player ridgid body
    private Rigidbody2D rb;
    private GameObject player;

    Renderer rnr;
    Vector3 move = Vector3.zero;        // Variable to change x-axis value

    // Set position of enemy at launch
    [HideInInspector]
    public Vector2 initPos;

    float someScale;
    public float fallMultiplier = 2.5f;

    void Start() 
    {
        playerAlive = true;
        playerMvmnt = FindObjectOfType<Player_Movement>();

        someScale = transform.localScale.x; // assuming this is facing right
        initPos = transform.position;

        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");

        rnr = GetComponent<Renderer>();
        // transform.localScale = new Vector2(-someScale, transform.localScale.y);

    }

    // Track and follow player location
    void Update() 
    {
        // Check if the enemy is visibly on the scene
        if (rnr.isVisible) 
        {
            // Debug.Log("The enemy is visible!!!");
            bossAttack();
        }

        // Control falling speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier -1) * Time.deltaTime;
        }
    }

    // WORK IN PROGRESS:
    // * Figure how to follow player and shoot
    // * Make bullet asset
    // * Attach muzzle to boss
    public Transform target; //where we want to shoot(player? mouse?)
    public Transform weaponMuzzle; //The empty game object which will be our weapon muzzle to shoot from
    public GameObject bullet; //Your set-up prefab
    public float fireRate = 1000f; //Fire every 3 seconds
    public float shootingPower = 20f; //force of projection
 
    private float shootingTime; //local to store last time we shot so we can make sure its done every 3s
  
    private void bossAttack()
    {
        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + fireRate / 1000; //set the local var. to current time of shooting
            Vector2 myPos = new Vector2(weaponMuzzle.position.x, weaponMuzzle.position.y); //our curr position is where our muzzle points
            GameObject projectile = Instantiate(bullet, myPos, Quaternion.identity); //create our bullet
            Vector2 direction = myPos - (Vector2)target.position; //get the direction to the target
            projectile.GetComponent<Rigidbody2D>().velocity = direction * shootingPower; //shoot the bullet
        }
    }
 
    void Die()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 direction = transform.position - collision.gameObject.transform.position;
        UIManager score = GameObject.Find("Score").GetComponent<UIManager>();

        if (playerAlive == true)
        {
            if (collision.collider.tag == "Player")
            {
                // see if the obect is futher left/right or top/bottom
                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {

                    if(direction.x > 0)
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss on your right.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                    else
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss on your left.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                
                }
                else
                {

                    if(direction.y > 0)
                    {
                        playerMvmnt.enabled = false;

                        Debug.Log("You died by the boss falling on you.");
                        
                        playerAlive = false;

                        Invoke("ResetLevel", restartWait);
                        Invoke("ReEnablePlayerMovement", restartWait);
                    }
                    else
                    {
                        Debug.Log("You killed the boss.");
                        
                        playerMvmnt.enabled = true;
                        score.AddScore(100);

                        Destroy(gameObject);
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
}
