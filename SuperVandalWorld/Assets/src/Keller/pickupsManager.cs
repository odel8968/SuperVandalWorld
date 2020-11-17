using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupsManager : MonoBehaviour
{    
    public int scoreValue;

    void Start()
    {
        //set both powerups to false on start
        GameObject.Find("Player").GetComponent<multiJump>().enabled = false;
        GameObject.Find("Player").GetComponent<powerAxe>().enabled = false;
    }
    
    //default method to be overriden in child classes
    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Default constructor called");
    }
    
    public static void removeAsset(GameObject gObject)
    {
        //remove asset from level via destroy function
        Destroy(gObject);
    }
    
}