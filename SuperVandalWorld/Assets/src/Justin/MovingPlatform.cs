using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovingPlatform : EnvObject
{
    //Reference to the platform
    public GameObject platform;

    //Array of points for the platform to move between
    public Transform[] movePoints;

    //current point of the platform
    private Transform startPoint;

    //the point platform moves towards
    public int endPoint;

    //variable in editor for speed
    public float setSpeed= 5f;

    //speed of the platform
    private float moveSpeed;

    public static event Action<Collision2D, string, GameObject> objectCollisionNotification = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        //set start point to end point to move towards
        startPoint = movePoints[endPoint];
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get platform speed from editor
        SetSpeed(setSpeed);

        //move platform from start point to end point
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, startPoint.position, moveSpeed *Time.deltaTime);
        
        //if platfrom reached end point, increment array
        if(platform.transform.position == startPoint.position)
        {
            endPoint++;

            //if at the end of the array, set pointSelect to beginning of array
            if(endPoint == movePoints.Length)
            {
                endPoint = 0;
            }

            //set current point to array position
            startPoint = movePoints[endPoint];
        }

    }

    //Dynamically bound function - overrides the parents statically bound function
     public override void OnCollisionEnter2D(Collision2D collision)
    {
            /*Set Player and Camera transforms to be children of the platform, so they move
            smoothly*/
            //collision.collider.transform.SetParent(transform); 
            //GameObject.Find("Main Camera").transform.SetParent(transform);

            GameObject currentPlatform = this.gameObject;

            if(collision.collider.tag == "Player")
            {
                objectCollisionNotification(collision, "Enter", currentPlatform);
            }           
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
            /*When the Player is no longer on the platform, the Player and the camera's 
            transforms should no longer be children of the platform*/
            //collision.collider.transform.SetParent(null);
           // GameObject.Find("Main Camera").transform.SetParent(null);
           GameObject currentPlatform = this.gameObject;
           objectCollisionNotification(collision, "Exit", currentPlatform);
    }

    //Function to set the platform's speed from the editor
    public float SetSpeed(float setSpeed)
    {
        moveSpeed = setSpeed;
        return moveSpeed;
    }

}
