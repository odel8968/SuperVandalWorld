using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Movement : Character_Movement
{
    private int seed;
    private int timer = 0;
    private bool left = false;
    private bool first_iteration = true;
    public Player_Movement Player;

    public override void Move()
    {
        if (first_iteration) 
        {
            Player = GameObject.Find("Player").GetComponent<Player_Movement>();
            Player.gameObject.SetActive(false);
            first_iteration = false;
        }

        if (!left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        
        timer++;
        /*if (timer > 100)
        {
            left = !left;
            timer = 0;
        }*/

        isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            jumps_taken = 0;
            Jump(isGrounded);
        }

        if (Input.anyKey)
        {
            Player.gameObject.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
