using Assets.Scripts;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _movX = 0;
    public int MovX => _movX;

    [SerializeField] private float _walkSpeed = 0;
    public float WalkSpeed => _walkSpeed;

    private float baseSpeed;
    private float multSpeed;
    [SerializeField] private float _speedFactor = 1.5f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private int _maxFallVelocity = 2;
    [SerializeField] private float _jumpHangTimeThreshold = 0.5f;
    [SerializeField] private float _knockbackForce;
    public bool isJumping;
    public bool isFalling;
    private bool isSprinting;
    private bool _isGrounded;
    private float _defaultGravity;
    public bool IsGrounded => _isGrounded;
    public float DefaultGravity => _defaultGravity;
    public float KnockbackForce => _knockbackForce;
    public float JumpForce => _jumpForce;
    public int MaxFallVelocity => _maxFallVelocity;
    public float JumpHangTimeThreshold => _jumpHangTimeThreshold;

    private float _isWalking;
    private bool isAttacked;
    public int potions = 0;
    GameObject groundCheckLeft, groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public HpManagerPlayer hpManager;

    private PlayerState _currentState;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hpManager = GetComponent<HpManagerPlayer>();
        animator = GetComponent<Animator>();

        _currentState = new PlayerStateGrounded(this);

        baseSpeed = _walkSpeed;
        multSpeed = _walkSpeed * _speedFactor;

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
            print($"nuevo estado {_currentState}");
        }
    }
    private void DetectXInput()
    {
        // movimiento horizontal
        if (Input.GetKey(KeyCode.D))
        {
            _movX = 1;
        }
        else if (Input.GetKey(KeyCode.A)) {
            _movX = -1;
        }
        else 
        { 
            _movX = 0; 
        }        
    }

    private void Sprint (float multSpeed)
    {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        _walkSpeed = isSprinting ? multSpeed : baseSpeed;
    }

    private void FlipSprite() 
    {
        if (_movX != 0) 
        {
            transform.localScale = new Vector3(_movX, 1, 1); 
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
        float _xVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("xVelocity", _xVelocity);
    }

    public void OnAttacked(int dmg, Vector2 enemyPos)
    {
        _currentState.OnAttacked(dmg, enemyPos);
        //Vector2 knockbackDir = (new Vector2(transform.position.x, transform.position.y) - enemyPos).normalized;
        //print("knockback vector: " + knockbackDir * 30f);

        //rb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        _currentState.HandleInput();

        // Movimiento
        DetectXMovement();

        Sprint(multSpeed);
        DetectXInput();
        FlipSprite();       
    }
    private void FixedUpdate()
    {
        CheckGrounded();     
        _currentState.PhysicsUpdate();
    }
}
