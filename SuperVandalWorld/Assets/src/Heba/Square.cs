using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveVelocity;

    private float curTime;
    public float moveTime = 3;
    private float direction = 1;

    #region Properties
    public float CurTime { get { return curTime; } }
    public float Direction { get { return direction; } }
    #endregion

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

        float absSpeed = Mathf.Abs(moveVelocity.x);
        animator.SetFloat("velocity", absSpeed);
    }

void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
    

}
