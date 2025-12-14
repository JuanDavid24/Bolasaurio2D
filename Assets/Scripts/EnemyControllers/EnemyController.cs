using Assets.Scripts;
using Assets.Scripts.EnemyStates;
using System;
using System.Data;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class EnemyController : MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public Collider2D Col { get; private set; }
    public Animator Anim { get; private set; }
    public HpManager Hp { get; private set; }
    public Timer TimerCooldown { get; private set; }
    public virtual float CurrentAttackVelocity => 0f;  // velocidad de la animacion de ataque

    public Transform player;
    protected EnemyStateManager _stateManager;

    [Header("Attack")]
    public int damage = 1;
    [SerializeField] private float _attackCooldown;
    
    public float AttackCooldown => _attackCooldown;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Col = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        Hp = GetComponent<HpManager>();
        TimerCooldown = GetComponent<Timer>();
    }

    protected virtual void Start()
    {
        _stateManager = new EnemyStateManager();
        // Se puede definir un initial state abstract como atributo?
    }

    protected virtual void Update()
    {
        if (Hp.isAlive)
            _stateManager.UpdateState();
        //Debug.Log(_stateManager.CurrentState.ToString());
    }
    protected virtual void FixedUpdate()
    {
        if (Hp.isAlive)
            _stateManager.FixedUpdateState();
    }
    public abstract EnemyState GetDefaultState();

    public virtual void OnAnimationEnd()
    {
        _stateManager.CurrentState.OnAnimationEnd();
    }

    public virtual void OnAttacked(int dmg, Vector2 dmgDealerPos)
    {
        _stateManager.CurrentState.OnAttacked(dmg, dmgDealerPos);
    }
    public virtual void AnimateMovement()
    {
        float isWalking = Rb.velocity.x != 0 ? 1 : 0;
        Anim.SetFloat("xVelocity", isWalking);
    }
    public void FlipSpriteAuto()
    {
        float dir = Mathf.Sign(Rb.velocity.x);
        if (dir != 0)
        {
            Vector3 prevScale = transform.localScale;
            transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
        }
    }
    public void FlipSpriteToPlayer()
    {
        float directionToPlayer = player.position.x - transform.position.x;
        float distanceToPlayer = Mathf.Abs(directionToPlayer);
        if (distanceToPlayer > 0.1f)
        {
            Vector3 prevScale = transform.localScale;
            float dir = Mathf.Sign(directionToPlayer);
            transform.localScale = new Vector3(dir * Mathf.Abs(prevScale.x), prevScale.y, prevScale.z);
        }
    }
    public virtual bool IsPlayerWithinRange(float detectionRange)
    {
        float playerDistance = Vector3.Distance(player.position, transform.position);
        return (playerDistance < detectionRange);
    }


    public virtual void StopMovement()
    {
        Rb.velocity = new Vector2(0, Rb.velocity.y);
    }
    public virtual void OnCooldown()
    {
        StopMovement();
    }
    public virtual void OnCooldownFinish() { }
    public virtual void Attack() { }
    public virtual void AttackPhysicMovement() { }
}