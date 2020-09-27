using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D PlayerRb;
    
    [SerializeField]
    private float movementSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        HandleMovement(horizontal);
    }

    private void HandleMovement(float horizontal)
    {

        PlayerRb.velocity = new Vector2(horizontal * movementSpeed, PlayerRb.velocity.y);
    }
}
