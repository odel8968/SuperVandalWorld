using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{   
    public float speed;    
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVelocity;
    public float curTime;
    public float moveTime = 3;
    public float direction = 1;
    // public float curTime { get {return curTime;}}
     //public float direction { get {return direction;}}


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();  
       animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
        curTime += Time.deltaTime;
        if(curTime >= moveTime)
        {
            direction *= -1;
            curTime = 0;
        }
        moveVelocity = new Vector2(direction, 0) * speed;
        animator.SetFloat("velocity", moveVelocity.x);
        
    }

void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    

}
