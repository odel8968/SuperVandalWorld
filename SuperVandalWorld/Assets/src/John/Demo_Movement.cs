using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo_Movement : Character_Movement
{
    public int seed;
    private int jumpTimer = 0;
    private int leftTimer = 0;
    private bool left = false;
    public Player_Movement Player;
    private float currentXPos;
    private float lastXPos;

    void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Player_Movement>();
        Player.gameObject.SetActive(false);
        seed = Random.Range(0, 2);
        lastXPos = 0;
        Debug.Log("Seed = " + seed);
    }

    public override void Move()
    {
        isGrounded = CheckIfGrounded();
        currentXPos = this.transform.position.x;

        if (seed != 0)
        {
            leftTimer++;
            if (leftTimer > 50)
            {
                if (left)
                {
                    left = false;
                }
                else if (currentXPos == lastXPos)
                {
                    left = true;
                }
                leftTimer = 0;
                lastXPos = currentXPos;
            }
        }

        if (!left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (spriteRenderer != null && isFlipped)
            {
                isFlipped = false;
                spriteRenderer.flipX = false; //sprite will now face right
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (spriteRenderer != null && !isFlipped)
            {
                isFlipped = true;
                spriteRenderer.flipX = true; //sprite will now face left
            }
        }

        if (speed != 0) //walking animation
        {
            if (isGrounded)
            {
                animator.enabled = true;
            }
            else
            {
                animator.enabled = false;
            }
        }
        
        if (isGrounded)
        {
            jumps_taken = 0;
            jumpTimer++;
            if ((seed == 0) || (seed == 1 && jumpTimer > 50)) //in seed 0, character jumps constantly; in seed 1; it waits 50 frames
            {
                Jump(isGrounded);
                jumpTimer = 0;
            }
        }

        if (Input.anyKey) //if a key is pressed; end the demo
        {
            Player.gameObject.SetActive(true);
            Destroy(this.gameObject);
            SceneManager.LoadScene(sceneName:"TitleScene");
        }
    }
}
