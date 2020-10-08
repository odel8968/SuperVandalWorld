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

    bool isGrounded = false;
    public float checkGroundRadius;
    public LayerMask groundLayer; //For this to work, all ground needs to be on its own layer

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        hasAbility = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = CheckIfGrounded();
            Jump(isGrounded);
        }

    }

    public void Jump(bool is_grounded)
    {
        if (is_grounded)
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else
        {
            Debug.Log("Jump pressed; but not grounded.");
        }
    }

    bool CheckIfGrounded()
    {
        RaycastHit2D RCH2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return RCH2D.collider != null; //null if not grounded; not null if grounded
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("Pickup obtained.");
            Destroy(other.gameObject);

        }
    }*/
}