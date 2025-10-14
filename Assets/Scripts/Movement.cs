using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour   
{
    private int movX = 0;
    private int movY = 0;
    private Vector2 mov = new Vector2(0, 0);
    [SerializeField] private float speed = 0;
    private float baseSpeed;
    private float multSpeed;
    [SerializeField] private float speedFactor = 1.5f;
    [SerializeField] private float jumpForce = 12f;
    private bool isSprinting;
    private bool isGrounded;
    private float isWalking;

    Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        baseSpeed = speed;
        multSpeed = speed * speedFactor;

        animator = GetComponent<Animator>();
    }

    private void HorizontalMovement()
    {
        // movimiento horizontal
        if (Input.GetKey(KeyCode.D))
        {
            movX = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
            movX = -1;
        }
        else 
        { 
            movX = 0; 
        }
    }

    private void Sprint (float multSpeed)
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        speed = isSprinting ? multSpeed : baseSpeed;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // resetea velocidad vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void detectXMovement()
    {
        isWalking = rb.velocity.x != 0 ? 1 : 0;
        animator.SetFloat("xVelocity", isWalking);
    }

    void Update()
    {   
        // Salto
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        // Movimiento
        detectXMovement();

        Sprint(multSpeed);
        HorizontalMovement();
    }
    private void FixedUpdate()
    {
        mov = new Vector2(movX, movY);
        mov = mov.normalized;
        //rb.AddForce(mov * speed * Time.fixedDeltaTime);
        //rb.velocity = mov * speed * Time.fixedDeltaTime; // en unity 6 ya no funciona -.-
        rb.velocity = new Vector2(mov.x * speed, rb.velocity.y);
    }
}
