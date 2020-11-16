using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    //Reference to rigidbody of object
    public Rigidbody2D rb;

    //Maximum range to the left
    public float leftPushRange;

    //Maximum range to the right
    public float rightPushRange;

    //Maximum velocity
    public float velocityThreshold;



    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody component and set velocity threshold hold
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        //Push the swing once per frame
        Push();
    }

    public void Push()
    {
        //Pushes the swing to the right if the rotation is greater than zero and less then the rightPushRange, and the angularVelocity is greater than zero but less than the velocity threshold
        if(transform.rotation.z > 0 && transform.rotation.z < rightPushRange && (rb.angularVelocity > 0) && rb.angularVelocity < velocityThreshold )
        {
            rb.angularVelocity = velocityThreshold;
        }

        //Pushes the swing to the left if the rotation is less than zero and less then the leftPushRange, and the angularVelocity is less than zero but greater than the velocity threshold * -1
        else if(transform.rotation.z < 0 && transform.rotation.z < leftPushRange && (rb.angularVelocity < 0) && rb.angularVelocity > velocityThreshold * -1)
        {
            rb.angularVelocity = velocityThreshold * -1;
        }
    }
    
}
