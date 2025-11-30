using Assets.Scripts;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int movX = 0;
    private Vector2 mov = new Vector2(0, 0);
    [SerializeField] private float speed = 0;
    private float baseSpeed;
    private float multSpeed;
    [SerializeField] private float _speedFactor = 1.5f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private int _maxFallVelocity = 2;
    [SerializeField] private float _jumpHangTimeThreshold = 0.5f;
    public bool isJumping;
    public bool isFalling;
    private bool isSprinting;
    private bool _isGrounded;
    private float _defaultGravity;
    public bool IsGrounded => _isGrounded;
    public float DefaultGravity => _defaultGravity;
    public float JumpForce => _jumpForce;
    public int MaxFallVelocity => _maxFallVelocity;
    public float JumpHangTimeThreshold => _jumpHangTimeThreshold;

    private float isWalking;
    private bool isAttacked;
    [SerializeField] private float knockbackForce;
    public int potions = 0;
    GameObject groundCheckLeft, groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private HpPlayer hpManager;

    private PlayerState _currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hpManager = GetComponent<HpPlayer>();
        animator = GetComponent<Animator>();

        _currentState = new PlayerStateGrounded(this);

        baseSpeed = speed;
        multSpeed = speed * _speedFactor;

        groundCheckLeft = transform.GetChild(0).gameObject;
        groundCheckRight = transform.GetChild(1).gameObject;

        _defaultGravity = rb.gravityScale;
    }

    public void TransitionToState(PlayerState newState)
    {
        if (_currentState != newState)
        {
            _currentState.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }
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

    private void FlipSprite() 
    {
        if (movX != 0) 
        {
            transform.localScale = new Vector3(movX, 1, 1); 
        }
    }

    private void CheckGrounded()
    { 
        bool isGroundedLeft = Physics2D.Raycast(groundCheckLeft.transform.position, Vector2.down, 0.1f, groundLayer);
        bool isGroundedRight = Physics2D.Raycast(groundCheckRight.transform.position, Vector2.down, 0.1f, groundLayer);
        _isGrounded = isGroundedLeft || isGroundedRight;
    }

    private void DetectXMovement()
    {
        isWalking = rb.velocity.x != 0 ? 1 : 0;
        animator.SetFloat("xVelocity", isWalking);
    }

    public void OnAttacked(int dmg, Vector2 enemyPos)
    {
        isAttacked = true;
        hpManager.TakeDamage(dmg);
        Vector2 knockbackDir = (new Vector2(transform.position.x, transform.position.y) - enemyPos).normalized;
        print("enemypos " + enemyPos);
        print("knockback vector: " + knockbackDir * 30f);

        rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        _currentState.HandleInput();
        Debug.Log(_currentState.ToString());

        // Movimiento
        DetectXMovement();

        Sprint(multSpeed);
        HorizontalMovement();
        FlipSprite();       
    }
    private void FixedUpdate()
    {
        CheckGrounded();

        if(!isAttacked)
            rb.velocity = new Vector2(movX * speed, rb.velocity.y);
    }
}
