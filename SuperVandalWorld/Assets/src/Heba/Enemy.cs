﻿using System.Collections;
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

    Character_Movement playerMovement;
    bool playerAlive;
    public float restartDelay = 2f;
        

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
       animator = GetComponent<Animator>();
        playerAlive = true;
        playerMovement = FindObjectOfType<Character_Movement>();
    }

    // Update is called once per frame
    public void Update()
    {
        curTime += Time.deltaTime;
        if(curTime >= moveTime)
        {
            direction *= -1;
            curTime = 0;
        }
        moveVelocity = new Vector2(direction, 0) * speed;
        //animator.SetFloat("velocity", moveVelocity.x);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerAlive == true)
        {
            if (collision.collider.tag == "Player")
            {
                playerMovement.enabled = false;

                Debug.Log("You Died");
                playerAlive = false;
                Invoke("ResetLevel", restartDelay);
                Invoke("ReEnablePlayerMovement", restartDelay);
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
}