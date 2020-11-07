using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    public Rigidbody2D rb;
    public float leftPushRange;
    public float rightPushRange;
    public float velocityThreshold;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    public void Push()
    {
        if(transform.rotation.z > 0 && transform.rotation.z < rightPushRange && (rb.angularVelocity > 0) && rb.angularVelocity < velocityThreshold )
        {
            rb.angularVelocity = velocityThreshold;
        }

        else if(transform.rotation.z < 0 && transform.rotation.z < leftPushRange && (rb.angularVelocity < 0) && rb.angularVelocity > velocityThreshold * -1)
        {
            rb.angularVelocity = velocityThreshold * -1;
        }
    }
    
}
