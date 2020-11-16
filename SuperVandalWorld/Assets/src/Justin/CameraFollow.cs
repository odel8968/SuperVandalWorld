using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Object to follow
    public Transform player;

    Vector3 targetPos;

    //zeroes out the velocity
    Vector3 velocity = Vector3.zero;

    //Time to follow the player
    public float smoothTime = .15f;

    //enable and set the maximum Y value
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;

    //enable and set the min Y value
    public bool yMinEnabled = false;
    public float yMinValue = 0;
    
    //enable and set the max X value
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;

    //enable and set the min X value
    public bool xMinEnabled = false;
    public float xMinValue = 0;

    void FixedUpdate()
    {
        //find player object transform to follow
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Get target position
        targetPos = player.position;

        //vertical clamp values that adjust the camera's Y-axis view
        if(yMinEnabled && yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(player.position.y, yMinValue, yMaxValue);
        }

        else if(yMinEnabled)
        {
            targetPos.y = Mathf.Clamp(player.position.y, yMinValue, player.position.y);
        }

        else if(yMaxEnabled)
        {
            targetPos.y = Mathf.Clamp(player.position.y, player.position.y, yMaxValue);
        }

        //horizontal clamp values that adjust the camera's X-axis view
        if(xMinEnabled && xMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(player.position.x, xMinValue, xMaxValue);
        }

        else if(yMinEnabled)
        {
            targetPos.x = Mathf.Clamp(player.position.x, xMinValue, player.position.x);
        }

        else if(yMaxEnabled)
        {
            targetPos.x = Mathf.Clamp(player.position.x, player.position.x, xMaxValue);
        }

        //Align the camera and the player z position
        targetPos.z = transform.position.z;

        //using smooth damp to gradually change the camera transfrom position to the player position based on camera transform velocity and smooth time
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        
    }

    //Function called that adjusted the clamp values when the player enters the BossArena
    public void BossCamera()
    {
        xMinValue = xMaxValue;
        targetPos.x = Mathf.Clamp(player.position.x, xMinValue, xMaxValue);
    }


    /*public GameObject followObject;
    public Vector2 followOffset;
    public float speed = 3;
    private Vector2 threshold;
    private Rigidbody2D rb;

    void Start()
    {
        
        threshold = calculateThreshold();
        
    }

    void FixedUpdate()
    {
        if(!followObject)
        {
            followObject = GameObject.FindGameObjectWithTag("Player");
            rb = followObject.GetComponent<Rigidbody2D>();
        }
        followPlayer();
        

    }

    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    private void followPlayer()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }

        if(Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }*/
}
