using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class powerAxe : PowerUp
{
    private float projVel = 8.0f;       //projectile velocity
    private float projDelay = 1.0f;     //delay between projectile fires
    private float projNextTime = 0.0f;  //next time projectile can be fired
    public GameObject axeProj;
    public KeyCode abilityKey = KeyCode.J;


    public void addAxeForce(GameObject axe)
    {
        axe.GetComponent<Rigidbody2D>().velocity = new Vector2(projVel, 1.5f * projVel);
    }

    public void removeProjectile(GameObject axe)
    {
 
        Destroy(axe, 5.0f);
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        Destroy(col.gameObject);
    }

    void Update()
    {
        GameObject proj = null;

        if(Input.GetKeyDown(abilityKey))
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
