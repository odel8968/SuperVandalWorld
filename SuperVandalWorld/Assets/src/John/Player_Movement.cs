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
            animator.enabled = true; //activates the walk animation
        }
        else
        {
            animator.enabled = false; //deactivates the walk animation if its playing
            spriteRenderer.sprite = spriteArray[0]; //standing still sprite
        }
        if (x < 0 && !isFlipped) //if character is moving left 
        {
            if (spriteRenderer != null)
            {
                isFlipped = true;
                spriteRenderer.flipX = true; //sprite will now face left
            }
        }
        if (x > 0 && isFlipped) //if character is moving right
        {
            if (spriteRenderer != null)
            {
                isFlipped = false;
                spriteRenderer.flipX = false; //sprite will now face right
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
