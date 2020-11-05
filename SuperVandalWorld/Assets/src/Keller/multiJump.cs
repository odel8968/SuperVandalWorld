using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class multiJump : MonoBehaviour
{
    Character_Movement player;
    public Rigidbody2D rb;
    public bool jumpUsed = false;
    private int maxJumps = 1;
    private bool grounded;
    void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
        GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;
    }
    

    void Update()
    {
        if(true)
        {
            jumpUsed = false;
        }
        if(Input.GetKeyDown(player.ability))
        {
            jumpUsed = Jump(jumpUsed);
        }
    }

    public bool Jump(bool jumpUsed)
    {
        //Debug.Log("is_grounded = " + is_grounded + ", jumps_taken = " + jumps_taken + ", jumps_allowed = " + jumps_allowed);
        if (!jumpUsed)
        {
            rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
            return true;
        }
        return true;
    }
}

