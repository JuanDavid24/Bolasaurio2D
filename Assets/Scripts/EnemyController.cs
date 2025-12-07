using Assets.Scripts;
using Assets.Scripts.EnemyStates;
using System.Data;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }

    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float _patrolSpeed;
    [Header("Ptayer Interaction")]
    [SerializeField] private float _sightDistance;
    [SerializeField] private float _attackDistance;
    public float PatrolSpeed => _patrolSpeed;
    public float SightDistance => _sightDistance;
    public float AttackDistance => _attackDistance;

    public Transform player;
    private EnemyStateManager _stateManager;

    float isWalking;
    public int damage = 1;

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    public void Start()
    {
        _stateManager = new EnemyStateManager();
        EnemyState _initialState = new EnemyStatePatrol(this, _stateManager);
        _stateManager.Initialize(_initialState);
    }

    //public void Attack()
    //{
    //    if (!isAttacking)
    //    {
    //        isAttacking = true;
    //        Anim.SetTrigger("attack");
    //        Rb.velocity = Vector2.zero;
    //    }
    //}

    private void Update()
    {
        _stateManager.UpdateState();
        //Debug.Log(_stateManager.CurrentState.ToString());
    }
    void FixedUpdate()
    {}
}