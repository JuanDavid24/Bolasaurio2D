using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour   
{
    private int movX = 0;
    private Vector2 mov = new Vector2(0, 0);
    [SerializeField] private float speed = 0;
    private float baseSpeed;
    private float multSpeed;
    [SerializeField] private float speedFactor = 1.5f;
    [SerializeField] private float jumpForce = 12f;
    private bool isSprinting;
    private bool isGrounded;
    private float isWalking;
    bool isTouchingWall;

    [Header("Wall Check")]
    [SerializeField] private Transform wallCheckBottom;
    [SerializeField] private Transform wallCheckTop;
    [SerializeField] private float wallCheckDistance = 0.3f;

    public int potions = 0;

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

    private void FlipSprite() 
    {
        if (movX != 0) 
        {
            transform.localScale = new Vector3(movX, 1, 1); 
        }
    }

    private void DetectXMovement()
    {
        isWalking = rb.velocity.x != 0 ? 1 : 0;
        animator.SetFloat("xVelocity", isWalking);
    }

    private bool CheckWallCollision(Transform wallcheck, float wallCheckDistance, bool debug=false)
    {
        bool wallCollision = Physics2D.Raycast(wallcheck.position, Vector2.right * Mathf.Sign(movX), wallCheckDistance, LayerMask.GetMask("Ground"));
        if(debug)
        {
            Color color = wallCollision ? Color.red : Color.green;
            Debug.DrawRay(wallcheck.position, Vector2.right * Mathf.Sign(movX) * wallCheckDistance, color);
        }
        return wallCollision;
    }

    void Update()
    {   
        // Salto
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        // Movimiento
        DetectXMovement();

        Sprint(multSpeed);
        HorizontalMovement();
        FlipSprite();       
    }
    private void FixedUpdate()
    {
        bool isTouchingWallBottom = CheckWallCollision(wallCheckBottom, wallCheckDistance, true);
        bool isTouchingWallTop = CheckWallCollision(wallCheckTop, wallCheckDistance, true);
        bool isTouchingWall = isTouchingWallBottom || isTouchingWallTop;
        //rb.AddForce(mov * speed * Time.fixedDeltaTime);
        //rb.velocity = mov * speed * Time.fixedDeltaTime; // en unity 6 ya no funciona -.-
        rb.velocity = isTouchingWall ? new Vector2(0, rb.velocity.y) : new Vector2(movX * speed, rb.velocity.y);
    }
}
