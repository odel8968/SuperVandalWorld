using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Movement : Character_Movement
{
    private int seed;
    private int timer = 0;
    private bool left = false;

    public override void Move()
    {
        if (!left)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        
        timer++;
        if (timer > 100)
        {
            left = !left;
            timer = 0;
        }

        isGrounded = CheckIfGrounded();
        if (isGrounded)
        {
            jumps_taken = 0;
            Jump(isGrounded);
        }

        if (Input.anyKey)
        {
            Destroy(this.gameObject);
        }
    }
}
