using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class multiJump : MonoBehaviour
{
    
    Character_Movement player;
    public Rigidbody2D rb;
    private bool grounded;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
        GameObject.Find("Player").GetComponent<multiJump>().enabled = false;
    }

    //called upon enable from another script
    private void OnEnable()
    {
        Debug.Log("multiJump enabled");
        //disable powerAxe powerup if enabled. 
        GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;

        //change max jumps of player 2, allowing for a second jump in mid air
        Character_Movement.jumps_allowed = 2;
    }
    
    //revert number of jumps to 1 when disabled
    private void OnDisable()
    {
        Character_Movement.jumps_allowed = 1;
    }
}

