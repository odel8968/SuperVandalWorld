using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Character_Movement
{
    public override void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        isGrounded = CheckIfGrounded();
        if (x != 0 && isGrounded)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
            spriteRenderer.sprite = spriteArray[0];
        }
        if (x < 0 && !isFlipped) {
            if (spriteRenderer != null)
            {
                isFlipped = true;
                spriteRenderer.flipX = true;
            }
        }
        if (x > 0 && isFlipped)
        {
            if (spriteRenderer != null)
            {
                isFlipped = false;
                spriteRenderer.flipX = false;
            }
        }
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = CheckIfGrounded();
            if (isGrounded)
                jumps_taken = 0;
            Jump(isGrounded);
        }
    }
}
