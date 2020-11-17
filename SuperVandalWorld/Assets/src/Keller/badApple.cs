using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badApple : MonoBehaviour
{
    public int healthChange = -1;
    Character_Movement player;
    private float delay = 2f;
    private Rigidbody2D rb;
    private float appleSpeed = 3.0f;
    private float gravity = 3.0f;
    private float location; 
    Vector3 force = Vector3.zero;
    Renderer render;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>();
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<Renderer>();
        location = this.transform.localScale.x;
    }

    void Update()
    {
        if(render.isVisible)
        {
            moveApple();
        }
        else
        {
            force.x = 0;
            transform.position += force * Time.deltaTime;
        }

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (gravity -1) * Time.deltaTime;
        }
    }

    private void moveApple()
    {
        float distance = player.transform.position.x - this.transform.position.x;
        if(distance > 0)
        {
            force.x = appleSpeed * Mathf.Min(distance, 1.5f);
            this.transform.localScale = new Vector2(location, this.transform.localScale.y);
        }
        else if(distance < 0)
        {
            force.x = -1 * (appleSpeed * Mathf.Min(distance, 1.5f));
            this.transform.localScale = new Vector2(-location, this.transform.localScale.y);
        }

        this.transform.position += force * Time.deltaTime;
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
