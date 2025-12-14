using Assets.Scripts;
using Assets.Scripts.EnemyStates;
using UnityEngine;

public class PatrolEnemyController : EnemyController
{
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float _patrolSpeed;
    [SerializeField] private float _chaseSpeed;

    [Header("Player Interaction")]
    [SerializeField] private float _sightDistance;
    [SerializeField] private float _attackDistance;

    [Header("Attack")]
    [SerializeField] private float _attackAnimationVelocity;
   
    public bool IsPlayerOnSight => IsPlayerWithinRange(_sightDistance);
    public bool IsPlayerWithinAttackRange => IsPlayerWithinRange(_attackDistance);
    public override float CurrentAttackVelocity => _attackAnimationVelocity;

    public float PatrolSpeed => _patrolSpeed;
    public float ChaseSpeed => _chaseSpeed;
    public float SightDistance => _sightDistance;
    public float AttackDistance => _attackDistance; 
    public float AttackAnimationVelocity => _attackAnimationVelocity;

    protected override void Start()
    {
        base.Start();
        EnemyState _initialState = new EnemyStatePatrol(this, _stateManager);
        _stateManager.Initialize(_initialState);
    }

    public override EnemyState GetDefaultState()
    {
        return new EnemyStatePatrol(this, _stateManager);
    }

    public virtual void MoveTowardsX(Vector2 target, float speed)
    {
        float direction = Mathf.Sign(target.x - transform.position.x);
        Rb.velocity = new Vector2(direction * speed, Rb.velocity.y);
    }

    public override void AttackPhysicMovement()
    {
        float dir = transform.localScale.x;
        Rb.velocity = new Vector2(CurrentAttackVelocity * dir, Rb.velocity.y);
    }

    public override void Attack()
    {
        StopMovement();
        FlipSpriteToPlayer();
    }

}
