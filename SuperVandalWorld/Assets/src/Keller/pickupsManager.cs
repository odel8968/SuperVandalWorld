using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{    
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Default constructor called");
    }
    
    public static void removeAsset(Collider2D other)
    {
        //remove asset from level via destroy function
        Destroy(other.gameObject);
    }
}