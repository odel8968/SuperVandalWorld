using System.Net.Sockets;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerAxe : MonoBehaviour
{
    private float projVel = 8.0f;       //projectile velocity
    private float projDelay = 1.0f;     //delay between projectile fires
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

    public void removeProjectile(GameObject axe)
    {
        Destroy(axe, 5.0f);
    }

    void Update()
    {
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

                switch(flipped)
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
        
        if (x < 0 && !isFlipped) 
        {
            isFlipped = true;
        }

        if (x > 0 && isFlipped)
        {
            isFlipped = false;
        }
        
        return isFlipped;
    }
}
