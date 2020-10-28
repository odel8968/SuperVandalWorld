using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public float speed;
    public float jumpForce;
    public static bool hasAbility;
    public static int MAX_JUMPS = 2;
    protected int jumps_taken;
    public int jumps_allowed;
    protected bool isGrounded = false;
    public float checkGroundRadius;
    public LayerMask groundLayer; //For this to work, all ground needs to be on its own layer

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        hasAbility = false;
        jumps_taken = 0;
        jumps_allowed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        isGrounded = CheckIfGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = CheckIfGrounded();
            if (isGrounded)
                jumps_taken = 0;
            Jump(isGrounded);
        }
    }

    public void Jump(bool is_grounded)
    {
        //Debug.Log("is_grounded = " + is_grounded + ", jumps_taken = " + jumps_taken + ", jumps_allowed = " + jumps_allowed);
        if (is_grounded || jumps_taken < jumps_allowed)
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumps_taken++;
        }
        else
        {
            Debug.Log("Jump pressed; but not grounded.");
        }
    }

    protected bool CheckIfGrounded()
    {
        RaycastHit2D RCH2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return RCH2D.collider != null; //null if not grounded; not null if grounded
    }

    protected void IncreaseMaxJumps()
    {
        if (jumps_allowed + 1 > MAX_JUMPS)
        {
            return;
        }

        jumps_allowed++;
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            jumps_taken = 0;
        }
    }*/
}