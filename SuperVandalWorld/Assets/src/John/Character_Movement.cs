using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public float speed;
    public float jumpForce;
    public bool hasAbility;
    public static int MAX_JUMPS = 2;
    protected int jumps_taken;
    public static int jumps_allowed;
    protected bool isGrounded = false;
    public float checkGroundRadius;
    public LayerMask groundLayer; //For this to work, all ground needs to be on its own layer
    public string abilityName;
    public KeyCode ability = KeyCode.J;
    public int characterHealth;
    protected SoundManager soundManager;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected bool isFlipped;
    public Sprite[] spriteArray;
    protected bool recentlyDamaged = false;
    protected int invulnerableTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        hasAbility = false;
        jumps_taken = 0;
        jumps_allowed = 1;
        abilityName = string.Empty;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isFlipped = false;
        spriteRenderer.sprite = spriteArray[0];
        characterHealth = 1;
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (!CheckIfGrounded() && spriteRenderer.sprite != spriteArray[1]) //if player is not grounded and current sprite is not jump sprite
        {
            spriteRenderer.sprite = spriteArray[1]; //set sprite to jump sprite
        }
        else if (CheckIfGrounded() && spriteRenderer.sprite == spriteArray[1]) //if player is grounded and current sprite is jump sprite
        {
            spriteRenderer.sprite = spriteArray[0];
        }

        if (recentlyDamaged)
        {
            invulnerableTimer++;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); //set sprite partially transparent
            if (invulnerableTimer >= 200)
            {
                recentlyDamaged = false;
                spriteRenderer.color = new Color(1, 1, 1, 1); //sprite now back to regular transparancy
            }
        }
    }

    public virtual void Move() { }
    /*public virtual void Move()
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
    }*/

    public void Jump(bool is_grounded)
    {
        if (is_grounded || jumps_taken < jumps_allowed)
        {
            Debug.Log("Jumping");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            soundManager.PlaySound("Jump");
            jumps_taken++;
        }
        else
        {
            Debug.Log("Jump pressed, but no jumps remaining.");
        }
    }

    protected bool CheckIfGrounded()
    {
        RaycastHit2D RCH2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, .1f, groundLayer);
        return RCH2D.collider != null; //null if not grounded; not null if grounded
    }

    public void IncreaseMaxJumps()
    {
        if (jumps_allowed + 1 > MAX_JUMPS)
        {
            return;
        }

        jumps_allowed++;
    }

    public void takeDamage(int damageTaken)
    {
        if (recentlyDamaged == true) return; //do nothing if the character was recently damaged
        characterHealth -= damageTaken;
        if (characterHealth <= 0)
        {
            characterDie();
            return;
        }
        invulnerableTimer = 0;
        recentlyDamaged = true;
        soundManager.PlaySound("PlayerHit");
    }

    public void characterDie()
    {
        soundManager.PlaySound("PlayerDeath");
        //insert death sprite here
    }
}