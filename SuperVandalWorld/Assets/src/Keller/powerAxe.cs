using System.Net.Sockets;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerAxe : MonoBehaviour
{
    private float projVel = 8.0f;       //projectile velocity
    private float projDelay = .5f;     //delay between projectile fires
    private float projNextTime = 0.0f;  //next time projectile can be fired
    private float projForce = 1.5f;
    private bool playerFlipped;
    private int dmg = 1;
    public GameObject axeProj;
    Character_Movement player;

    //method called when enabled
    void OnEnable()
    {
        //find player
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
        //disable multiJump script
        GameObject.Find("Player").GetComponent<multiJump>().enabled = false;
    }

    //add force to axe by passing in object and direction
    public void addAxeForce(GameObject axe, float direction)
    {
        axe.GetComponent<Rigidbody2D>().velocity = new Vector2(direction, projForce * projVel);
    }

    //Destroy projectile after 5 seconds if collision has not occurred
    public void removeProjectile(GameObject axe)
    {
        Destroy(axe, 5.0f);
    }

    void Update()
    {
        //check if player is flipped. Player facing left is false, right is true
        playerFlipped = checkPlayerFlipped(playerFlipped);

        if(Input.GetKeyDown(player.ability))
        {
            Debug.Log(playerFlipped);
            throwAxe(playerFlipped);    
        }   
    }

    public void throwAxe(bool flipped)
    {
        GameObject proj = null;

        if(proj == null)
        {
            if(Time.time >= projNextTime)
            {
                proj = GameObject.Instantiate(axeProj, transform.position, transform.rotation);
                Physics2D.IgnoreCollision(proj.GetComponent<Collider2D>(), GameObject.Find("Player").GetComponent<Collider2D>(), true);

                switch (flipped)
                {
                    //if player is flipped
                    case true:
                    {
                        addAxeForce(proj, -projVel);
                    }
                    break;

                    //if player is not flipped
                    case false:
                    {
                        addAxeForce(proj, projVel);
                    }
                    break;
                }
                removeProjectile(proj);
                projNextTime = Time.time + projDelay;
            }
        }
    }

    private bool checkPlayerFlipped(bool flipped)
    {
        bool isFlipped = flipped;

        float x = Input.GetAxisRaw("Horizontal");
        
        //if player is facing left on update and was facing right before
        if (x < 0 && !isFlipped) 
        {
            isFlipped = true;
        }
        //if player is facing right on update and was facing left before
        if (x > 0 && isFlipped)
        {
            isFlipped = false;
        }
        
        //default returns bool passed into method
        return isFlipped;
    }
}
