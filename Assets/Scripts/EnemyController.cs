using Assets.Scripts;
using Assets.Scripts.EnemyStates;
using System.Data;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public Transform Ch { get; private set; }

    //[Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float _patrolSpeed;
    private int _movX = -1;
    private int _movXPrev;
    public float PatrolSpeed => _patrolSpeed;
    public int MovX => _movX;

    public Transform player;
    private EnemyStateManager _stateManager;

    float isWalking;
    public bool isAttacking = false;
    public int damage = 1;

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Ch = transform.Find("Character");
    }

    public void Start()
    {
        EnemyState _initialState = new EnemyStatePatrol(this, _stateManager);
        _stateManager = new EnemyStateManager();
        _stateManager.Initialize(_initialState);
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Anim.SetTrigger("attack");
            Rb.velocity = Vector2.zero;
        }
    }

    private void DetectWalking()
    {
        isWalking = Rb.velocity.x != 0 ? 1 : 0;
        Anim.SetFloat("xVelocity", isWalking);
        _movX = (int) Mathf.Sign(Rb.velocity.x);
    }
    private void Update()
    {
        _stateManager.UpdateState();
        DetectWalking();
    }
    void FixedUpdate()
    {}
}