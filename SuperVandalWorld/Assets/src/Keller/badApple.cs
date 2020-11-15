using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badApple : MonoBehaviour
{
    public int healthChange = -1;
    Character_Movement player;
    private float delay = 2f;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
        var apple = col.gameObject.GetComponent<badApple>();

        //check if player has a powerup
        if(GameObject.Find("Player").GetComponent<powerAxe>().enabled == false && 
            GameObject.Find("Player").GetComponent<multiJump>().enabled == false)
        {
                //if player does not have power up, restart from checkpoint or beginning 
                player.enabled = false;
                Invoke("ResetLevel", delay);
                Invoke("ReEnablePlayerMovement", delay);
        }
        else
        {
            //disable all powerup scripts on polayer
            GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;
            GameObject.Find("Player").GetComponent<multiJump>().enabled = false;
        }
        

        Destroy(apple);
    }
}
