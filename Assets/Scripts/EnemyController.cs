using Assets.Scripts;
using Assets.Scripts.EnemyStates;
using System.Data;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Animator Anim { get; private set; }
    public Timer TimerCooldown { get; private set; }

    public Transform player;
    public int damage = 1;
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float _patrolSpeed;
    [Header("Player Interaction")]
    [SerializeField] private float _sightDistance;
    [SerializeField] private float _attackDistance;

    [Header("Attack")]
    [SerializeField] private float _attackAnimationVelocity;
    [SerializeField] private float _attackCooldown;
    public float PatrolSpeed => _patrolSpeed;
    public float SightDistance => _sightDistance;
    public float AttackDistance => _attackDistance;
    public float AttackCooldown => _attackCooldown;
    public float AttackAnimationVelocity => _attackAnimationVelocity;

    private EnemyStateManager _stateManager;

    float isWalking;

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        TimerCooldown = GetComponent<Timer>();
    }

    public void Start()
    {
        _stateManager = new EnemyStateManager();
        EnemyState _initialState = new EnemyStatePatrol(this, _stateManager);
        _stateManager.Initialize(_initialState);
    }

    private void Update()
    {
        _stateManager.UpdateState();
        //Debug.Log(_stateManager.CurrentState.ToString());
    }
    void FixedUpdate()
    {
        _stateManager.FixedUpdateState();
    }
    public void OnAnimationEnd()
    {
        _stateManager.CurrentState.OnAnimationEnd();
    }
}