using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //object to follow - player
    public GameObject followObject;

    //how far the player can move before the camera moves
    public Vector2 followOffset;

    //boundary box
    private Vector2 threshold;
    public float speed = 3;
    
    private Rigidbody2D rb;

    void Start()
    {
        threshold = calculateThreshold();
        rb = followObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        followPlayer();

    }

    //Takes camera aspect ratio and orthographic size and subtraces the offset to create the 
    //boundary box
    private Vector3 calculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    //draws a gizmo in the editor to see where our boundary box is
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }

    //follows the player based on the boundary box around the player
    private void followPlayer()
    {
        Vector2 follow = followObject.transform.position;
        //distance character is from the center of the x-axis
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        //distance character is from the center of the y-axis
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            //move the camera in the same direction as the follow object
            newPosition.x = follow.x;
        }

        if(Mathf.Abs(yDifference) >= threshold.y)
        {
            //move the camera in the same direction as the follow object
            newPosition.y = follow.y;
        }
        //set the speed to the exact speed that the character is moving
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        //incrementally move towards new position each frame
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
}
