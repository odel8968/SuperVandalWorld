using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerAxe : MonoBehaviour
{
    private float projVel = 8.0f;       //projectile velocity
    private float projDelay = 1.0f;     //delay between projectile fires
    private float projNextTime = 0.0f;  //next time projectile can be fired
    private int dmg = 1;
    public GameObject axeProj;
    Character_Movement player;
    void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<Character_Movement>(); 
        GameObject.Find("Player").GetComponent<multiJump>().enabled = false;

    }
    public void addAxeForce(GameObject axe)
    {
        axe.GetComponent<Rigidbody2D>().velocity = new Vector2(projVel, 1.5f * projVel);
    }

    public void removeProjectile(GameObject axe)
    {
        Destroy(axe, 5.0f);
    }

    void Update()
    {
        GameObject proj = null;

        if(Input.GetKeyDown(player.ability))
        {
            if(proj == null)
            {
                if(Time.time >= projNextTime)
                {
                    projNextTime = Time.time + projDelay;

                    proj = GameObject.Instantiate(axeProj, transform.position, transform.rotation);
                    addAxeForce(proj);
                    removeProjectile(proj);
                }
            }      
        }   
    }   
}
