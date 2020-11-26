using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeProjectile : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Axe collided with " + col.collider.name);
        Destroy(this.gameObject);
    }
}
