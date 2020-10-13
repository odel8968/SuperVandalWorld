using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : EnvObject
{
    public GameObject platform;
    public Transform[] movePoints;

    //current point of the platform
    private Transform startPoint;

    //which point platform moves towards
    public int endPoint;
    public float setSpeed= 5f;

    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //set start point to end point to move towards
        startPoint = movePoints[endPoint];
        
    }

    // Update is called once per frame
    void Update()
    {
        SetSpeed(setSpeed);

        //move platform from start point to end point
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, startPoint.position, moveSpeed * Time.deltaTime);
        
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

     public override void OnCollisionEnter2D(Collision2D collision)
    {
            collision.collider.transform.SetParent(transform); 
            GameObject.Find("Main Camera").transform.SetParent(transform);           
    }

    public override void OnCollisionExit2D(Collision2D collision)
    {
            collision.collider.transform.SetParent(null);
            GameObject.Find("Main Camera").transform.SetParent(null);
    }

    public float SetSpeed(float setSpeed)
    {
        moveSpeed = setSpeed;
        return moveSpeed;
    }

}
