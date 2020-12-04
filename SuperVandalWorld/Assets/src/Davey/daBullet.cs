using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class daBullet : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float speed = 500f;

    public float restartWait = 1f;

    Player_Movement playerMvmnt;
    Character_Movement characterMvmnt;

    PauseMenu easyButton;
    bool easyMode;
        
    void Start()
    {
        playerMvmnt = FindObjectOfType<Player_Movement>();
        characterMvmnt = FindObjectOfType<Player_Movement>();
        easyButton = FindObjectOfType<PauseMenu>();
    }

    void Update() {
        easyMode = easyButton.DrBCMode();
    }
            
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Transform target = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 bulletAccuracy = new Vector3(Random.Range(0,6f), Random.Range(0,6f), 0);
        Vector2 direction = (target.position - transform.position) + bulletAccuracy;
        rb.AddForce(direction * speed * Time.deltaTime);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (easyMode)
            {
                Destroy(gameObject);
                
                Physics.IgnoreLayerCollision(0,10);
            }
            else
            {
                Debug.Log("Hit!");
                // characterMvmnt.characterHealth -= 1;
                if (characterMvmnt.characterHealth == 0) 
                {
                    playerMvmnt.enabled = false;

                    Debug.Log("You died by the boss!");
                    
                    Invoke("ResetLevel", restartWait);
                    Invoke("ReEnablePlayerMovement", restartWait);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject, 2f);
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