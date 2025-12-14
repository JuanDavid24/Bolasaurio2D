using Assets.Scripts.EnemyStates;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemyStates
{
    public class EnemyStateChase : EnemyState
    {
        private PatrolEnemyController _patrolEnemy;
        private float playerDistance;

        public EnemyStateChase(PatrolEnemyController enemy, EnemyStateManager stateManager) : base(enemy, stateManager) 
        {
            _patrolEnemy = enemy;
        }

        public override void HandleState()
        {
            _enemy.FlipSpriteToPlayer();
            CheckPlayerDistance();
            _enemy.AnimateMovement();
        }

        private void CheckPlayerDistance()
        {
            if (_patrolEnemy.IsPlayerOnSight)
            {
                if (_patrolEnemy.IsPlayerWithinAttackRange)
                {
                    // atacar
                    _stateManager.TransitionToState(new EnemyStateAttack(_enemy, _stateManager));
                    return;
                }
            }
            else
            {
                _stateManager.TransitionToState(new EnemyStatePatrol(_patrolEnemy, _stateManager));
            }
        }
        public override void PhysicsUpdate()
        {
            _patrolEnemy.MoveTowardsX(_player.position, _patrolEnemy.ChaseSpeed); // chase player
        }
    }
}