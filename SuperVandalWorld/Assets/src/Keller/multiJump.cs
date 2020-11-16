using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class multiJump : MonoBehaviour
{

    //memento pattern possibly?????????


    
    Character_Movement player;
    public Rigidbody2D rb;
    private bool grounded;

    //function called when enabled by another script
    void OnEnable()
    {
        //disable powerAxe powerup if enabled. 
        GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;

        //change max jumps of player 2, allowing for a second jump in mid air
        player.IncreaseMaxJumps();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
    }

    void OnDisable()
    {
        player.jumps_allowed = 1;
    }
}

