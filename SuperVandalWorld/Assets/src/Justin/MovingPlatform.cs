using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject platform;
    public Transform[] movePoints;

    //current point of the platform
    private Transform startPoint;

    //which point platform moves towards
    public int endPoint;
    public float moveSpeed = 5f;

    private Rigidbody2D pointB;
    private Rigidbody2D pointA;

    private Rigidbody2D MovingPlatform1;

    // Start is called before the first frame update
    void Start()
    {
        //set start point to end point to move towards
        MovingPlatform1 = GetComponent<Rigidbody2D>();
        startPoint = movePoints[endPoint];
        pointB = GetComponent<Rigidbody2D>();
        pointA = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

}
